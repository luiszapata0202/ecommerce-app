﻿using System;
using ECommerceApp.Helpers;
using ECommerceApp.Interfaces;
using ECommerceApp.Pages;
using ECommerceApp.PlatformSpecific;
using ECommerceApp.Services;
using ECommerceApp.ViewModels;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ECommerceApp
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected async override void OnInitialized()
        {
            InitializeComponent();

            var accessToken = Preferences.Get("accessToken", string.Empty);

            if (string.IsNullOrEmpty(accessToken))
            {
                await NavigationService.NavigateAsync("NavigationPage/SignUpPage");
            }
            else
            {
                if (TokenValidator.CheckTokenValidity())
                {
                    await NavigationService.NavigateAsync("NavigationPage/HomePage");
                }
                else
                {
                    await NavigationService.NavigateAsync("NavigationPage/SignUpPage");
                }
            }
        }

        protected override void OnStart()
        {
            var setupTheme = DependencyService.Get<ISetupTheme>();
            setupTheme.SetStatusBarColor((Color)Current.Resources["StatusBar"]);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();

            containerRegistry.Register<IUserService, UserService>();
        }
    }
}
