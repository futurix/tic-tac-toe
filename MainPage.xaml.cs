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
        private List<GameCell> cells = new List<GameCell>();
        
        public MainPage()
        {
            InitializeComponent();

            TiltEffect.TiltableItems.Add(typeof(GameCell));

            cells.AddRange(
                new GameCell[] { block1, block2, block3, block4, block5, block6, block7, block8, block9 }
            );
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // setup gameplay
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
                        cells[i].CellState = GameCellState.Clear;
                        break;

                    case 'O':
                        cells[i].CellState = GameCellState.O;
                        break;

                    case 'X':
                        cells[i].CellState = GameCellState.X;
                        break;
                }
            }

            for (int i = 0; i < app.GameHilite.Length; i++)
                cells[i].IsHighlighted = app.GameHilite[i];

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
                switch (cells[i].CellState)
                {
                    case GameCellState.Clear:
                        app.GameState[i] = ' ';
                        break;

                    case GameCellState.O:
                        app.GameState[i] = 'O';
                        break;

                    case GameCellState.X:
                        app.GameState[i] = 'X';
                        break;
                }

                app.GameHilite[i] = cells[i].IsHighlighted;
            }
        }

        private void block_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Move((GameCell)sender);
        }

        private void PhoneApplicationPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            if ((e.Orientation == PageOrientation.LandscapeRight) || (e.Orientation == PageOrientation.LandscapeLeft))
                TitlePanel.Visibility = Visibility.Collapsed;
            else if ((e.Orientation == PageOrientation.PortraitDown) || (e.Orientation == PageOrientation.PortraitUp))
                TitlePanel.Visibility = Visibility.Visible;
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