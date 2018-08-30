using GeCO.Models;
using GeCO.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GeCO.Views {

	public partial class MeusAutosPage : ContentPage {

        private Geral _geral;
        private Pessoa _pessoa;
        private MeuAuto _meuAuto;

        public MeusAutosPage() {
            InitializeComponent();

            BindingContext = new MeusAutosVM();
        }


        protected override async void OnAppearing() {
            autosListView.ItemsSource = await (BindingContext as MeusAutosVM).GetAutos();
            base.OnAppearing();
        }   


        /// <summary>
        /// Altera os autos visiveis na ListView consoante o que o utilizador escrever na searchbox.
        /// </summary>
        async void OnTextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e) {
            if (!string.IsNullOrWhiteSpace(e.NewTextValue))
                autosListView.ItemsSource = (BindingContext as MeusAutosVM).Filtered(e.NewTextValue);
            else {
                autosListView.ItemsSource = await (BindingContext as MeusAutosVM).GetAutos();
            }
        }


        /// <summary>
        /// É acionada ao selecionar um auto da lista. Chama a função EditAuto. 
        /// </summary>
        async void OnAutoSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e) {
            var item = e.SelectedItem as MeuAuto;
            await EditAuto(item);
        }

        /// <summary>
        /// Chama a função EditAuto. É acionado ao clicar na ContextAction 'Editar' da ViewCell.
        /// </summary>
        async void OnEditClicked(object sender, System.EventArgs e) {
            var item = ((MenuItem)sender);
            await EditAuto(item.CommandParameter as MeuAuto);
        }


        /// <summary>
        /// Abre a página AutoPrincipalPage onde se pode adicionar um novo auto.
        /// </summary>
        async void OnAddClicked(object sender, System.EventArgs e) {
            var page = new AutoPrincipalPage(0, true);
            await Navigation.PushAsync(page);
        }


        /// <summary>
        /// Cria uma nova MainPage (que por sua vez abre a Dashboard)
        /// </summary>
        protected override bool OnBackButtonPressed() {
            var page = new MainMasterPage();
            Application.Current.MainPage = page;
            return true;
        }


        /// <summary>
        /// Abre a AutoPrincipalPage mas preenchida com a informação do auto selecionado.
        /// </summary>
        async Task EditAuto(MeuAuto meuAuto) {
            var page = new AutoPrincipalPage(meuAuto.AutoId, false);
            await Navigation.PushAsync(page);
        }


        
#region Inicialização de propriedades
        public Geral Geral {
            get { return _geral; }
            set {
                _geral = value;
                OnPropertyChanged();
            }
        }
        
        public Pessoa Pessoa {
            get { return _pessoa; }
            set {
                _pessoa = value;
                OnPropertyChanged();
            }
        }
        
        public MeuAuto MeuAuto{
            get { return _meuAuto; }
            set
            {
                _meuAuto = value;
                OnPropertyChanged();
            }
        }
#endregion
    }
}
