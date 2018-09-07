using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using GeCO.Models;
namespace GeCO.ViewModels
{
    public class AutoApreensaoVM : PropertyChangedVM
    {
        public List<String> Objeto => objetos;
        public List<String> Lei => leis;
        public List<String> Tipo => tipos;

        private Apreensao _apreensao;


        public AutoApreensaoVM() {
            InicializacaoPropriedades();
        }



        #region Gets
        public async Task<Geral> GetGeral(int id) {
            return await App.Database.GetGeral(id);
        }

        public async Task<Apreensao> GetApreensao(int id) {
            return await App.Database.GetApreensao(id);
        }

        public async Task<Lei> GetLei(int id)
        {
            if (id != 0)
                return await App.Database.GetLei(id);

            return null;
        }
        #endregion


        #region Saves
        /// <summary>
        /// Insere (ou atualiza) a tabela Apreensao com o input das entries. Retornar o id da apreensao
        /// </summary>
        public async Task<Int32> GuardarApreensao(Apreensao apreensao, int autoId) {
            apreensao = await App.Database.SaveApreensao(apreensao, autoId);
            return apreensao.ApreensaoId;
        }
        #endregion


        #region Deletes
        /// <summary>
        /// Apaga a atual apreensao (row) da tabela Apreensao.
        /// </summary>
        public async Task ApagarApreensao(Apreensao apreensao, int autoId) {
            try {
                await App.Database.ApagarApreensao(apreensao);
                await App.Database.UpdateApreensao(0, autoId);
            }
            catch { Debug.WriteLine("Erro ao apagar apreensao. AutoApreensaoVM.cs, ApagarApreensao()."); }
        }

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




        #region getters/setters
        public Apreensao Apreensao {
            get { return _apreensao; }
            set {
                _apreensao = value;
                OnPropertyChanged();
            }
        }
        #endregion


        #region Properties
        private void InicializacaoPropriedades() {
            Apreensao = new Apreensao {
                Objeto =        "Não Definido",
                DataApreensao = DateTime.Today,
                HoraApreensao = DateTime.Now.TimeOfDay,
                LeiApreensao =  0,
                TipoApreensao = "Não Definido",
                Coima =         0,
                Motivo =        ""
            };
        }
        #endregion


        #region Lists (objetos, leis, tipos)
        List<String> objetos = new List<string> {
            "Não Definido",
            "Objeto 1",
            "Objeto 2",
            "Objeto 3",
            "Objeto 4",
            "Objeto 5"
        };

        List<String> leis = new List<string> {
            "Não Definido",
            "Legislação Geral, Artigo 1º",
            "Legislação Geral, Artigo 2º",
            "Legislação Geral, Artigo 3º",
            "Legislação Geral, Artigo 4º",
            "Legislação Geral, Artigo 5º",
            "Legislação Geral, Artigo 6º",
            "Legislação Geral, Artigo 7º",
            "Legislação Geral, Artigo 8º",
            "Legislação Geral, Artigo 9º",
            "Legislação Geral, Artigo 10º"
        };

        List<String> tipos = new List<string> {
            "Não Definido",
            "Definitiva",
            "Temporária"
        };
        #endregion
    }
}
