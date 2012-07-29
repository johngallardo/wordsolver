using System;

namespace WordSolver
{
    public class Constraints
    {
        public Constraints(string tiles, string template)
        {
            if (tiles == null)
                tiles = string.Empty;
            if (template == null)
                template = string.Empty;
            Tiles = tiles;
            Template = template;

            _tileCriteria = new TileCriteria(Tiles, Template);

            if(!string.IsNullOrEmpty(Template))
                _templateCriteria = new TemplateCriteria(Template);
        }

        public string Tiles { get; private set; }
        public string Template { get; private set; }

        public bool TryCandidateWord(string candidate, out Word solvedWord)
        {
            bool satisfied = true;
            var tileResults = _tileCriteria.Check(candidate);
            satisfied &= tileResults.IsSatisfied;
            if (satisfied && _templateCriteria != null)
            {
                satisfied &= _templateCriteria.Satisfies(candidate);
            }

            solvedWord = null;
            if (satisfied)
            {
                solvedWord = new Word { Score = tileResults.Score, Text = candidate };
            }
            return (solvedWord != null);
        }

        private readonly TileCriteria _tileCriteria;
        private readonly TemplateCriteria _templateCriteria;
    }
}
