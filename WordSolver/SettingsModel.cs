using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WordSolver
{
    [DataContract]
    public class SettingsModel : INotifyPropertyChanged
    {
        public SettingsModel()
        {
            _dispCollection = new DictionaryDisplayCollection();
        }

        [OnDeserialized]
        public void DeserializedComplete(StreamingContext c)
        {
            _dispCollection = new DictionaryDisplayCollection();
        }
        
        private DictionaryDisplay _dictionary;        
        [DataMember]
        public DictionaryDisplay Dictionary {
            get { return _dictionary; }
            set
            {
                if (value != _dictionary)
                {
                    _dictionary = value;
                    NotifyPropertyChanged("Dictionary");
                }                    
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private DictionaryDisplayCollection _dispCollection;
        [IgnoreDataMember]
        public IEnumerable<DictionaryDisplay> DictionaryCollection
        {
            get { return _dispCollection; }
        }
    }

    public class DictionaryDisplay
    {
        public string Name { get; set; }
        public string File { get; set; }

        public override bool Equals(object obj)
        {
            var dd = obj as DictionaryDisplay;
            if (dd == null)
                return false;
            var equals = string.Equals(dd.File, File, StringComparison.OrdinalIgnoreCase);
            return equals;
        }

        public override int GetHashCode()
        {
            if (File == null)
                return 0;
            return (File.ToLowerInvariant().GetHashCode());
        }
    }

    public class DictionaryDisplayCollection : List<DictionaryDisplay>
    {
        public DictionaryDisplayCollection()
        {
            Capacity = 2;
            Add(new DictionaryDisplay { Name = "TWL", File = "twl06.txt" }); 
            Add(new DictionaryDisplay { Name = "SOWPODS", File = "sowpods.txt" });            
        }
    }
}
