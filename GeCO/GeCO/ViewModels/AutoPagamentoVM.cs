using System;
using System.Collections.Generic;
using GeCO.Models;
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


        #region Gets
        public async Task<Geral>GetGeral(int id) {
            return await App.Database.GetGeral(id);
        }

        public async Task<Pagamento>GetPagamento(int id) {
            return await App.Database.GetPagamento(id);
        }
        #endregion


        #region Delete
        /// <summary>
        /// Apaga o Auto (Geral) da base de dados
        /// </summary>
        public async Task ApagarAuto(int id) {
            var geral = await App.Database.GetGeral(id);

            var aut = await App.Database.GetAutuante(geral.AutuanteId);
            await App.Database.ApagarAutuante(aut);

            var apr = await App.Database.GetApreensao(geral.ApreensaoId);
            await App.Database.ApagarApreensao(apr);

            var pag = await App.Database.GetPagamento(geral.PagamentoId);
            await App.Database.ApagarPagamento(pag);

            await App.Database.ApagarGeral(geral);
        }
        #endregion



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
