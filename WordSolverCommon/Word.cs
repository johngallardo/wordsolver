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
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace WordSolver
{
    [DataContract]
    public class Word
    {
        private static Scorer _s = new Scorer();
        
        public Word()
        {
        }
        
        private readonly static Scorer _scorer = new Scorer();

        [DataMember]
        public int Score
        {
            get;
            set;
        }

        [DataMember]
        public string Text 
        {
            get { return _text; }
            set { _text = value; GenerateTileInfo(); }
        }

        [IgnoreDataMember]
        public List<TileInfo> TileInfos
        {
            get { return _tileInfos; }
        }

        private void GenerateTileInfo()
        {
            var d = from c in (Text as IEnumerable<char>)
                    select new TileInfo { Letter = c.ToString(), Score = _s.ScoreString(c) };
            _tileInfos = d.ToList();
        }

        private string _text;
        private List<TileInfo> _tileInfos;
    }

    public class TileInfo
    {
        public string Letter { get; set; }
        public string Score { get; set; }
    }

    public class TileInfoCollection : List<TileInfo>
    {
    }
}
