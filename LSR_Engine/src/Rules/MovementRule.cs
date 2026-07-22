using LSR_Engine.src.Common;
using LSR_Engine.src.Input;
using LSR_Engine.src.Logger;
using LSR_Engine.src.Logics;
using LSR_Engine.src.MatchContext;
using LSR_Engine.src.Rules.Interface;
using LSR_Engine.src.States;

namespace LSR_Engine.src.Rules
{
    internal class MovementRule : IRule<(Actions, ActionState), Empty>
    {
        private readonly PreviewBlockState previewBlockState;
        private readonly MatchConfig matchConfig;
        private readonly ILogger logger;

        private const int DAS_DELAY = 10;
        private const int ARR_DELAY = 2;

        public MovementRule(PreviewBlockState previewBlockState, MatchConfig matchConfig, ILogger logger)
        {
            this.previewBlockState = previewBlockState;
            this.matchConfig = matchConfig;
            this.logger = logger;
        }

        public Empty Execute((Actions, ActionState) input)
        {
            var (action, state) = input;

            if (state.IsTriggered)
            {
                logger.Trace($"[Input] {action} Triggered (Frame: {state.HeldFrames})");
                Process(action);
                return default;
            }

            if (state.IsDown && ShouldRepeat(state.HeldFrames))
            {
                logger.Trace($"[Input] {action} Repeat Fired (HeldFrames: {state.HeldFrames})");
                Process(action);
            }

            return default;
        }

        private bool ShouldRepeat(int heldFrames)
        {
            if (heldFrames < DAS_DELAY) return false;
            return (heldFrames - DAS_DELAY) % ARR_DELAY == 0;

        }

        private void Process(Actions action)
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
        }
    }
}
