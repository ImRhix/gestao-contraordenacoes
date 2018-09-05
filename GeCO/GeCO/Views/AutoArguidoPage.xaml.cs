using GeCO.Models;
using System;
using Xamarin.Forms;
using GeCO.ViewModels;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GeCO.Views {
    
    public partial class AutoArguidoPage : ContentPage {
        private int currentAutoId, currentArguidoId;
        private Pessoa _pessoa;
        private QualidadeArguido _qualidadeArguido;


        public AutoArguidoPage(int id) {
            InitializeComponent();

            BindingContext = new AutoArguidoVM();
            currentAutoId = id;
        }

        protected override async void OnAppearing() {
            var geral = await (BindingContext as AutoArguidoVM).GetGeral(currentAutoId);
            Debug.WriteLine($"\nArgId {geral.ArguidoId}\nDenId {geral.DenuncianteId}\nTesId {geral.TestemunhaId}\n");
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
        /// Colocar o VM.Filtered igual ao MeusAutosVM.Filterded..
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
        public async Task OnGuardarClicked(object sender, System.EventArgs e) {
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

            var page = new AutoApreensaoPage(currentAutoId);
            await Navigation.PushAsync(page);

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

            QualidadeArguido = new QualidadeArguido {
                QualidadeTipo =     qualidade.ToString(),
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

            qualidade.SelectedItem =    "Não Definido";
        }



#region REGION -> Properties
        public Pessoa Pessoa {
            get { return _pessoa; }
            set
            {
                _pessoa = value;
                OnPropertyChanged();
            }
        }

        public QualidadeArguido QualidadeArguido {
            get { return _qualidadeArguido; }
            set
            {
                _qualidadeArguido = value;
                OnPropertyChanged();
            }
        }
#endregion

#region REGION -> tap nos separadores
        void OnIdentificacaoTapped(object sender, System.EventArgs e) {
            identificacaoStack.IsVisible = !identificacaoStack.IsVisible;

            if (identificacaoArrow.Rotation == 0)
                identificacaoArrow.RotateTo(-180, 225);
            else
                identificacaoArrow.RotateTo(0, 225);
        }

        void OnQualidadeTapped(object sender, System.EventArgs e) {
            qualidadeStack.IsVisible = !qualidadeStack.IsVisible;

            if (qualidadeArrow.Rotation == 0)
                qualidadeArrow.RotateTo(-180, 225);
            else
                qualidadeArrow.RotateTo(0, 225);
        }
#endregion
    }
}
