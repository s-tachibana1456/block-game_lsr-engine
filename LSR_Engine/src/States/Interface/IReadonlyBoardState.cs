using LSR_Engine.src.States.Enum;
using System;

namespace LSR_Engine.src.States.Interface
{
    public interface IReadonlyBoardState
    {
        /// <summary>
        /// 現在の盤面の状態を取得する
        /// 盤面は読み取り専用の1次元配列として返される。
        /// </summary>
        public ReadOnlySpan<BlockType> GetBoard();
    }
}
