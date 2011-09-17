using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Text;

namespace WordSolver
{
    public class TileCriteria
    {          
        public TileCriteria(string rackTiles, string templateTiles)
        {
            var totalTiles = rackTiles;
            if (!string.IsNullOrEmpty(templateTiles))
                totalTiles += GetTemplateTiles(templateTiles);
            _count = BuildCount(totalTiles, true);
            _buffer = new int[_count.Length];
            _scorer = new Scorer();
            _bingoCheck = BuildCount(rackTiles, true);
            _bingoBuffer = new int[_bingoCheck.Length];

            CheckBingo = rackTiles.Length >= 7;
        }

        public string GetTemplateTiles(string templateTiles)
        {
            var validTiles = templateTiles.Where(c => (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'));
            StringBuilder sb = new StringBuilder();
            sb.Append(validTiles.ToArray());
            return sb.ToString();
        }

        public bool CheckBingo { get; private set; }

        public TileCriteriaResult Check(string candidate)
        {
            int score = 0;
            Array.Copy(_count, _buffer, _count.Length);
            Array.Copy(_bingoCheck, _bingoBuffer, _bingoCheck.Length);

            foreach (var c in candidate)
            {
                if (--_buffer[c - 'A'] < 0)
                {
                    if (--_buffer[26] < 0)
                    {
                        return new TileCriteriaResult { IsSatisfied = false };
                    }
                    --_bingoBuffer[26];
                    _buffer[c - 'A'] = 0;
                }
                else
                {
                    --_bingoBuffer[c - 'A'];
                    score += _scorer.Score(c);
                }
            }

            var isBingo = false;
            if(CheckBingo)
            {
                isBingo = _bingoBuffer.All(i => i == 0);
                if (isBingo)
                    score += 50;
            }

            return new TileCriteriaResult { IsSatisfied = true, Score = score, IsBingo = isBingo };
        }

        private static int[] BuildCount(string str, bool allowWildcard)
        {
            int[] count = new int[27 /* alphabet plus wildcard */];
            str = str.ToUpperInvariant();
            foreach (var c in str)
            {
                if (c >= 'A' && c <= 'Z')
                    count[c - 'A']++;
                else if ((c == '*' || c == '.') && allowWildcard)
                    count[26]++;
                // else ignore this character
                   
            }
            return count;
        }

        private int[] _count;
        private int[] _buffer;
        private int[] _bingoBuffer;
        private int[] _bingoCheck;
        private Scorer _scorer;
    }
}
