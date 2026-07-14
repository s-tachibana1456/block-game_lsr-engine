using LSR_Engine.src.Event;
using LSR_Engine.src.Logics;
using LSR_Engine.src.MatchContext;
using LSR_Engine.src.States;

namespace LSR_Engine.src.Rules
{
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

            
        }
    }
}
