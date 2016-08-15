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

namespace TicTacToe
{
    public partial class GameCell : UserControl
    {
        public GameCell()
        {
            InitializeComponent();
        }
        
        public GameCellState CellState
        {
            get
            {
                if (XPath.Visibility == Visibility.Visible)
                    return GameCellState.X;
                else if (OPath.Visibility == Visibility.Visible)
                    return GameCellState.O;
                else
                    return GameCellState.Clear;
            }
            set
            {
                switch (value)
                {
                    case GameCellState.X:
                        OPath.Visibility = Visibility.Collapsed;
                        XPath.Visibility = Visibility.Visible;
                        break;

                    case GameCellState.O:
                        XPath.Visibility = Visibility.Collapsed;
                        OPath.Visibility = Visibility.Visible;
                        break;

                    case GameCellState.Clear:
                        XPath.Visibility = Visibility.Collapsed;
                        OPath.Visibility = Visibility.Collapsed;
                        break;
                }
            }
        }

        public bool IsHighlighted
        {
            get 
            {
                bool res = false;
                Path temp = null;

                if (XPath.Visibility == System.Windows.Visibility.Visible)
                    temp = XPath;
                else if (OPath.Visibility == System.Windows.Visibility.Visible)
                    temp = OPath;

                if (temp != null)
                    res = (temp.Style == Resources["SelectedStyle"] as Style);

                return res;
            }
            set
            {
                Path temp = null;

                if (XPath.Visibility == System.Windows.Visibility.Visible)
                    temp = XPath;
                else if (OPath.Visibility == System.Windows.Visibility.Visible)
                    temp = OPath;

                if (temp != null)
                {
                    if (value)
                        temp.Style = Resources["SelectedStyle"] as Style;
                    else
                        temp.Style = Resources["ClearStyle"] as Style;
                }
            }
        }
    }
    
    public enum GameCellState
    {
        X,
        O,
        Clear
    }
}
