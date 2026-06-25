using LSR_Engine.src.Common;

namespace LSR_Engine.src.MatchContext
{
    internal readonly struct MatchConfig
    {
        readonly public GameMode GameMode { get; }

        readonly public int MapSize { get; }

        public MatchConfig(GameMode gameMode, int mapSize)
        {
            GameMode = gameMode;
            MapSize = mapSize;
        }
    }
}
