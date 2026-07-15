namespace LSR_Engine.src.Logger
{
    /// <summary>
    /// ログを出力するためのLoggerインターフェース
    /// ライブラリ内ではこのinterfaceを使用して呼び出す。
    /// 具象クラスはクライアント・サーバー側で実装する必要がある。
    /// </summary>
    public interface ILogger
    {
        public void Info(string message);
        public void Warn(string message);
        public void Error(string message);
        public void Debug(string message);
        public void Trace(string message);
    }
}
