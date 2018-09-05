using GeCO.Models;
using GeCO.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GeCO.Views {
	[XamlCompilation(XamlCompilationOptions.Compile)]

	public partial class PessoaPage : ContentPage {

        private int currentPessoaId;
        private Pessoa _pessoa;

        public PessoaPage(int id) {
            InitializeComponent();
            BindingContext = new PessoasVM();
            currentPessoaId = id;
        }


        protected override async void OnAppearing() {
            if (currentPessoaId != 0) {
                var pess = await (BindingContext as PessoasVM).GetPessoa(currentPessoaId);

                currentPessoaId =           pess.PessoaId;
                nome.Text =                 pess.Nome;
                dataNasc.Date =             pess.DataNascimento;
                genero.SelectedItem =       pess.Genero;
                estadoCiv.SelectedItem =    pess.EstadoCivil;
                nacionalidade.Text =        pess.Nacionalidade;
                naturalidade.Text =         pess.Naturalidade;
                nif.Text =                  pess.NIF.ToString();
                contacto1.Text =            pess.Contacto1.ToString();
                contacto2.Text =            pess.Contacto2.ToString();
                email.Text =                pess.Email;    
            }
            else {
                clearInput();
            }

            base.OnAppearing();
        }


        /// <summary>
        /// Apaga a pessoa e volta para a página anterior
        /// </summary>
        async void OnApagarClicked(object sender, System.EventArgs e) {
            IsEnabled = false;
            loadObjetos();
            await (BindingContext as PessoasVM).ApagarPessoa(Pessoa as Pessoa);
            await Navigation.PopAsync();
            IsEnabled = true;
        }



        /// <summary>
        /// Guarda a pessoa
        /// </summary>
        async void OnGuardarClicked(object sender, System.EventArgs e) {
            IsEnabled = false;
            await loadAndSave();
            await DisplayAlert("Info", "Pessoa guardada com sucesso.", "Ok");
            IsEnabled = true;
        }



        /// <summary>
        /// Carrega os objetos com o input correspondente das entries/pickers
        /// </summary>
        private void loadObjetos() {
            Pessoa = new Pessoa {
                PessoaId =          currentPessoaId,
                Nome =              nome.Text,
                DataNascimento =    dataNasc.Date,
                Genero =            genero.SelectedItem.ToString(),
                EstadoCivil =       estadoCiv.SelectedItem.ToString(),
                Nacionalidade =     nacionalidade.Text,
                Naturalidade =      naturalidade.Text,
                NIF =               Int32.Parse(nif.Text),
                Contacto1 =         Int32.Parse(contacto1.Text),
                Contacto2 =         Int32.Parse(contacto2.Text),
                Email =             email.Text
            };
        }



        /// <summary>
        /// Chama a funçao loadObjetos() e grava/atualiza a pessoa na BD
        /// </summary>
        private async Task loadAndSave() {
            loadObjetos();
            var pess = await (BindingContext as PessoasVM).GuardarPessoa(Pessoa as Pessoa);
            currentPessoaId = pess.PessoaId;
        }


        /// <summary>
        /// Limpa o texto das entries e seleciona os valores default dos dropdowns.
        /// </summary>
        private void clearInput() {
            currentPessoaId =           0;

            nome.Text =                 "";
            dataNasc.Date =             DateTime.Today;
            genero.SelectedItem =       "Não Definido";
            estadoCiv.SelectedItem =    "Não Definido";
            nacionalidade.Text =        "";
            naturalidade.Text =         "";
            nif.Text =                  0.ToString();
            contacto1.Text =            0.ToString();
            contacto2.Text =            0.ToString();
            email.Text =                "";
        }





        #region REGION -> Properties
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
