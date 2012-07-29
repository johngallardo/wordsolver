using System;
using System.Text;
using System.Text.RegularExpressions;

namespace WordSolver
{
    public class TemplateCriteria
    {
        public TemplateCriteria(string template)
        {
            ExtractedLetters = string.Empty;
            _r = ConvertTemplateToRegex(template);
        }

        private Regex ConvertTemplateToRegex(string template)
        {
            // '.' means any single tile
            // 'a-z' means a specific tile already on the board
            // ',' means any number of tiles
            // '#' means up to # number of tiles

            StringBuilder pattern = new StringBuilder();
            pattern.Append("^");
            foreach (var c in template)
            {                
                if (c == '.')
                    pattern.Append("[a-zA-Z]");
                else if (c == ',')
                    pattern.Append("[a-zA-Z]*");
                else if (c >= '0' && c <= '9')
                    pattern.Append("[a-zA-Z]{0," + c + "}");
                else if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
                {
                    pattern.AppendFormat("[{0}{1}]", char.ToLowerInvariant(c), char.ToUpperInvariant(c));
                    ExtractedLetters += c;
                }                
            }
            pattern.Append("$");
            Regex r = new Regex(pattern.ToString());
            return r;
        }

        private Regex _r;

        public string ExtractedLetters { get; set; }

        public bool Satisfies(string candidate)
        {
            return _r.IsMatch(candidate);
        }
    }
}
