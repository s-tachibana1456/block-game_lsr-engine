using LSR_Engine.src.Common;
using LSR_Engine.src.Logics;
using LSR_Engine.src.MatchContext;
using LSR_Engine.src.Rules.Interface;
using LSR_Engine.src.States;

namespace LSR_Engine.src.Rules
{
    internal class MovementRule : IRule<Actions, Empty>
    {
        private readonly PreviewBlockState previewBlockState;
        private readonly MatchConfig matchConfig;

        public MovementRule(PreviewBlockState previewBlockState, MatchConfig matchConfig)
        {
            this.previewBlockState = previewBlockState;
            this.matchConfig = matchConfig;
        }

        public Empty Execute(Actions action)
        {
            Position newPosition = MoveLogic.Move(
                action,
                previewBlockState.Position,
                previewBlockState.CurrentBlock,
                matchConfig.MapSize
            );

            previewBlockState.Move(newPosition);

            return default;
        }
    }
}
