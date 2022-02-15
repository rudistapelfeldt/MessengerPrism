using System;
using System.Threading.Tasks;
using MessengerPrism.Services;
using MessengerPrism.Views;
using Prism;
using Prism.Ioc;
using Prism.Plugin.Popups;
using DryIoc;
using Prism.DryIoc;
using Prism.Logging;
using Xamarin.Forms;

using DebugLogger = MessengerPrism.Services.DebugLogger;
using MessengerPrism.ViewModels;

namespace MessengerPrism
{
    public partial class App : PrismApplication
    {
        /* 
         * NOTE: 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() 
            : this(null)
        {
        }

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            //handle incoming messages with the subscribe method
            new MessageStream().Subscribe(Console.WriteLine);

            //publish messages with the send method
            new MessageStream().Send("grp1", "My message");

            await NavigationService.NavigateAsync("MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
           
            // Navigating to "TabbedPage?createTab=ViewA&createTab=ViewB&createTab=ViewC will generate a TabbedPage
            // with three tabs for ViewA, ViewB, & ViewC
            // Adding `selectedTab=ViewB` will set the current tab to ViewB
            containerRegistry.RegisterForNavigation<TabbedPage>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<GroupPage, GroupPageViewModel>();
            containerRegistry.RegisterForNavigation<SplashScreenPage>();
            containerRegistry.RegisterForNavigation<TodoItemDetail>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle IApplicationLifecycle
            base.OnSleep();

            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle IApplicationLifecycle
            base.OnResume();

            // Handle when your app resumes
        }
    }
}
