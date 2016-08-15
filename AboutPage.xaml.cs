﻿using System;
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
using System.Reflection;

namespace TicTacToe
{
    public partial class AboutPage : PhoneApplicationPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string asmFullName = Assembly.GetExecutingAssembly().FullName;
            string[] asmOne = asmFullName.Split('=');

            if (asmOne.Length > 1)
            {
                string[] asmTwo = asmOne[1].Split(',');

                if (asmTwo.Length > 0)
                    VersionSpan.Text = String.Format("Version {0}", asmTwo[0]);
            }
        }
    }
}