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
    public partial class MainPage : PhoneApplicationPage
    {
        private List<TextBlock> cells = new List<TextBlock>();
        
        public MainPage()
        {
            InitializeComponent();

            cells.AddRange(
                new TextBlock[] { block1, block2, block3, block4, block5, block6, block7, block8, block9 }
            );
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            App app = App.Current as App;

            PlayerX = app.GamePlayerX;
            PlayerO = app.GamePlayerO;
            IsFirstX = app.GameIsFirstX;

            turn = app.GameTurn;
            xWon = app.GamexWon;
            oWon = app.GameoWon;
            catsGame = app.GameCats;

            for (int i = 0; i < app.GameState.Length; i++)
            {
                switch (app.GameState[i])
                {
                    case ' ':
                        cells[i].Text = String.Empty;
                        break;

                    case 'O':
                        cells[i].Text = sZero;
                        break;

                    case 'X':
                        cells[i].Text = sCross;
                        break;
                }
            }

            if (app.IsFirstLaunch)
            {
                app.IsFirstLaunch = false;
                
                NextTurn();
            }
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            App app = App.Current as App;

            app.GamePlayerX = PlayerX;
            app.GamePlayerO = PlayerO;
            app.GameIsFirstX = IsFirstX;
            app.GameTurn = turn;
            app.GamexWon = xWon;
            app.GameoWon = oWon;
            app.GameCats = catsGame;

            for (int i = 0; i < cells.Count; i++)
            {
                switch (cells[i].Text)
                {
                    case "":
                        app.GameState[i] = ' ';
                        break;

                    case sZero:
                        app.GameState[i] = 'O';
                        break;

                    case sCross:
                        app.GameState[i] = 'X';
                        break;
                }
            }
        }

        private void block_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Move((TextBlock)sender);
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void StatsButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/StatsPage.xaml", UriKind.Relative));
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }
    }
}