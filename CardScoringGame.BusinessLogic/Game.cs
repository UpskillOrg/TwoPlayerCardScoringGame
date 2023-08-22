using CardScoringGame.Model;
using System.Reflection;
using matchOption = CardScoringGame.Model.CardMatchOption;

namespace TwoPlayerCardScoringGame
{
    public class Game : IGame
    {
        public Deck Deck { get; set; }
        public Player[] Players { get; set; }
        public matchOption? CardMatchOption { get; set; }
        public Card? CurrentCard { get; set; }

        public Game(int? numberOfDecks, matchOption? cardMatchOption)
        {
            Deck = new Deck(numberOfDecks);
            Players = new Player[] { new Player("Player 1"), new Player("Player 2") };
            CardMatchOption = cardMatchOption;
            CurrentCard = null;
        }

        public bool IsMatch(Card card1, Card card2)
        {
            if (card1 == null || card2 == null) return false;
            switch (CardMatchOption)
            {
                case matchOption.CardValue:
                    return card1.Value == card2.Value;
                case matchOption.Suite:
                    return card1.Suit == card2.Suit;
                case matchOption.Both:
                    return card1.Value == card2.Value && card1.Suit == card2.Suit;
                default:
                    return false;
            }
        }
        
        public async Task<MatchResult> PlayRound()
        {
            var result = await Task.Factory.StartNew(() => {
                Card? newCard = Deck.Deal();

                if (newCard == null)
                {
                    return new MatchResult { Type = ResultType.None, Message = "No more cards in the deck" };
                }
                
                if (CurrentCard != null && IsMatch(newCard, CurrentCard))
                {

                    Random random = new Random();
                    int winner = random.Next(2);
                    Players[winner].AddCard(newCard);
                    Players[winner].AddCard(CurrentCard);                                    
                    return new MatchResult { Message = Players[winner].Name + " scored with " + newCard + " and " + CurrentCard, Type = ResultType.Scored };
                }
                else
                {                    
                    CurrentCard = newCard;
                    return new MatchResult { Message = "No match. New card is " + newCard, Type = ResultType.NoMatch };                    
                }
            });

            return result;
        }

        public EndResult EndGame()
        {
            int count1 = Players[0].CountCards();
            int count2 = Players[1].CountCards();         
            if (count1 > count2)
            {
                return new EndResult() { Message = Players[0].Name + " wins with " + count1 + " cards" };
            }
            else if (count2 > count1)
            {
                return new EndResult() { Message = Players[1].Name + " wins with " + count2 + " cards" };
            }
            else
            {
                return new EndResult() { Message = "It's a draw with " + count1 + " cards each" };
            }
        }
    }
}