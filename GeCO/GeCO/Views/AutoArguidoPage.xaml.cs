﻿using GeCO.Models;
using System;
using Xamarin.Forms;
using GeCO.ViewModels;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GeCO.Views {
    
    public partial class AutoArguidoPage : ContentPage {

        private int currentAutoId, currentArguidoId;
        private bool isNewAuto;
        private Pessoa _pessoa;


        public AutoArguidoPage(int id, bool state) {
            InitializeComponent();

            BindingContext = new AutoArguidoVM();
            
            isNewAuto = state;
            currentAutoId = id;
        }



        protected override async void OnAppearing() {

            var geral = await (BindingContext as AutoArguidoVM).GetGeral(currentAutoId);
            currentArguidoId = geral.ArguidoId;

            if (currentArguidoId != 0) {
                var pess = await (BindingContext as AutoArguidoVM).GetPessoa(currentArguidoId);

                currentArguidoId =          pess.PessoaId;
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

            pessoaListView.ItemsSource = await (BindingContext as AutoArguidoVM).GetListaPessoas();

            base.OnAppearing();
        }



        /// <summary>
        /// Colocar o VM.Filtered igual ao MeusAutosVM.Filtered..
        /// </summary>
        async void OnTextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e) {
            if (!string.IsNullOrWhiteSpace(e.NewTextValue)) {
                var lista = (BindingContext as AutoArguidoVM).Filtered(e.NewTextValue);
                pessoaListView.ItemsSource = lista;
                pessoaListView.IsVisible = true;
                pessoaListView.HeightRequest = (pessoaListView.RowHeight * lista.Count);
            }
            else {
                pessoaListView.ItemsSource = await (BindingContext as AutoArguidoVM).GetListaPessoas();
                pessoaListView.IsVisible = false;
            }
        }


        /// <summary>
        /// Popula (ou repopula) o StackLayout do Denunciante com informação de um objeto Pessoa.
        /// </summary>
        void OnPessoaSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e) {
            var pessSelecionada =       e.SelectedItem as Pessoa;

            currentArguidoId =          pessSelecionada.PessoaId;
            nome.Text =                 pessSelecionada.Nome;
            dataNasc.Date =             pessSelecionada.DataNascimento;
            genero.SelectedItem =       pessSelecionada.Genero;
            estadoCiv.SelectedItem =    pessSelecionada.EstadoCivil;
            nacionalidade.Text =        pessSelecionada.Nacionalidade;
            naturalidade.Text =         pessSelecionada.Naturalidade;
            nif.Text =                  pessSelecionada.NIF.ToString();
            contacto1.Text =            pessSelecionada.Contacto1.ToString();
            contacto2.Text =            pessSelecionada.Contacto2.ToString();
            email.Text =                pessSelecionada.Email;

            pessoaListView.IsVisible =  false;
            searchBox.Text =            "";

        }


        

        /// <summary>
        /// Guarda a pessoa e associa o id à FK TestemunhaId na tabela Geral
        /// </summary>
        public async void OnGuardarClicked(object sender, System.EventArgs e) {
            await loadAndSave();
        }


        /// <summary>
        /// Limpa o input das entries e desassocia o id da testemunha pessoa do presente Auto.
        /// </summary>
        private async void OnApagar(object sender, EventArgs e) {
            clearInput();

            await (BindingContext as AutoArguidoVM).DesassociarPessoa(currentArguidoId, currentAutoId);
        }




        /// <summary>
        /// Destrói a presente página.
        /// </summary>
        async void OnAnteriorClicked(object sender, System.EventArgs e) {
            IsEnabled = false;

            await Navigation.PopAsync();

            IsEnabled = true;
        }



        /// <summary>
        /// Grava informação e abre a próxima página.
        /// </summary>
        async void OnProximoClicked(object sender, System.EventArgs e) {
            IsEnabled = false;

            await loadAndSave();

            var page = new AutoApreensaoPage(currentAutoId, isNewAuto);
            await Navigation.PushAsync(page);

            IsEnabled = true;
        }


        /// <summary>
        /// Fecha todas as janelas do form e volta à página inicial. Se o utilizdor desejar pode também apagar a informação do auto.
        /// </summary>
        async void OnCancelClicked(object sender, System.EventArgs e) {
            IsEnabled = false;

            if (isNewAuto) {
                bool isDeletable = await DisplayAlert("Atenção", "Está prestes a sair do formulário.\nPretende também apagar a informação já inserida?", "Sim", "Não");
                if (isDeletable)
                    await (BindingContext as AutoArguidoVM).ApagarAuto(currentAutoId);
            }
            await Navigation.PopToRootAsync();
            IsEnabled = true;
        }


        /// <summary>
        /// Carrega os objetos com o input correspondente das entries/pickers
        /// </summary>
        private void loadObjetos() {
            Pessoa = new Pessoa {
                PessoaId =          currentArguidoId,
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

            var pess = await (BindingContext as AutoArguidoVM).GuardarPessoa(Pessoa as Pessoa, currentAutoId);
            currentArguidoId = pess.PessoaId;
        }


        /// <summary>
        /// Limpa o texto das entries e seleciona os valores default dos dropdowns.
        /// </summary>
        private void clearInput() {
            currentArguidoId =          0;

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

#region Tap Separadores
        void OnIdentificacaoTapped(object sender, System.EventArgs e) {
            identificacaoStack.IsVisible = !identificacaoStack.IsVisible;

            if (identificacaoArrow.Rotation == 0)
                identificacaoArrow.RotateTo(-180, 225);
            else
                identificacaoArrow.RotateTo(0, 225);
        }
#endregion
    }
}
