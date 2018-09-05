using System;
using SQLite;
using System.Text.RegularExpressions;
using System.Diagnostics;
using GeCO.ViewModels;

namespace GeCO.Models {

    /// <summary>
    /// Tabela Pessoa. Contém 3 campos (bool) para distinguir o tipo de pessoa se trata: IsDenunciante, IsArguido e IsTestemunha.
    /// </summary>
    [Table("Pessoa")]
    public class Pessoa : PropertyChangedVM {
        private string _nome;
        private DateTime _dataNascimento;
        private string _genero;
        private string _estadoCivil;
        private string _nacionalidade;
        private string _naturalidade;
        private int _nif;
        private int _contacto1;
        private int _contacto2;
        private string _email;


        [PrimaryKey, AutoIncrement]
        public int PessoaId { get; set; }
        
        public bool IsDenunciante { get; set; }
        public bool IsArguido { get; set; }
        public bool IsTestemunha { get; set; }

        public string Nome {
            get { return _nome; }
            set { _nome = value; }
        }

        public DateTime DataNascimento {
            get { return _dataNascimento; }
            set {
                if (value.ToString().Length > 10)
                    _dataNascimento = value;
                else _dataNascimento = value;
            }
        }

        public string Genero {
            get { return _genero; }
            set {
                if (value.Equals("") || value == null)
                    _genero = "Não Definido";
                else
                    _genero = value;
            }
        }

        public string EstadoCivil {
            get { return _estadoCivil; }
            set {
                if (value.Equals("") || value == null)
                    _estadoCivil = "Não Definido";
                else
                    _estadoCivil = value;
            }
        }

        public string Nacionalidade {
            get { return _nacionalidade; }
            set { _nacionalidade = value; }
        }

        public string Naturalidade {
            get { return _naturalidade; }
            set { _naturalidade = value; }
        }

        [MaxLength(9)]
        public int NIF {
            get { return _nif; }
            set {
                if (Regex.Matches(value.ToString(), @"[a-zA-Z]").Count > 0 || value.ToString().Length != 9) {
                    Debug.WriteLine("Pessoa.NIF contem caracteres errados.");
                    Random r = new Random();
                    _nif = r.Next(100000000, 999999999);
                }
                else {
                    _nif = value;
                }
            }
        }

        [MaxLength(9)]
        public int Contacto1 {
            get { return _contacto1; }
            set {
                if (Regex.Matches(value.ToString(), @"[a-zA-Z]").Count > 0 || value.ToString().Length < 8) {
                    _contacto1 = 0;
                    Debug.WriteLine("Pessoa.Contacto1 contem caracteres errados.");
                }
                else {
                    _contacto1 = value;
                }
            }
        }

        [MaxLength(9)]
        public int Contacto2 { 
            get { return _contacto2; }
            set {
                if (Regex.Matches(value.ToString(), @"[a-zA-Z]").Count > 0 || value.ToString().Length < 8) {
                    _contacto2 = 0;
                    Debug.WriteLine("Pessoa.Contacto2 contem caracteres errados.");
                }
                else {
                    _contacto2 = value;
                }
            }
        }

        public string Email {
            get { return _email; }
            set {
                if (value.Contains(@"@"))
                    _email = value;
                else {
                    Debug.WriteLine("Email errado.");
                    _email = @"example@email.com";
                }
            }
        }
    }



    [Table("QualidadeArguido")]
    public class QualidadeArguido {
        [PrimaryKey, AutoIncrement]
        public int QualidadeId { get; set; }

        public string QualidadeTipo { get; set; }
    }
}
