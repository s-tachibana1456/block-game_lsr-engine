using LSR_Engine.src.MatchContext;

namespace LSR_Engine.src.Managers
{
    internal class BattleManager : SesstionManager
    {
        protected readonly GameActionManager _sessionB;

        public BattleManager(
            GameActionManager sessionA,
            GameActionManager sessionB,
            MatchState matchState
        )
        : base(sessionA, matchState)
        {
            _sessionB = sessionB;
        }

        public new void Tick()
        {
            base.Tick();
            _sessionB.DispatchActions();
        }
    }
}
