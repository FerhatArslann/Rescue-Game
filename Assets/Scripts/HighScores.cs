using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.scripts
{
    [Serializable]
    class HighScores
    {
        public HighScore[] scores;
    }
    [Serializable]
    class HighScore
    {
        public int id = 0;
        public string playername = "";
        public float score = 0.0f;
        public string playtime = "";
    }
}
