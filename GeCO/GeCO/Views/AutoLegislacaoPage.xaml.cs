using GeCO.Models;
using System;
using Xamarin.Forms;
using GeCO.ViewModels;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GeCO.Views {
    public partial class AutoLegislacaoPage : ContentPage {

        private int currentAutoId, currentLeiId;
        private bool isNewAuto;

        private Lei _lei;

        public AutoLegislacaoPage(int id, bool state) {
            InitializeComponent();

            currentAutoId = id;
            isNewAuto = state;
            BindingContext = new AutoLegislacaoVM();
        }



        protected override async void OnAppearing() {
            var geral = await (BindingContext as AutoLegislacaoVM).GetGeral(currentAutoId);
            currentLeiId = geral.LeiId;

            if (currentLeiId != 0) { 
                var leg = await (BindingContext as AutoLegislacaoVM).GetLei(currentLeiId);
                
                titulo.SelectedItem =   leg.Titulo;
                pontos.Text =           leg.Pontos.ToString();
                descricao.Text=         leg.Descricao;
                min.Text =              leg.Min.ToString();
                max.Text =              leg.Max.ToString();
                prazo.Text =            leg.Prazo.ToString();
            }

            leiArrow.Rotation = 180;
            
            base.OnAppearing();
        }


        /// <summary>
        /// Atualiza as informações sobre a lei de acordo com a lei selecionada no dropdown.
        /// </summary>
        async void OnIndexChanged(object sender, System.EventArgs e) {
            var leg = await (BindingContext as AutoLegislacaoVM).GetLei(titulo.SelectedIndex);

            if (leg.LeiId != 0) {
                titulo.SelectedItem =   leg.Titulo;
                pontos.Text =           leg.Pontos.ToString();
                descricao.Text =        leg.Descricao;
                min.Text =              leg.Min.ToString();
                max.Text =              leg.Max.ToString();
                prazo.Text =            leg.Prazo.ToString();

                currentLeiId =          leg.LeiId;
            } else {
                clearInput();
            }
        }



        /// <summary>
        /// Atualiza a tabela Geral com o id da lei selecionada.
        /// </summary>
        public async Task GuardarClicked(object sender, System.EventArgs e) {
            await loadAndSave();
        }


        /// <summary>
        /// Limpa o texto das labels e elimina a FK LeiId da tabela Geral
        /// </summary>
        private async void OnApagar(object sender, EventArgs e) {
            clearInput();

            await (BindingContext as AutoLegislacaoVM).ApagarLei(currentLeiId, currentAutoId);
        }



        /// <summary>
        /// Destrói (fecha) a página atual
        /// </summary>
        async void OnAnteriorClicked(object sender, System.EventArgs e) {
            IsEnabled = false;

            await Navigation.PopAsync();

            IsEnabled = true;
        }

        /// <summary>
        /// Atualiza a FK LeiId na tabela Geral com o id correspondente à lei selecionada e abre a proxima página.
        /// </summary>
        async void OnProximoClicked(object sender, System.EventArgs e) {
            IsEnabled = false;

            await loadAndSave();

            var page = new AutoArguidoPage(currentAutoId);
            await Navigation.PushAsync(page);

            IsEnabled = true;
        }




        /// <summary>
        /// Carrega os objetos com o input correspondente das entries/pickers
        /// </summary>
        private void loadObjetos() {
            Lei = new Lei {
                LeiId =         currentLeiId,
                Titulo =        titulo.SelectedItem.ToString(),
                Pontos =        Int32.Parse(pontos.Text),
                Descricao =     descricao.Text,
                Min =           decimal.Parse(min.Text),
                Max =           decimal.Parse(max.Text),
                Prazo =         Int32.Parse(prazo.Text)
            };
        }

        /// <summary>
        /// Chama a função loadObjetos() e atualiza a FK LeiId na tabela Geral com o Id da lei selecionada.
        /// </summary>
        private async Task loadAndSave() {
            loadObjetos();

            var leg = await (BindingContext as AutoLegislacaoVM).GuardarLei(Lei as Lei, currentAutoId);
            currentLeiId = leg.LeiId;
        }

        /// <summary>
        /// Limpa o texto das entries e seleciona os valores default dos dropdowns.
        /// </summary>
        private void clearInput() {
            titulo.SelectedIndex =  0;
            pontos.Text =           "0";
            descricao.Text =        "";
            min.Text =              "0";
            max.Text =              "0";
            prazo.Text =            "0";

            currentLeiId =          0;
        }



        public Lei Lei {
            get { return _lei; }
            set {
                _lei = value;
                OnPropertyChanged();
            }
        }

        void OnLeiTapped(object sender, System.EventArgs e) {
            leiStack.IsVisible = !leiStack.IsVisible;

            if (leiArrow.Rotation == 0)
                leiArrow.RotateTo(-180, 225);
            else
                leiArrow.RotateTo(0, 225);
        }
    }
}
