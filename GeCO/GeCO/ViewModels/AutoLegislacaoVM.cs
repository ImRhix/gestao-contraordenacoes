using System;
using System.Collections.Generic;
using GeCO.Models;
using System.Threading.Tasks;


namespace GeCO.ViewModels {

    public class AutoLegislacaoVM : PropertyChangedVM {

        public List<String> Titulo => titulos;

        private Lei _lei;

        public AutoLegislacaoVM()
        {
            Lei = new Lei
            {
                Titulo =    "",
                Pontos =    0,
                Descricao = "",
                Min =       0,
                Max =       0,
                Prazo =     0
            };
        }


        public async Task<Geral> GetGeral(int id) {
            return await App.Database.GetGeral(id);
        }

        public async Task<Lei> GetLei(int id) {
            return await App.Database.GetLei(id);
        }



        /// <summary>
        /// Realiza um UPDATE na FK da row tabela Geral (caso haja alguma lei selecionada no dropdown).
        /// </summary>
        public async Task<Lei>GuardarLei(Lei lei, int autoId) {
            if (lei.LeiId != 0)
                lei = await App.Database.SaveLei(lei, autoId);

            return lei;
        }


        /// <summary>
        /// Na verdade não apaga lei nenhuma. Apenas altera a FK LeiId de uma certa row da tabela Geral para '0'
        /// </summary>
        public async Task ApagarLei(int leiId, int autoId)
        {
            await App.Database.UpdateLei(leiId, autoId);
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
        }


        public Lei Lei {
            get { return _lei; }
            set { _lei = value; OnPropertyChanged(); }
        }
          
          


 #region REGION -> Lists (títulos)
        List<String> titulos = new List<string> {
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
#endregion
    }
}