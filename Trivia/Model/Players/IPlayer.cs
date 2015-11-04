namespace Trivia.Model.Players
{
    public interface IPlayer
    {
        string ToString();
        void Penalize();
        bool IsInPenaltyBox();
        void ChangeLocation(int step);
        int Location();
        void IncreaseCoinsByOne();
        int Coins();
        bool IsNotWinning();
        string Name();
    }
}