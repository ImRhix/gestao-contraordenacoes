using GeCO.Models;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GeCO.Views
{
    public partial class DashboardPage : ContentPage
    {
        ObservableCollection<Auto> autos = new ObservableCollection<Auto>();

        public Geral Geral { get; set; }
        public Localizacao Localizacao { get; set; }
        public Pessoa Pessoa { get; set; }
        public Autuante Autuante { get; set; }
        public Lei Lei { get; set; }
        public Apreensao Apreensao { get; set; }
        public Pagamento Pagamento { get; set; }


        public DashboardPage() {
            InitializeComponent();
        }



        protected override async void OnAppearing() {
            autos.Clear();
            autos.Add(new Auto { ProcessoId = "256/2018", DataLimite = "05/06/2018" });
            autos.Add(new Auto { ProcessoId = "327/2018", DataLimite = "23/06/2018" });
            autos.Add(new Auto { ProcessoId = "329/2018", DataLimite = "04/07/2018" });
            autos.Add(new Auto { ProcessoId = "356/2018", DataLimite = "17/07/2018" });
            autos.Add(new Auto { ProcessoId = "258/2018", DataLimite = "15/06/2018" });
            autos.Add(new Auto { ProcessoId = "373/2018", DataLimite = "18/07/2018" });

            expirarListView.ItemsSource = autos;

            await UnexpectedCreationism();

            await App.Database.CheckPessoa(123456789);

            base.OnAppearing();
        }



        async void OnAddClicked(object sender, System.EventArgs e) {
            var page = new AutoPrincipalPage(0, true);
            await Navigation.PushAsync(page);
        }







        // ******************************************************************** //


#region REGION -> Criação de 4 Autos com informação hardcoded
        private async Task UnexpectedCreationism() {
            
            var id = await App.Database.GetNextGeralId();
            if (id == 0) {
                // **********************************************************************************
                // **********************************************************************************
                Geral = new Geral {
                    CodProcesso =       "2018-DENR/1",
                    PastaFisica =       "53194-DJSO/2018",
                    DataAuto =          new DateTime(2018, 07, 15),
                    HoraAuto =          DateTime.Now.TimeOfDay,
                    IsAcidenteViacao =  false,
                    EntDecisora =       "Tribunal",
                    EntidadeAutuante =  "PE",
                    EntResponsavel =    "DGTR",
                    CodigoInfracao =    "COD-302",
                    TipoInfracao =      "Teste 1",
                    CategoriaInfracao = "Teste 2"
                };
                await App.Database._PHInsert_Geral(Geral);
                Geral = new Geral {
                    CodProcesso =       "2018-JSCE/2",
                    PastaFisica =       "56391-LAKE/2018",
                    DataAuto =          new DateTime(2018, 07, 17),
                    HoraAuto =          DateTime.Now.TimeOfDay,
                    IsAcidenteViacao =  true,
                    EntDecisora =       "Tribunal",
                    EntidadeAutuante =  "AN",
                    EntResponsavel =    "PE",
                    CodigoInfracao =    "COD-563",
                    TipoInfracao =      "Teste 2",
                    CategoriaInfracao = "Teste 1"
                };
                await App.Database._PHInsert_Geral(Geral);
                Geral = new Geral {
                    CodProcesso =       "2018-ATBN/3",
                    PastaFisica =       "45380-EKAL/2018",
                    DataAuto =          new DateTime(2018, 06, 30),
                    HoraAuto =          DateTime.Now.TimeOfDay,
                    IsAcidenteViacao =  false,
                    EntDecisora =       "Tribunal",
                    EntidadeAutuante =  "AN",
                    EntResponsavel =    "DGTR",
                    CodigoInfracao =    "COD-941",
                    TipoInfracao =      "Teste 3",
                    CategoriaInfracao = "Teste 4"
                };
                await App.Database._PHInsert_Geral(Geral);
                Geral = new Geral {
                    CodProcesso =       "2018-MBAE/4",
                    PastaFisica =       "78513-ORNA/2018",
                    DataAuto =          new DateTime(2018, 07, 22),
                    HoraAuto =          DateTime.Now.TimeOfDay,
                    IsAcidenteViacao =  false,
                    EntDecisora =       "DGTR",
                    EntidadeAutuante =  "DGTR",
                    EntResponsavel =    "Tribunal",
                    CodigoInfracao =    "COD-915",
                    TipoInfracao =      "Teste 4",
                    CategoriaInfracao = "Teste 1"
                };
                await App.Database._PHInsert_Geral(Geral);
                Geral = new Geral {
                    CodProcesso =       "2018-AJEN/5",
                    PastaFisica =       "28370-LAIR/2018",
                    DataAuto =          new DateTime(2018, 07, 25),
                    HoraAuto =          DateTime.Now.TimeOfDay,
                    IsAcidenteViacao =  false,
                    EntDecisora =       "AN",
                    EntidadeAutuante =  "PE",
                    EntResponsavel =    "Tribunal",
                    CodigoInfracao =    "COD-226",
                    TipoInfracao =      "Teste 2",
                    CategoriaInfracao = "Teste 3"
                };
                await App.Database._PHInsert_Geral(Geral);
                Geral = new Geral {
                    CodProcesso =       "2018-LSAN/6",
                    PastaFisica =       "47382-LAIR/2018",
                    DataAuto =          new DateTime(2018, 08, 03),
                    HoraAuto =          DateTime.Now.TimeOfDay,
                    IsAcidenteViacao =  true,
                    EntDecisora =       "PE",
                    EntidadeAutuante =  "AN",
                    EntResponsavel =    "Tribunal",
                    CodigoInfracao =    "COD-826",
                    TipoInfracao =      "Teste 4",
                    CategoriaInfracao = "Teste 1"
                };
                await App.Database._PHInsert_Geral(Geral);


                // **********************************************************************************
                // **********************************************************************************
                Localizacao = new Localizacao {
                    Rua =               "Rua Morais Soares",
                    EdificioPorta =     "143",
                    Cidade =            "Lisboa",
                    Localidade =        "Arroios"
                };
                await App.Database._PHInsert_Local(Localizacao);
                Localizacao = new Localizacao {
                    Rua =               "Rua António Pereira Carrilho",
                    EdificioPorta =     "5",
                    Cidade =            "Lisboa",
                    Localidade =        "Arroios"
                };
                await App.Database._PHInsert_Local(Localizacao);
                Localizacao = new Localizacao {
                    Rua =               "Rua António Pedro",
                    EdificioPorta =     "46",
                    Cidade =            "Lisboa",
                    Localidade =        "Areeiro"
                };
                await App.Database._PHInsert_Local(Localizacao);
                Localizacao = new Localizacao {
                    Rua =               "Rua São Vicente",
                    EdificioPorta =     "19",
                    Cidade =            "Loures",
                    Localidade =        "São João da Talha"
                };
                await App.Database._PHInsert_Local(Localizacao);
                Localizacao = new Localizacao {
                    Rua =               "Rua da Estrela",
                    EdificioPorta =     "12",
                    Cidade =            "Loures",
                    Localidade =        "Catujal"
                };
                await App.Database._PHInsert_Local(Localizacao);
                Localizacao = new Localizacao {
                    Rua =               "Rua Carlos Mardel",
                    EdificioPorta =     "5",
                    Cidade =            "Alverca",
                    Localidade =        "Alfundão"
                };
                await App.Database._PHInsert_Local(Localizacao);


                // **********************************************************************************
                // **********************************************************************************
                Pessoa = new Pessoa {
                    Nome =              "Filipa Alexandra Silva",
                    DataNascimento =    new DateTime(1974, 01, 23),
                    NIF =               new Random().Next(100000000, 299999999),
                    Genero =            "Feminino",
                    EstadoCivil =       "Solteiro",
                    Nacionalidade =     "Portugal",
                    Naturalidade =      "Lisboa",
                    Contacto1 =         new Random().Next(910000000, 939999999),
                    Contacto2 =         new Random().Next(211000000, 299999999),
                    Email =             "falexsilva@gmail.com"
                };
                await App.Database._PHInsert_Pess(Pessoa);
                Pessoa = new Pessoa {
                    Nome =              "Ricardo Brito",
                    DataNascimento =    new DateTime(1991, 03, 09),
                    NIF =               new Random().Next(100000000, 299999999),
                    Genero =            "Masculino",
                    EstadoCivil =       "Divorciado",
                    Nacionalidade =     "Portugal",
                    Naturalidade =      "Loures",
                    Contacto1 =         new Random().Next(910000000, 939999999),
                    Contacto2 =         new Random().Next(211000000, 299999999),
                    Email =             "ric.brito@gmail.com"
                };
                await App.Database._PHInsert_Pess(Pessoa);
                Pessoa = new Pessoa {
                    Nome =              "Aníbal Pascoal de Melo",
                    DataNascimento =    new DateTime(1954, 06, 15),
                    NIF =               new Random().Next(100000000, 299999999),
                    Genero =            "Masculino",
                    EstadoCivil =       "Viúvo",
                    Nacionalidade =     "Brasil",
                    Naturalidade =      "Campinas",
                    Contacto1 =         new Random().Next(910000000, 939999999),
                    Contacto2 =         new Random().Next(211000000, 299999999),
                    Email =             "aniba.pasmelo@yahoo.br"
                };
                await App.Database._PHInsert_Pess(Pessoa);
                Pessoa = new Pessoa {
                    Nome =              "Manuel Santigao Ortega",
                    DataNascimento =    new DateTime(1984, 04, 03),
                    NIF =               new Random().Next(100000000, 299999999),
                    Genero =            "Masculino",
                    EstadoCivil =       "Solteiro",
                    Nacionalidade =     "México",
                    Naturalidade =      "Guadalajara",
                    Contacto1 =         new Random().Next(910000000, 939999999),
                    Contacto2 =         new Random().Next(211000000, 299999999),
                    Email =             "manorte1984@hotmail.com"
                };
                await App.Database._PHInsert_Pess(Pessoa);
                Pessoa = new Pessoa {
                    Nome =              "José Pinheiro Douglas",
                    DataNascimento =    new DateTime(1985, 04, 25),
                    NIF =               new Random().Next(100000000, 299999999),
                    Genero =            "Masculino",
                    EstadoCivil =       "Solteiro",
                    Nacionalidade =     "Portugal",
                    Naturalidade =      "Londres",
                    Contacto1 =         new Random().Next(910000000, 939999999),
                    Contacto2 =         new Random().Next(211000000, 299999999),
                    Email =             "josepinh@hotmail.com"
                };
                await App.Database._PHInsert_Pess(Pessoa);
                Pessoa = new Pessoa {
                    Nome =              "Teresa Maria Lopes",
                    DataNascimento =    new DateTime(1953, 09, 11),
                    NIF =               new Random().Next(100000000, 299999999),
                    Genero =            "Feminino",
                    EstadoCivil =       "Casado",
                    Nacionalidade =     "Angola",
                    Naturalidade =      "Luanda",
                    Contacto1 =         new Random().Next(910000000, 939999999),
                    Contacto2 =         new Random().Next(211000000, 299999999),
                    Email =             "temalo@gmail.com"
                };
                await App.Database._PHInsert_Pess(Pessoa);
                Pessoa = new Pessoa {
                    Nome =              "Joana Filipa Albuquerque Brito",
                    DataNascimento =    new DateTime(1991, 08, 04),
                    NIF =               new Random().Next(100000000, 299999999),
                    Genero =            "Feminino",
                    EstadoCivil =       "Casado",
                    Nacionalidade =     "Portugal",
                    Naturalidade =      "Aveiro",
                    Contacto1 =         new Random().Next(910000000, 939999999),
                    Contacto2 =         new Random().Next(211000000, 299999999),
                    Email =             "jofialb@gmail.com"
                };
                await App.Database._PHInsert_Pess(Pessoa);
                Pessoa = new Pessoa {
                    Nome =              "Norberto Leonel Santos",
                    DataNascimento =    new DateTime(1966, 07, 23),
                    NIF =               new Random().Next(100000000, 299999999),
                    Genero =            "Masculino",
                    EstadoCivil =       "Divorciado",
                    Nacionalidade =     "Brasil",
                    Naturalidade =      "Minas Gerais",
                    Contacto1 =         new Random().Next(910000000, 939999999),
                    Contacto2 =         new Random().Next(211000000, 299999999),
                    Email =             "noberto_santos@yahoo.com"
                };
                await App.Database._PHInsert_Pess(Pessoa);
                Pessoa = new Pessoa {
                    Nome =              "Filipe Faria Maria",
                    DataNascimento =    new DateTime(1948, 02, 28),
                    NIF =               new Random().Next(100000000, 299999999),
                    Genero =            "Masculino",
                    EstadoCivil =       "Viúvo",
                    Nacionalidade =     "Portugal",
                    Naturalidade =      "Braga",
                    Contacto1 =         new Random().Next(910000000, 939999999),
                    Contacto2 =         new Random().Next(211000000, 299999999),
                    Email =             "fifama@gmail.com"
                };
                await App.Database._PHInsert_Pess(Pessoa);


                // **********************************************************************************
                // **********************************************************************************
                Autuante = new Autuante {
                    EntidadeAutuante =  "PE",
                    IsPresenciado =     false,
                    Equipamento =       "Alcoolímetro"
                };
                await App.Database._PHInsert_Autuante(Autuante);
                Autuante = new Autuante {
                    EntidadeAutuante =  "AN",
                    IsPresenciado =     true,
                    Equipamento =       "Multímetro"
                };
                await App.Database._PHInsert_Autuante(Autuante);
                Autuante = new Autuante {
                    EntidadeAutuante =  "AN",
                    IsPresenciado =     false,
                    Equipamento =       "Não Definido"
                };
                await App.Database._PHInsert_Autuante(Autuante);
                Autuante = new Autuante {
                    EntidadeAutuante =  "DGTR",
                    IsPresenciado =     true,
                    Equipamento =       "Voltímetro"
                };
                await App.Database._PHInsert_Autuante(Autuante);
                Autuante = new Autuante {
                    EntidadeAutuante =  "PE",
                    IsPresenciado =     true,
                    Equipamento =       "Alcoolímetro"
                };
                await App.Database._PHInsert_Autuante(Autuante);
                Autuante = new Autuante {
                    EntidadeAutuante =  "AN",
                    IsPresenciado =     true,
                    Equipamento =       "Multímetro"
                };
                await App.Database._PHInsert_Autuante(Autuante);


                // **********************************************************************************
                // **********************************************************************************
                Lei = new Lei {
                    Titulo =            "Legislação Geral, Artigo 1º",
                    Pontos =            2,
                    Descricao =         "Placeholder Text 1",
                    //Descricao =         "As you do not know the path of the wind, or how the body is formed in a mother’s womb, so you cannot understand the work of God, Maker of all things. Now you shall have 15 days to pay thy 75€ fee.",
                    Min =               30,
                    Max =               90,
                    Prazo =             15
                };
                await App.Database._PHInsert_Lei(Lei);
                Lei = new Lei {
                    Titulo =            "Legislação Geral, Artigo 2º",
                    Pontos =            4,
                    Descricao =         "Placeholder Text 2",
                    //Descricao =         "This is my commandment, that you love one another as I have loved you. Greater love has no one than this, that someone lay down his life for his friends. You are my friends if you do what I command you.",
                    Min =               90,
                    Max =               220,
                    Prazo =             25
                };
                await App.Database._PHInsert_Lei(Lei);
                Lei = new Lei {
                    Titulo =            "Legislação Geral, Artigo 3º",
                    Pontos =            6,
                    Descricao =         "Placeholder Text 3",
                    //Descricao =         "All that the Father gives me will come to me, and whoever comes to me I will never cast out. You shall now cast out those 120€ from thy wallet.",
                    Min =               120,
                    Max =               380,
                    Prazo =             30
                };
                await App.Database._PHInsert_Lei(Lei);
                Lei = new Lei {
                    Titulo =            "Legislação Geral, Artigo 4º",
                    Pontos =            6,
                    Descricao =         "Placeholder Text 4",
                    //Descricao =         "For sin will have no dominion over you, since you are not under law but under grace. Just kidding.. thy license is still losing 6 points, don't forget the 340€ as well.",
                    Min =               220,
                    Max =               500,
                    Prazo =             40
                };
                await App.Database._PHInsert_Lei(Lei);
                Lei = new Lei {
                    Titulo =            "Legislação Geral, Artigo 5º",
                    Pontos =            12,
                    Descricao =         "Placeholder Text 5",
                    //Descricao =         "Yes Richard, 12 points off... Should have thinked twice before using the elderly golf camp as a mini rally circuit for your monster truck!",
                    Min =               220,
                    Max =               500,
                    Prazo =             40
                };
                await App.Database._PHInsert_Lei(Lei);
                Lei = new Lei {
                    Titulo =            "Legislação Geral, Artigo 6º",
                    Pontos =            4,
                    Descricao =         "Placeholder Text 6",
                    //Descricao =         "For I know the plans I have for you, plans to prosper you and not to harm you, plans to give you hope and a future. Plans to get your 100€ on the next 20 days.",
                    Min =               70,
                    Max =               200,
                    Prazo =             20
                };
                await App.Database._PHInsert_Lei(Lei);
                Lei = new Lei {
                    Titulo =            "Legislação Geral, Artigo 7º",
                    Pontos =            2,
                    Descricao =         "Placeholder Text 7",
                    //Descricao =         "Taste and see that the LORD is good; blessed is the one who takes refuge in him. But even more blessed is the one willing to pay 20€ so the officer can pretend you never met eachother.",
                    Min =               40,
                    Max =               100,
                    Prazo =             8
                };
                await App.Database._PHInsert_Lei(Lei);
                Lei = new Lei {
                    Titulo =            "Legislação Geral, Artigo 8º",
                    Pontos =            6,
                    Descricao =         "Placeholder Text 8",
                    //Descricao =         "What, then, shall we say in response to these things? If God is for us, who can be against us? Certainly not the court. Nope, they can't be against us. Nope.",
                    Min =               250,
                    Max =               560,
                    Prazo =             45
                };
                await App.Database._PHInsert_Lei(Lei);
                Lei = new Lei {
                    Titulo =            "Legislação Geral, Artigo 9º",
                    Pontos =            12,
                    Descricao =         "Placeholder Text 9",
                    //Descricao =         "For now we see only a reflection as in a mirror; then we shall see face to face. Now i know in part; then a shall know fully. But you pay me now 20€ and i - miraculously - will not know at all you were ever here.",
                    Min =               345,
                    Max =               700,
                    Prazo =             60
                };
                await App.Database._PHInsert_Lei(Lei);
                Lei = new Lei {
                    Titulo =            "Legislação Geral, Artigo 10º",
                    Pontos =            1,
                    Descricao =         "Placeholder Text 10",
                    //Descricao =         "Too bad, I ran out of quotes. Sorry.",
                    Min =               0,
                    Max =               15,
                    Prazo =             1
                };
                await App.Database._PHInsert_Lei(Lei);


                // **********************************************************************************
                // **********************************************************************************
                Apreensao = new Apreensao {
                    Objeto =            "Objeto 1",
                    DataApreensao =     new DateTime(2018, 07, 05),
                    HoraApreensao =     DateTime.Now.TimeOfDay,
                    LeiApreensao =      1,
                    TipoApreensao =     "Temporária",
                    Coima =             30,
                    Motivo =            "Sem motivo aparente."
                };
                await App.Database._PHInsert_Apreensao(Apreensao);
                Apreensao = new Apreensao {
                    Objeto =            "Objeto 2",
                    DataApreensao =     new DateTime(2018, 07, 06),
                    HoraApreensao =     DateTime.Now.TimeOfDay,
                    LeiApreensao =      3,
                    TipoApreensao =     "Temporária",
                    Coima =             50,
                    Motivo =            "Sem motivo aparente."
                };
                await App.Database._PHInsert_Apreensao(Apreensao);
                Apreensao = new Apreensao {
                    Objeto =            "Objeto 3",
                    DataApreensao =     new DateTime(2018, 07, 07),
                    HoraApreensao =     DateTime.Now.TimeOfDay,
                    LeiApreensao =      5,
                    TipoApreensao =     "Definitiva",
                    Coima =             20,
                    Motivo =            "Sem motivo aparente."
                };
                await App.Database._PHInsert_Apreensao(Apreensao);
                Apreensao = new Apreensao {
                    Objeto =            "Objeto 4",
                    DataApreensao =     new DateTime(2018, 07, 08),
                    HoraApreensao =     DateTime.Now.TimeOfDay,
                    LeiApreensao =      8,
                    TipoApreensao =     "Não Definido",
                    Coima =             35,
                    Motivo =            "Sem motivo aparente."
                };
                await App.Database._PHInsert_Apreensao(Apreensao);
                Apreensao = new Apreensao {
                    Objeto =            "Objeto 5",
                    DataApreensao =     new DateTime(2018, 07, 09),
                    HoraApreensao =     DateTime.Now.TimeOfDay,
                    LeiApreensao =      6,
                    TipoApreensao =     "Temporária",
                    Coima =             40,
                    Motivo =            "Sem motivo aparente."
                };
                await App.Database._PHInsert_Apreensao(Apreensao);
                Apreensao =             new Apreensao {
                    Objeto =            "Objeto 1",
                    DataApreensao =     new DateTime(2018, 08, 03),
                    HoraApreensao =     DateTime.Now.TimeOfDay,
                    LeiApreensao =      1,
                    TipoApreensao =     "Temporária",
                    Coima =             30,
                    Motivo =            "Sem motivo aparente."
                };
                await App.Database._PHInsert_Apreensao(Apreensao);


                // **********************************************************************************
                // **********************************************************************************
                Pagamento = new Pagamento {
                    NIF =               new Random().Next(100000000, 299999999),
                    DUC =               new Random().Next(100000000, 299999999),
                    DataLimite =        new DateTime(2018, 09, 14),
                    HoraPagamento =     DateTime.Now.TimeOfDay,
                    TipoPagamento =     "Monetário",
                    Valor =             new Random().Next(30, 520),
                    DataInicial =       new DateTime(2018, 07, 14),
                    DataFinal =         new DateTime(2018, 09, 14),
                    DataDevolucao =     new DateTime(2018, 07, 27)
                };
                await App.Database._PHInsert_Pagamento(Pagamento);
                Pagamento = new Pagamento {
                    NIF =               new Random().Next(100000000, 299999999),
                    DUC =               new Random().Next(100000000, 299999999),
                    DataLimite =        new DateTime(2018, 08, 04),
                    HoraPagamento =     DateTime.Now.TimeOfDay,
                    TipoPagamento =     "Transferência Bancária",
                    Valor =             new Random().Next(30, 520),
                    DataInicial =       new DateTime(2018, 07, 04),
                    DataFinal =         new DateTime(2018, 08, 04),
                    DataDevolucao =     new DateTime(2018, 07, 06)
                };
                await App.Database._PHInsert_Pagamento(Pagamento);
                Pagamento = new Pagamento {
                    NIF =               new Random().Next(100000000, 299999999),
                    DUC =               new Random().Next(100000000, 299999999),
                    DataLimite =        new DateTime(2018, 10, 20),
                    HoraPagamento =     DateTime.Now.TimeOfDay,
                    TipoPagamento =     "Prestações Mensais",
                    Valor =             new Random().Next(30, 520),
                    DataInicial =       new DateTime(2018, 08, 05),
                    DataFinal =         new DateTime(2018, 08, 20),
                    DataDevolucao =     new DateTime(2018, 08, 05)
                };
                await App.Database._PHInsert_Pagamento(Pagamento);
                Pagamento = new Pagamento {
                    NIF =               new Random().Next(100000000, 299999999),
                    DUC =               new Random().Next(100000000, 299999999),
                    DataLimite =        new DateTime(2018, 10, 08),
                    HoraPagamento =     DateTime.Now.TimeOfDay,
                    TipoPagamento =     "Monetário",
                    Valor =             new Random().Next(30, 520),
                    DataInicial =       new DateTime(2018, 07, 08),
                    DataFinal =         new DateTime(2018, 10, 08),
                    DataDevolucao =     new DateTime(2018, 08, 02)
                };
                await App.Database._PHInsert_Pagamento(Pagamento);
                Pagamento = new Pagamento {
                    NIF =               new Random().Next(100000000, 299999999),
                    DUC =               new Random().Next(100000000, 299999999),
                    DataLimite =        new DateTime(2018, 07, 30),
                    HoraPagamento =     DateTime.Now.TimeOfDay,
                    TipoPagamento =     "Referência Multibanco",
                    Valor =             new Random().Next(30, 520),
                    DataInicial =       new DateTime(2018, 07, 15),
                    DataFinal =         new DateTime(2018, 07, 30),
                    DataDevolucao =     new DateTime(2018, 07, 27)
                };
                await App.Database._PHInsert_Pagamento(Pagamento);
                Pagamento = new Pagamento {
                    NIF =               new Random().Next(100000000, 299999999),
                    DUC =               new Random().Next(100000000, 299999999),
                    DataLimite =        new DateTime(2018, 10, 20),
                    HoraPagamento =     DateTime.Now.TimeOfDay,
                    TipoPagamento =     "Prestações Mensais",
                    Valor =             new Random().Next(30, 520),
                    DataInicial =       new DateTime(2018, 08, 05),
                    DataFinal =         new DateTime(2018, 08, 20),
                    DataDevolucao =     new DateTime(2018, 08, 05)
                };
                await App.Database._PHInsert_Pagamento(Pagamento);


                await App.Database._PHUpdate_Geral(1, 1, 1, 7, 3, 1, 1, 1, 1);
                await App.Database._PHUpdate_Geral(2, 2, 2, 8, 4, 2, 2, 2, 2);
                await App.Database._PHUpdate_Geral(3, 3, 3, 9, 5, 3, 3, 3, 3);
                await App.Database._PHUpdate_Geral(4, 4, 4, 1, 6, 4, 4, 4, 4);
                await App.Database._PHUpdate_Geral(5, 5, 5, 2, 7, 5, 5, 5, 5);
                await App.Database._PHUpdate_Geral(6, 6, 9, 6, 8, 6, 6, 6, 6);

                Debug.WriteLine("\nTodos os 'placeholders' criados. Alegadamente.\n");
            }
        }
#endregion
    }
}