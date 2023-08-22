using CardScoringGame.Model;

namespace TwoPlayerCardScoringGame
{
    public interface IGame
    {
        CardMatchOption? CardMatchOption { get; set; }
        Card? CurrentCard { get; set; }
        Deck Deck { get; set; }
        Player[] Players { get; set; }

        EndResult EndGame();
        bool IsMatch(Card card1, Card card2);
        Task<MatchResult> PlayRound();
    }
}