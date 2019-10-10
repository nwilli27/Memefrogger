using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    class Player
    {

        public int Score { get; private set; }

        public int Lives { get; private set; }

        public Player()
        {
            this.Score = 0;
            this.Lives = 3;
        }

        public void decrementLivesByOne()
        {
            this.Lives--;
        }

        public void incrementScoreByOne()
        {
            this.Score++;
        }

        public bool isOutOfLives()
        {
            return this.Lives == 0;
        }

        public bool scoredThreePoints()
        {
            return this.Score == 3;
        }
    }
}
