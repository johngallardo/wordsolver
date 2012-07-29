using System;

namespace WordSolver
{
    public struct TileCriteriaResult
    {
        public bool IsSatisfied { get; set; }
        public bool IsBingo { get; set; }
        public int Score { get; set; }
    }
}
