using System.Collections.Generic;

namespace LSR_Engine.src.States.Interface
{
    internal interface IBoardState : IReadonlyBoardState
    {
        /// <summary>
        /// ブロックを盤面に書き込む。
        /// </summary>
        /// <param name="block">盤面に書き込みを行ないたいブロックのデータ</param>
        /// <param name="posX">ブロックを設置したX座標</param>
        /// <param name="posY">ブロックを設置したY座標</param>
        void SetBlock(IReadOnlyList<IReadOnlyList<int>> block, int posX, int posY);

        /// <summary>
        /// 任意の列・行を削除する。
        /// </summary>
        /// <param name="direction">削除する方向 Vertical: 縦方向, Horizontal: 横方向</param>
        /// <param name="line">削除を行う行・列のインデックス</param>
        void DeleteLine(Direction direction, int line);
    }
}
