using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TabletopDiceRoller
{
    public partial class App : Application
    {
        static SavedRollDatabase database;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public static SavedRollDatabase Database
        {            
            get
            {
                if (database == null)
                {
                    database = new SavedRollDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CustomRoll.db3"));
                }
                return database;
            }
        }

        public int ResumeAtProfileId { get; set; }
        public int ResumeAtFolderId { get; set; }
        public int ResumeAtRollId { get; set; }
        public int ResumeAtLevelId { get; set; }
        public int ResumeAtCritId { get; set; }
        public int ResumeAtSaveId { get; set; }
                
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
