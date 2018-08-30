using System;
using System.Collections.Generic;

using Xamarin.Forms;
using GeCO.Models;

namespace GeCO.Views{
    public partial class LoginPage : ContentPage{
        
        public LoginPage(){
            InitializeComponent();
            Init();
        }



       void Init(){
            activityIndicator.IsVisible = false;
            logoImage.HeightRequest = Constants.logoHeight;
        }



        async void OnSignInClicked(object sender, System.EventArgs e){
            IsEnabled = false;
            if (true){
                await DisplayAlert("Login", "Login successful", "Ok");
                var page = new MainMasterPage();
                Application.Current.MainPage = page;        // MainPage inicial é LoginPage. Com um login bem sucedido basta trocar MainPage para a pagina desejada. 
            }
            IsEnabled = true;
        }
    }
}
