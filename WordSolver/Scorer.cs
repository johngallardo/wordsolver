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
using System.Globalization;

namespace WordSolver
{
    public class Scorer
    {
        private int[] _scoreMap;
        private string[] _scoreStrings;
        
        public Scorer()
        {
            _scoreMap = new int['Z'+1];
            _scoreStrings = new string['Z' + 1];
            var scoring = Resources.Scoring;
            var scores = from p in scoring.Split(';')
                         select p.Split(',');
            foreach(var entry in scores)
            {
                var score = int.Parse(entry.First(), CultureInfo.InvariantCulture);
                foreach(var letters in entry.Skip(1))
                {
                    _scoreMap[letters[0]] = score;
                    _scoreStrings[letters[0]] = score.ToString();
                }
            }                         
        }

        public int Score(char c)
        {
            if (c < _scoreMap.Length)
                return _scoreMap[c];
            return 0;
        }

        public string ScoreString(char c)
        {
            if (c < _scoreMap.Length)
                return _scoreStrings[c];
            return null;
        }
    }
}
