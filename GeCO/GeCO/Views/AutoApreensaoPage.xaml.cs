using System;
using System.Collections.Generic;
using System.Diagnostics;
using GeCO.Models;
using GeCO.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GeCO.Views {
    
    public partial class AutoApreensaoPage : ContentPage {
        private Apreensao _apreensao;
        int currentAutoId, currentApreensaoId, currentLeiId;
        bool isNewAuto;

        public AutoApreensaoPage(int id, bool state) {
            InitializeComponent();

            BindingContext = new AutoApreensaoVM();
            currentAutoId = id;
            isNewAuto = state;
        }


        protected override async void OnAppearing() {
            var geral = await (BindingContext as AutoApreensaoVM).GetGeral(currentAutoId);

            if (geral.ApreensaoId != 0) {
                Debug.WriteLine(geral.ApreensaoId);
                var apr = await (BindingContext as AutoApreensaoVM).GetApreensao(geral.ApreensaoId);

                objeto.SelectedItem =       apr.Objeto;
                data.Date =                 apr.DataApreensao;
                hora.Time =                 apr.HoraApreensao;
                lei.SelectedIndex =         0;
                coima.Text =                0.ToString();
                tipo.SelectedItem =         apr.TipoApreensao;
                editor.Text =               apr.Motivo;

                Debug.WriteLine(apr.LeiApreensao);
                if (apr.LeiApreensao != 0) {
                    var leg = await (BindingContext as AutoApreensaoVM).GetLei(apr.LeiApreensao);
                    currentLeiId = leg.LeiId;
                    lei.SelectedIndex = currentLeiId;
                    coima.Text = 0.ToString();
                }
            }

            base.OnAppearing();
        }



        public async void OnSelectedndexChanged(object sender, System.EventArgs e) {
            var leg = await (BindingContext as AutoApreensaoVM).GetLei(lei.SelectedIndex);
            coima.Text = ((leg.Min + leg.Max) / 2).ToString();
        }


        /// <summary>
        /// Insere ou atualiza a tabela Geral com o id da apreensao.
        /// </summary>
        public async void GuardarClicked(object sender, System.EventArgs e) {
            await loadAndSave();
        }


        private async void OnApagar(object sender, EventArgs e) {
            clearInput();

            await (BindingContext as AutoApreensaoVM).ApagarApreensao(Apreensao as Apreensao, currentAutoId);
        }



        /// <summary>
        /// Regressa para a página anterior
        /// </summary>
        async void OnAnteriorClicked(object sender, System.EventArgs e) {
            IsEnabled = false;

            await Navigation.PopAsync();

            IsEnabled = true;
        }



        /// <summary>
        /// Grava e segue em frente
        /// </summary>
        async void OnProximoClicked(object sender, System.EventArgs e) {
            IsEnabled = false;

            await loadAndSave();

            var page = new AutoPagamentoPage(currentAutoId, isNewAuto);
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
                    await (BindingContext as AutoApreensaoVM).ApagarAuto(currentAutoId);
            }
            await Navigation.PopToRootAsync();
            IsEnabled = true;
        }


        /// <summary>
        /// Cria e popula objetos novos com o input proveniente das entries/pickers etc
        /// </summary>
        private void loadObjetos() {
            Apreensao = new Apreensao {
                Objeto =        objeto.SelectedItem.ToString(),
                DataApreensao = data.Date,
                HoraApreensao = hora.Time,
                LeiApreensao =  lei.SelectedIndex,
                TipoApreensao = tipo.SelectedItem.ToString(),
                Coima =         decimal.Parse(coima.Text),
                Motivo =        editor.Text
            };
        }



        /// <summary>
        /// Chama a funçao loadObjetos() e grava/atualiza a pessoa na BD
        /// </summary>
        private async Task loadAndSave() {
            loadObjetos();

            var aprId = await (BindingContext as AutoApreensaoVM).GuardarApreensao(Apreensao as Apreensao, currentAutoId);
            currentApreensaoId = aprId;
        }



        /// <summary>
        /// Limpa o texto das entries e seleciona os valores default dos dropdowns.
        /// </summary>
        private void clearInput() {
            currentApreensaoId =    0;

            objeto.SelectedItem =   "Não Definido";
            data.Date =             DateTime.Today;
            hora.Time =             DateTime.Now.TimeOfDay;
            lei.SelectedItem =      "Não Definido";
            tipo.SelectedItem =     "Não Definido";
            coima.Text =            "";
            editor.Text =           "";
        }



#region getters/setters
        public Apreensao Apreensao {
            get { return _apreensao; }
            set {
                _apreensao = value;
                OnPropertyChanged();
            }
        }
#endregion

  
#region Taps Separadores
        void OnApreensaoTapped(object sender, System.EventArgs e) {
            apreensaoStack.IsVisible = !apreensaoStack.IsVisible;
            
            if (apreensaoArrow.Rotation == 0)
                apreensaoArrow.RotateTo(-180, 225);
            else
                apreensaoArrow.RotateTo(0, 225);
        }
#endregion
    }
}
