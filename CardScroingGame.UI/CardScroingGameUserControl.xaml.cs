using CardScoringGame.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CardScroingGame.UI
{
    public sealed partial class CardScroingGameUserControl : UserControl
    {
        public CardScoringGameViewModel ViewModel
        {
            get { return (CardScoringGameViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(CardScoringGameViewModel), typeof(CardScroingGameUserControl), new PropertyMetadata(null));

        public CardScroingGameUserControl()
        {
            this.InitializeComponent();            
        }
    }
}
