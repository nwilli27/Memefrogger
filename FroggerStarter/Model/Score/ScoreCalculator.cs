
using FroggerStarter.Model.Levels;
using FroggerStarter.Model.Player;

namespace FroggerStarter.Model.Score
{
    /// <summary>
    ///     Class responsible for calculating the score based
    ///     on the current events in the game
    /// </summary>
    internal class ScoreCalculator
    {

        /// <summary>
        ///     Calculates the score.
        ///     Takes the time left, current level and
        ///     adds a correlating amount of points.
        /// </summary>
        /// <returns>Amount of points to add.</returns>
        public static int CalculateScore()
        {
            var timeLeft = (int) ScoreTimer.ScoreTick;
            var currentLevel = LevelManager.CurrentLevel;
            var scoreCalculation = timeLeft * currentLevel;

            if (PlayerAbilities.HasDoublePoints)
            {
                return scoreCalculation * 2;
            }
            return scoreCalculation;
        }

        /// <summary>
        ///     Adds the points based on game performance.
        ///     Multiplies current score by number of hearts left.
        /// </summary>
        public static void MultiplyPointsByHeartsRemaining()
        {
            PlayerStats.Score *= PlayerStats.TotalLives;
        }
    }
}
