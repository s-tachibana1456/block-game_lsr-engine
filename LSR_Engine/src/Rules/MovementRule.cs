using LSR_Engine.src.Common;
using LSR_Engine.src.Logger;
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
        private readonly ILogger logger;

        public MovementRule(PreviewBlockState previewBlockState, MatchConfig matchConfig, ILogger logger)
        {
            this.previewBlockState = previewBlockState;
            this.matchConfig = matchConfig;
            this.logger = logger;
        }

        public Empty Execute(Actions action)
        {
            logger.Trace($"Move Start. action: {action}");

            Position newPosition = MoveLogic.Move(
                action,
                previewBlockState.Position,
                previewBlockState.CurrentBlock,
                matchConfig.MapSize
            );

            previewBlockState.Move(newPosition);

            logger.Debug($"Move Success. pos: ({newPosition.X}, {newPosition.Y})");

            return default;
        }
    }
}
