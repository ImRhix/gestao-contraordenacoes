using System;
using GeCO.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace GeCO.ViewModels {
    
    public class MeusAutosVM : PropertyChangedVM {
        
        ObservableCollection<MeuAuto> meuAutoOC = new ObservableCollection<MeuAuto>();
        ObservableCollection<MeuAuto> newMeuAutoOC = new ObservableCollection<MeuAuto>();

        private Geral _geral;
        private Localizacao _localizacao;
        private Pessoa _pessoa;

        public MeusAutosVM() {
        }

        /// <summary>
        /// Retorna uma ObservableCollection com a seguinte informação: AutoId, CodProcesso, DataAuto, LocalId, Nome (do denunciante) e PessoaId.
        /// </summary>
        public async Task<ObservableCollection<MeuAuto>> GetAutos(){
            meuAutoOC.Clear();
            var lista = await GetGeralList();
            foreach (var item in lista) {
                var pessoa = await GetPessoa(item.DenuncianteId);
                meuAutoOC.Add(new MeuAuto { AutoId = item.AutoId, CodProcesso = item.CodProcesso, DataAuto = item.DataAuto, Nome = pessoa.Nome, PessoaId = pessoa.PessoaId, LocalId = item.LocalId });
            }
            return meuAutoOC;
        }



        public ObservableCollection<MeuAuto> Filtered(string text){
            newMeuAutoOC.Clear();
            var lista = meuAutoOC.Where(i => i.Nome.ToLowerInvariant().StartsWith(text.ToLowerInvariant()));
            foreach (var item in lista)
                newMeuAutoOC.Add(new MeuAuto { AutoId = item.AutoId, CodProcesso = item.CodProcesso, DataAuto = item.DataAuto, Nome = item.Nome, PessoaId = item.PessoaId, LocalId = item.LocalId });

            return newMeuAutoOC;
        }


        /// <summary>
        /// Retorna uma List de objetos Geral (todas as rows da tabela Geral)
        /// </summary>
        public async Task<List<Geral>> GetGeralList() {
            return await App.Database.GetGeralList();
        }

        /// <summary>
        /// Retorna um objeto Pessoa (uma row da tabela Pessoa, baseado no id)
        /// </summary>
        public async Task<Pessoa> GetPessoa(int id){
            return await App.Database.GetPessoa(id);
        }
        
        




        #region Region: gettets/setters (Geral, Localizacao, Pessoa)
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
        #endregion
    }
}
