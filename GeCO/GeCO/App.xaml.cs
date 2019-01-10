using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GeCO.Views;
using GeCO.Data;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]    // importante
namespace GeCO {
    public partial class App : Application {
        static AutoDatabase database;

        public App() {
            InitializeComponent();
            MainPage = new LoginPage();
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }



        public static AutoDatabase Database {
            get {
                if (database == null){
                    database = new AutoDatabase(DependencyService.Get<ISQLiteHelper>().GetLocalPath("Autos.db3"));
                }
                return database;
            }
        }
    }
}
