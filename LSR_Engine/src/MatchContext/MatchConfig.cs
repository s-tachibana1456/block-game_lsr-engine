using LSR_Engine.src.Common;

namespace LSR_Engine.src.MatchContext
{
    internal class MatchConfig
    {
        public GameMode GameMode { get; }

        public int MapSize { get; }

        public MatchConfig(GameMode gameMode, int mapSize)
        {
            GameMode = gameMode;
            MapSize = mapSize;
        }
    }
}
