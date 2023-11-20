namespace Items
{
    public interface IGameViewObserver
    {
        void UpdateTimeLeft(string timeLeft);
        void UpdateScore(string score);
        void GameOver(float score);
    }
}   