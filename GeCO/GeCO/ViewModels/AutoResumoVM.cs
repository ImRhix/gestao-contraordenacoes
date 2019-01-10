using System;
using System.Threading.Tasks;
using GeCO.Models;

namespace GeCO.ViewModels {
    
    public class AutoResumoVM {
        
        public AutoResumoVM() {
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
    }
}
