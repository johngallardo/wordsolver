using System;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using Microsoft.Phone.Marketplace;

using Interlocked = System.Threading.Interlocked;
using System.Windows;

namespace WordSolver
{
    public class AppViewModel : INotifyPropertyChanged
    {
        public AppViewModel()
        {
#if TRIAL
            _isTrial = true;
#else
            _isTrial = new LicenseInformation().IsTrial();
#endif
            Solutions = new ObservableCollection<Word>();
            ConstraintState cs;
            if (!IsolatedStorageSettings.ApplicationSettings.TryGetValue("LastUsedValues", out cs))
            {
                cs = new ConstraintState();
                IsolatedStorageSettings.ApplicationSettings.Add("LastUsedValues", cs);
            }

            SettingsModel settings;
            if (!IsolatedStorageSettings.ApplicationSettings.TryGetValue("Settings", out settings))
            {
                settings = new SettingsModel();
                settings.Dictionary = new DictionaryDisplay { File = "twl06.txt" };
                IsolatedStorageSettings.ApplicationSettings.Add("Settings", settings);
            }

            if (cs.TemplateFactory == null)
                cs.TemplateFactory = new StartsWithTemplateFactory();

            ActiveConstraints = cs;
            Settings = settings;
            
            Settings.PropertyChanged += new PropertyChangedEventHandler(Settings_PropertyChanged);
            ActiveConstraints.PropertyChanged += new PropertyChangedEventHandler(ActiveConstraints_PropertyChanged);

            _solverInitialized = new ManualResetEvent(false);
            _queryId = 0;
            _querySync = new object();
            InitializeSolver();
        }

        void ActiveConstraints_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Solve();
        }

        void Settings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Dictionary")
            {
                Solutions.Clear();
                InitializeSolver();
            }
            Solve();
        }
        
        public ObservableCollection<Word> Solutions 
        {
            get { return _solutions; }
            set
            {
                if (value != _solutions)
                {
                    _solutions = value;
                    NotifyPropertyChanged("Solutions");
                }
            }
        }

        public ConstraintState ActiveConstraints { get; set; }

        private SettingsModel _settings;
        public SettingsModel Settings
        {
            get { return _settings; }
            set
            {
                if (value != _settings)
                {
                    _settings = value;
                    NotifyPropertyChanged("Settings");
                }
            }
        }

        public void EnsureSolution()
        {
            if(_queryId == 0)
                Solve();
        }

        public void Solve()
        {
            int myQueryId;

            // lock both setting the id and clearing the results to prevent
            // race condition where we add an item to the results just as we are
            // about to solve again
            lock (_querySync)
            {
                myQueryId = ++_queryId;
                Solutions.Clear();
            }
            var cs = new Constraints(ActiveConstraints.Tiles, ActiveConstraints.GetAdjustedTemplate());
            var dispatcher = Deployment.Current.Dispatcher;
            dispatcher.BeginInvoke(OnSearchBeginning);
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = false;
            worker.DoWork += new DoWorkEventHandler((obj, eargs) =>
            {
                Stopwatch sw = Stopwatch.StartNew();
                var words = SolverService.GetWords(
                    cs).OrderByDescending(w=>w.Score).Take(100);                
                Debug.WriteLine("Selection took {0} ms", sw.ElapsedMilliseconds);
                int skip = (words.Count() > 3 && _isTrial ) ? 1 : 0;
                words = words.Skip(skip);
				sw.Stop();
                dispatcher.BeginInvoke(() =>
                {
                    foreach (var w in words)
                    {
                        lock (_querySync)
                        {
                            if (myQueryId != _queryId)
                                break;
                            Solutions.Add(w);
                        }
                    }
                });
            }
            );
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.RunWorkerAsync();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnSearchCompleted();
        }

        private void OnSearchBeginning()
        {
            IsQueryRunning = true;
            IsNoSolutions = false;
            EventHandler h = SearchBeginning;
            if (h != null)
                h(this, new EventArgs());
        }

        private void OnSearchCompleted()
        {
            _hasSolution = true;
            IsQueryRunning = false;
            IsNoSolutions = (Solutions.Count == 0);
            EventHandler h = SearchCompleted;
            if (h != null)
                h(this, new EventArgs());
        }

        private void InitializeSolver()
        {
            BackgroundWorker worker = new BackgroundWorker();
            _solverInitialized.Reset();
            worker.DoWork += new DoWorkEventHandler((obj, eargs) =>
                {
                    _solver = new Solver();
                    _solver.DictionaryFile = Settings.Dictionary.File;
                    _solver.LoadDictionary();
                    _solverInitialized.Set();
                }
                );
            worker.RunWorkerAsync();
        }

        public event EventHandler SearchBeginning;
        public event EventHandler SearchCompleted;

        private Solver SolverService
        {
            get
            {
                while (_solver == null)
                    _solverInitialized.WaitOne();
                return _solver;
            }
        }

        private volatile Solver _solver;
        private ObservableCollection<Word> _solutions;
        private ManualResetEvent _solverInitialized;
        private int _queryId;
        private readonly object _querySync;
        private bool _isTrial;
        private bool _hasSolution;

        private bool _isQueryRunning;
        public bool IsQueryRunning
        {
            get { return _isQueryRunning; }
            set
            {
                if (value != _isQueryRunning)
                {
                    _isQueryRunning = value;
                    NotifyPropertyChanged("IsQueryRunning");
                }
            }
        }

        public bool _noSolutions;
        public bool IsNoSolutions
        {
            get { return _noSolutions; }
            set
            {
                if (value != _noSolutions)
                {
                    _noSolutions = value;
                    NotifyPropertyChanged("IsNoSolutions");
                }
            }
        }

        public bool IsTrialVersion
        {
            get { return _isTrial; }
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
