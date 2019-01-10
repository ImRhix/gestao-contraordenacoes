using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace GeCO.Views {
    
    public partial class MainMasterPage : MasterDetailPage {

        //Color colorPrimary = (Color)Application.Current.Resources["PrimaryColor"];
        Color colorPrimaryDark = (Color)Application.Current.Resources["PrimaryDarkColor"];

        public MainMasterPage() {
            InitializeComponent();

            Detail = new NavigationPage(new DashboardPage());
            IsPresented = false;
        }

        protected override void OnAppearing() {
            base.OnAppearing();
        }


        /// <summary>
        /// Side-menu. Tap no logo do Funchal redireciona para a Dashboard.
        /// </summary>
        void OnDashboardTapped(object sender, System.EventArgs e) {
            Detail = new NavigationPage(new DashboardPage());
            IsPresented = false;
        }


        /// <summary>
        /// Side-menu. Expande ou esconde os sub-items da categoria Contraordenações. 
        /// </summary>
        void OnContraordenacoesTapped(object sender, System.EventArgs e) {
            subMenuCO.IsVisible = !subMenuCO.IsVisible;

            menuCO.BackgroundColor = Color.Transparent;
            if (subMenuCO.IsVisible == true){
                menuCO.BackgroundColor = colorPrimaryDark;
            }
        }


        /// <summary>
        /// Side-menu. Tap no botão Pessoas.
        /// </summary>
        void OnPessoasTapped(object sender, System.EventArgs e) {
            Detail = new NavigationPage(new PessoasListPage());
            IsPresented = false;
        }

        /// <summary>
        /// Side-menu (Sub-item de Contraordenações). Redireciona para a pagina MeusAutos, onde se encontra a lista de Autos em sistema.
        /// </summary>
        void OnMeusAutosTapped(object sender, System.EventArgs e) {
            Detail = new NavigationPage();
            Detail.Navigation.PushAsync(new MeusAutosPage());
			IsPresented = false;
        }


        /// <summary>
        /// Side-menu. Inativo.
        /// </summary>
        void OnPlaceholderTapped(object sender, System.EventArgs e) {
            DisplayAlert("Placeholder Tapped.", "There is no page associated with this item yet.", "Ok");
            IsPresented = false;
        }
    }
}
