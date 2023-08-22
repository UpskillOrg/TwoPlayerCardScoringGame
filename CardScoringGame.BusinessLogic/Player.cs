using System.Collections.Generic;

namespace TwoPlayerCardScoringGame
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> Cards { get; set; }

        public Player(string name)
        {
            Name = name;
            Cards = new List<Card>();
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        public int CountCards()
        {
            return Cards.Count;
        }
    }
}