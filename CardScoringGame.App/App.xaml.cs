using CardScoringGame.App.ViewModel;
using CardScoringGame.ViewModel;
using CardScroingGame.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using System;

namespace CardScoringGame.App
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        #region Private Members
        private IHost _host;
        private CardGameWindow _cardGameWindow;
        #endregion


        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);
            _host = Host.CreateDefaultBuilder()
           .ConfigureServices((hostContext, services) =>
           {
               services.AddScoped<CardGameWindowViewModel>();
               services.AddScoped<CardScroingGameUserControl>();
               services.AddScoped<CardScoringGameViewModel>();
               services.AddSingleton<CardGameWindow>();
           }).Build();

            using (var serviceScope = _host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    _cardGameWindow = services.GetRequiredService<CardGameWindow>();
                    _cardGameWindow.Activate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Occured" + ex.Message);
                }
            }
        }
    }
}
