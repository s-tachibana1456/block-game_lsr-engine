using LSR_Engine.src.Common;

namespace LSR_Engine.src.MatchContext
{
    internal class MatchState
    {
        public GameState GameState { get; private set; }

        public int Tick { get; private set; }

        public void IncrementTick() => Tick++;

        public void SetGameState(GameState state) => GameState = state;
    }
}
