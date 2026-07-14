using LSR_Engine.src.Event;
using LSR_Engine.src.Logics;
using LSR_Engine.src.MatchContext;
using LSR_Engine.src.States;

namespace LSR_Engine.src.Rules
{
    internal readonly struct LineClearEvent
    {
        public readonly int TotalClearedLines;

        public LineClearEvent(int totalClearedLines)
        {
            TotalClearedLines = totalClearedLines;
        }
    }

    internal class LineClearRule
    {
        private readonly BoardState _boardState;
        private readonly MatchConfig _matchConfig;
        private readonly EventBus _bus;

        public LineClearRule(BoardState boardState, MatchConfig matchConfig, EventBus bus)
        {
            _boardState = boardState;
            _matchConfig = matchConfig;
            _bus = bus;
        }

        public void Execute()
        {
            var deletableLines = BoardLogic.FindFilledLines(_boardState.GetBoard(), _matchConfig.MapSize);
            int mapSize = _matchConfig.MapSize;

            for (int i = 0; i < mapSize; i++)
            {
                if (deletableLines.IsHorizontalCleared(i))
                {
                    _boardState.DeleteLine(Direction.Horizontal, i);
                }
                if (deletableLines.IsVerticalCleared(i))
                {
                    _boardState.DeleteLine(Direction.Vertical, i);
                }
            }

            _bus.Publish(new LineClearEvent(deletableLines.CleardCount));
        }
    }
}
