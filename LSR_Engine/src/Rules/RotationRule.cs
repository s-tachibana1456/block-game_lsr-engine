using LSR_Engine.src.Cache;
using LSR_Engine.src.Common;
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

        public RotationRule(PreviewBlockState previewBlockState,  MatchConfig matchConfig, BlockCache blockCache)
        {
            _previewBlockState = previewBlockState;
            _matchConfig = matchConfig;
            _blockCache = blockCache;
        }

        public Empty Execute(Actions action)
        {
            Block currentBlock = _previewBlockState.CurrentBlock;
            byte[][][] cache = _blockCache.GetBlockCache(currentBlock.Shape);

            Rotated rotated = RotateLogic.Rotate(
                action,
                cache,
                currentBlock,
                _previewBlockState.Position,
                _matchConfig.MapSize
            );

            _previewBlockState.Rotate(rotated.AfterBlock, rotated.Position);
            return default;
        }
    }
}
