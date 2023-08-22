using CardScoringGame.App.ViewModel;
using Microsoft.UI.Xaml;

namespace CardScoringGame.App
{
    public sealed partial class CardGameWindow
    {
        public CardGameWindowViewModel ViewModel { get; set; }

        public CardGameWindow(CardGameWindowViewModel viewModel)
        {
            this.InitializeComponent();
            Title = "Two Player Card Scoring Game";
            ViewModel = viewModel;
            var windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(this);
            PInvoke.User32.ShowWindow(windowHandle, PInvoke.User32.WindowShowStyle.SW_MAXIMIZE);
        }
    }
}
