using GeCO.ViewModels;
using SQLite;
using System;

namespace GeCO.Models {

    /// <summary>
    /// IdLei, Titulo, Pontos, IsCrime, Descricao, IdCoima
    /// </summary>
    [Table("Lei")]
    public class Lei : PropertyChangedVM {

        private string _titulo;
        private int _pontos;
        private string _descricao;
        private decimal _min;
        private decimal _max;
        private int _prazo;

        [PrimaryKey, AutoIncrement]
        public int LeiId { get; set; }

        [MaxLength(100)]
        public string Titulo {
            get { return _titulo; }
            set { _titulo = value; }
        }

        public int Pontos {
            get { return _pontos; }
            set { _pontos = value; }
        }


        [MaxLength(1500)]
        public string Descricao {
            get { return _descricao; }
            set { _descricao = value; }
        }

        public decimal Min {
            get { return _min; }
            set { _min = value; }
        }
        public decimal Max {
            get { return _max; }
            set { _max = value; }
        }

        public int Prazo {
            get { return _prazo; }
            set {
                _prazo = value;
            }
        }
    }


    /// <summary>
    /// Nao será necesário por enquanto
    /// </summary>
    [Table("Coima")]
    public class Coima : PropertyChangedVM {
        private decimal _min;
        private decimal _max;
        private int _prazo;

        [PrimaryKey, AutoIncrement]
        public int CoimaId { get; set; }

        public decimal Min {
            get { return _min; }
            set { _min = value; }
        }
        public decimal Max {
            get { return _max; }
            set { _max = value; }
        }

        public int Prazo {
            get { return _prazo; }
            set { _prazo = value; }
        }
    }
}
