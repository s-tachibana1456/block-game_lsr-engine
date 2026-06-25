using LSR_Engine.src.Common;

namespace LSR_Engine.src.States.Interface
{
    internal interface IPreviewBlockState : IReadonlyPreviewBlockState
    {
        /// <summary>
        /// ブロックの座標を更新する (プレビューブロックのが移動した際に呼ばれる)
        /// </summary>
        /// <param name="position">新しいブロックの座標</param>
        public void Move(Position position);

        /// <summary>
        /// 既存ブロックを回転させる。新しいブロックのデータに置き換わる。
        /// </summary>
        /// <param name="newBlock">回転後のブロックのデータ</param>
        /// <param name="newPosition">回転後の新しい座標</param>
        public void Rotate(Block newBlock, Position newPosition);

        /// <summary>
        /// 新しいブロックをセットする。ブロックが設置され新しいブロックが生成された際に呼ばれる
        /// </summary>
        /// <param name="newBlock">新しいブロック</param>
        /// <param name="newPosition">新しい座標</param>
        public void SetUp(Block newBlock, Position newPosition);
    }
}
