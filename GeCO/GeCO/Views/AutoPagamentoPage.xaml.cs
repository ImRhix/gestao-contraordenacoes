using System;
using System.Collections.Generic;
using Xamarin.Forms;
using GeCO.ViewModels;
using System.Diagnostics;
using GeCO.Models;

namespace GeCO.Views {
    
    public partial class AutoPagamentoPage : ContentPage {
        
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
            }

            base.OnAppearing();
        }


        private void OnApagar(object sender, EventArgs e) {
            nif.Text =               0.ToString();
            duc.Text =               0.ToString();
            dataLimite.Date =        DateTime.Today;
            horaPagamento.Time =     DateTime.Now.TimeOfDay;
            tipo.SelectedItem =      "Não Definido";
            dataInicial.Date =       DateTime.Today;
            valor.Text =             0.ToString();
            dataFinal.Date =         DateTime.Today;
            dataDevolucao.Date =     DateTime.Today;

            tipoCusto.SelectedItem = "Não Definido";
            valorCusto.Text =        0.ToString();
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

            var page = new AutoTestemunhaPage(currentAutoId);
            await Navigation.PushAsync(page);

            IsEnabled = true;
        }

        
        #region Taps Separadores
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
        #endregion
    }
}
