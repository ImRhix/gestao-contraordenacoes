using System;
using System.Collections.Generic;

using Xamarin.Forms;
using GeCO.ViewModels;

namespace GeCO.Views {
    
    public partial class AutoResumoPage : ContentPage {
        private int currentAutoId;

        public AutoResumoPage(int id) {
            InitializeComponent();

            currentAutoId = id;
            BindingContext = new AutoResumoVM();
        }

        async void OnAnteriorClicked(object sender, System.EventArgs e) {
            IsEnabled = false;

            await Navigation.PopAsync();

            IsEnabled = true;
        }

        async void OnDeleteClicked(object sender, System.EventArgs e) {
            IsEnabled = false;

            bool isDeletable = await DisplayAlert("Apagar Auto", "Está prestes a eliminar o presente Auto.\nPretende proseguir?", "Sim", "Não");
            if (isDeletable) {
                await (BindingContext as AutoResumoVM).ApagarAuto(currentAutoId);
                await Navigation.PopToRootAsync();
            }

            IsEnabled = true;
        }
    }
}
