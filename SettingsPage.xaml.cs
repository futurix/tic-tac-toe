using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace TicTacToe
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Reset all statistics", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                (App.Current as App).GamexWon = 0;
                (App.Current as App).GameoWon = 0;
                (App.Current as App).GameCats = 0;
            }
        }

        public string[] DifficultyOptions
        {
            get 
            {
                return new string[] { 
                    DifficultyToString(GameDifficulty.Human),
                    DifficultyToString(GameDifficulty.Novice),
                    DifficultyToString(GameDifficulty.Intermediate),
                    DifficultyToString(GameDifficulty.Experienced),
                    DifficultyToString(GameDifficulty.Expert) 
                };
            }
        }

        public string DifficultyX
        {
            get
            {
                return DifficultyToString((App.Current as App).GamePlayerX);
            }
            set
            {
                (App.Current as App).GamePlayerX = StringToDifficulty(value);
            }
        }

        public string DifficultyO
        {
            get
            {
                return DifficultyToString((App.Current as App).GamePlayerO);
            }
            set
            {
                (App.Current as App).GamePlayerO = StringToDifficulty(value);
            }
        }

        public string DifficultyToString(GameDifficulty input)
        {
            switch (input)
            {
                case GameDifficulty.Human:
                    return "human player";

                case GameDifficulty.Novice:
                    return "phone (dumb)";

                case GameDifficulty.Intermediate:
                    return "phone (stupid)";

                case GameDifficulty.Experienced:
                    return "phone (ok-ish)";

                case GameDifficulty.Expert:
                    return "phone (good)";
            }

            return String.Empty;
        }

        public GameDifficulty StringToDifficulty(string input)
        {
            switch (input)
            {
                case "human player":
                    return GameDifficulty.Human;

                case "phone (dumb)":
                    return GameDifficulty.Novice;

                case "phone (stupid)":
                    return GameDifficulty.Intermediate;

                case "phone (ok-ish)":
                    return GameDifficulty.Experienced;

                case "phone (good)":
                    return GameDifficulty.Expert;
            }

            return GameDifficulty.Expert;
        }
    }
}