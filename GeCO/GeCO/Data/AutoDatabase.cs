using System;
using SQLite;
using GeCO.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;


namespace GeCO.Data {
    
    public class AutoDatabase {
        readonly SQLiteAsyncConnection database;

        /// <summary>
        /// Cria as tables necessárias no path (que é passado como parâmetro).
        /// </summary>
        public AutoDatabase(string dbPath) {
            database = new SQLiteAsyncConnection(dbPath);

            database.CreateTablesAsync<Geral, Localizacao, Autuante, Lei, Pessoa>().Wait();
            database.CreateTablesAsync<Coima, Apreensao>().Wait();
            database.CreateTablesAsync<Pagamento, CustosProcessuais, QualidadeArguido>().Wait();
            Debug.WriteLine("All tables created.\n");
        }



#region REGION -> Get (IDs / PKs)
        /// <summary>
        /// Encontra a ultima linha da tabela Geral, incrementa uma unidade à PK (AutoId) e retorna o valor. 
        /// (NOTA: é suposto a PK ser auto-incrementada mas por algum motivo não está a acontecer e este é o walkaround.)
        /// </summary>
        public async Task<Int32> GetNextGeralId() {
            int nextId = 0;
            string query = $"SELECT * FROM Geral ORDER BY AutoId DESC LIMIT 1";

            try {
                var result = await database.QueryAsync<Geral>(query);
                foreach (var item in result)
                    nextId = ++item.AutoId;
                return nextId;
            } catch {
                Debug.WriteLine("Error in AutoDatabase.cs: GetNexGeralId()");
                return nextId;
            }
        }


        /// <summary>
        /// Retorna o ultimo LocalId incrementado para não existir repetição nos inserts. Ex.: se ultimo id = 5, este metodo retorna 6.
        /// </summary>
        public async Task<Int32> GetNextLocalId() {
            int nextId = 0;
            string query = $"SELECT * FROM Localizacao ORDER BY LocalId DESC LIMIT 1";

            try {
                var result = await database.QueryAsync<Localizacao>(query);
                foreach (var item in result)
                    nextId = ++item.LocalId;
                return nextId;
            } catch {
                Debug.WriteLine("Error in AutoDatabase.cs: GetNexLocalId()");
                return nextId;
            }
        }


        /// <summary>
        /// Retorna o ultimo PessoaId incrementado para não existir repetição nos inserts. Ex.: se ultimo id = 5, este methodo retorna 6.
        /// </summary>
        public async Task<Int32> GetNextPessoaId() {
            int nextId = 0;
            string query = $"SELECT * FROM Pessoa ORDER BY PessoaId DESC LIMIT 1";

            try {
                var result = await database.QueryAsync<Pessoa>(query);
                foreach (var id in result)
                    nextId = ++id.PessoaId;
                return nextId;
            } catch {
                Debug.WriteLine("Error in AutoDatabase.cs: GetNexPessoaId()");
                return nextId;
            }
        }


        /// <summary>
        /// Retorna o ultimo LeiId incrementado para não existir repetição nos inserts. Ex.: se ultimo id = 5, este methodo retorna 6.
        /// </summary>
        public async Task<Int32> GetNextLeiId() {
            int nextId = 0;
            string query = $"SELECT * FROM Lei ORDER BY LeiId DESC LIMIT 1";

            try {
                var result = await database.QueryAsync<Lei>(query);
                foreach (var id in result)
                    nextId = ++id.LeiId;
                return nextId;
            } catch {
                Debug.WriteLine("Error in AutoDatabase.cs: GetNextLeiId()");
                return nextId;
            }
        }


