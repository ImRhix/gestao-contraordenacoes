using System;
using GeCO.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GeCO.ViewModels {
    
    public class AutoPrincipalVM : PropertyChangedVM {
        
        ObservableCollection<Pessoa> pessoasOC = new ObservableCollection<Pessoa>();
        ObservableCollection<Pessoa> newPessoaOC = new ObservableCollection<Pessoa>();

        public List<String> Entidades => entidades;
        public List<String> Placeholders => placeholders;
        public List<String> Genero => genero;
        public List<String> EstadoCivil => estadoCivil;
        public List<String> Equipamentos => equipamentos;

        private Geral _geral;
        private Localizacao _localizacao;
        private Pessoa _pessoa;
        private Autuante _autuante;



        public AutoPrincipalVM() {
            InicializacaoPropriedades();
        }



#region REGION -> Get Ids
        public async Task<Int32> GetAutoId(){
            return await App.Database.GetNextGeralId();
        }

        public async Task<Int32> GetLocalId(){
            return await App.Database.GetNextLocalId();
        }

        public async Task<Int32> GetPessoaId(){
            return await App.Database.GetNextPessoaId();
        }

        public async Task<Int32> GetAutuanteId(){
            return await App.Database.GetNextLeiId();
        }

        public async Task<Int32> GetLeiId(){
            return await App.Database.GetNextLeiId();
        }
#endregion


#region REGION -> Get (objects)
        public async Task<Geral> GetGeral(int id) {
            return await App.Database.GetGeral(id);
        }

        public async Task<Localizacao> GetLocalizacao(int id) {
            return await App.Database.GetLocalizacao(id);
        }

        public async Task<Pessoa> GetPessoa(int id) {
            return await App.Database.GetPessoa(id);
        }

        public async Task<Autuante> GetAutuante(int id) {
            return await App.Database.GetAutuante(id);
        }

        public async Task<Lei> GetLei(int id) {
            return await App.Database.GetLei(id);
        }
#endregion


        /// <summary>
        /// Retorna uma ObservableCollection com a seguinte informação: AutoId, CodProcesso, DataAuto, LocalId, Nome (do denunciante) e PessoaId.
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
            newPessoaOC.Clear();

            var novaLista = pessoasOC.Where(i => i.Nome.ToLowerInvariant().StartsWith(text.ToLowerInvariant()));
            foreach (var item in novaLista)
                newPessoaOC.Add(new Pessoa { PessoaId = item.PessoaId, Nome = item.Nome, DataNascimento = item.DataNascimento, Genero = item.Genero, EstadoCivil = item.EstadoCivil, Nacionalidade = item.Nacionalidade, Naturalidade = item.Naturalidade, NIF = item.NIF, Contacto1 = item.Contacto1, Contacto2 = item.Contacto2, Email = item.Email });

            return newPessoaOC;
        }



        /// <summary>
        /// Insere (ou atualiza) as tabelas com o input das entries. Retorna o id do Auto em questão.
        /// </summary>
        public async Task<Geral> GuardarAuto(Geral geral, Localizacao localizacao, Pessoa pessoa, Autuante autuante) {
            geral = await App.Database.SaveGeral(geral);

            localizacao = await App.Database.SaveLocalizacao(localizacao);
            await App.Database.UpdateLocalizacao(localizacao.LocalId, geral.AutoId);

            // Sendo pessoaid == 0 é criada uma nova pessoa e adicionado o id à tabela Geral.
            // Sendo pessoaid != 0, verifica-se se foi introduzido um NIF novo. Se o nif existir faz-se um update;
            // caso seja um NIF novo é adicionada uma nova pessoa e adicionado o Geral com o novo ID.
            if (pessoa.PessoaId != 0) {
                bool pessoaExists = await App.Database.CheckPessoa(pessoa.NIF);
                if (pessoaExists) {
                    await App.Database.UpdatePessoa(pessoa);
                    await App.Database.UpdateDenunciante(pessoa.PessoaId, geral.AutoId);
                }
                else {
                    pessoa.PessoaId = 0;
                    pessoa = await App.Database.SavePessoa(pessoa);
                    await App.Database.UpdateDenunciante(pessoa.PessoaId, geral.AutoId);
                }
            }
            else {
                pessoa = await App.Database.SavePessoa(pessoa);
                await App.Database.UpdateDenunciante(pessoa.PessoaId, geral.AutoId);
            }

            autuante = await App.Database.SaveAutuantePrincipal(autuante, geral.AutoId);

            geral = await App.Database.GetGeral(geral.AutoId);
            return geral;
        }




#region REGION -> Deletes
        /// <summary>
        /// Apaga todas as tabelas e a informação associada.
        /// </summary>
        public async Task Vaccum(){
            await App.Database.Vacuum();
        }

        /// <summary>
        /// Apaga a atual apreensao (row) da tabela Apreensao.
        /// </summary>
        public async Task ApagarGeral(Geral geral) {
            await App.Database.ApagarGeral(geral);
        }
        public async Task ApagarPessoa(Pessoa pessoa) {
            await App.Database.ApagarPessoa(pessoa);
        }
        public async Task ApagarLocalizacao(Localizacao localizacao) {
            await App.Database.ApagarLocalizacao(localizacao);
        }
        public async Task ApagarAutuante(Autuante autuante) {
            await App.Database.ApagarAutuante(autuante);
        }
#endregion

        // **********************************************************

 #region REGION -> gettets/setters
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

        public Autuante Autuante {
            get { return _autuante; }
            set { 
                _autuante = value;
                OnPropertyChanged();
            }
        }
        #endregion

        private void InicializacaoPropriedades() {
            Geral = new Geral {
                CodProcesso = "",
                PastaFisica = "",
                DataAuto = DateTime.Today,
                HoraAuto = DateTime.Now.TimeOfDay,
                IsAcidenteViacao = false,
                EntDecisora = "",
                EntidadeAutuante = "",
                EntResponsavel = "",
                CodigoInfracao = "",
                TipoInfracao = "",
                CategoriaInfracao = ""
            };

            Localizacao = new Localizacao {
                Rua = "",
                EdificioPorta = "",
                Cidade = "",
                Localidade = ""
            };

            Pessoa = new Pessoa {
                Nome = "",
                DataNascimento = DateTime.Today,
                NIF = 0,
                Genero = "",
                EstadoCivil = "",
                Nacionalidade = "",
                Naturalidade = "",
                Contacto1 = 0,
                Contacto2 = 0,
                Email = ""
            };

            Autuante = new Autuante {
                EntidadeAutuante = "",
                IsPresenciado = false,
                Equipamento = ""

            };
        }

#region REGION -> Lists (entidades, placeholders, genero, estado civil)
        List<String> entidades = new List<string> {
            "Não Definido",
            "DGTR",
            "Tribunal",
            "AN",
            "PE"
        };

        List<String> placeholders = new List<string> {
            "Não Definido",
            "Teste 1",
            "Teste 2",
            "Teste 3",
            "Teste 4"
        };

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

        List<String> equipamentos = new List<string> {
            "Não Definido",
            "Alcoolímetro",
            "Multímetro",
            "Voltímetro",
            "Placeholderímetro"
        };
#endregion
    }
}
