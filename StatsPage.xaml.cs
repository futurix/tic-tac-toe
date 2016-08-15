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
    public partial class StatsPage : PhoneApplicationPage
    {
        public StatsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //

            /*
            var b = document.board;
            var totalGames = xWon + oWon + catsGame;
            b.xWon.value = xWon;
            b.oWon.value = oWon;
            b.catsGame.value = catsGame;
            b.xWonPer.value = ((xWon == 0) ? 0 : (Math.round(xWon * 1000 / totalGames) / 10)) + '%';
            b.oWonPer.value = ((oWon == 0) ? 0 : (Math.round(oWon * 1000 / totalGames) / 10)) + '%';
            b.catsGamePer.value = ((catsGame == 0) ? 0 : (Math.round(catsGame * 1000 / totalGames) / 10)) + '%';
            */
        }

        public string WinsX
        {
            get
            {
                return (App.Current as App).GamexWon.ToString();
            }
        }

        public string WinsO
        {
            get
            {
                return (App.Current as App).GameoWon.ToString();
            }
        }

        public string Draws
        {
            get
            {
                return (App.Current as App).GameCats.ToString();
            }
        }
    }
}