        /// <summary>
        /// Retorna o ultimo ApreensaoId incrementado para não existir repetição nos inserts. Ex.: se ultimo id = 5, este methodo retorna 6.
        /// </summary>
        public async Task<Int32> GetNextApreensaoId() {
            int nextId = 0;
            string query = $"SELECT * FROM Apreensao ORDER BY ApreensaoId DESC LIMIT 1";

            try {
                var result = await database.QueryAsync<Apreensao>(query);
                foreach (var id in result)
                    nextId = ++id.ApreensaoId;
                return nextId;
            } catch {
                Debug.WriteLine("Error in AutoDatabase.cs: GetNextApreensaoId()");
                return nextId;
            }
        }


        /// <summary>
        /// Retorna o ultimo PagamentoId incrementado para não existir repetição nos inserts. Ex.: se ultimo id = 5, o return será 6.
        /// </summary>
        public async Task<Int32> GetNexPagamentoId() {
            int nextId = 0;
            string query = $"SELECT * FROM Pagamento ORDER BY PagamentoId DESC LIMIT 1";

            try {
                var result = await database.QueryAsync<Pagamento>(query);
                foreach (var id in result)
                    nextId = ++id.PagamentoId;
                return nextId;
            }
            catch {
                Debug.WriteLine("Error in AutoDatabase.cs: GetNextPagamentoId()");
                return nextId;
            }
        }
#endregion


#region REGION -> Get (Objects)
        /// <summary>
        /// Retorna um objeto (row) da tabela Geral baseado no id (PK)
        /// </summary>
        public async Task<Geral> GetGeral(int id) {
            try {
                return await database.GetAsync<Geral>(id);
            } catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: GetGeral()\n");
                return null;
            }
        }


        /// <summary>
        /// Retorna um objeto (row) da tabela Localizacao baseado no id (PK)
        /// </summary>
        public async Task<Localizacao> GetLocalizacao(int id) {
            try {
                return await database.GetAsync<Localizacao>(id);
            } catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: GetLocalizacao()\n");
                return null;
            }
        }


        /// <summary>
        /// Retorna um objeto (row) da tabela Pessoa baseado no id (PK)
        /// </summary>
        public async Task<Pessoa> GetPessoa(int id) {
            try {
                return await database.GetAsync<Pessoa>(id);
            } catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: GetPessoa()\n");
                return null;
            }
        }


        /// <summary>
        /// Retorna um objeto (row) da tabela Autuante baseado no id (PK)
        /// </summary>
        public async Task<Autuante> GetAutuante(int id) {
            try {
                return await database.GetAsync<Autuante>(id);
            } catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: GetAutuante()\n");
                return null;
            }
        }


        /// <summary>
        /// Retorna um objeto (row) da tabela Lei baseado no id (PK)
        /// </summary>
        public async Task<Lei> GetLei(int id) {
            try {
                return await database.GetAsync<Lei>(id);
            } catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: GetLei()\n");
                return null;
            }
        }


        /// <summary>
        /// Retorna um objeto (row) da tabela Apreensao baseado no id (PK)
        /// </summary>
        public async Task<Apreensao> GetApreensao(int id) {
            try {
                return await database.GetAsync<Apreensao>(id);
            } catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: GetApreensao()\n");
                return null;
            }
        }


        /// <summary>
        /// Retorna um objeto (row) da tabela Pagamento baseado no id (PK)
        /// </summary>
        public async Task<Pagamento> GetPagamento(int id) {
            try {
                return await database.GetAsync<Pagamento>(id);
            } catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: GetPagamento()\n");
                return null;
            }
        }


        /// <summary>
        /// Retorna uma List com todos os objetos pessoa baseado num NIF
        /// </summary>
        public async Task<bool> CheckPessoa(int nif) {
            string query = $"SELECT * FROM Pessoa WHERE NIF = {nif}";
            try {
                var pessoaList = await database.QueryAsync<Pessoa>(query);
                foreach (var item in pessoaList){
                    return true;
                }
                return false;
            } catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: CheckPessoa(). Returned false.\n");
                return false;
            }
        }


        /// <summary>
        /// Retorna uma List com todos os objetos (rows da tabela) Geral
        /// </summary>
        public async Task<List<Geral>> GetGeralList() {
            string queryGeral = "SELECT * FROM Geral";
            try {
                return await database.QueryAsync<Geral>(queryGeral);
            }
            catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: GetGeralList()\n");
                return null;
            }
        }


        public async Task<List<Pessoa>> GetPessoaList() {
            string queryGeral = "SELECT * FROM Pessoa";
            try {
                return await database.QueryAsync<Pessoa>(queryGeral);
            }
            catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: GetPessoaList()\n");
                return null;
            }
        }


        public async Task<List<Pessoa>> GetPessoaListOrdered() {
            string queryGeral = "SELECT * FROM Pessoa ORDER BY Nome COLLATE NOCASE";
            try {
                return await database.QueryAsync<Pessoa>(queryGeral);
            }
            catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: GetPessoaListOrdered()\n");
                return null;
            }
        }


        /// <summary>
        /// Retorna uma lista com todos os Pagamentos
        /// </summary>
        public async Task<List<Pagamento>> GetPagamentoList() {
            string queryGeral = "SELECT * FROM Pagamento";
            try {
                return await database.QueryAsync<Pagamento>(queryGeral);
            }
            catch {
                Debug.WriteLine("Error in AutoDatabase.cs: GetPagamentoList()");
                return null;
            }
        }


        /// <summary>
        /// Retorna uma lista com todas as Leis
        /// </summary>
        public async Task<List<Lei>> GetLeiList() {
            string queryGeral = "SELECT * FROM Lei";
            try {
                return await database.QueryAsync<Lei>(queryGeral);
            }
            catch {
                Debug.WriteLine("Error in AutoDatabase.cs: GetLeiList()");
                return null;
            }
        }

