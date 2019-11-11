namespace FroggerStarter.Enums
{
    /// <summary>
    ///     Sort type enum types.
    /// </summary>
    public enum HighScoreSortType
    {
        /// <summary>
        ///     The sort order of score, then name, then level.
        /// </summary>
        ScoreNameLevel,

        /// <summary>
        ///     The sort order of name, then score, then level.
        /// </summary>
        NameScoreLevel,

        /// <summary>
        ///     The sort order of level, then score, then name.
        /// </summary>
        LevelScoreName
    }
}