using LSR_Engine.src.Common.Interface;
using LSR_Engine.src.MatchContext;

namespace LSR_Engine.src.Managers
{
    internal class SesstionManager : IUpdatable
    {
        protected readonly GameActionManager _sessionA;
        protected readonly MatchState _matchState;

        public SesstionManager(GameActionManager sessionA, MatchState matchState)
        {
            _sessionA = sessionA;
            _matchState = matchState;
        }

        public void Tick()
        {
            // ゲームのティックを更新
            _matchState.IncrementTick();

            // Bufferからアクションを取得して処理させる
            _sessionA.DispatchActions();
        }
    }
}
