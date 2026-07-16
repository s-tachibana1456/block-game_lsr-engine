using LSR_Engine.src.Cache;
using LSR_Engine.src.Common;
using LSR_Engine.src.Logger;
using LSR_Engine.src.Logics;
using LSR_Engine.src.MatchContext;
using LSR_Engine.src.Rules.Interface;
using LSR_Engine.src.States;

namespace LSR_Engine.src.Rules
{
    internal class RotationRule : IRule<Actions, Empty>
    {
        private readonly PreviewBlockState _previewBlockState;
        private readonly MatchConfig _matchConfig;
        private readonly BlockCache _blockCache;
        private readonly ILogger _logger;

        public RotationRule(PreviewBlockState previewBlockState,  MatchConfig matchConfig, BlockCache blockCache, ILogger logger)
        {
            _previewBlockState = previewBlockState;
            _matchConfig = matchConfig;
            _blockCache = blockCache;
            _logger = logger;
        }

        public Empty Execute(Actions action)
        {
            Block currentBlock = _previewBlockState.CurrentBlock;
            byte[][][] cache = _blockCache.GetBlockCache(currentBlock.Shape);
            Position position = _previewBlockState.Position;

            _logger.Trace($"Rotate Start. Shape: {currentBlock.Shape}, pos: ({position.X}, {position.Y}), angle: {currentBlock.Angle}");

            var (afterBlock, afterPosition) = RotateLogic.Rotate(
                action,
                cache,
                currentBlock,
                _previewBlockState.Position,
                _matchConfig.MapSize
            );

            _previewBlockState.Rotate(afterBlock, afterPosition);
            _logger.Debug($"Rotate Success. Shape: {afterBlock.Shape}, pos: ({afterPosition.X}, {afterPosition.Y}), angle: {afterBlock.Angle}");

            return default;
        }
    }
}
