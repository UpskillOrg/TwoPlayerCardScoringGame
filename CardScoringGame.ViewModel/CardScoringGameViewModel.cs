using CardScoringGame.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TwoPlayerCardScoringGame;

namespace CardScoringGame.ViewModel
{
    public partial class CardScoringGameViewModel : ObservableObject
    {
        #region Private Fields
        private bool _isGameStarted;

        private double _noOfPacks;

        [ObservableProperty]
        private int _pendingCardsToBePlayed;

        [ObservableProperty]
        private Model.CardMatchOption? _cardMatchOption = Model.CardMatchOption.CardValue;

        [ObservableProperty]
        private ObservableCollection<BaseResult> _gameResults = new ObservableCollection<BaseResult>();
        #endregion

        #region Public Members
        public List<Model.CardMatchOption>? CardMatchOptions { get; set; } = new List<Model.CardMatchOption>() { Model.CardMatchOption.CardValue, Model.CardMatchOption.Suite, Model.CardMatchOption.Both };

        public IGame? Game { get; set; }

        public IRelayCommand? StartGameCommand { get; set; }

        public IRelayCommand? PlayGameCommand { get; set; }

        public IRelayCommand? AutoPlayGameCommand { get; set; }

        public double NoOfPacks 
        { 
            get => _noOfPacks;
            set 
            {
                value = Math.Round(value);
                SetProperty(ref _noOfPacks, value);
                TriggerCanExcute();
                PendingCardsToBePlayed = (int)(_noOfPacks * 52);
            } 
        }
        #endregion

        #region Constructor
        public CardScoringGameViewModel()
        {
            NoOfPacks = 1;
            StartGameCommand = new AsyncRelayCommand(StartGame, CanStartGame);
            PlayGameCommand = new AsyncRelayCommand(PlayGame, CanPlayGame);
            AutoPlayGameCommand = new AsyncRelayCommand(AutoPlayGame, CanAutoPlayGame);
        }
        #endregion

        #region Private Methods
        private bool CanAutoPlayGame()
        {
            return NoOfPacks > 0 && CardMatchOption != null && _isGameStarted;
        }

        private async Task AutoPlayGame()
        {
            if (Game != null)
            {
                while (Game.Deck.Cards.Count != 0)
                {
                    await PlayGame();
                }
            }
        }

        private bool CanPlayGame()
        {
            return NoOfPacks > 0 && CardMatchOption != null && _isGameStarted;
        }

        private async Task PlayGame()
        {       
            if(Game!=null)
            {
                var result = await Game.PlayRound();
                GameResults.Add(result);
                
                PendingCardsToBePlayed = Game.Deck.Cards.Count;

                if (PendingCardsToBePlayed == 0)
                {                    
                    var finalResult = Game.EndGame();
                    GameResults.Add(finalResult);
                    _isGameStarted = false;
                    TriggerCanExcute();
                }

            }
        }

        private bool CanStartGame()
        {
            return NoOfPacks > 0 && CardMatchOption != null && !_isGameStarted;
        }

        private async Task StartGame()
        {
            Game = new Game((int?)NoOfPacks, CardMatchOption);
            await Game.Deck.Shuffle();
            PendingCardsToBePlayed = Game.Deck.Cards.Count;
            GameResults.Clear();
            _isGameStarted = true;
            TriggerCanExcute();
        }

        private void TriggerCanExcute()
        {
            StartGameCommand?.NotifyCanExecuteChanged();
            PlayGameCommand?.NotifyCanExecuteChanged();
            AutoPlayGameCommand?.NotifyCanExecuteChanged();
        }
        #endregion

        #region Partial Methods
        partial void OnCardMatchOptionChanged(Model.CardMatchOption? value)
        {
            TriggerCanExcute();
        }
        #endregion
    }
}
