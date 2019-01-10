using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using SQLite;

namespace GeCO.Models {
    
    [Table("Pagamento")]
    public class Pagamento {
        
        private int _nif;
        private int _duc;
        private DateTime _dataLimite;
        private TimeSpan _horaPagamento;
        private string _tipoPagamento;
        private decimal _valor;
        private DateTime _dataInicial;
        private DateTime _dataFinal;
        private DateTime _dataDevolucao;


        [PrimaryKey, AutoIncrement]
        public int PagamentoId { get; set; }

        [MaxLength(9)]
        public int NIF {
            get { return _nif; }
            set {
                if (Regex.Matches(value.ToString(), @"[a-zA-Z]").Count > 0 || value.ToString().Length != 9) {
                    Debug.WriteLine("Pessoa.NIF contem caracteres errados.");
                    Random r = new Random();
                    _nif = r.Next(100000000, 999999999);
                } else {
                    _nif = value;
                }
            }
        }

        [MaxLength(9)]
        public int DUC {
            get { return _duc; }
            set {
                if (Regex.Matches(value.ToString(), @"[a-zA-Z]").Count > 0 || value.ToString().Length != 9) {
                    Debug.WriteLine("Pessoa.NIF contem caracteres errados.");
                    Random r = new Random();
                    _duc = r.Next(100000000, 999999999);
                } else {
                    _duc = value;
                }
            }
        }

        public DateTime DataLimite {
            get { return _dataLimite; }
            set {
                if (value.ToString().Length > 10)
                    _dataLimite = value;
                _dataLimite = value;
            }
        }

        public TimeSpan HoraPagamento {
            get { return _horaPagamento; }
            set { _horaPagamento = value; }
        }

        public string TipoPagamento {
            get { return _tipoPagamento; }
            set { _tipoPagamento = value; }
        }

        public decimal Valor {
            get { return _valor; }
            set { _valor = value; }
        }

        public DateTime DataInicial {
            get { return _dataInicial; }
            set { _dataInicial = value; }
        }

        public DateTime DataFinal {
            get { return _dataFinal; }
            set { _dataFinal = value; }
        }

        public DateTime DataDevolucao {
            get { return _dataDevolucao; }
            set {
                if (value.ToString().Length > 10)
                    _dataDevolucao = value;
                _dataDevolucao = value;
            }
        }

        public int CustoId { get; set; }
    }


    [Table("CustosProcessuais")]
    public class CustosProcessuais {
        [PrimaryKey, AutoIncrement]
        public int CustoId { get; set; }

        public string TipoCusto { get; set; }
        public decimal Valor { get; set; }
    }
}
