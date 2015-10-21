namespace Trivia
{
    public class Player
    {
        private readonly string _name;
        private bool _isInPenaltyBox = false;
        private int _location;
        private int _coins;
        private readonly int WinningCoinNumber = 6;

        public Player(string name, int location, int coins)
        {
            _name = name;
            _location = location;
            _coins = coins;
        }

        public override string ToString()
        {
            return _name; 
        }

        public void Penalize()
        {
            _isInPenaltyBox = true;
        }

        public bool IsInPenaltyBox()
        {
            return _isInPenaltyBox;
        }

        public void ChangeLocationBy(int step)
        {
            _location += step;
            if (_location > 11)
            {
                _location -= 12;
            }
        }

        public int Location()
        {
            return _location;
        }

        public void IncreaseCoinsByOne()
        {
            _coins++;
        }

        public int Coins()
        {
            return _coins;
        }

        public bool IsWinning()
        {
            return _coins == WinningCoinNumber;
        }
    }
}