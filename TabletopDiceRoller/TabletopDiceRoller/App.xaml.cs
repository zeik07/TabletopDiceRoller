using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Diagnostics;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TabletopDiceRoller
{
    public partial class App : Application
    {
        static CustomRollDatabase database;

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        public static CustomRollDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new CustomRollDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CustomRoll.db3"));
                }
                return database;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
