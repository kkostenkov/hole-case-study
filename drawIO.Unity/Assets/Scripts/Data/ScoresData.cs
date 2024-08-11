using System.Collections.Generic;

namespace Data
{
    public class ScoresData
    {
        public Queue<int> LastScores = new();
        public int BestScore;
    }
}