﻿using System;
using ECommerceApp.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECommerceApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new SignUpPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
