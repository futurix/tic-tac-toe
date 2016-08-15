using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace TicTacToe
{
    public partial class App : Application
    {
        public PhoneApplicationFrame RootFrame { get; private set; }

        public GameDifficulty GamePlayerX { get; set; }
        public GameDifficulty GamePlayerO { get; set; }
        public bool GameIsFirstX { get; set; }
        public int GameTurn { get; set; }
        public int GamexWon { get; set; }
        public int GameoWon { get; set; }
        public int GameCats { get; set; }
        public char[] GameState { get; set; }
        public bool[] GameHilite { get; set; }

        public bool IsFirstLaunch { get; set; }

        public App()
        {
            UnhandledException += Application_UnhandledException;

            InitializeComponent();
            InitializePhoneApplication();
        }

        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            LoadSettings();
        }

        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            LoadSettings();
        }

        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            SaveSettings();
        }

        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            SaveSettings();
        }

        private void LoadSettings()
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

            IsFirstLaunch = !settings.Contains("GameTurn");

            if (settings.Contains("GamePlayerX"))
                GamePlayerX = (GameDifficulty)settings["GamePlayerX"];
            else
                GamePlayerX = GameDifficulty.Human;

            if (settings.Contains("GamePlayerO"))
                GamePlayerO = (GameDifficulty)settings["GamePlayerO"];
            else
                GamePlayerO = GameDifficulty.Experienced;

            if (settings.Contains("GameIsFirstX"))
                GameIsFirstX = (bool)settings["GameIsFirstX"];
            else
                GameIsFirstX = true;

            if (settings.Contains("GameTurn"))
                GameTurn = (int)settings["GameTurn"];
            else
            {
                if (GameIsFirstX)
                    GameTurn = -1;
                else
                    GameTurn = 1;
            }

            if (settings.Contains("GameWonX"))
                GamexWon = (int)settings["GameWonX"];
            else
                GamexWon = 0;

            if (settings.Contains("GameWonO"))
                GameoWon = (int)settings["GameWonO"];
            else
                GameoWon = 0;

            if (settings.Contains("GameCats"))
                GameCats = (int)settings["GameCats"];
            else
                GameCats = 0;

            if (settings.Contains("GameState"))
                GameState = (char[])settings["GameState"];
            else
                GameState = new char[] { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };

            if (settings.Contains("GameHilite"))
                GameHilite = (bool[])settings["GameHilite"];
            else
                GameHilite = new bool[] { false, false, false, false, false, false, false, false, false };
        }

        private void SaveSettings()
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

            settings["GamePlayerX"] = GamePlayerX;
            settings["GamePlayerO"] = GamePlayerO;
            settings["GameIsFirstX"] = GameIsFirstX;
            settings["GameTurn"] = GameTurn;
            settings["GameWonX"] = GamexWon;
            settings["GameWonO"] = GameoWon;
            settings["GameCats"] = GameCats;
            settings["GameState"] = GameState;
            settings["GameHilite"] = GameHilite;
        }

        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (Debugger.IsAttached)
                Debugger.Break();
        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
                Debugger.Break();
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}