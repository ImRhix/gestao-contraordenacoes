using GeCO.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeCO.ViewModels {
    public class PessoasVM : PropertyChangedVM {

        ObservableCollection<Pessoa> pessoasOC = new ObservableCollection<Pessoa>();
        ObservableCollection<Pessoa> newPessoasOC = new ObservableCollection<Pessoa>();

        public List<String> Genero => genero;
        public List<String> EstadoCivil => estadoCivil;

        private Pessoa _pessoa;


        public PessoasVM() {
            InicializacaoPropriedades();
        }




        #region Gets
        /// <summary>
        /// Retorna o presente auto (Geral)
        /// </summary>
        public async Task<Geral> GetGeral(int id) {
            return await App.Database.GetGeral(id);
        }


        /// <summary>
        /// Retorna um objeto Pessoa
        /// </summary>
        public async Task<Pessoa> GetPessoa(int id) {
            return await App.Database.GetPessoa(id);
        }


        /// <summary>
        /// Guardars the pessoa.
        /// </summary>
        public async Task<ObservableCollection<Pessoa>> GetPessoas() {
            pessoasOC.Clear();
            var lista = await App.Database.GetPessoaListOrdered();
            foreach (var item in lista)
                pessoasOC.Add(new Pessoa { PessoaId = item.PessoaId, Nome = item.Nome, DataNascimento = item.DataNascimento, Genero = item.Genero, EstadoCivil = item.EstadoCivil, Nacionalidade = item.Nacionalidade, Naturalidade = item.Naturalidade, NIF = item.NIF, Contacto1 = item.Contacto1, Contacto2 = item.Contacto2, Email = item.Email });

            return pessoasOC;
        }


        /// <summary>
        ///  Filtra os items da ObservableCollection pessoasOC de acordo com o texto inserido na SearchBox e cria outra ObservableCollection com a nova informação filtrada.
        ///  Fazendo incialmente o load da lista de pessoas e usando uma outra collection para guardar os resultados filtrados
        ///  evita-se a necessidade de estar sempre a aceder ao servidor/BD
        /// </summary>
        public ObservableCollection<Pessoa> Filtered(string text) {
            newPessoasOC.Clear();

            var novaLista = pessoasOC.Where(i => i.Nome.ToLowerInvariant().StartsWith(text.ToLowerInvariant()));
            foreach (var item in novaLista)
                newPessoasOC.Add(new Pessoa { PessoaId = item.PessoaId, Nome = item.Nome, DataNascimento = item.DataNascimento, Genero = item.Genero, EstadoCivil = item.EstadoCivil, Nacionalidade = item.Nacionalidade, Naturalidade = item.Naturalidade, NIF = item.NIF, Contacto1 = item.Contacto1, Contacto2 = item.Contacto2, Email = item.Email });

            return newPessoasOC;
        }
        #endregion


        #region Saves
        /// <summary>
        /// Guarda um novo objeto Pessoa, ou se o NIF já existir na BD atualiza esse objeto com a nova informação.
        /// </summary>
        public async Task<Pessoa> GuardarPessoa(Pessoa pessoa) {
            if (pessoa.PessoaId != 0) {
                bool pessoaExists = await App.Database.CheckPessoa(pessoa.NIF);
                if (pessoaExists) {
                    await App.Database.UpdatePessoa(pessoa);
                }
                else {
                    pessoa.PessoaId = 0;
                    pessoa = await App.Database.SavePessoa(pessoa);
                }
            }
            else if (!pessoa.Nome.Equals("") && pessoa.NIF != 0) {
                pessoa = await App.Database.SavePessoa(pessoa);
            }
            else {
                pessoa.PessoaId = 0;
            }
            return pessoa;
        }
        #endregion


        #region Deletes
        /// <summary>
        /// Apaga a Pessoa da BD e atualiza os Autos que a estavam a utilizar
        /// </summary>
        public async Task ApagarPessoa(Pessoa pessoa) {
            var id = pessoa.PessoaId;
            await App.Database.ApagarPessoa(pessoa);
            await App.Database.UpdateDeletedPessoa(id);
        }
        #endregion


        #region gettets/setters
        public Pessoa Pessoa {
            get { return _pessoa; }
            set {
                _pessoa = value;
                OnPropertyChanged();
            }
        }
        #endregion


        #region Properties
        public void InicializacaoPropriedades() {
            Pessoa =                new Pessoa {
                Nome =              "",
                DataNascimento =    DateTime.Today,
                NIF =               0,
                Genero =            "",
                EstadoCivil =       "",
                Nacionalidade =     "",
                Naturalidade =      "",
                Contacto1 =         0,
                Contacto2 =         0,
                Email =             ""
            };
        }
        #endregion


        #region Lists
        List<String> genero = new List<string> {
            "Não Definido",
            "Feminino",
            "Masculino",
            "Inconclusivo"
        };
        List<String> estadoCivil = new List<string> {
            "Não Definido",
            "Solteiro",
            "Casado",
            "União de facto",
            "Divorciado",
            "Viúvo"
        };
        #endregion
    }
}