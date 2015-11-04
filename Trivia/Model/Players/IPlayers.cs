namespace Trivia.Model.Players
{
    public interface IPlayers
    {
        void Add(IPlayer player);
        IPlayer GetNextPlayer();

    }
}