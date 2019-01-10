using GeCO.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GeCO.ViewModels {
    
    public class AutoPagamentoVM : PropertyChangedVM {
        
        public List<String> TipoPagamento => pagamentos;
        public List<String> TipoCusto => custos;


        private Pagamento _pagamento;
        private CustosProcessuais _custosProcessuais;


        public AutoPagamentoVM() {
            InicializacaoPropriedades();
        }



        public async Task<Geral>GetGeral(int id) {
            return await App.Database.GetGeral(id);
        }



        /// <summary>
        /// Executa a função GetPagamento em AutoDatabase.cs e retorna o seu resultado
        /// </summary>
        public async Task<Pagamento>GetPagamento(int id) {
            return await App.Database.GetPagamento(id);
        }


        /// <summary>
        /// Apaga o Auto (Geral) da base de dados
        /// </summary>
        public async Task ApagarAuto(int id) {
            var geral = await App.Database.GetGeral(id);


        /// <summary>
        /// Grava o pagamento e associa-o ao Auto ou atualiza um já existente.
        /// </summary>
        public async Task<Pagamento>GuardarPagamento(Pagamento pagamento, int autoId) {

            if (pagamento.PagamentoId != 0) {
                await App.Database.UpdatePagamento(pagamento);
                await App.Database.UpdateGeralPagamento(pagamento.PagamentoId, autoId);
            }
            else {
                pagamento.PagamentoId = 0; // supostamente o id já é 0 (zero) mas ao menos com isto nunca terá numeros negativos. Mais vale prevenir do que remediar
                pagamento = await App.Database.SavePagamento(pagamento);
                await App.Database.UpdateGeralPagamento(pagamento.PagamentoId, autoId);
            }

            return pagamento;
        }
    


        /// <summary>
        /// Limpa a informação presente no ecrã e desassocia o pagamento do auto
        /// </summary>
        public async Task Desassociar_e_Apagar_Pagamento(int pagamentoId, int autoId) {
            var pag = await App.Database.GetPagamento(pagamentoId);

            await App.Database.ApagarPagamento(pag);
            await App.Database.UpdateDeletedPagamento(pagamentoId);
        }


            var apr = await App.Database.GetApreensao(geral.ApreensaoId);
            await App.Database.ApagarApreensao(apr);

            var pag = await App.Database.GetPagamento(geral.PagamentoId);
            await App.Database.ApagarPagamento(pag);

            await App.Database.ApagarGeral(geral);
        }




#region gettets/setters

        public Pagamento Pagamento {
            get { return _pagamento; }
            set {
                _pagamento = value;
                OnPropertyChanged();
            }
        }

        public CustosProcessuais CustosProcessuais {
            get { return _custosProcessuais; }
            set {
                _custosProcessuais = value;
                OnPropertyChanged();
            }
        }
#endregion


#region Properties
        public void InicializacaoPropriedades() {
            Pagamento = new Pagamento {
                NIF =           0,
                DUC =           0,
                DataLimite =    DateTime.Today,
                HoraPagamento = DateTime.Now.TimeOfDay,
                TipoPagamento = "",
                Valor =         0,
                DataInicial =   DateTime.Today,
                DataFinal =     DateTime.Today,
                DataDevolucao = DateTime.Today
            };

            CustosProcessuais = new CustosProcessuais {
                TipoCusto =     "",
                Valor =         0
            };
        }
#endregion


#region Lists (pagamentos, custos)
        List<String> pagamentos = new List<string> {
                "Não Definido",
                "Monetário",
                "Transferência Bancária",
                "Referência Multibanco",
                "Prestações Mensais"
            };
        List<String> custos = new List<string> {
                "Não Definido",
                "Tipo 1",
                "Tipo 2",
                "Tipo 3"
            };
#endregion
    }
}
