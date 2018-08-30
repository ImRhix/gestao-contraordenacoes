using GeCO.ViewModels;
using SQLite;
using System;


namespace GeCO.Models {
    
    public class Auto : PropertyChangedVM {
        [PrimaryKey, AutoIncrement] 
        public int trueId { get; set; }
        public string ProcessoId { get; set; }      // **
        public string Data { get; set; }            // **
        public string DataLimite { get; set; }      // **
        public string NomeArguido { get; set; }     // **
    }



    public class MeuAuto : PropertyChangedVM {
        public int AutoId { get; set; }
        public string CodProcesso { get; set; }

        private DateTime _dataAuto;
        public DateTime DataAuto {
            get { return _dataAuto; }
            set {
                if (value.ToString().Length > 10)
                    _dataAuto = value;
                else _dataAuto = value;
            }
        }
        public int PessoaId { get; set; }
        public string Nome { get; set; }
        public int LocalId { get; set; }
    }




    /// <summary>
    /// Inclui campos como Cod.Processo, data, entidades, Cods de Infração, etc.
    /// </summary>
    [Table("Geral")]
    public class Geral : PropertyChangedVM {
        private string _codProcesso;
        private string _pastaFisica;
        private DateTime _dataAuto;
        private TimeSpan _horaAuto;
        private string _entDecisora;
        private string _entidadeAutuante;
        private string _entResponsavel;
        private string _codInfracao;
        private string _tipoInfracao;
        private string _categoriaInfracao;


        [PrimaryKey, AutoIncrement]
        public int AutoId { get; set; }

        [Unique]
        public string CodProcesso {
            get { return _codProcesso; }
            set {
                if (value.Equals("") || value.Equals(AutoId.ToString()))
                    _codProcesso = $"{DateTime.Today.Year}-rand-{AutoId}";
                else
                {
                    _codProcesso = value;
                }
            }
        }

        [MaxLength(30)]
        public string PastaFisica {
            get { return _pastaFisica; }
            set { _pastaFisica = value; }
        }

        public DateTime DataAuto {
            get { return _dataAuto; }
            set {
                if (value.ToString().Length > 10)
                    _dataAuto = value;
                else _dataAuto = value;
            }
        }

        public TimeSpan HoraAuto {
            get { return _horaAuto; }
            set {
                value = DateTime.Now.TimeOfDay;
                _horaAuto = value;
            }
        }


        public bool IsAcidenteViacao { get; set; }


        public string EntDecisora {
            get { return _entDecisora; }
            set {
                if (value.Equals("") || value == null)
                    _entDecisora = "Não Definido.";
                else
                    _entDecisora = value;
            }
        }
        public string EntidadeAutuante {
            get { return _entidadeAutuante; }
            set {
                if (value.Equals("") || value == null)
                    _entidadeAutuante = "Não Definido.";
                else
                    _entidadeAutuante = value;
            }
        }
        public string EntResponsavel {
            get { return _entResponsavel; }
            set {
                if (value.Equals("") || value == null)
                    _entResponsavel = "Não Definido.";
                else
                    _entResponsavel = value;
            }
        }

        public string CodigoInfracao {
            get { return _codInfracao; }
            set { _codInfracao = value; }
        }

        public string TipoInfracao {
            get { return _tipoInfracao; }
            set {
                if (value.Equals("") || value == null)
                    _tipoInfracao = "Não Definido.";
                else
                    _tipoInfracao = value;
            }
        }

        public string CategoriaInfracao {
            get { return _categoriaInfracao; }
            set {
                if (value.Equals("") || value == null)
                    _categoriaInfracao = "Não Definido.";
                else
                    _categoriaInfracao = value;
            }
        }


        #region ForeignKeys
        public int LocalId { get; set; } //
        public int CoimaId { get; set; } //

        public int DenuncianteId { get; set; } //
        public int ArguidoId { get; set; }
        public int TestemunhaId { get; set; }
        
        public int AutuanteId { get; set; } //
        public int ApreensaoId { get; set; }
        public int LeiId { get; set; }
        public int PagamentoId { get; set;}
        #endregion
    }


    /// <summary>
    /// Inclui os campos referentes à morada/localização
    /// </summary>
    [Table("Localizacao")]
    public class Localizacao : PropertyChangedVM {
        private string _rua;
        private string _edificioPorta;
        private string _localidade;
        private string _cidade;


        [PrimaryKey, AutoIncrement]
        public int LocalId { get; set; }

        public string Rua {
            get { return _rua; }
            set { _rua = value; }
        }

        public string EdificioPorta {
            get { return _edificioPorta; }
            set { _edificioPorta = value; }
        }

        public string Localidade {
            get { return _localidade; }
            set { _localidade = value; }
        }

        public string Cidade {
            get { return _cidade; }
            set { _cidade = value; }
        }
    }


    /// <summary>
    /// Tabela que inclui os objectos Autuante e Equipamento
    /// </summary>
    [Table("Autuante")]
    public class Autuante : PropertyChangedVM {
        
        [PrimaryKey, AutoIncrement]
        public int AutuanteId { get; set; }

        public string EntidadeAutuante { get; set; }
        public bool IsPresenciado { get; set; }
        public string Equipamento { get; set; }
    }


    /// <summary>
    /// Tabela que inclui os objectos Apreensao
    /// </summary>
    [Table("Apreensao")]
    public class Apreensao : PropertyChangedVM {
        private string _objeto;
        private DateTime _dataApreensao;
        private TimeSpan _horaApreensao;
        private int _leiApreensao;
        private string _tipoApreensao;
        private decimal _coima;
        private string _motivo;

        [PrimaryKey, AutoIncrement]
        public int ApreensaoId { get; set; }

        public string Objeto {
            get { return _objeto; }
            set { _objeto = value; }
        }

        public DateTime DataApreensao {
            get { return _dataApreensao; }
            set {
                if (value.ToString().Length > 10)
                    _dataApreensao = value;
                _dataApreensao = value;
            }
        }

        public TimeSpan HoraApreensao {
            get { return _horaApreensao; }
            set { _horaApreensao = value; }
        }

        public int LeiApreensao {
            get { return _leiApreensao; }
            set { _leiApreensao = value; }
        }

        public string TipoApreensao {
            get { return _tipoApreensao; }
            set { _tipoApreensao = value; }
        }

        public decimal Coima {
            get { return _coima; }
            set { _coima = value; }
        }

        [MaxLength(1500)]
        public string Motivo {
            get { return _motivo; }
            set { _motivo = value; }
        }
    }
}