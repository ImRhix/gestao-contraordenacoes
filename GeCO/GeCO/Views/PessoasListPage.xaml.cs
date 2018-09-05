using GeCO.Models;
using GeCO.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeCO.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PessoasListPage : ContentPage {

       
        private Pessoa _pessoa;

        public PessoasListPage() {
            InitializeComponent();
            BindingContext = new PessoasVM();
        }



        protected override async void OnAppearing() {
            pessoasListView.ItemsSource = await (BindingContext as PessoasVM).GetPessoas();
            base.OnAppearing();
        }


        /// <summary>
        /// Altera as pessoas visiveis na ListView consoante o que o utilizador escrever na searchbox.
        /// </summary>
        async void OnTextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e) {
            if (!string.IsNullOrWhiteSpace(e.NewTextValue))
                pessoasListView.ItemsSource = (BindingContext as PessoasVM).Filtered(e.NewTextValue);
            else {
                pessoasListView.ItemsSource = await (BindingContext as PessoasVM).GetPessoas();
            }
        }


        /// <summary>
        /// É acionada ao selecionar um auto da lista. Chama a função EditAuto. 
        /// </summary>
        async void OnPessoaSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e) {
            var item = e.SelectedItem as Pessoa;
            await EditPessoa(item);
        }

        /// <summary>
        /// Chama a função EditPessoa. É acionado ao clicar na ContextAction 'Editar' da ViewCell.
        /// </summary>
        async void OnEditClicked(object sender, System.EventArgs e) {
            var item = ((MenuItem)sender);
            await EditPessoa(item.CommandParameter as Pessoa);
        }


        /// <summary>
        /// Abre a página PessoaPage onde se pode adicionar um novo auto.
        /// </summary>
        async void OnAddClicked(object sender, System.EventArgs e) {
            var page = new PessoaPage(0);
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
        /// Abre a PessoaPage mas preenchida com a informação do auto selecionado.
        /// </summary>
        async Task EditPessoa(Pessoa pessoa) {
            var page = new PessoaPage(pessoa.PessoaId);
            await Navigation.PushAsync(page);
        }



        #region Inicialização de propriedades

        public Pessoa Pessoa {
            get { return _pessoa; }
            set {
                _pessoa = value;
                OnPropertyChanged();
            }
        }
        #endregion


    }
}