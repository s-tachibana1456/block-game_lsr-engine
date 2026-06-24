using LSR_Engine.src.Common;
using System;
using System.Collections.Generic;

namespace LSR_Engine.src.States
{
    [Flags]
    internal enum PreviewBlockFlags
    {
        None = 0,
        Position = 1 << 0,
        Shape = 1 << 1,
        Rotation = 1 << 2,
    }

    internal class PreviewBlockState
    {
        public IReadOnlyList<IReadOnlyList<int>> CurrentBlock { get; private set; }
        public int Angle { get; private set; }
        public Position Position { get; private set; }
        
        private PreviewBlockFlags Flags;

        public PreviewBlockState(IReadOnlyList<IReadOnlyList<int>> block, int angle, Position position)
        {
            CurrentBlock = block;
            Angle = angle;
            Position = position;

            Flags |= PreviewBlockFlags.None;
        }

        public PreviewBlockFlags ConsumeUpdates()
        {
            var flags = Flags;
            Flags = PreviewBlockFlags.None;
            return flags;
        }

        /// <summary>
        /// ブロックの座標を更新する (プレビューブロックのが移動した際に呼ばれる)
        /// </summary>
        /// <param name="position">新しいブロックの座標</param>
        public void Move(Position position)
        {
            Apply(CurrentBlock, Angle, position);
        }

        /// <summary>
        /// 既存ブロックを回転させる。新しいブロックのデータに置き換わる。
        /// </summary>
        /// <param name="newBlock">回転後のブロックのデータ</param>
        /// <param name="angle">回転値</param>
        /// <param name="newPosition">回転後の新しい座標</param>
        public void Rotate(IReadOnlyList<IReadOnlyList<int>> newBlock, int angle, Position newPosition)
        {
            Apply(newBlock, angle, newPosition);
        }

        /// <summary>
        /// 新しいブロックをセットする。ブロックが設置され新しいブロックが生成された際に呼ばれる
        /// </summary>
        /// <param name="newBlock">新しいブロック</param>
        /// <param name="angle">回転値</param>
        /// <param name="newPosition">新しい座標</param>
        public void SetUp(IReadOnlyList<IReadOnlyList<int>> newBlock, int angle, Position newPosition)
        {
            Apply(newBlock, angle, newPosition);
        }

        /// <summary>
        /// クラス内部のプロパティを一括で変更する。前の値と異なれば自動的に変更フラグが立つ
        /// </summary>
        private void Apply(IReadOnlyList<IReadOnlyList<int>> block, int angle, Position position)
        {
            if (CurrentBlock != block) Flags |= PreviewBlockFlags.Shape;
            if (Position.Equals(position)) Flags |= PreviewBlockFlags.Position;
            if (Angle != angle) Flags |= PreviewBlockFlags.Rotation;

            CurrentBlock = block;
            Position = position;
            Angle = angle;
        }
    }
}
