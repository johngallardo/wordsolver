using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Collections.Generic;
using System.Diagnostics;

namespace WordSolver
{
    public class WordDisplayConverter : IValueConverter
    {
        private static Scorer _s;
        private static long _time;

        static WordDisplayConverter()
        {
            _s = new Scorer();
        }
        
        public object  Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string toConvert;

            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                if (value == null)
                    return null;

                if (value is string)
                    toConvert = value as string;
                else if (value is Word)
                    toConvert = (value as Word).Text;
                else
                    throw new ArgumentException("Unknown type", "value");

                var d = from c in (toConvert as IEnumerable<char>)
                        select new Converted { Letter = c.ToString(), Score = _s.ScoreString(c) };
                return d.ToList();
            }
            finally
            {
                sw.Stop();
                lock (_s)
                {
                    _time += sw.ElapsedMilliseconds;
                }
            }
        }

        public object  ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
 	        throw new NotImplementedException();
        }
    }

    public class Converted
    {
        public string Letter { get; set; }
        public Uri LetterUri { get { return new Uri(@"/Tiles/sc_" + Letter + "01.gif", UriKind.Relative); } }
        public string Score { get; set; }
    }
}
