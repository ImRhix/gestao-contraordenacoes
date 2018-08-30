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


        public async Task<Geral>GetGeral(int id) {
            return await App.Database.GetGeral(id);
        }

        public async Task<Pagamento>GetPagamento(int id) {
            return await App.Database.GetPagamento(id);
        }






#region REGION -> gettets/setters

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


#region REGION -> Lists (entidades, placeholders, genero, estado civil)

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