#endregion


#region REGION -> Saves (INSERTs)

        /// <summary>
        /// Insere ou Atualiza um objecto (row) da tabela Geral
        /// </summary>
        public async Task<Geral> SaveGeral(Geral geral) {
            if (geral.AutoId == 0) {
                try {
                    geral.AutoId = await GetNextGeralId();
                    geral.CodProcesso = geral.AutoId.ToString();

                    await database.InsertAsync(geral);

                    return geral;
                }
                catch {
                    Debug.WriteLine("\nErro in AutoDatabase.cs: SaveGeral().\n");
                    return geral;
                }
            }
            await database.UpdateAsync(geral);
            return geral;
        }


        /// <summary>
        /// Insere ou Atualiza um objecto (row) na tabela Localizacao
        /// </summary>
        public async Task<Localizacao> SaveLocalizacao(Localizacao localizacao) {
            if (localizacao.LocalId == 0) {
                try {
                    localizacao.LocalId = await GetNextLocalId();
                    await database.InsertAsync(localizacao);

                    return localizacao;
                }
                catch {
                    Debug.WriteLine("\nErro in AutoDatabase.cs: SaveLocal().\n");
                    return localizacao;
                }
            }
            await database.UpdateAsync(localizacao);

            return localizacao;
        }


        /// <summary>
        /// Insere ou Atualiza um objecto (row) na tabela Pessoa
        /// </summary>
        public async Task<Pessoa> SavePessoa(Pessoa pessoa) {
            if (pessoa.PessoaId == 0) {
                try {
                    pessoa.PessoaId = await GetNextPessoaId();
                    await database.InsertAsync(pessoa);

                    return pessoa;
                }
                catch {
                    Debug.WriteLine("\nError in AutoDatabase.cs: SavePessoa().\n");
                    return pessoa;
                }
            }
            await database.UpdateAsync(pessoa);

            return pessoa;
        }


        /// <summary>
        /// Insere/Atualiza um objecto (row) na tabela Autuante e atualiza a FK na respetiva row da tabela Geral
        /// </summary>
        public async Task<Autuante> SaveAutuantePrincipal(Autuante autuante, int autoId) {
            if (autuante.AutuanteId == 0) {
                try {
                    autuante.AutuanteId = await GetNextLeiId();
                    await database.InsertAsync(autuante);

                    var query = $"UPDATE Geral SET AutuanteId = { autuante.AutuanteId } WHERE AutoId = { autoId }";
                    await database.QueryAsync<Geral>(query);

                    return autuante;
                }
                catch {
                    Debug.WriteLine("\nError in AutoDatabase.cs: SaveAutuantePrincipal().\n");
                    return autuante;
                }
            }
            await database.UpdateAsync(autuante);

            var query2 = $"UPDATE Geral SET AutuanteId = { autuante.AutuanteId } WHERE AutoId = { autoId }";
            await database.QueryAsync<Geral>(query2);

            return autuante;
        }


        /// <summary>
        /// Atualiza um objecto na tabela Apreensao
        /// </summary>
        public async Task<Apreensao> SaveApreensao(Apreensao apreensao) {
            if (apreensao.ApreensaoId == 0) {
                try {
                    apreensao.ApreensaoId = await GetNextApreensaoId();
                    await database.InsertAsync(apreensao);

                    return apreensao;
                }
                catch {
                    Debug.WriteLine("\nError in AutoDatabase.cs: SaveApreensao().\n");
                    return apreensao;
                }
            }
            await database.UpdateAsync(apreensao);

            return apreensao;
        }

        /// <summary>
        /// Realiza um UPDATE na FK da row na tabela Geral.
        /// </summary>
        public async Task<Lei> SaveLei(Lei lei, int autoId) {
            if (lei.LeiId != 0) { 
                try {
                    var query = $"UPDATE Geral SET LeiId = { lei.LeiId } WHERE AutoId = { autoId }";
                    await database.QueryAsync<Geral>(query);

                    return lei;
                }
                catch {
                    Debug.WriteLine("\nError in AutoDatabase.cs: SaveLei().\n");
                    return lei;
                }
            }
            return lei;
        }


        /// <summary>
        /// Insere ou atualiza um objeto na tabela Pagamento
        /// </summary>
        public async Task<Pagamento> SavePagamento(Pagamento pagamento) {
            if (pagamento.PagamentoId == 0) {
                try {
                    pagamento.PagamentoId = await GetNexPagamentoId();
                    await database.InsertAsync(pagamento);

                    return pagamento;
                }
                catch {
                    Debug.WriteLine("\nError in AutoDatabase.cs: SavePagamento().\n");
                    return pagamento;
                }
            }
            await database.UpdateAsync(pagamento);

            return pagamento;
        }
