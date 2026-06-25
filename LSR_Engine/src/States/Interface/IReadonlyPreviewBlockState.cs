using LSR_Engine.src.Common;

namespace LSR_Engine.src.States.Interface
{
    public interface IReadonlyPreviewBlockState
    {
        /// <summary>
        /// 現在立っているフラグを取得する。フラグはConsumeUpdates()でリセットされる。
        /// このメソッドはクライアントのみで呼び出す。サーバー側は呼び出す必要はない。
        /// </summary>
        public PreviewBlockFlags ConsumeUpdates();
        public Block CurrentBlock { get; }
        public Position Position { get; }
    }
}
