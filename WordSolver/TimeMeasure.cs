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
using System.Diagnostics;

namespace WordSolver
{
    public class TimeMeasure : IDisposable
    {
        public TimeMeasure()
        {
            sw = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            sw.Stop();
            Debug.WriteLine("PerfLog {0}ms: {1}", sw.ElapsedMilliseconds, Message);
        }

        private Stopwatch sw;
        public string Message { get; set; }
    }
}
