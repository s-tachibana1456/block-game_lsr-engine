using LSR_Engine.src.Common;
using LSR_Engine.src.Input;
using LSR_Engine.src.MatchContext;
using LSR_Engine.src.PipeLines;
using LSR_Engine.src.Rules;

namespace LSR_Engine.src.Managers
{
    internal class GameActionManager
    {
        private readonly InputBuffer _inputBuffer;
        private readonly MovementRule _movementRule;
        private readonly RotationRule _rotationRule;
        private readonly PlacementPipeline _placementPipeline;
        private readonly MatchState _matchState;

        public GameActionManager(
            InputBuffer inputBuffer,
            MovementRule movementRule,
            RotationRule rotationRule,
            PlacementPipeline placementPipeline,
            MatchState matchState
        )
        {
            _inputBuffer = inputBuffer;
            _movementRule = movementRule;
            _rotationRule = rotationRule;
            _placementPipeline = placementPipeline;
            _matchState = matchState;
        }

        public void DispatchActions()
        {
            foreach (var (action, state) in _inputBuffer.GetActiveActions())
            {
                switch (action)
                {
                    case Actions.MoveLeft:
                    case Actions.MoveRight:
                    case Actions.MoveUp:
                    case Actions.MoveDown:
                        _movementRule.Execute((action, state));
                        break;
                    case Actions.Rotate:
                    case Actions.RotateReverse:
                        _rotationRule.Execute(action);
                        break;
                    case Actions.Drop:
                        _placementPipeline.Execute(default);
                        break;
                }
            }
        }
    }
}