#endregion


#region REGION -> UPDATES

        /// <summary>
        /// Atualiza um objeto pessoa
        /// </summary>
        public async Task UpdatePessoa(Pessoa pessoa) {
            try {
                int x = await database.UpdateAsync(pessoa);
            } catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: UpdatePessoa().\n");
            }
        }


        /// <summary>
        /// Corre a lista de autos e modifica todas as FKs da pessoa que se deseja dessassociar do auto para 0
        /// </summary>
        public async Task UpdateDeletedPessoa(int pessoaId) {
            var lista = await GetGeralList();
            foreach (var item in lista) {
                if (item.DenuncianteId == pessoaId) {
                    await UpdateDenunciante(0, item.AutoId);
                }
                if (item.ArguidoId == pessoaId) {
                    await UpdateArguido(0, item.AutoId);
                }
                if (item.TestemunhaId == pessoaId) {
                    await UpdateTestemunha(0, item.AutoId);
                }
            }
            //try
            //{
            //    var query = $"UPDATE Geral SET DenuncianteId = '0' WHERE DenuncianteId = {pessoaId}";
            //    await database.QueryAsync<Geral>(query);
            //    Debug.WriteLine("\n1 [Denunciante].\n");
            //}
            //catch { Debug.WriteLine("\nError in AutoDatabase.cs: UpdateDeletedPessoa() [Denunciante].\n"); }

            //try {
            //    var query = $"UPDATE Geral SET TestemunhaId = '0' WHERE TestemunhaId = {pessoaId}";
            //    await database.QueryAsync<Geral>(query);
            //    Debug.WriteLine("\n2 [Testemunha].\n");

            //}
            //catch { Debug.WriteLine("\nError in AutoDatabase.cs: UpdateDeletedPessoa() [Testemunha].\n"); }

            //try {
            //    var query = $"UPDATE Geral SET ArguidoId = '0' WHERE TestemunhaId = {pessoaId}";
            //    await database.QueryAsync<Geral>(query);
            //    Debug.WriteLine("\n3 [Arguido].\n");

            //}
            //catch { Debug.WriteLine("\nError in AutoDatabase.cs: UpdateDeletedPessoa() [Arguido].\n"); }
        }


        /// <summary>
        /// Atualiza a FK referente ao denunciante num determinado Auto
        /// </summary>
        public async Task UpdateDenunciante(int pessoaId, int autoId) {
            try {
                var query = $"UPDATE Geral SET DenuncianteId = { pessoaId } WHERE AutoId = { autoId }";
                await database.QueryAsync<Geral>(query);
            } catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: UpdateDenunciante().\n");
            }
        }


        /// <summary>
        /// Atualiza a FK referente ao arguido num determinado Auto
        /// </summary>
        public async Task UpdateArguido(int pessoaId, int autoId) {
            try {
                var query = $"UPDATE Geral SET ArguidoId = { pessoaId } WHERE AutoId = { autoId }";
                await database.QueryAsync<Geral>(query);
            } catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: UpdateArguido().\n");
            }
        }


        /// <summary>
        /// Atualiza a FK referente à testemunha num determinado Auto
        /// </summary>
        public async Task UpdateTestemunha(int pessoaId, int autoId) {
            try {
                var query = $"UPDATE Geral SET TestemunhaId = { pessoaId } WHERE AutoId = { autoId }";
                await database.QueryAsync<Geral>(query);
            } catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: UpdateTestemunha().\n");
            }
        }


        /// <summary>
        /// Atualiza o id da localização num determinado auto
        /// </summary>
        public async Task UpdateLocalizacao(int localId, int autoId) {
            try {
                var query = $"UPDATE Geral SET LocalId = { localId } WHERE AutoId = { autoId }";
                await database.QueryAsync<Geral>(query);
            } catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: UpdateLocalizacao().\n");
            }
        }


        /// <summary>
        /// Atualiza o ApreensaoId para 0 num determinado auto
        /// </summary>
        public async Task UpdateApreensao (int apreensaoId, int autoId) {
            try {
                var query = $"UPDATE Geral SET ApreensaoId = { apreensaoId } WHERE AutoId = { autoId }";
                await database.QueryAsync<Geral>(query);
            } catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: UpdateApreensao().\n");
            }
        }


        /// <summary>
        /// Atualiza a FK LeiId num determinado auto
        /// </summary>
        public async Task UpdateLei(int leiId, int autoId) {
            try {
                var query = $"UPDATE Geral SET LeiId = { leiId } WHERE AutoId = { autoId }";
                await database.QueryAsync<Geral>(query);
            } catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: UpdateLei().\n");
            }
        }


        /// <summary>
        /// Atualiza um objeto Pagamento
        /// </summary>
        public async Task UpdatePagamento(Pagamento pagamento) {
            try {
                int x = await database.UpdateAsync(pagamento);
            }
            catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: UpdatePagamento().\n");
            }
        }


        /// <summary>
        /// Atualiza a FK PagamentoId num determinado Auto
        /// </summary>
        public async Task UpdateGeralPagamento(int pagamentoId, int autoId) {
            try {
                var query = $"UPDATE Geral SET PagamentoId = { pagamentoId } WHERE AutoId = { autoId }";
                await database.QueryAsync<Geral>(query);
            } catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: UpdateGeralPagamento().\n");
            }
        }


        /// <summary>
        /// Atualiza para 0 a FK PagamentoID de um determinado auto através da função UpdateGeralPagamento()
        /// </summary>
        public async Task UpdateDeletedPagamento(int pagamentoId) {

            var lista = await GetGeralList();

            foreach (var item in lista) {
                if (item.PagamentoId == pagamentoId) {
                    await UpdateGeralPagamento(0, item.AutoId);
                }
            }
        }
