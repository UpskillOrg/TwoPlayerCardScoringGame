
using CardScoringGame.ViewModel;

namespace CardScoringGame.App.ViewModel
{
    public class CardGameWindowViewModel
    {
        public CardScoringGameViewModel CardScoringGameViewModel { get; set; }

        public CardGameWindowViewModel(CardScoringGameViewModel cardScoringGameViewModel)
        {
            CardScoringGameViewModel = cardScoringGameViewModel;
        }
    }
}
