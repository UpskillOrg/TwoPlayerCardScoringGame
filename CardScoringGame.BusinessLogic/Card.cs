namespace TwoPlayerCardScoringGame
{
    public class Card
    {
        public int Value { get; set; }
        public string? Suit { get; set; }

        public override string ToString()
        {
            string[] values = { "Ace", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King" };
            return values[Value - 1] + " of " + Suit;
        }
    }    
}