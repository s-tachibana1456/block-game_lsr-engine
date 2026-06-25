using LSR_Engine.src.Common;
using LSR_Engine.src.States.Interface;
using System;

namespace LSR_Engine.src.States
{
    [Flags]
    public enum PreviewBlockFlags : byte
    {
        None = 0,
        Shape = 1 << 0,
        Position = 1 << 1,
        Rotation = 1 << 2
    }

    /// <summary>
    /// ゲームのプレビューブロックの状態を表す。
    /// フラグ管理はクライアントの描画専用だが、ややこしくなるためまとめてある。
    /// </summary>
    internal class PreviewBlockState : IPreviewBlockState
    {
        public Block CurrentBlock { get; private set; }
        public Position Position { get; private set; }

        private PreviewBlockFlags Flags;

        public PreviewBlockState(Block block, Position position)
        {
            CurrentBlock = block;
            Position = position;

            Flags = PreviewBlockFlags.None;
        }

        /// <summary>
        /// ゲームのプレビューブロックの状態を表す。
        /// クライアント描画用の更新フラグも保持する。
        /// サーバーではこのフラグは使用しないが、型を分ける複雑さを避けるため同居させている。
        /// </summary>
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
            Apply(CurrentBlock, position);
        }

        /// <summary>
        /// 既存ブロックを回転させる。新しいブロックのデータに置き換わる。
        /// </summary>
        /// <param name="newBlock">回転後のブロックのデータ</param>
        /// <param name="angle">回転値</param>
        /// <param name="newPosition">回転後の新しい座標</param>
        public void Rotate(Block newBlock, Position newPosition)
        {
            Apply(newBlock, newPosition);
        }

        /// <summary>
        /// 新しいブロックをセットする。ブロックが設置され新しいブロックが生成された際に呼ばれる
        /// </summary>
        /// <param name="newBlock">新しいブロック</param>
        /// <param name="angle">回転値</param>
        /// <param name="newPosition">新しい座標</param>
        public void SetUp(Block newBlock, Position newPosition)
        {
            Apply(newBlock, newPosition);
        }

        /// <summary>
        /// クラス内部のプロパティを一括で変更する。前の値と異なれば自動的に変更フラグが立つ
        /// </summary>
        private void Apply(Block block, Position position)
        {
            if (CurrentBlock != block) Flags |= PreviewBlockFlags.Shape;
            if (!Position.Equals(position)) Flags |= PreviewBlockFlags.Position;

            CurrentBlock = block;
            Position = position;
        }
    }
}
