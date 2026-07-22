using LSR_Engine.src.Rules;
using LSR_Engine.src.Rules.Interface;

namespace LSR_Engine.src.PipeLines
{
    internal class PlacementPipeline : IRule<Empty, Empty>
    {
        private readonly PlacementRule placementRule;
        private readonly LineClearRule lineClearRule;
        private readonly NextBlockRule nextBlockRule;
        private readonly GameOverRule gameOverRule;
        private readonly FieldSaturationRule fieldSaturationRule;

        public PlacementPipeline(
            PlacementRule placementRule,
            LineClearRule lineClearRule,
            NextBlockRule nextBlockRule,
            GameOverRule gameOverRule,
            FieldSaturationRule fieldSaturationRule
        )
        {
            this.placementRule = placementRule;
            this.lineClearRule = lineClearRule;
            this.nextBlockRule = nextBlockRule;
            this.gameOverRule = gameOverRule;
            this.fieldSaturationRule = fieldSaturationRule;
        }

        public Empty Execute(Empty empty)
        {
            // 1. 盤面の設置判定と書き込みを行う
            bool isPlacementSuccessful = placementRule.Execute(empty);

            // falseの場合は、次のルールを実行せずに終了する
            if (!isPlacementSuccessful) return default;

            // 2. ライン消去判定と書き込みを行う
            lineClearRule.Execute(empty);

            // 3. 次のブロックが設置できるかどうかの判定を行う
            bool isPlaceable = gameOverRule.Execute(empty);

            // 出来ない場合はゲームオーバー判定
            if (!isPlaceable) return default;

            // 4. 次のブロックを生成する
            nextBlockRule.Execute(empty);

            // 5. フィールドの飽和度を計算し、ピンチ常態かどうかを判定する
            fieldSaturationRule.Execute(empty);

            return default;
        }
    }
}
