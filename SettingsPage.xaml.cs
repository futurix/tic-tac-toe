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
                    return "human";

                case GameDifficulty.Novice:
                    return "newbie";

                case GameDifficulty.Intermediate:
                    return "beginner";

                case GameDifficulty.Experienced:
                    return "experienced";

                case GameDifficulty.Expert:
                    return "expert";
            }

            return String.Empty;
        }

        public GameDifficulty StringToDifficulty(string input)
        {
            switch (input)
            {
                case "human":
                    return GameDifficulty.Human;

                case "newbie":
                    return GameDifficulty.Novice;

                case "beginner":
                    return GameDifficulty.Intermediate;

                case "experienced":
                    return GameDifficulty.Experienced;

                case "expert":
                    return GameDifficulty.Expert;
            }

            return GameDifficulty.Experienced;
        }
    }
}