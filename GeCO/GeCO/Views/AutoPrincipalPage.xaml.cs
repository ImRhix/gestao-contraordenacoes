using GeCO.Models;
using GeCO.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GeCO.Views {
    
    public partial class AutoPrincipalPage : ContentPage {
        
        private int currentAutoId, currentLocalId, currentDenuncianteId, currentAutuanteId;
        private int apreensaoFK, arguidoFK, testemunhaFK, leiFK, pagamentoFK;
        private bool isNewAuto;

        private Geral _geral;
        private Localizacao _localizacao;
        private Pessoa _pessoa;
        private Autuante _autuante;


       
        public AutoPrincipalPage(int Id, bool state) {
            InitializeComponent();

            currentAutoId = Id;
            isNewAuto = state;
            BindingContext = new AutoPrincipalVM();
        }



        protected override async void OnAppearing() {
            if (currentAutoId != 0) {
                var ger = await (BindingContext as AutoPrincipalVM).GetGeral(currentAutoId);

                var loc = await (BindingContext as AutoPrincipalVM).GetLocalizacao(ger.LocalId);
                var pes = await (BindingContext as AutoPrincipalVM).GetPessoa(ger.DenuncianteId);
                var aut = await (BindingContext as AutoPrincipalVM).GetAutuante(ger.AutuanteId);

                currentAutoId =             ger.AutoId;
                codProcesso.Text =          ger.CodProcesso;
                pastaFisica.Text =          ger.PastaFisica;
                data.Date =                 ger.DataAuto;
                hora.Time =                 ger.HoraAuto;
                isAcidente.IsToggled =      ger.IsAcidenteViacao;
                entDecisora.SelectedItem =  ger.EntDecisora;
                entAutuante.SelectedItem =  ger.EntidadeAutuante;
                entResp.SelectedItem =      ger.EntResponsavel;
                codInfracao.Text =          ger.CodigoInfracao;
                tipoInfracao.SelectedItem = ger.TipoInfracao;
                catInfracao.SelectedItem =  ger.CategoriaInfracao;
                apreensaoFK =               ger.ApreensaoId;        // apreensaoId, arguidoId, testemunhaId, leiId e pagamentoId:
                arguidoFK =                 ger.ArguidoId;          // estão aqui explícitos  para que  não se perca  o seu 
                testemunhaFK =              ger.TestemunhaId;       // valor ao efetuar a gravação do auto.
                leiFK =                     ger.LeiId;               
                pagamentoFK =               ger.PagamentoId;

                currentLocalId =            loc.LocalId;
                rua.Text =                  loc.Rua;
                edificoPorta.Text =         loc.EdificioPorta;
                localidade.Text =           loc.Localidade;
                cidade.Text =               loc.Cidade;

                currentDenuncianteId =      pes.PessoaId;
                nome.Text =                 pes.Nome;
                dataNasc.Date =             pes.DataNascimento;
                genero.SelectedItem =       pes.Genero;
                estadoCiv.SelectedItem =    pes.EstadoCivil;
                nacionalidade.Text =        pes.Nacionalidade;
                naturalidade.Text =         pes.Naturalidade;
                nif.Text =                  pes.NIF.ToString();
                contacto1.Text =            pes.Contacto1.ToString();
                contacto2.Text =            pes.Contacto2.ToString();
                email.Text =                pes.Email;

                currentAutuanteId =         aut.AutuanteId;
                autuante.SelectedItem =     aut.EntidadeAutuante;
                isPresenciado.IsToggled =   aut.IsPresenciado;
                equipamentos.SelectedItem = aut.Equipamento;
            }

            pessoaListView.ItemsSource = await (BindingContext as AutoPrincipalVM).GetListaPessoas();
         
            base.OnAppearing();
        }




        /// <summary>
        /// Guarda o input das entries nas respetivas tabelas da BD
        /// </summary>
        public async Task GuardarClicked(object sender, System.EventArgs e) {
            await loadAndSave();
        }


        /// <summary>
        /// Limpa o input das entries e reverte os pickers para os valores default.
        /// </summary>
        void OnApagar(object sender, System.EventArgs e) {
            codProcesso.Text =              "";
            pastaFisica.Text =              "";
            data.Date =                     DateTime.Today;
            hora.Time =                     DateTime.Now.TimeOfDay;
            isAcidente.IsToggled =          false;
            entDecisora.SelectedItem =      "Não Definido";
            entAutuante.SelectedItem =      "Não Definido";
            entResp.SelectedItem =          "Não Definido";
            codInfracao.Text =              "";
            tipoInfracao.SelectedItem =     "Não Definido";
            catInfracao.SelectedItem =      "Não Definido";

            rua.Text =                      "";
            edificoPorta.Text =             "";
            localidade.Text =               "";
            cidade.Text =                   "";

            nome.Text =                     "";
            dataNasc.Date =                 DateTime.Today;
            genero.SelectedItem =           "NãlistaDePessoas.ItemsSourceo Definido";
            estadoCiv.SelectedItem =        "Não Definido";
            nacionalidade.Text =            "";
            naturalidade.Text =             "";
            nif.Text =                      "";
            contacto1.Text =                "";
            contacto2.Text =                "";
            email.Text =                    "";

            autuante.SelectedItem =         "Não Definido";
            isPresenciado.IsToggled =       false;
            equipamentos.SelectedItem =     "Não Definido";
        }


        /// <summary>
        /// Elimina(drop) todas as tabelas da base de dados. Maneira rapida de limpar o lixo gerado ao testar coisas (ao re-iniciar a app será injetada informação hard-coded para repopular a BD)
        /// </summary>
        async void OnReset(object sender, System.EventArgs e) {
            await (BindingContext as AutoPrincipalVM).Vaccum();
        }



        /// <summary>
        /// Torna a ListView visivel caso hajam letras na searchBox e popula-a de acordo com o texto inserido.
        /// </summary>
        private async void OnTextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e) {
            if (!string.IsNullOrWhiteSpace(e.NewTextValue)) {
                var lista = (BindingContext as AutoPrincipalVM).Filtered(e.NewTextValue);
                pessoaListView.ItemsSource = lista;
                pessoaListView.IsVisible = true;
                pessoaListView.HeightRequest = (pessoaListView.RowHeight * lista.Count);
            } else {
                pessoaListView.ItemsSource = await (BindingContext as AutoPrincipalVM).GetListaPessoas();
                pessoaListView.IsVisible = false;
            }
        }


        /// <summary>
        /// Popula (ou repopula) o StackLayout do Denunciante com informação de um objeto Pessoa.
        /// </summary>
        void OnPessoaSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e) {
            var pessSelecionada = e.SelectedItem as Pessoa;

            currentDenuncianteId =      pessSelecionada.PessoaId;
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
               
            pessoaListView.IsVisible = false;
            searchBox.Text = "";

        }




        /// <summary>
        /// Destrói a página e volta para a anterior.
        /// </summary>
        async void OnCancelarClicked(object sender, System.EventArgs e) {
            IsEnabled = false;

            if (isNewAuto) {
                await (BindingContext as AutoPrincipalVM).ApagarGeral(Geral as Geral);
                await (BindingContext as AutoPrincipalVM).ApagarPessoa(Pessoa as Pessoa);
                await (BindingContext as AutoPrincipalVM).ApagarLocalizacao(Localizacao as Localizacao);
                await (BindingContext as AutoPrincipalVM).ApagarAutuante(Autuante as Autuante);
            }

            await Navigation.PopAsync();

            IsEnabled = true;
        }


        /// <summary>
        /// Realiza o load da informação, grava e prosegue para a página seguinte. Caso seja um auto novo, o load é simplesmente a informação das entries, se 
        /// </summary>
        private async void OnProximoClicked(object sender, System.EventArgs e) {
            IsEnabled = false;

            await loadAndSave();

            var page = new AutoLegislacaoPage(currentAutoId, isNewAuto);
            await Navigation.PushAsync(page);

            IsEnabled = true;
        }




        /// <summary>
        /// Cria e popula objetos novos com o input proveniente das entries/pickers etc
        /// </summary>
        private void loadObjetos() {
            Geral = new Geral {
                AutoId =                currentAutoId,
                CodProcesso =           codProcesso.Text,
                PastaFisica =           pastaFisica.Text,
                DataAuto =              data.Date,
                HoraAuto =              hora.Time,
                IsAcidenteViacao =      isAcidente.IsToggled,
                EntDecisora =           entDecisora.SelectedItem.ToString(),
                EntidadeAutuante =      entAutuante.SelectedItem.ToString(),
                EntResponsavel =        entResp.SelectedItem.ToString(),
                CodigoInfracao =        codInfracao.Text,
                TipoInfracao =          tipoInfracao.SelectedItem.ToString(),
                CategoriaInfracao =     catInfracao.SelectedItem.ToString(),
                ApreensaoId =           apreensaoFK,
                ArguidoId =             arguidoFK,
                TestemunhaId =          testemunhaFK,
                LeiId =                 leiFK,
                PagamentoId =           pagamentoFK
            };

            Localizacao = new Localizacao {
                LocalId =               currentLocalId,
                Rua =                   rua.Text,
                EdificioPorta =         edificoPorta.Text,
                Localidade =            localidade.Text,
                Cidade =                cidade.Text
            };

            Pessoa = new Pessoa {
                PessoaId =              currentDenuncianteId,
                Nome =                  nome.Text,
                DataNascimento =        dataNasc.Date,
                Genero =                genero.SelectedItem.ToString(),
                EstadoCivil =           estadoCiv.SelectedItem.ToString(),
                Nacionalidade =         nacionalidade.Text,
                Naturalidade =          naturalidade.Text,
                NIF =                   Int32.Parse(nif.Text),
                Contacto1 =             Int32.Parse(contacto1.Text),
                Contacto2 =             Int32.Parse(contacto2.Text),
                Email =                 email.Text
            };

            Autuante = new Autuante {
                AutuanteId =            currentAutuanteId,
                EntidadeAutuante =      autuante.SelectedItem.ToString(),
                IsPresenciado =         isPresenciado.IsToggled,
                Equipamento =           equipamentos.SelectedItem.ToString()
            };
        } 


        /// <summary>
        /// Chama a função loadObjetos(), grava o Auto na BD e atualiza os id de cada objeto (geralId, localizacaoId, denuncianteId e autuanteId
        /// </summary>
        private async Task loadAndSave() {
            loadObjetos();

            var geral = await (BindingContext as AutoPrincipalVM).GuardarAuto(Geral as Geral, Localizacao as Localizacao, Pessoa as Pessoa, Autuante as Autuante);
            currentAutoId =         geral.AutoId;
            currentLocalId =        geral.LocalId;
            currentDenuncianteId =  geral.DenuncianteId;
            currentAutuanteId =     geral.AutuanteId;
        }