#endregion


#region REGION -> Deletes
        /// <summary>
            /// Apaga da tabela o atual objeto Geral
            /// </summary>
        public async Task ApagarGeral(Geral geral) {
            try {
                await database.DeleteAsync(geral);
            } catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: ApagarGeral()\n");
            }
        }


        /// <summary>
        /// Apaga da tabela o atual objeto Pessoa
        /// </summary>
        public async Task ApagarPessoa(Pessoa pessoa) {
            try {
                await database.DeleteAsync(pessoa);
            } catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: ApagarPessoa()\n");
            }
        }


        /// <summary>
        /// Apaga da tabela o atual objeto Localizacao
        /// </summary>
        public async Task ApagarLocalizacao(Localizacao localizacao) {
            try {
                await database.DeleteAsync(localizacao);
            } catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: ApagarLocalizacao()\n");
            }
        }


        /// <summary>
        /// Apaga da tabela o atual objeto Lei
        /// </summary>
        public async Task ApagarAutuante(Autuante autuante) {
            try {
                await database.DeleteAsync(autuante);
            } catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: ApagarLei)\n");
            }
        }


        /// <summary>
        /// Apaga da tabela o atual objeto Lei
        /// </summary>
        public async Task ApagarLei(Lei lei) {
            try {
                await database.DeleteAsync(lei);
            } catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: ApagarLei)\n");
            }
        }


        /// <summary>
        /// Apaga a atual apreensao (row) da tabela Apreensao e retorna o valor da FK que ficou guardado em Geral (supostamente deverá ser 0 pois acabámos de apagar a 'Apreensao').
        /// </summary>
        public async Task ApagarApreensao(Apreensao apreensao) {
            try {
                await database.DeleteAsync(apreensao.ApreensaoId);
            } catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: ApagarApreensao().\n");
            }
        }


        /// <summary>
        /// Apaga a atual apreensao (row) da tabela Pagamento e retorna o valor da FK que ficou guardado em Geral (supostamente deverá ser 0 pois acabámos de apagar o 'Pagamento').
        /// </summary>
        public async Task ApagarPagamento(Pagamento pagamento) {
            try { 
                await database.DeleteAsync(pagamento.PagamentoId);
            } catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: ApagarPagamento().\n");
            }
        }


        /// <summary>
        /// Apaga todas as tabelas. (reset)
        /// </summary>
        public async Task Vacuum() {
            try {
                await database.DropTableAsync<Geral>();
                await database.DropTableAsync<Localizacao>();
                await database.DropTableAsync<Pessoa>();
                await database.DropTableAsync<Autuante>();
                await database.DropTableAsync<Apreensao>();
                await database.DropTableAsync<Lei>();
                await database.DropTableAsync<Pagamento>();
                Debug.WriteLine("Tables droped successfully.");
            }
            catch {
                Debug.WriteLine("Error in AutoDatabase.cs: Vacuum().");
            }
        }
