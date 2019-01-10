using System;
using System.Collections.Generic;

using Xamarin.Forms;
using GeCO.ViewModels;

namespace GeCO.Views {
    
    public partial class AutoResumoPage : ContentPage {

        private int currentAutoId;
        private bool isNewAuto;


        public AutoResumoPage(int id, bool state) {
            InitializeComponent();

            currentAutoId = id;
            isNewAuto = state;
            BindingContext = new AutoResumoVM();
        }



        /// <summary>
        /// "Guarda" o auto e retorna para a dashboard.
        /// </summary>
        private async void OnGuardarClicked(object sender, EventArgs e) {
            await DisplayAlert("Guardar e Sair", "O Auto foi guardado e o formulário será fechado.\n\nPoderá realizar uma consulta na página 'Meus Autos'.", "Ok");
            await Navigation.PopToRootAsync();
        }




        /// <summary>
        /// Retorna para a página anterior
        /// </summary>
        async void OnAnteriorClicked(object sender, System.EventArgs e) {
            IsEnabled = false;

            await Navigation.PopAsync();

            IsEnabled = true;
        }
                     



        /// <summary>
        /// Apaga o auto da DB caso a resposta ao alerta seja positiva
        /// </summary>
        async void OnDeleteClicked(object sender, System.EventArgs e){
            IsEnabled = false;

            bool isDeletable = await DisplayAlert("Apagar Auto", "Está prestes a eliminar o presente Auto.\nPretende proseguir?", "Sim", "Não");
            if (isDeletable)
            {
                await (BindingContext as AutoResumoVM).ApagarAuto(currentAutoId);
                await Navigation.PopToRootAsync();
            }

            IsEnabled = true;
        }
    }
}
