using GeCO.Models;
using GeCO.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GeCO.Views {
    
    public partial class AutoPagamentoPage : ContentPage {

        private Pagamento _pagamento;
        private int currentAutoId, currentPagamentoId;

        public AutoPagamentoPage(int id) {
            InitializeComponent();

            currentAutoId = id;
            BindingContext = new AutoPagamentoVM();
        }


        protected override async void OnAppearing() {
            var geral = await (BindingContext as AutoPagamentoVM).GetGeral(currentAutoId);
            currentPagamentoId = geral.PagamentoId;


            if (currentPagamentoId != 0) {
                var pag = await (BindingContext as AutoPagamentoVM).GetPagamento(currentPagamentoId);

                nif.Text =              pag.NIF.ToString();
                duc.Text =              pag.DUC.ToString();
                dataLimite.Date =       pag.DataLimite;
                horaPagamento.Time =    pag.HoraPagamento;
                tipo.SelectedItem =     pag.TipoPagamento;
                valor.Text =            pag.Valor.ToString();
                dataInicial.Date =      pag.DataInicial;
                dataFinal.Date =        pag.DataFinal;
                dataDevolucao.Date =    pag.DataDevolucao;
            } else {
                nif.Text =              0.ToString();
                duc.Text =              0.ToString();
            }

            base.OnAppearing();
        }

        /// <summary>
        /// Limpa o input das entries e desassocia o id do pagamento do presente Auto.
        /// </summary>
        private async void OnApagar(object sender, EventArgs e) {
            clearInput();
            await (BindingContext as AutoPagamentoVM).Desassociar_e_Apagar_Pagamento(currentPagamentoId, currentAutoId);
            Console.WriteLine("\n\ncurrentPagamentoID " + currentPagamentoId + "\n\n");
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

            var page = new AutoTestemunhaPage(currentAutoId);
            await Navigation.PushAsync(page);

            IsEnabled = true;
        }




        private async Task loadAndSave() {
            loadObjetos();

            var pag = await (BindingContext as AutoPagamentoVM).GuardarPagamento(Pagamento as Pagamento, currentAutoId);
            currentPagamentoId = pag.PagamentoId;
        }



        private void loadObjetos(){
            Pagamento = new Pagamento
            {
                NIF = int.Parse(nif.Text),
                DUC = int.Parse(duc.Text),
                DataLimite = dataLimite.Date,
                HoraPagamento = horaPagamento.Time,
                TipoPagamento = tipo.SelectedItem.ToString(),
                Valor = decimal.Parse(valor.Text),
                DataInicial = dataInicial.Date,
                DataFinal = dataFinal.Date,
                DataDevolucao = dataDevolucao.Date
            };
        }



        private void clearInput() {
            nif.Text =                  0.ToString();
            duc.Text =                  0.ToString();
            dataLimite.Date =           DateTime.Today;
            horaPagamento.Time =        DateTime.Now.TimeOfDay;
            tipo.SelectedItem =         "Não Definido";
            dataInicial.Date =          DateTime.Today;
            valor.Text =                0.ToString();
            dataFinal.Date =            DateTime.Today;
            dataDevolucao.Date =        DateTime.Today;

            tipoCusto.SelectedItem =    "Não Definido";
            valorCusto.Text =           0.ToString();
        }




        public Pagamento Pagamento {
            get { return _pagamento; }
            set { _pagamento = value;
                OnPropertyChanged();
            }
        }


        void OnPagamentoTapped(object sender, System.EventArgs e) {
            pagamentoStack.IsVisible = !pagamentoStack.IsVisible;

            if (pagamentoArrow.Rotation == 0)
                pagamentoArrow.RotateTo(-180, 225);
            else
                pagamentoArrow.RotateTo(0, 225);
        }

        void OnCustosTapped(object sender, System.EventArgs e) {
            custosStack.IsVisible = !custosStack.IsVisible;

            if (custosArrow.Rotation == 0)
                custosArrow.RotateTo(-180, 225);
            else
                custosArrow.RotateTo(0, 225);
        }

    }
}