#endregion









#region REGION -> apenas para os placeholders
        public async Task<Geral> _PHInsert_Geral(Geral geral) {
            try {
                await database.InsertAsync(geral);
                return geral;
            } catch {
                Debug.WriteLine("\nErro in AutoDatabase.cs: _PHInsert_Geral().\n");
                return geral;
            }
        }

        public async Task<Localizacao> _PHInsert_Local(Localizacao localizacao) {
            try {
                await database.InsertAsync(localizacao);
                return localizacao;
            } catch {
                Debug.WriteLine("\nErro in AutoDatabase.cs: _PHInsert_Local().\n");
                return localizacao;
            }
        }

        public async Task<Pessoa> _PHInsert_Pess(Pessoa pessoa) {
            try {
                await database.InsertAsync(pessoa);
                return pessoa;
            } catch {
                Debug.WriteLine("\nErro in AutoDatabase.cs: _PHInsert_Pess().\n");
                return pessoa;
            }
        }

        public async Task<Autuante> _PHInsert_Autuante(Autuante autuante) {
            try {
                await database.InsertAsync(autuante);
                return autuante;
            } catch {
                Debug.WriteLine("\nErro in AutoDatabase.cs: _PHInsert_Autuante().\n");
                return autuante;
            }
        }

        public async Task<Lei> _PHInsert_Lei(Lei lei) {
            try {
                await database.InsertAsync(lei);
                return lei;
            } catch {
                Debug.WriteLine("\nErro in AutoDatabase.cs: _PHInsert_Lei().\n");
                return lei;
            }
        }

        public async Task<Apreensao> _PHInsert_Apreensao(Apreensao apreensao) {
            try {
                await database.InsertAsync(apreensao);
                return apreensao;
            } catch {
                Debug.WriteLine("\nErro in AutoDatabase.cs: _PHInsert_Apreensao().\n");
                return apreensao;
            }
        }

        public async Task<Pagamento> _PHInsert_Pagamento(Pagamento pagamento) {
            try {
                await database.InsertAsync(pagamento);
                return pagamento;
            } catch {
                Debug.WriteLine("\nErro in AutoDatabase.cs: _PHInsert_Pagamento().\n");
                return pagamento;
            }
        }



        /// <summary>
        /// Ordem: autoId, localId, denuncianteId, arguidoId, testemunhaId, autuanteId, leiId, apreensaoId, pagamentoId
        /// </summary>
        public async Task _PHUpdate_Geral(int autoId, int localId, int denuncianteId, int arguidoId, int testemunhaId, int autuanteId, int leiId, int apreensaoId, int pagamentoId) {
            //var query = $"UPDATE Geral SET LocalId = { localId }, DenuncianteId = {denuncianteId}, ArguidoId = {arguidoId}, TestemunhaId = {testemunhaId}, AutuanteId = {autuanteId}, LeiId = {leiId}, ApreensaoId = {apreensaoId}, PagamentoId = {pagamentoId} WHERE AutoId = {autoId}";
            try {
                //await database.QueryAsync<Geral>(query);
                var query = $"UPDATE Geral SET LocalId = { localId } WHERE AutoId = {autoId}";
                await database.QueryAsync<Geral>(query);
                query = $"UPDATE Geral SET DenuncianteId = { denuncianteId } WHERE AutoId = {autoId}";
                await database.QueryAsync<Geral>(query);
                query = $"UPDATE Geral SET ArguidoId = { arguidoId } WHERE AutoId = {autoId}";
                await database.QueryAsync<Geral>(query);
                query = $"UPDATE Geral SET TestemunhaId = { testemunhaId } WHERE AutoId = {autoId}";
                await database.QueryAsync<Geral>(query);
                query = $"UPDATE Geral SET AutuanteId = { autuanteId } WHERE AutoId = {autoId}";
                await database.QueryAsync<Geral>(query);
                query = $"UPDATE Geral SET LeiId = { leiId } WHERE AutoId = {autoId}";
                await database.QueryAsync<Geral>(query);
                query = $"UPDATE Geral SET ApreensaoId = { apreensaoId } WHERE AutoId = {autoId}";
                await database.QueryAsync<Geral>(query);
                query = $"UPDATE Geral SET PagamentoId = { pagamentoId } WHERE AutoId = {autoId}";
                await database.QueryAsync<Geral>(query);
            } catch {
                Debug.WriteLine("\nError in AutoDatabase.cs: _PHUpdate_Geral().\n");
            }
        }
#endregion
    }
}