#region REGION -> Inicialização de propriedades
        public Geral Geral {
            get { return _geral; }
            set {
                _geral = value;
                OnPropertyChanged();
            }
        }

        public Localizacao Localizacao {
            get { return _localizacao; }
            set {
                _localizacao = value;
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

        public Autuante Autuante {
            get { return _autuante; }
            set {
                _autuante = value;
                OnPropertyChanged();
            }
        }
#endregion


#region REGION -> Taps nos Separadores
        void OnGeralTapped(object sender, System.EventArgs e) {
            geralStack.IsVisible = !geralStack.IsVisible;

            if (geralArrow.Rotation == 0)
                geralArrow.RotateTo(-180, 225);
            else
                geralArrow.RotateTo(0, 225);
        }

        void OnLocalizacaoTapped(object sender, System.EventArgs e) {
            localizacaoStack.IsVisible = !localizacaoStack.IsVisible;

            if (localizacaoArrow.Rotation == 0)
                localizacaoArrow.RotateTo(-180, 225);
            else
                localizacaoArrow.RotateTo(0, 225);
        }

        void OnDenuncianteTapped(object sender, System.EventArgs e) {
            denuncianteStack.IsVisible = !denuncianteStack.IsVisible;

            if (denuncianteArrow.Rotation == 0)
                denuncianteArrow.RotateTo(-180, 225);
            else
                denuncianteArrow.RotateTo(0, 225);
        }

        void OnAutuantesTapped(object sender, System.EventArgs e) {
            autuanteStack.IsVisible = !autuanteStack.IsVisible;

            if (autuanteArrow.Rotation == 0)
                autuanteArrow.RotateTo(-180, 225);
            else
                autuanteArrow.RotateTo(0, 225);
        }
#endregion
    }
}


