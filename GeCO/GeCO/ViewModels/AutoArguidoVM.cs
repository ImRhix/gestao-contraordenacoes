using System;
using GeCO.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Diagnostics;

namespace GeCO.ViewModels {
    
    public class AutoArguidoVM : PropertyChangedVM {

        ObservableCollection<Pessoa> pessoasOC = new ObservableCollection<Pessoa>();
        ObservableCollection<Pessoa> newPessoasOC = new ObservableCollection<Pessoa>();

        public List<String> Genero => genero;
        public List<String> EstadoCivil => estadoCivil;
        public List<String> Qualidade => qualidade;

        private Pessoa _pessoa;
        private QualidadeArguido _qualidadeArguido;


        public AutoArguidoVM() {
            InicializacaoPropriedades();
        }



#region REGION -> Gets
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
        public async Task<ObservableCollection<Pessoa>> GetListaPessoas() {
            pessoasOC.Clear();
            var lista = await App.Database.GetPessoaList();
            foreach (var item in lista) {
                pessoasOC.Add(new Pessoa { PessoaId = item.PessoaId, Nome = item.Nome, DataNascimento = item.DataNascimento, Genero = item.Genero, EstadoCivil = item.EstadoCivil, Nacionalidade = item.Nacionalidade, Naturalidade = item.Naturalidade, NIF = item.NIF, Contacto1 = item.Contacto1, Contacto2 = item.Contacto2, Email = item.Email });
            }
            return pessoasOC;
        }


        /// <summary>
        ///  Filtra os items da ObservableCollection pessoasOC de acordo com o texto inserido na SearchBox e cria outra ObservableCollection com a nova informação filtrada.
        ///  Fazendo incialmente o load da lista de pessoas e usando uma outra collection para guardar os resultados filtrados
        /// evita-se a necessidade de estar sempre a aceder ao servidor/BD
        /// </summary>
        public ObservableCollection<Pessoa> Filtered(string text) {
            newPessoasOC.Clear();

            var novaLista = pessoasOC.Where(i => i.Nome.ToLowerInvariant().StartsWith(text.ToLowerInvariant()));
            foreach (var item in novaLista)
                newPessoasOC.Add(new Pessoa { PessoaId = item.PessoaId, Nome = item.Nome, DataNascimento = item.DataNascimento, Genero = item.Genero, EstadoCivil = item.EstadoCivil, Nacionalidade = item.Nacionalidade, Naturalidade = item.Naturalidade, NIF = item.NIF, Contacto1 = item.Contacto1, Contacto2 = item.Contacto2, Email = item.Email });

            return newPessoasOC;
        }
#endregion


#region REGION -> Saves
        /// <summary>
        /// Guarda um novo objeto Pessoa, ou se o NIF já existir na BD atualiza esse objeto com a nova informação.
        /// </summary>
        public async Task<Pessoa> GuardarPessoa(Pessoa pessoa, int autoId) {
            if (pessoa.PessoaId != 0) {
                bool pessoaExists = await App.Database.CheckPessoa(pessoa.NIF);
                if (pessoaExists) {
                    await App.Database.UpdatePessoa(pessoa);
                    await App.Database.UpdateArguido(pessoa.PessoaId, autoId);
                }
                else {
                    pessoa.PessoaId = 0;
                    pessoa = await App.Database.SavePessoa(pessoa);
                    await App.Database.UpdateArguido(pessoa.PessoaId, autoId);
                }
            }
            else if (!pessoa.Nome.Equals("") && pessoa.NIF != 0) {
                pessoa = await App.Database.SavePessoa(pessoa);
                await App.Database.UpdateArguido(pessoa.PessoaId, autoId);
            } else {
                await App.Database.UpdateArguido(0, autoId);
                pessoa.PessoaId = 0;
            }
            return pessoa;
        }
#endregion


#region REGION -> Deletes
        /// <summary>
        /// Remove (atualiza) o valor da FK ArguidoId na tabela Geral para 0
        /// </summary>
        public async Task DesassociarPessoa(int pessoaId, int autoId) {
            await App.Database.UpdateArguido(pessoaId, autoId);
        }
#endregion


#region REGION -> gettets/setters
        public Pessoa Pessoa {
            get { return _pessoa; }
            set {
                _pessoa = value;
                OnPropertyChanged();
            }
        }

        public QualidadeArguido QualidadeArguido {
            get { return _qualidadeArguido; }
            set {
                _qualidadeArguido = value;
                OnPropertyChanged();
            }
        }
#endregion

        public void InicializacaoPropriedades() {
            Pessoa = new Pessoa {
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

            QualidadeArguido = new QualidadeArguido {
                QualidadeTipo =     ""
            };

        }


#region REGION -> Lists
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
        List<String> qualidade = new List<string> {
            "Não Definido",
            "Sem Atecedentes",
            "Reincidente"
        };
#endregion

    }
}