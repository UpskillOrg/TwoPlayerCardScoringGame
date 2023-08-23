using CardScoringGame.Model;

namespace TwoPlayerCardScoringGame
{
    public class Deck
        {
            public List<Card> Cards { get; set; }
 
            public Deck(int? numberOfDecks)
            {
                Cards = new List<Card>();
                for (int i = 0; i < numberOfDecks; i++)
                {
                    foreach (var suit in new Suit[] { Suit.Clubs, Suit.Spades, Suit.Diamonds, Suit.Hearts })
                    {
                        for (int value = 1; value <= 13; value++)
                        {
                            Cards.Add(new Card { Value = value, Suit = suit });
                        }
                    }
                }
            }
            
            public async Task Shuffle()
            {
                await Task.Factory.StartNew(() => {
                    Random random = new Random();
                    for (int i = Cards.Count - 1; i > 0; i--)
                    {
                        int j = random.Next(i + 1);
                        Card temp = Cards[i];
                        Cards[i] = Cards[j];
                        Cards[j] = temp;
                    }
                });                
            }

            public Card? Deal()
            {
                if (Cards.Count > 0)
                {
                    Card card = Cards[0];
                    Cards.RemoveAt(0);
                    return card;
                }
                else
                {
                   return null;
                }
            }
        }   
}