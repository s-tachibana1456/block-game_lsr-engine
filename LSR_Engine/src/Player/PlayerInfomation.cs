namespace LSR_Engine.src.Player
{
    internal struct PlayerInfomation
    {
        public bool IsEnemy { get; }
        public int Score { get; set; }
        public int Combo { get; set; }
        public int LastCleard { get; set; }
        public int LinesCleard { get; set; }

        public PlayerInfomation(bool isEnemy)
        {
            IsEnemy = isEnemy;
            Score = 0;
            Combo = 0;
            LastCleard = 0;
            LinesCleard = 0;
        }
    }
}
