using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Transform;

using Petra.DAO.Util;
using Dardani.EDU.Entities.Model;
using System.Reflection;
using Dardani.EDU.BO.NH;
using System.IO;
using Petra.Util.Funcoes;
//using Dardani.GER.BO.NH;
using System.Data.OleDb;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using Dardani.GER.BO.NH;
using Dardani.EDU.Entities.VO;

//using Dardani.EDU.BO;


namespace EduTestes
{
    class Program
    {
        static void Main(string[] args)
        {
            LoadNHibernateCfg(true);
            /*
            using (ISession session = NHibernateBase.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    int escolaId = 7;
                    int anoLetivoAno = 2013;
                    IEnumerable<TurmaVO> lista = new TurmaDAO().GetListagemByEscolaAno(escolaId, anoLetivoAno);
                    
                    //TurmaDAO tdao = new TurmaDAO();

                    //IEnumerable<Turma> lista = tdao.GetByEscolaAnoAluno(1,1,76);

                    transaction.Commit();
                }
            }
            */

            PopularTabelasBasicas();
            //PopularOutrasTabelas();
            //CarregarArquivo();

            Console.WriteLine("oua......");
            Console.ReadLine();
        }

        private static void LoadNHibernateCfg(bool recriarBanco)
        {

            System.Reflection.Assembly[] assemblies = new System.Reflection.Assembly[1];

            assemblies[0] = typeof(Turma).Assembly;
            //assemblies[1] = typeof(Sexo).Assembly;

            //var cfg = NHibernateBase.ConfigureNHibernate(assemblies);
            var cfg = NHibernateBase.ConfigureNHibernate(typeof(Turma).Assembly);


            /*

            var cfg = new NHibernate.Cfg.Configuration();
            cfg.Configure(); // read config default style
            Fluently.Configure(cfg)
                .Mappings(
                  m => m.FluentMappings.AddFromAssemblyOf<Turma>())
                .BuildSessionFactory();
            */

            if (recriarBanco)
            {
                //new SchemaExport(cfg).Execute(true, true, false);
                new SchemaUpdate(cfg).Execute(false, true);
            }

            /*
            Fluently.Configure()
                      .Database(
                      MsSqlConfiguration.MsSql2008.ConnectionString(
                      c => c.FromConnectionStringWithKey("ConexaoEDU")))
                      .Mappings(x => x.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                      .ExposeConfiguration(ConstroiSchema)
                      .BuildSessionFactory();
            */



            /*
            System.Reflection.Assembly[] assemblies = new System.Reflection.Assembly[2];

            assemblies[0] = typeof(Turma).Assembly;
            assemblies[1] = typeof(Sexo).Assembly;

            var cfg = NHibernateBase.ConfigureNHibernate(assemblies);
            if (recriarBanco)
            {
                //new SchemaExport(cfg).Execute(true, true, false);
                new SchemaUpdate(cfg).Execute(false, true);
            }
             */ 
        }

        private static void ConstroiSchema(Configuration config)
        {
            // delete the existing db on each run

            //if (File.Exists(DbFile))
            //    File.Delete(DbFile);

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            ///////////new SchemaExport(config).Create(true, true);

            new SchemaUpdate(config).Execute(false, true);
        }


        private static void CarregarArquivo() {

            AnoLetivo anoLetivo = new AnoLetivo() { Ano = 2013, FlagStatus = "S" };
            using (ISession session = NHibernateBase.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    AnoLetivoDAO aldao = new AnoLetivoDAO();
                    aldao.Add( anoLetivo );

                    transaction.Commit();
                }
            }



            int i = 0;
            foreach (string line in File.ReadAllLines(@"E:\projetos_dardani\repositorio_classes\patro\patro.txt"))
            {
                //string[] parts = line.Split(',');
                string[] resultsArray = line.Split('|');
                if (resultsArray[0] == "00")
                {
                    Carregar00(line, anoLetivo);
                } else
                if (resultsArray[0] == "10")
                {
                    Carregar10(line);
                } else 
                if (resultsArray[0] == "20")
                {
                    Carregar20(line);
                } else  
                if (resultsArray[0] == "30") // Profissionais
                {
                    Carregar30(line);
                } else 
                if (resultsArray[0] == "40") // Endereço e Documentação de Profissionais
                {
                    Carregar40(line);
                }/* else
                if (resultsArray[0] == "51") // Dados de Docência
                {
                    Carregar51(line);
                }
                */
                if (resultsArray[0] == "60")
                {
                    Carregar60(line);
                } else
                if (resultsArray[0] == "70")
                {
                    Carregar70(line);
                } else
                if (resultsArray[0] == "80")
                {
                    Carregar80(line, anoLetivo);
                }
                
                /*
                foreach (string campo in resultsArray)
                {
                    Console.WriteLine("{0}:{1}",
                        i,
                        campo);
                }
                 */
                Console.WriteLine("Processando linha {0}", i);
                i++; // For demonstration.

            }
        }

        // Escolas...
        private static void Carregar00(string line, AnoLetivo anoLetivo) {
            using (ISession session = NHibernateBase.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {

                    string[] campos = line.Split('|');
                    Escola e = new Escola();
                    e.CodigoINEP = campos[1];
                    e.Nome = campos[5];
                    e.NomeGestor = "NÃO IDENTIFICADO";
                    e.FlagAlimentacaoEscolar = "S";
                    e.FlagGestorDiretor = "S";

                    int isf = 0;
                    int.TryParse(campos[2], out isf);
                    SituacaoFuncionamentoDAO sfdao = new SituacaoFuncionamentoDAO();
                    SituacaoFuncionamento sf = sfdao.GetByValorEducacenso(isf);
                    e.SituacaoFuncionamento = sf;

                    int ita = 0;
                    int.TryParse(campos[23], out ita);
                    TipoAdministracaoDAO tadao = new TipoAdministracaoDAO();
                    TipoAdministracao ta = tadao.GetByValorEducacenso(ita);
                    e.TipoAdministracao = ta;

                    int ier = 0;
                    int.TryParse(campos[34], out ier);
                    EstagioRegulamentacaoDAO erdao = new EstagioRegulamentacaoDAO();
                    EstagioRegulamentacao er = erdao.GetByValorEducacenso(ier);
                    e.EstagioRegulamentacao = er;

                    EscolaDAO dao = new EscolaDAO();
                    dao.Add(e);

                    EscolaEnderecoDAO edao = new EscolaEnderecoDAO();
                    EscolaEndereco ee = new EscolaEndereco();
                    ee.Latitude = campos[6];
                    ee.Longitude = campos[7];
                    ee.CEP = campos[8];
                    ee.Logradouro = campos[9];
                    short numero = 0;
                    short.TryParse(campos[10], out numero);

                    ee.Numero = numero;
                    ee.Complemento = campos[11];
                    ee.Bairro = campos[12];


                    int iuf = 0;
                    int.TryParse(campos[13], out iuf);
                    Estado uf = new EstadoDAO().GetByValorEducacenso(iuf);
                    ee.UF = uf;

                    ee.Telefone = campos[17];
                    ee.Escola = e;
                    edao.Add(ee);

                    short _d = 0;
                    short _m = 0;
                    short _a = 0;
                    string[] _dma = campos[3].Split('/');

                    short.TryParse(_dma[0], out _d);
                    short.TryParse(_dma[1], out _m);
                    short.TryParse(_dma[2], out _a);

                    DateTime dini = new DateTime(_a, _m, _d);

                    _dma = campos[4].Split('/');
                    short.TryParse(_dma[0], out _d);
                    short.TryParse(_dma[1], out _m);
                    short.TryParse(_dma[2], out _a);

                    DateTime dfim = new DateTime(_a, _m, _d);

                    //AnoLetivoDAO aldao = new AnoLetivoDAO();
                    //AnoLetivo ano = aldao.GetByAno(2013);

                    CalendarioDAO caldao = new CalendarioDAO();
                    caldao.Add(new Calendario() { Escola = e, Descricao = "CALENDARIO 2013", 
                        AnoLetivo = anoLetivo, DataInicio = dini, DataTermino = dfim, DiasLetivos = 0 });

                    transaction.Commit();
                } // end transaction
            } // end session
        }

        // Infraestrutura Escolas...
        private static void Carregar10(string line)
        {
            using (ISession session = NHibernateBase.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    string[] campos = line.Split('|');

                    EscolaDAO edao = new EscolaDAO();
                    Escola e = edao.GetByCodigoINEP(campos[1]);

                    InfraestruturaItemDAO iedao = new InfraestruturaItemDAO();
                    EscolaInfraestruturaItemDAO eidao = new EscolaInfraestruturaItemDAO();

                    if (e != null)
                    {
                        /*
                        for (int i = 24; i <= 73; i++)
                        {
                            if (campos[i] == "1")
                            {
                                InfraestruturaItem item = iedao.GetByValorEducacenso(i);
                                if (item != null)
                                {
                                    eidao.Add(new EscolaInfraestruturaItem() { Escola = e, InfraestruturaItem = item });
                                }
                            }
                        } // end itens de infraestrutura


                        // Modalidades da Escola
                        EscolaModalidadeDAO emdao = new EscolaModalidadeDAO();
                        ModalidadeDAO mdao = new ModalidadeDAO();
                        for (int x = 94; x <= 96; x++)
                        {
                            if (campos[x] == "1")
                            {
                                int cod = x - 93;
                                Modalidade mod = mdao.GetByValorEducacenso(cod);
                                if (mod != null)
                                {
                                    emdao.Add(new EscolaModalidade() { Escola = e, Modalidade = mod});
                                }

                            }
                        } // end modalidades

                        // Etapas da Escola
                        EscolaEtapaDAO eedao = new EscolaEtapaDAO();
                        EtapaEscolaDAO etpdao = new EtapaEscolaDAO();
                        for (int x = 97; x <= 117; x++)
                        {
                            if (campos[x] == "1")
                            {
                                EtapaEscola etp = etpdao.GetByValorEducacenso(x);
                                if (etp != null)
                                {
                                    eedao.Add(new EscolaEtapa() { Escola = e, EtapaEscola = etp });
                                }

                            }
                        } // end etapas
                        */
                        //Escola Educacional
                        EscolaEducacionalDAO edudao = new EscolaEducacionalDAO();
                        EscolaEducacional edu = new EscolaEducacional();
                        edu.Escola = e;
                        edu.FlagEnsinoFundamentalCiclos = campos[118] == "1" ? "S" : "N";
                        edu.FlagMaterialQuilombola = campos[121] == "1" ? "S" : "N";
                        edu.FlagMaterialIndigena = campos[122] == "1" ? "S" : "N";
                        edu.FlagEducacaoIndigena = campos[123] == "1" ? "S" : "N";
                        edu.FlagEnsinoLinguaIndigena = campos[124] == "1" ? "S" : "N";
                        edu.FlagEnsinoLinguaPortuguesa = campos[125] == "1" ? "S" : "N";
                        edu.FlagBrasilAlfabetizado = campos[127] == "1" ? "S" : "N";
                        edu.FlagFinalSemana = campos[128] == "1" ? "S" : "N";
                        edu.FlagAlternancia = campos[129] == "1" ? "S" : "N";

                        int codigo = 0;
                        int.TryParse(campos[92], out codigo);
                        AEE aee = new AEEDAO().GetByValorEducacenso(codigo);
                        edu.AEE = aee;

                        int.TryParse(campos[93], out codigo);
                        AtividadeComplementar ac = new AtividadeComplementarDAO().GetByValorEducacenso(codigo);
                        edu.AtividadeComplementar = ac;

                        int.TryParse(campos[119], out codigo);
                        LocalizacaoDiferenciada ld = new LocalizacaoDiferenciadaDAO().GetByValorEducacenso(codigo);
                        edu.LocalizacaoDiferenciada = ld;

                        int.TryParse(campos[126], out codigo);
                        LinguaIndigena li = new LinguaIndigenaDAO().GetByValorEducacenso(codigo);
                        edu.LinguaIndigena = li;
                        edudao.Add(edu);


                    } // Escola não nula



                    transaction.Commit();
                } // end transaction
            } // end session
        }

        // Turmas...
        private static void Carregar20(string line)
        {
            using (ISession session = NHibernateBase.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    string[] campos = line.Split('|');

                    EscolaDAO edao = new EscolaDAO();
                    Escola e = edao.GetByCodigoINEP(campos[1]);

                    TurmaDAO tdao = new TurmaDAO();
                    Turma t = new Turma();

                    TurnoDAO turnodao = new TurnoDAO();
                    HorarioDAO hdao = new HorarioDAO();


                    InfraestruturaItemDAO iedao = new InfraestruturaItemDAO();
                    EscolaInfraestruturaItemDAO eidao = new EscolaInfraestruturaItemDAO();

                    if (e != null)
                    {
                        t.Escola = e;

                        //CalendarioDAO caldao = new CalendarioDAO();
                        //caldao.Add(new Calendario() { Escola = e, Ano = 2013, DataInicio = new DateTime(), DataTermino = new DateTime(), DiasLetivos = 0 });
                        
                        t.CodigoINEP = campos[2];
                        t.Nome = campos[4];

                        string horaInicial = campos[5] + ":" + campos[6];
                        string horaFinal = campos[7] + ":" + campos[8];

                        //Turno trn = turnodao.GetByEscolaHorario(horaInicial, horaFinal);

                        Horario h = new HorarioDAO().GetByEscolaHorario(horaInicial, horaFinal);

                        Turno trn = turnodao.GetById(1);
                        if (trn == null){
                            trn = new Turno() { Descricao = "TURNO NÃO IDENTIFICADO"};
                        }
                        t.Turno = trn;

                        if (h == null)
                        {
                            h = new Horario() { Descricao = string.Format("Horário {0} - {1}",horaInicial, horaFinal), HoraInicial = horaInicial, HoraFinal = horaFinal };
                            hdao.Add(h);
                        }
                        t.Horario = h;

                        t.FlagDomingo = "N";
                        t.FlagSegunda = campos[10] == "1" ? "S" : "N";
                        t.FlagTerca = campos[11] == "1" ? "S" : "N";
                        t.FlagQuarta = campos[12] == "1" ? "S" : "N";
                        t.FlagQuinta = campos[13] == "1" ? "S" : "N";
                        t.FlagSexta = campos[14] == "1" ? "S" : "N";
                        t.FlagSabado = campos[15] == "1" ? "S" : "N";

                        int ita = 0;
                        int.TryParse(campos[16], out ita);
                        TipoAtendimentoDAO tadao = new TipoAtendimentoDAO();
                        TipoAtendimento ta = tadao.GetByValorEducacenso(ita);
                        t.TipoAtendimento = ta;


                        t.FlagPrograma = campos[17] == "1" ? "S" : "N";

                        int imod = 0;
                        int ietp = 0;
                        int.TryParse(campos[35], out imod);
                        int.TryParse(campos[36], out ietp);

                        Modalidade m = new ModalidadeDAO().GetByValorEducacenso(imod);
                        if (m != null)
                        {
                            t.Modalidade = m;
                            EtapaDAO etdao = new EtapaDAO();
                            Etapa etapa = etdao.GetByValorEducacenso(m.Id, ietp);
                            if (etapa != null)
                            {
                                t.Etapa = etapa;
                            }
                        }

                        t.Calendario = new CalendarioDAO().GetById(1);
                        //t.Turno = new TurnoDAO().GetById(1);

                        tdao.Add(t);
                    }

                    transaction.Commit();
                } // end transaction
            } // end session
        }

        // Profissionais de Ensino
        private static void Carregar30(string line)
        {
            using (ISession session = NHibernateBase.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    PessoaDAO pdao = new PessoaDAO();

                    string[] campos = line.Split('|');
                    Pessoa p = new Pessoa();
                    p.CodigoINEP = campos[2];
                    p.Nome = campos[4];
                    p.FlagTipoPessoa = "P";

                    p.EmailResponsavel = campos[5];
                    
                    
                    p.NumeroNIS = campos[6];

                    short _d = 0;
                    short _m = 0;
                    short _a = 0;
                    string[] _dma = campos[7].Split('/');
                    short.TryParse(_dma[0], out _d);
                    short.TryParse(_dma[1], out _m);
                    short.TryParse(_dma[2], out _a);
                    DateTime dn = new DateTime(_a, _m, _d);
                    p.DataNascimento = dn;

                    int x = 0;
                    int.TryParse(campos[8], out x);
                    Sexo s = new SexoDAO().GetByValorEducacenso(x);
                    p.Sexo = s;

                    int.TryParse(campos[9], out x);
                    Raca r = new RacaDAO().GetByValorEducacenso(x);
                    p.Raca = r;

                    p.NomeMae = campos[10];

                    int.TryParse(campos[11], out x);
                    TipoNacionalidade tn = new TipoNacionalidadeDAO().GetByValorEducacenso(x);
                    p.TipoNacionalidade = tn;


                    int ipais = 0;
                    int.TryParse(campos[12], out ipais);
                    Pais pais = new PaisDAO().GetByValorEducacenso(ipais);
                    p.PaisOrigem = pais;


                    int iuf = 0;
                    int.TryParse(campos[13], out iuf);
                    Estado uf = new EstadoDAO().GetByValorEducacenso(iuf);
                    p.UFNascimento = uf;

                    //int icdd = 0;
                    //int.TryParse(campos[15], out icdd);
                    Municipio cdd = new MunicipioDAO().GetByValorEducacenso(campos[14]);
                    p.CidadeNascimento = cdd;

                    p.FlagDeficiencia = campos[15] == "1" ? "S" : "N";

                    pdao.Add(p);

                    // Pessoa Especial
                    EspecialItemDAO eidao = new EspecialItemDAO();
                    PessoaEspecialItemDAO pedao = new PessoaEspecialItemDAO();
                    if (p != null)
                    {
                        for (int i = 16; i <= 23; i++)
                        {
                            if (campos[i] == "1")
                            {
                                EspecialItem item = eidao.GetByValorEducacenso(i + 2);
                                if (item != null)
                                {
                                    pedao.Add(new PessoaEspecialItem() { Pessoa = p, EspecialItem = item });
                                }
                            }
                        }
                    }

                    transaction.Commit();
                } // end transaction
            } // end session
        }

        // Profissionais - Endereço e Documentação...
        private static void Carregar40(string line)
        {
            using (ISession session = NHibernateBase.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    string[] campos = line.Split('|');

                    PessoaDAO pdao = new PessoaDAO();
                    Pessoa p = pdao.GetByCodigoINEP(campos[2]);

                    if (p.Documentacao == null)
                    {

                        PessoaDocumentacao pd = new PessoaDocumentacao();

                        pd.Pessoa = p;

                        pd.CPFNumero = campos[4];

                        new PessoaDocumentacaoDAO().Add(pd);

                    }

                    if (p.Endereco == null)
                    {

                        PessoaEndereco pe = new PessoaEndereco();
                        pe.Pessoa = p;

                        short x = 0;
                        short.TryParse(campos[5], out x);
                        Zona z = new ZonaDAO().GetByValorEducacenso(x);
                        pe.Zona = z;

                        pe.CEP = campos[6];
                        pe.Logradouro = campos[7];
                        short.TryParse(campos[8], out x);
                        pe.Numero = x;
                        pe.Complemento = campos[9];
                        pe.Bairro = campos[10];

                        int iuf = 0;
                        int.TryParse(campos[11], out iuf);
                        Estado uf = new EstadoDAO().GetByValorEducacenso(iuf);
                        pe.UF = uf;

                        Municipio cdd = new MunicipioDAO().GetByValorEducacenso(campos[12]);
                        pe.Cidade = cdd;

                        new PessoaEnderecoDAO().Add(pe);
                    }

                    transaction.Commit();
                } // end transaction
            } // end session
        }

        /*
        // Profissionais - Dados de Docência...
        private static void Carregar51(string line)
        {
            using (ISession session = NHibernateBase.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    string[] campos = line.Split('|');

                    PessoaDAO pdao = new PessoaDAO();
                    Pessoa p = pdao.GetByCodigoINEP(campos[2]);
                    TurmaDAO tdao = new TurmaDAO();
                    Turma t = tdao.GetByCodigoINEP(campos[4]);

                    if ((p != null) && (t != null))
                    {

                        // Função que exerce na turma
                        //campos[6]
                        // Regime de Contratação
                        //campos[7]

                        DisciplinaDAO ddao = new DisciplinaDAO();
                        ProfissionalDisciplinaDAO pddao = new ProfissionalDisciplinaDAO();
                        int x = 1;
                        

                        for (int i = 8; i <= 21; i++)
                        {
                            if (campos[i] != "")
                            {
                                int.TryParse(campos[i], out x);
                                Disciplina dis = ddao.GetByValorEducacenso(x);
                                if (dis != null)
                                {
                                    pddao.Add(new ProfissionalDisciplina() { Pessoa = p, Turma = t, Disciplina = dis });
                                }
                            }
                            i++;
                        }


                        // Adicionar Vínculo a Turma
                        //new PessoaDocumentacaoDAO().Add(pd);

                    }


                    transaction.Commit();
                } // end transaction
            } // end session
        }
        */


        // Alunos...
        private static void Carregar60(string line)
        {
            using (ISession session = NHibernateBase.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    PessoaDAO pdao = new PessoaDAO();

                    string[] campos = line.Split('|');
                    Pessoa p = new Pessoa();
                    p.CodigoINEP = campos[2];
                    p.Nome = campos[4];
                    p.FlagTipoPessoa = "A";
                    p.NumeroNIS = campos[5];

                    short _d = 0;
                    short _m = 0;
                    short _a = 0;
                    string[] _dma = campos[6].Split('/');
                    short.TryParse(_dma[0], out _d);
                    short.TryParse(_dma[1], out _m);
                    short.TryParse(_dma[2], out _a);
                    DateTime dn = new DateTime(_a, _m, _d);
                    p.DataNascimento = dn;

                    int x = 0;
                    int.TryParse(campos[7], out x);
                    Sexo s = new SexoDAO().GetByValorEducacenso(x);
                    p.Sexo = s;

                    int.TryParse(campos[8], out x);
                    Raca r = new RacaDAO().GetByValorEducacenso(x);
                    p.Raca = r;

                    p.NomeMae = campos[10];
                    p.NomePai = campos[11];


                    int.TryParse(campos[12], out x);
                    TipoNacionalidade tn = new TipoNacionalidadeDAO().GetByValorEducacenso(x);
                    p.TipoNacionalidade = tn;

                    
                    int ipais = 0;
                    int.TryParse(campos[13], out ipais);
                    Pais pais = new PaisDAO().GetByValorEducacenso(ipais);
                    p.PaisOrigem = pais;


                    int iuf = 0;
                    int.TryParse(campos[14], out iuf);
                    Estado uf = new EstadoDAO().GetByValorEducacenso(iuf);
                    p.UFNascimento = uf;

                    //int icdd = 0;
                    //int.TryParse(campos[15], out icdd);
                    Municipio cdd = new MunicipioDAO().GetByValorEducacenso(campos[15]);
                    p.CidadeNascimento = cdd;

                    p.FlagDeficiencia = campos[16] == "1" ? "S" : "N";

                    pdao.Add(p);

                    // Pessoa Especial
                    EspecialItemDAO eidao = new EspecialItemDAO();
                    PessoaEspecialItemDAO pedao = new PessoaEspecialItemDAO();
                    if (p != null)
                    {
                        for (int i = 17; i <= 29; i++)
                        {
                            if (campos[i] == "1")
                            {
                                EspecialItem item = eidao.GetByValorEducacenso(i+1);
                                if (item != null)
                                {
                                    pedao.Add(new PessoaEspecialItem() { Pessoa = p, EspecialItem = item });
                                }
                            }
                        }
                    }


                    // RecursoINEP
                    RecursoINEPDAO ridao = new RecursoINEPDAO();
                    PessoaRecursoINEPDAO prdao = new PessoaRecursoINEPDAO();
                    if (p != null)
                    {
                        for (int i = 30; i <= 38; i++)
                        {
                            if (campos[i] == "1")
                            {
                                RecursoINEP item = ridao.GetByValorEducacenso(i + 1);
                                if (item != null)
                                {
                                    prdao.Add(new PessoaRecursoINEP() { Pessoa = p, RecursoINEP = item });
                                }
                            }
                        }
                    }

                    transaction.Commit();
                } // end transaction
            } // end session
        }

        // Alunos - Endereço e Documentação...
        private static void Carregar70(string line)
        {
            using (ISession session = NHibernateBase.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    string[] campos = line.Split('|');

                    PessoaDAO pdao = new PessoaDAO();
                    Pessoa p = pdao.GetByCodigoINEP(campos[2]);

                    if (p.Documentacao == null)
                    {

                        PessoaDocumentacao pd = new PessoaDocumentacao();

                        pd.Pessoa = p;
                        pd.RGNumero = campos[4];
                        pd.RGComplemento = campos[5];
                        //pd.RGOrgao = campos[6]

                        int iuf = 0;
                        int.TryParse(campos[7], out iuf);
                        Estado uf = new EstadoDAO().GetByValorEducacenso(iuf);
                        pd.RGUF = uf;


                        short _d = 0;
                        short _m = 0;
                        short _a = 0;
                        DateTime dta;
                        if (campos[8] != "")
                        {
                            string[] _dma = campos[8].Split('/');
                            short.TryParse(_dma[0], out _d);
                            short.TryParse(_dma[1], out _m);
                            short.TryParse(_dma[2], out _a);
                            dta = new DateTime(_a, _m, _d);
                            pd.RGDataEmissao = dta;
                        }

                        //pd.ModeloCertidao  ModeloNovo / Antigo
                        //pd.TipoCertidao

                        pd.CertidaoTermo = campos[11];
                        pd.CertidaoFolha = campos[12];
                        pd.CertidaoLivro = campos[13];

                        if (campos[14] != "")
                        {
                            string[] _dma = campos[14].Split('/');
                            short.TryParse(_dma[0], out _d);
                            short.TryParse(_dma[1], out _m);
                            short.TryParse(_dma[2], out _a);
                            dta = new DateTime(_a, _m, _d);
                            pd.CertidaoDataEmissao = dta;
                        }

                        int.TryParse(campos[15], out iuf);
                        uf = new EstadoDAO().GetByValorEducacenso(iuf);
                        pd.CertidaoUF = uf;

                        Municipio cdd = new MunicipioDAO().GetByValorEducacenso(campos[16]);
                        pd.CertidaoCidade = cdd;

                        //pd.CertidaoCidade = "Patrocínio";
                        pd.CertidaoCartorio = campos[17];
                        pd.CertidaoNumeroNovo = campos[18];
                        pd.CPFNumero = campos[19];
                        pd.DocumentoEstrangeiroNumero = campos[20];

                        new PessoaDocumentacaoDAO().Add(pd);

                    }

                    if (p.Endereco == null)
                    {

                        PessoaEndereco pe = new PessoaEndereco();
                        pe.Pessoa = p;

                        short x = 0;
                        short.TryParse(campos[23], out x);
                        Zona z = new ZonaDAO().GetByValorEducacenso(x);
                        pe.Zona = z;

                        pe.CEP = campos[24];
                        pe.Logradouro = campos[25];
                        short.TryParse(campos[26], out x);
                        pe.Numero = x;
                        pe.Complemento = campos[27];
                        pe.Bairro = campos[28];

                        int iuf = 0;
                        int.TryParse(campos[29], out iuf);
                        Estado uf = new EstadoDAO().GetByValorEducacenso(iuf);
                        pe.UF = uf;

                        Municipio cdd = new MunicipioDAO().GetByValorEducacenso(campos[30]);
                        pe.Cidade = cdd;

                        new PessoaEnderecoDAO().Add(pe);
                    }

                    transaction.Commit();
                } // end transaction
            } // end session
        }


        // Matrículas...
        private static void Carregar80(string line, AnoLetivo anoLetivo)
        {
            using (ISession session = NHibernateBase.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    string[] campos = line.Split('|');

                    PessoaDAO pdao = new PessoaDAO();
                    Pessoa p = pdao.GetByCodigoINEP(campos[2]);

                    TurmaDAO tdao = new TurmaDAO();
                    Turma t = tdao.GetByCodigoINEP(campos[4]);

                    int x = 0;

                    string stu = campos[7] == "" ? "0" : campos[7];
                    int.TryParse(stu, out x);
                    TurmaUnificada tu = new TurmaUnificadaDAO().GetByValorEducacenso(x);

                    int.TryParse(campos[9], out x);
                    EscolarizacaoEspecial ee = new EscolarizacaoEspecialDAO().GetByValorEducacenso(x);

                    string stp = campos[11] == "" ? "0" : campos[11];
                    int.TryParse(stp, out x);
                    TransportePublico tp = new TransportePublicoDAO().GetByValorEducacenso(x);

                    Matricula m = new Matricula() { 
                        Pessoa = p, Turma = t, TransportePublico = tp, TurmaUnificada = tu, EscolarizacaoEspecial = ee,
                        AnoLetivo = anoLetivo, FlagRematricular="S"};

                    new MatriculaDAO().Add(m);

                    // Matrícula Veículo Tipo
                    VeiculoTipoDAO vtdao = new VeiculoTipoDAO();
                    MatriculaVeiculoTipoDAO mvdao = new MatriculaVeiculoTipoDAO();
                    if (m != null)
                    {
                        for (int i = 12; i <= 22; i++)
                        {
                            if (campos[i] == "1")
                            {
                                VeiculoTipo vt = vtdao.GetByValorEducacenso(i + 1);
                                if (vt != null)
                                {
                                    mvdao.Add(new MatriculaVeiculoTipo() { Matricula = m, VeiculoTipo = vt });
                                }
                            }
                        }
                    }


                    transaction.Commit();
                } // end transaction
            } // end session
        }

        private static void PopularOutrasTabelas()
        {
            string con = "";
            //int intcod = 0;
            short shortcod = 0;

            
            // Estados
            con = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\projetos_dardani\repositorio_classes\patro\TBUF.XLS;Extended Properties='Excel 8.0;HDR=Yes;'";
            using(OleDbConnection connection = new OleDbConnection(con))
            {
                connection.Open();
                
                //OleDbCommand cmd = new OleDbCommand("CREATE TABLE [TB02] ([Column1] string, [Column2] string)", connection);
                //cmd.ExecuteNonQuery();
                
                
                OleDbCommand command = new OleDbCommand("select * from [PLAN1$]", connection); 
                using(OleDbDataReader dr = command.ExecuteReader())
                {
                    using (ISession session = NHibernateBase.OpenSession())
                    {
                        using (ITransaction transaction = session.BeginTransaction())
                        {

                            EstadoDAO edao = new EstadoDAO();

                            while (dr.Read())
                            {
                                string codigo = dr[0].ToString();
                                string sigla = dr[1].ToString();
                                string nome = dr[2].ToString();

                                short.TryParse(codigo, out shortcod);
                                if (shortcod != 0)
                                {
                                    edao.Add(new Estado() { Descricao = nome, Sigla = sigla, ValorEducacenso = shortcod, FlagAtivo = "S" });
                                }

                            } // end while
                            transaction.Commit();
                        } // end tx
                    } // end session
                } // end dr 
                 
            }
            

            
            // Municípios
            string conMun = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\projetos_dardani\repositorio_classes\patro\TBMUNICIPIO.XLS;Extended Properties='Excel 8.0;HDR=Yes;'";
            using (OleDbConnection connection = new OleDbConnection(conMun))
            {
                connection.Open();
                
                //OleDbCommand cmd = new OleDbCommand("CREATE TABLE [TB02] ([Column1] string, [Column2] string)", connection);
                //cmd.ExecuteNonQuery();

                OleDbCommand command = new OleDbCommand("select * from [PLAN1$]", connection);
                using (OleDbDataReader dr = command.ExecuteReader())
                {
                    using (ISession session = NHibernateBase.OpenSession())
                    {
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            MunicipioDAO mdao = new MunicipioDAO();
                            EstadoDAO ufdao = new EstadoDAO();
                            while (dr.Read())
                            {
                                string codigo = dr[0].ToString();
                                string codigoUF = dr[1].ToString();
                                string nome = dr[2].ToString();

                                //short.TryParse(codigo, out shortcod);
                                short.TryParse(codigoUF, out shortcod);
                                Estado uf = ufdao.GetByValorEducacenso(shortcod);

                                if (codigo != "")
                                {
                                    mdao.Add(new Municipio() { Descricao = nome, Estado = uf, ValorEducacenso = codigo, FlagAtivo = "S" });
                                }
                            
                            } // end while
                            transaction.Commit();
                        } // end tx
                        
                    } // end session

                }// end dr

            }
            

            // Municípios
            string conDis = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\projetos_dardani\repositorio_classes\patro\TBDISCIPLINA.XLS;Extended Properties='Excel 8.0;HDR=Yes;'";
            using (OleDbConnection connection = new OleDbConnection(conDis))
            {
                connection.Open();
                
                //OleDbCommand cmd = new OleDbCommand("CREATE TABLE [TB02] ([Column1] string, [Column2] string)", connection);
                //cmd.ExecuteNonQuery();
                

                OleDbCommand command = new OleDbCommand("select * from [PLAN1$]", connection);
                using (OleDbDataReader dr = command.ExecuteReader())
                {

                    using (ISession session = NHibernateBase.OpenSession())
                    {
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            DisciplinaEducacensoDAO ddao = new DisciplinaEducacensoDAO();
                            while (dr.Read())
                            {
                                string codigo = dr[0].ToString();
                                string nome = dr[1].ToString();
                                string sigla = dr[2].ToString();

                                short.TryParse(codigo, out shortcod);
                                if (shortcod != 0)
                                {
                                    ddao.Add(new DisciplinaEducacenso() { Descricao = nome, /*DescricaoAbreviada = sigla, */ValorEducacenso = shortcod, FlagAtivo = "S" });
                                }
                            } // end while
                            transaction.Commit();
                        } // end tx
                    } // end session

                } // end dr

            }



            
            using (ISession session = NHibernateBase.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {


                    transaction.Commit();
                } // end transaction
            } // end session
             
        }

        private static void PopularTabelasBasicas()
        {
            using (ISession session = NHibernateBase.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    SexoDAO gersdao = new SexoDAO();
                    Sexo sexoBase = new Sexo() { Descricao = "Masculino", ValorEducacenso = 1 };
                    gersdao.Add(sexoBase);
                    gersdao.Add(new Sexo() { Descricao = "Feminino", ValorEducacenso = 2 });

                    string senhaMd5 = Criptografia.MD5("admin");
                    UsuarioAcesso acesso = new UsuarioAcesso() { NomeUsuario = "admin", SenhaAcesso = senhaMd5, Email = "admin@escola.com.br" };
                    Usuario usuario = new Usuario() { Nome = "Administrador", DataNascimento = new DateTime(2000, 01, 01), NumeroCPF = "12345678901", Nivel = "Administrador", Sexo = sexoBase, Ativo = "S"};

                    acesso.Usuario = usuario;
                    //usuario.Acesso = acesso;

                    UsuarioDAO gerudao = new UsuarioDAO();
                    gerudao.Add(usuario);

                    UsuarioAcessoDAO geruadao = new UsuarioAcessoDAO();
                    geruadao.Add(acesso);




                    RacaDAO gerrdao = new RacaDAO();
                    gerrdao.Add(new Raca() { Descricao = "Não Declarada", ValorEducacenso = 0});
                    gerrdao.Add(new Raca() { Descricao = "Branca", ValorEducacenso = 1});
                    gerrdao.Add(new Raca() { Descricao = "Preta", ValorEducacenso = 2});
                    gerrdao.Add(new Raca() { Descricao = "Parda", ValorEducacenso = 3});
                    gerrdao.Add(new Raca() { Descricao = "Amarela", ValorEducacenso = 4});
                    gerrdao.Add(new Raca() { Descricao = "Indígena", ValorEducacenso = 5});

                    EstadoCivilDAO gerecdao = new EstadoCivilDAO();
                    gerecdao.Add(new EstadoCivil() { Descricao = "Solteiro(a)", ValorEducacenso = 1 });
                    gerecdao.Add(new EstadoCivil() { Descricao = "Casado(a)", ValorEducacenso = 2 });
                    gerecdao.Add(new EstadoCivil() { Descricao = "Viúvo(a)", ValorEducacenso = 3 });
                    gerecdao.Add(new EstadoCivil() { Descricao = "União Estável", ValorEducacenso = 4 });
                    gerecdao.Add(new EstadoCivil() { Descricao = "Separado(a)/Divorciado(a)", ValorEducacenso = 5 });
                    gerecdao.Add(new EstadoCivil() { Descricao = "Ignorado", ValorEducacenso = 6 });

                    TipoAdministracaoDAO tdao = new TipoAdministracaoDAO();
                    tdao.Add(new TipoAdministracao() { Descricao = "Federal", ValorEducacenso = 1, FlagPadrao = "N", FlagAtivo = "S" });
                    tdao.Add(new TipoAdministracao() { Descricao = "Estadual", ValorEducacenso = 2, FlagPadrao = "N", FlagAtivo = "S" });
                    tdao.Add(new TipoAdministracao() { Descricao = "Municipal", ValorEducacenso = 3, FlagPadrao = "S", FlagAtivo = "S" });
                    tdao.Add(new TipoAdministracao() { Descricao = "Privada", ValorEducacenso = 4, FlagPadrao = "N", FlagAtivo = "S" });

                    ZonaDAO zdao = new ZonaDAO();
                    zdao.Add(new Zona() { Descricao = "Urbana", ValorEducacenso = 1, FlagPadrao = "S", FlagAtivo = "S" });
                    zdao.Add(new Zona() { Descricao = "Rural", ValorEducacenso = 2, FlagPadrao = "N", FlagAtivo = "S" });

                    ConvenioPublicoDAO cpdao = new ConvenioPublicoDAO();
                    cpdao.Add(new ConvenioPublico() { Descricao = "Estadual", ValorEducacenso = 1, FlagPadrao = "N", FlagAtivo = "S" });
                    cpdao.Add(new ConvenioPublico() { Descricao = "Municipal", ValorEducacenso = 2, FlagPadrao = "S", FlagAtivo = "S" });
                    cpdao.Add(new ConvenioPublico() { Descricao = "Estadual e Municipal", ValorEducacenso = 3, FlagPadrao = "S", FlagAtivo = "S" });

                    MantenedorPrivadoDAO mpdao = new MantenedorPrivadoDAO();
                    mpdao.Add(new MantenedorPrivado() { Descricao = "Empresa, grupo empresarial do setor privado ou pessoa física", ValorEducacenso = 28, FlagPadrao = "S", FlagAtivo = "S" });
                    mpdao.Add(new MantenedorPrivado() { Descricao = "Sindicatos de trabalhadores ou patronais, associações, cooperativas", ValorEducacenso = 29, FlagPadrao = "N", FlagAtivo = "S" });
                    mpdao.Add(new MantenedorPrivado() { Descricao = "ONG - Organização não governamental – internacional ou  nacional / Oscip", ValorEducacenso = 30, FlagPadrao = "N", FlagAtivo = "S" });
                    mpdao.Add(new MantenedorPrivado() { Descricao = "Instituições sem fins lucrativos", ValorEducacenso = 31, FlagPadrao = "N", FlagAtivo = "S" });
                    mpdao.Add(new MantenedorPrivado() { Descricao = "Sistema S (Sesi, Senai, Sesc, outros)", ValorEducacenso = 32, FlagPadrao = "N", FlagAtivo = "S" });

                    EstagioRegulamentacaoDAO erdao = new EstagioRegulamentacaoDAO();
                    erdao.Add(new EstagioRegulamentacao() { Descricao = "Regulamentada/Autorizada", ValorEducacenso = 1, FlagPadrao = "S", FlagAtivo = "S" });
                    erdao.Add(new EstagioRegulamentacao() { Descricao = "Em tramitação", ValorEducacenso = 2, FlagPadrao = "N", FlagAtivo = "S" });
                    erdao.Add(new EstagioRegulamentacao() { Descricao = "Não Regulamentada/Autorizada", ValorEducacenso = 0, FlagPadrao = "N", FlagAtivo = "S" });

                    LocalEscolaDAO ledao = new LocalEscolaDAO();
                    ledao.Add(new LocalEscola() { Descricao = "Prédio escolar", FlagPadrao = "S", FlagAtivo = "S" });
                    ledao.Add(new LocalEscola() { Descricao = "Templo/Igreja", FlagPadrao = "N", FlagAtivo = "S" });
                    ledao.Add(new LocalEscola() { Descricao = "Salas de empresa", FlagPadrao = "N", FlagAtivo = "S" });
                    ledao.Add(new LocalEscola() { Descricao = "Casa do professor", FlagPadrao = "N", FlagAtivo = "S" });
                    ledao.Add(new LocalEscola() { Descricao = "Salas em outra escola", FlagPadrao = "N", FlagAtivo = "S" });
                    ledao.Add(new LocalEscola() { Descricao = "Galpão/Rancho/Paiol/Barracão", FlagPadrao = "N", FlagAtivo = "S" });
                    ledao.Add(new LocalEscola() { Descricao = "Unidade de internação socioeducativa", FlagPadrao = "N", FlagAtivo = "S" });
                    ledao.Add(new LocalEscola() { Descricao = "Unidade prisional", FlagPadrao = "N", FlagAtivo = "S" });
                    ledao.Add(new LocalEscola() { Descricao = "Outros", FlagPadrao = "N", FlagAtivo = "S" });

                    FormaOcupacaoDAO fodao = new FormaOcupacaoDAO();
                    fodao.Add(new FormaOcupacao() { Descricao = "Cedido", ValorEducacenso = 3, FlagPadrao = "N", FlagAtivo = "S" });
                    fodao.Add(new FormaOcupacao() { Descricao = "Próprio", ValorEducacenso = 1, FlagPadrao = "S", FlagAtivo = "S" });
                    fodao.Add(new FormaOcupacao() { Descricao = "Alugado", ValorEducacenso = 2, FlagPadrao = "N", FlagAtivo = "S" });

                    InfraestruturaCategoriaDAO icdao = new InfraestruturaCategoriaDAO();
                    //InfraestruturaCategoria c1 = new InfraestruturaCategoria() { Descricao = "Água consumida pelos alunos", FlagMuitos = "N", FlagAtivo = "S" };
                    InfraestruturaCategoria c2 = new InfraestruturaCategoria() { Descricao = "Abastecimento de Água", FlagMuitos = "S", FlagAtivo = "S" };
                    InfraestruturaCategoria c3 = new InfraestruturaCategoria() { Descricao = "Abastecimento de energia elétrica", FlagMuitos = "N", FlagAtivo = "S" };
                    InfraestruturaCategoria c4 = new InfraestruturaCategoria() { Descricao = "Esgoto sanitário", FlagMuitos = "N", FlagAtivo = "S" };
                    InfraestruturaCategoria c5 = new InfraestruturaCategoria() { Descricao = "Destinação do lixo", FlagMuitos = "N", FlagAtivo = "S" };
                    InfraestruturaCategoria c6 = new InfraestruturaCategoria() { Descricao = "Dependências existentes na escola", FlagMuitos = "S", FlagAtivo = "S" };

                    //icdao.Add(c1);
                    icdao.Add(c2);
                    icdao.Add(c3);
                    icdao.Add(c4);
                    icdao.Add(c5);
                    icdao.Add(c6);

                    InfraestruturaItemDAO iidao = new InfraestruturaItemDAO();
                    //iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c1, Descricao = "Filtrada", ValorEducacenso = 2, FlagAtivo = "S" });
                    //iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c1, Descricao = "Não Filtrada", ValorEducacenso = 1, FlagAtivo = "S" });

                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c2, Descricao = "Rede pública", ValorEducacenso = 25,  FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c2, Descricao = "Poço artesiano", ValorEducacenso = 26, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c2, Descricao = "Cacimba/Cisterna/Poço", ValorEducacenso = 27, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c2, Descricao = "Fonte/Rio/Igarapé/Riacho/Córrego", ValorEducacenso = 28, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c2, Descricao = "Inexistente", ValorEducacenso = 29, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c3, Descricao = "Rede pública", ValorEducacenso =30, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c3, Descricao = "Gerador", ValorEducacenso =31, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c3, Descricao = "Outros (energia alternativa)", ValorEducacenso =32, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c3, Descricao = "Inexistente", ValorEducacenso =33, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c4, Descricao = "Rede pública", ValorEducacenso = 34, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c4, Descricao = "Fossa", ValorEducacenso = 35, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c4, Descricao = "Inexistente", ValorEducacenso = 36, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c5, Descricao = "Coleta periódica", ValorEducacenso =37, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c5, Descricao = "Queima", ValorEducacenso =38, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c5, Descricao = "Joga em outra área", ValorEducacenso =39, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c5, Descricao = "Recicla", ValorEducacenso =40, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c5, Descricao = "Enterra", ValorEducacenso =41, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c5, Descricao = "Outros", ValorEducacenso =42, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Sala de diretoria", ValorEducacenso =43, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Sala de professores", ValorEducacenso =44, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Sala de secretaria", ValorEducacenso =45, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Laboratório de informática", ValorEducacenso =46, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Laboratório de ciências", ValorEducacenso =47, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Sala de recursos multifuncionais para Atendimento Educacional Especializado(AEE)", ValorEducacenso = 48, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Quadra de esportes coberta", ValorEducacenso =49, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Quadra de esportes descoberta", ValorEducacenso = 50, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Cozinha", ValorEducacenso = 51, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Biblioteca", ValorEducacenso =52, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Sala de leitura", ValorEducacenso = 53, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Parque infantil", ValorEducacenso =54, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Berçário", ValorEducacenso =55, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Banheiro fora do prédio", ValorEducacenso = 56, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Banheiro dentro do prédio", ValorEducacenso = 57, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Banheiro adequado à educação infantil", ValorEducacenso =58, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Banheiro adequado a alunos com deficiência ou mobilidade reduzida", ValorEducacenso = 59, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Dependências e vias adequadas a alunos com deficiência ou mobilidade reduzida", ValorEducacenso = 60, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Banheiro com chuveiro", ValorEducacenso = 61, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Refeitório", ValorEducacenso =62, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Despensa", ValorEducacenso = 63, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Almoxarifado", ValorEducacenso = 64, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Auditório", ValorEducacenso =65, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Pátio coberto", ValorEducacenso =66, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Pátio descoberto", ValorEducacenso =67, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Alojamento de aluno", ValorEducacenso =68, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Alojamento de professor", ValorEducacenso =69, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Área verde", ValorEducacenso = 70, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Lavanderia", ValorEducacenso = 71, FlagAtivo = "S" });
                    iidao.Add(new InfraestruturaItem() { InfraestruturaCategoria = c6, Descricao = "Nenhuma das dependências relacionadas", ValorEducacenso = 72, FlagAtivo = "S" });

                    AEEDAO aeedao = new AEEDAO();
                    aeedao.Add(new AEE() { Descricao = "Não ofrece", ValorEducacenso = 0, FlagPadrao = "S", FlagPossui = "N", FlagAtivo = "S" });
                    aeedao.Add(new AEE() { Descricao = "Não Exclusivamente", ValorEducacenso = 1, FlagPadrao = "N", FlagPossui = "S", FlagAtivo = "S" });
                    aeedao.Add(new AEE() { Descricao = "Exclusivamente", ValorEducacenso = 2, FlagPadrao = "N", FlagPossui = "S", FlagAtivo = "S" });

                    AtividadeComplementarDAO acdao = new AtividadeComplementarDAO();
                    acdao.Add(new AtividadeComplementar() { Descricao = "Não oferece", ValorEducacenso = 0, FlagPadrao = "S", FlagPossui = "N", FlagAtivo = "S" });
                    acdao.Add(new AtividadeComplementar() { Descricao = "Não Exclusivamente", ValorEducacenso = 1, FlagPadrao = "N", FlagPossui = "S", FlagAtivo = "S" });
                    acdao.Add(new AtividadeComplementar() { Descricao = "Exclusivamente", ValorEducacenso = 2, FlagPadrao = "N", FlagPossui = "S", FlagAtivo = "S" });


                    LocalizacaoDiferenciadaDAO lddao = new LocalizacaoDiferenciadaDAO();
                    lddao.Add(new LocalizacaoDiferenciada() { Descricao = "Área de assentamento", ValorEducacenso = 1, FlagPadrao = "N", FlagPossui = "S", FlagAtivo = "S" });
                    lddao.Add(new LocalizacaoDiferenciada() { Descricao = "Terra indígena", ValorEducacenso = 2, FlagPadrao = "N", FlagPossui = "S", FlagAtivo = "S" });
                    lddao.Add(new LocalizacaoDiferenciada() { Descricao = "Área remanescente de quilombos", ValorEducacenso = 3, FlagPadrao = "N", FlagPossui = "S", FlagAtivo = "S" });
                    lddao.Add(new LocalizacaoDiferenciada() { Descricao = "Unidade de uso sustentável", ValorEducacenso = 4, FlagPadrao = "N", FlagPossui = "S", FlagAtivo = "S" });
                    lddao.Add(new LocalizacaoDiferenciada() { Descricao = "Unidade de uso sustentável em terra indígena", ValorEducacenso = 5, FlagPadrao = "N", FlagPossui = "S", FlagAtivo = "S" });
                    lddao.Add(new LocalizacaoDiferenciada() { Descricao = "Unidade de uso sustentável em área remanescente de quilombos", ValorEducacenso = 6, FlagPadrao = "N", FlagPossui = "S", FlagAtivo = "S" });
                    lddao.Add(new LocalizacaoDiferenciada() { Descricao = "Não se aplica", ValorEducacenso = 7, FlagPadrao = "S", FlagPossui = "N", FlagAtivo = "S" });

                    MaterialEspecificoDAO mesdao = new MaterialEspecificoDAO();
                    mesdao.Add(new MaterialEspecifico() { Descricao = "Quilombolas", FlagPadrao = "N", FlagAtivo = "S" });
                    mesdao.Add(new MaterialEspecifico() { Descricao = "Indígenas", FlagPadrao = "N", FlagAtivo = "S" });
                    mesdao.Add(new MaterialEspecifico() { Descricao = "Não Utiliza", FlagPadrao = "S", FlagAtivo = "S" });

                    EquipamentoDAO edao = new EquipamentoDAO();
                    edao.Add(new Equipamento() { Descricao = "Aparelho de televisão", FlagAtivo = "S" });
                    edao.Add(new Equipamento() { Descricao = "Videocassete", FlagAtivo = "S" });
                    edao.Add(new Equipamento() { Descricao = "DVD", FlagAtivo = "S" });
                    edao.Add(new Equipamento() { Descricao = "Copiadora", FlagAtivo = "S" });
                    edao.Add(new Equipamento() { Descricao = "Antena parabólica", FlagAtivo = "S" });
                    edao.Add(new Equipamento() { Descricao = "Retroprojetor", FlagAtivo = "S" });
                    edao.Add(new Equipamento() { Descricao = "Impressora", FlagAtivo = "S" });
                    edao.Add(new Equipamento() { Descricao = "Aparelho de som", FlagAtivo = "S" });
                    edao.Add(new Equipamento() { Descricao = "Projetor multimídia (Datashow)", FlagAtivo = "S" });
                    edao.Add(new Equipamento() { Descricao = "Fax", FlagAtivo = "S" });
                    edao.Add(new Equipamento() { Descricao = "Máquina fotográfica/Filmadora", FlagAtivo = "S" });
                    edao.Add(new Equipamento() { Descricao = "Computador", FlagAtivo = "S" });


                    TipoNacionalidadeDAO tndao = new TipoNacionalidadeDAO();
                    tndao.Add(new TipoNacionalidade() { Descricao = "Brasileira", ValorEducacenso = 1, FlagBrasileira = "S", FlagAtivo = "S", FlagPadrao = "S" });
                    tndao.Add(new TipoNacionalidade() { Descricao = "Brasileira – nascido no exterior ou naturalizado", ValorEducacenso = 2, FlagBrasileira = "S", FlagAtivo = "S", FlagPadrao = "S" });
                    tndao.Add(new TipoNacionalidade() { Descricao = "Estrangeira", ValorEducacenso = 3, FlagAtivo = "S", FlagBrasileira = "N", FlagPadrao = "S" });

                    RecursoINEPDAO ridao = new RecursoINEPDAO();
                    ridao.Add(new RecursoINEP() { Descricao = "Auxílio ledor", ValorEducacenso = 31, FlagAtivo = "S"});
                    ridao.Add(new RecursoINEP() { Descricao = "Auxílio transcrição", ValorEducacenso = 32, FlagAtivo = "S" });
                    ridao.Add(new RecursoINEP() { Descricao = "Guia-intérprete", ValorEducacenso = 33, FlagAtivo = "S" });
                    ridao.Add(new RecursoINEP() { Descricao = "Intérprete de Libras", ValorEducacenso = 34, FlagAtivo = "S" });
                    ridao.Add(new RecursoINEP() { Descricao = "Leitura Labial", ValorEducacenso = 35, FlagAtivo = "S" });
                    ridao.Add(new RecursoINEP() { Descricao = "Prova Ampliada (Fonte tamanho 16)", ValorEducacenso = 36, FlagAtivo = "S" });
                    ridao.Add(new RecursoINEP() { Descricao = "Prova Ampliada (Fonte tamanho 20)", ValorEducacenso = 37, FlagAtivo = "S" });
                    ridao.Add(new RecursoINEP() { Descricao = "Prova Ampliada (Fonte tamanho 24)", ValorEducacenso = 38, FlagAtivo = "S" });
                    ridao.Add(new RecursoINEP() { Descricao = "Prova em Braille", ValorEducacenso = 39, FlagAtivo = "S" });

                    ModeloCertidaoDAO mcdao = new ModeloCertidaoDAO();
                    mcdao.Add(new ModeloCertidao() { Descricao = "Modelo antigo", ValorEducacenso = 1, FlagAtivo = "S", FlagPadrao = "S" });
                    mcdao.Add(new ModeloCertidao() { Descricao = "Modelo novo", ValorEducacenso = 2, FlagAtivo = "S", FlagPadrao = "N" });

                    TipoCertidaoDAO tcdao = new TipoCertidaoDAO();
                    tcdao.Add(new TipoCertidao() { Descricao = "Nascimento", ValorEducacenso = 1, FlagAtivo = "S", FlagPadrao = "S" });
                    tcdao.Add(new TipoCertidao() { Descricao = "Casamento", ValorEducacenso = 2, FlagAtivo = "S", FlagPadrao = "N" });
                    
                    EscolarizacaoEspecialDAO eedao = new EscolarizacaoEspecialDAO();
                    eedao.Add(new EscolarizacaoEspecial() { Descricao = "Em hospital", ValorEducacenso = 1, FlagRecebe = "S", FlagAtivo = "S", FlagPadrao = "N" });
                    eedao.Add(new EscolarizacaoEspecial() { Descricao = "Em domicílio", ValorEducacenso = 2, FlagRecebe = "S", FlagAtivo = "S", FlagPadrao = "N" });
                    eedao.Add(new EscolarizacaoEspecial() { Descricao = "Não recebe", ValorEducacenso = 3, FlagRecebe = "N", FlagAtivo = "S", FlagPadrao = "S" });
                    

                    TransportePublicoDAO tpdao = new TransportePublicoDAO();
                    tpdao.Add(new TransportePublico() { Descricao = "Não Utiliza", ValorEducacenso = 0, FlagAtivo = "S", FlagUtiliza = "N", FlagPadrao = "N" });
                    tpdao.Add(new TransportePublico() { Descricao = "Utiliza – Poder Municipal", ValorEducacenso = 2, FlagAtivo = "S", FlagUtiliza = "S", FlagPadrao = "N" });
                    tpdao.Add(new TransportePublico() { Descricao = "Utiliza – Poder Estadual", ValorEducacenso = 1, FlagAtivo = "S", FlagUtiliza = "S", FlagPadrao = "N" });

                    TurmaUnificadaDAO tudao = new TurmaUnificadaDAO();
                    tudao.Add(new TurmaUnificada() { Descricao = "Não se Aplica", ValorEducacenso = 0, FlagAtivo = "S", FlagPossui = "N", FlagPadrao = "N" });
                    tudao.Add(new TurmaUnificada() { Descricao = "Creche", ValorEducacenso = 2, FlagAtivo = "S", FlagPossui = "S", FlagPadrao = "N" });
                    tudao.Add(new TurmaUnificada() { Descricao = "Pré-Escola", ValorEducacenso = 1, FlagAtivo = "S", FlagPossui = "S", FlagPadrao = "N" });

                    FormaIngressoFederalDAO fidao = new FormaIngressoFederalDAO();
                    fidao.Add(new FormaIngressoFederal() { Descricao = "Sem processo seletivo", FlagPadrao = "S", FlagAtivo = "S"});
                    fidao.Add(new FormaIngressoFederal() { Descricao = "Sorteio", FlagPadrao = "N", FlagAtivo = "S"});
                    fidao.Add(new FormaIngressoFederal() { Descricao = "Transferência", FlagPadrao = "N", FlagAtivo = "S"});
                    fidao.Add(new FormaIngressoFederal() { Descricao = "Exame de seleção sem reserva de vaga", FlagPadrao = "N", FlagAtivo = "S"});
                    fidao.Add(new FormaIngressoFederal() { Descricao = "Exame de seleção, vaga reservada para alunos da rede pública de ensino", FlagPadrao = "N", FlagAtivo = "S"});
                    fidao.Add(new FormaIngressoFederal() { Descricao = "Exame de seleção, vaga reservada para alunos da rede pública de ensino, com baixa renda", FlagPadrao = "N", FlagAtivo = "S"});
                    fidao.Add(new FormaIngressoFederal() { Descricao = "Exame de seleção, vaga reservada para alunos da rede pública de ensino, com baixa renda e autodeclarados pretos, pardos ou indígenas", FlagPadrao = "N", FlagAtivo = "S"});
                    fidao.Add(new FormaIngressoFederal() { Descricao = "Exame de seleção, vaga reservada para outros programas de ação afirmativa", FlagPadrao = "N", FlagAtivo = "S"});
                    fidao.Add(new FormaIngressoFederal() { Descricao = "Outra forma de ingresso", FlagPadrao = "N", FlagAtivo = "S" });

                    SituacaoCursoSuperiorDAO scsdao = new SituacaoCursoSuperiorDAO();
                    scsdao.Add(new SituacaoCursoSuperior() { Descricao = "Concluído", FlagPadrao = "S", FlagAtivo = "S" });
                    scsdao.Add(new SituacaoCursoSuperior() { Descricao = "Em andamento", FlagPadrao = "N", FlagAtivo = "S" });

                    TipoInstituicaoDAO tidao = new TipoInstituicaoDAO();
                    tidao.Add(new TipoInstituicao() { Descricao = "Pública", FlagPadrao = "S", FlagAtivo = "S" });
                    tidao.Add(new TipoInstituicao() { Descricao = "Privada", FlagPadrao = "N", FlagAtivo = "S" });

                    PosGraduacaoDAO pgdao = new PosGraduacaoDAO();
                    pgdao.Add(new PosGraduacao(){ Descricao = "Nenhum", FlagPosGraduacao = "N", FlagPadrao = "S", FlagAtivo = "S"});
                    pgdao.Add(new PosGraduacao(){ Descricao = "Especialização", FlagPosGraduacao = "S", FlagPadrao = "N", FlagAtivo = "S"});
                    pgdao.Add(new PosGraduacao(){ Descricao = "Mestrado", FlagPosGraduacao = "S", FlagPadrao = "N", FlagAtivo = "S"});
                    pgdao.Add(new PosGraduacao(){ Descricao = "Doutorado", FlagPosGraduacao = "S", FlagPadrao = "N", FlagAtivo = "S"});

                    /*
                    FuncaoDocenteDAO fdodao = new FuncaoDocenteDAO();
                    fdodao.Add(new FuncaoDocente() { Descricao = "Docente", FlagPadrao = "S", FlagAtivo = "S" });
                    fdodao.Add(new FuncaoDocente() { Descricao = "Auxiliar/Assistente Educacional", FlagPadrao = "N", FlagAtivo = "S" });
                    fdodao.Add(new FuncaoDocente() { Descricao = "Profissional/Monitor de Atividade Complementar", FlagPadrao = "N", FlagAtivo = "S" });
                    fdodao.Add(new FuncaoDocente() { Descricao = "Tradutor Intérprete de Libras", FlagPadrao = "N", FlagAtivo = "S" });

                    VinculoEmpregaticioDAO vedao = new VinculoEmpregaticioDAO();
                    vedao.Add(new VinculoEmpregaticio() { Descricao = "Concursado/Efetivo/Estável", FlagPadrao = "S", FlagAtivo = "S" });
                    vedao.Add(new VinculoEmpregaticio() { Descricao = "Contrato temporário", FlagPadrao = "N", FlagAtivo = "S" });
                    vedao.Add(new VinculoEmpregaticio() { Descricao = "Contrato terceirizado", FlagPadrao = "N", FlagAtivo = "S" });
                    vedao.Add(new VinculoEmpregaticio() { Descricao = "Contrato CLT", FlagPadrao = "N", FlagAtivo = "S" });
                    */

                    EspecialCategoriaDAO pecdao = new EspecialCategoriaDAO();
                    EspecialCategoria pec1 = new EspecialCategoria() { Descricao = "Deficiência", FlagPadrao = "N", FlagAtivo = "S" };
                    EspecialCategoria pec2 = new EspecialCategoria() { Descricao = "Transtorno global do desenvolvimento", FlagPadrao = "N", FlagAtivo = "S" };
                    EspecialCategoria pec3 = new EspecialCategoria() { Descricao = "Altas habilidades/Superdotação", FlagPadrao = "N", FlagAtivo = "S" };

                    pecdao.Add(pec1);
                    pecdao.Add(pec2);
                    pecdao.Add(pec3);

                    EspecialItemDAO peidao = new EspecialItemDAO();
                    peidao.Add(new EspecialItem() { Descricao = "Cegueira", EspecialCategoria = pec1, ValorEducacenso = 18, FlagPadrao = "N", FlagAtivo = "S" });
                    peidao.Add(new EspecialItem() { Descricao = "Baixa visão", EspecialCategoria = pec1, ValorEducacenso = 19, FlagPadrao = "N", FlagAtivo = "S" });
                    peidao.Add(new EspecialItem() { Descricao = "Surdez", EspecialCategoria = pec1, ValorEducacenso = 20, FlagPadrao = "N", FlagAtivo = "S" });
                    peidao.Add(new EspecialItem() { Descricao = "Deficiência auditiva", EspecialCategoria = pec1, ValorEducacenso = 21, FlagPadrao = "N", FlagAtivo = "S" });
                    
                    peidao.Add(new EspecialItem() { Descricao = "Surdocegueira", EspecialCategoria = pec1, ValorEducacenso = 22, FlagPadrao = "N", FlagAtivo = "S" });
                    peidao.Add(new EspecialItem() { Descricao = "Deficiência física", EspecialCategoria = pec1, ValorEducacenso = 23, FlagPadrao = "N", FlagAtivo = "S" });
                    peidao.Add(new EspecialItem() { Descricao = "Deficiência intelectual", EspecialCategoria = pec1, ValorEducacenso = 24, FlagPadrao = "N", FlagAtivo = "S" });
                    peidao.Add(new EspecialItem() { Descricao = "Deficiência múltipla", EspecialCategoria = pec1, ValorEducacenso = 25, FlagPadrao = "N", FlagAtivo = "S" });

                    peidao.Add(new EspecialItem() { Descricao = "Autismo Infantil", EspecialCategoria = pec2, ValorEducacenso = 26, FlagPadrao = "N", FlagAtivo = "S" });
                    peidao.Add(new EspecialItem() { Descricao = "Síndrome de Asperger", EspecialCategoria = pec2, ValorEducacenso = 27, FlagPadrao = "N", FlagAtivo = "S" });
                    peidao.Add(new EspecialItem() { Descricao = "Síndrome de Rett", EspecialCategoria = pec2, ValorEducacenso = 28, FlagPadrao = "N", FlagAtivo = "S" });
                    peidao.Add(new EspecialItem() { Descricao = "Transtorno desintegrativo da infância", EspecialCategoria = pec2, ValorEducacenso = 29, FlagPadrao = "N", FlagAtivo = "S" });

                    peidao.Add(new EspecialItem() { Descricao = "Altas habilidades/Superdotação", EspecialCategoria = pec3, ValorEducacenso = 30, FlagPadrao = "N", FlagAtivo = "S" });

                    VeiculoCategoriaDAO vcdao = new VeiculoCategoriaDAO();
                    VeiculoCategoria vc1 = new VeiculoCategoria() { Descricao = "Rodoviário", FlagAtivo = "S" };
                    VeiculoCategoria vc2 = new VeiculoCategoria() { Descricao = "Aquaviário/Embarcação", FlagAtivo = "S" };
                    VeiculoCategoria vc3 = new VeiculoCategoria() { Descricao = "Ferroviário", FlagAtivo = "S" };

                    vcdao.Add(vc1);
                    vcdao.Add(vc2);
                    vcdao.Add(vc3);

                    VeiculoTipoDAO vtdao = new VeiculoTipoDAO();
                    vtdao.Add(new VeiculoTipo() { Descricao = "Vans / VW Kombi", VeiculoCategoria = vc1, ValorEducacenso = 13, FlagPadrao = "N", FlagAtivo = "S" });
                    vtdao.Add(new VeiculoTipo() { Descricao = "Micro-ônibus", VeiculoCategoria = vc1, ValorEducacenso = 14, FlagPadrao = "N", FlagAtivo = "S" });
                    vtdao.Add(new VeiculoTipo() { Descricao = "Ônibus", VeiculoCategoria = vc1, ValorEducacenso = 15, FlagPadrao = "N", FlagAtivo = "S" });
                    vtdao.Add(new VeiculoTipo() { Descricao = "Bicicleta", VeiculoCategoria = vc1, ValorEducacenso = 16, FlagPadrao = "N", FlagAtivo = "S" });
                    vtdao.Add(new VeiculoTipo() { Descricao = "Tração Animal", VeiculoCategoria = vc1, ValorEducacenso = 17, FlagPadrao = "N", FlagAtivo = "S" });
                    vtdao.Add(new VeiculoTipo() { Descricao = "Outro tipo de veículo rodoviário", ValorEducacenso = 18, VeiculoCategoria = vc1, FlagPadrao = "N", FlagAtivo = "S" });

                    vtdao.Add(new VeiculoTipo() { Descricao = "Capacidade de até 5 alunos", VeiculoCategoria = vc2, ValorEducacenso = 19, FlagPadrao = "N", FlagAtivo = "S" });
                    vtdao.Add(new VeiculoTipo() { Descricao = "Capacidade de 5 a 15 alunos", VeiculoCategoria = vc2, ValorEducacenso = 20, FlagPadrao = "N", FlagAtivo = "S" });
                    vtdao.Add(new VeiculoTipo() { Descricao = "Capacidade de 15 a 35 alunos", VeiculoCategoria = vc2, ValorEducacenso = 21, FlagPadrao = "N", FlagAtivo = "S" });
                    vtdao.Add(new VeiculoTipo() { Descricao = "Capacidade acima de 35 alunos", VeiculoCategoria = vc2, ValorEducacenso = 22, FlagPadrao = "N", FlagAtivo = "S" });

                    vtdao.Add(new VeiculoTipo() { Descricao = "Trem/Metrô", VeiculoCategoria = vc3, ValorEducacenso = 23, FlagPadrao = "N", FlagAtivo = "S" });

                    LinguaIndigenaDAO lidao = new LinguaIndigenaDAO();
                    lidao.Add(new LinguaIndigena() { Codigo = 000, Descricao = "---", FlagIndigena = "N", FlagPadrao = "S", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 001, Descricao = "Aikaná/Aikanã", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 002, Descricao = "Ajuru/Wayoro, Ajurú", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 004, Descricao = "Suruí do Pará/Suruí do Tocantins/Aikewara", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 005, Descricao = "Xavánte/Xavante", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 006, Descricao = "Xerénte", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 007, Descricao = "Amanayé", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 010, Descricao = "Apalaí", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 011, Descricao = "Apiaká", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 012, Descricao = "Apinayé", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 013, Descricao = "Apurinã", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 014, Descricao = "Arapáso", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 015, Descricao = "Arara do Acre, Shawãdawa", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 017, Descricao = "Araweté", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 018, Descricao = "Arikapú/Jabutí", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 019, Descricao = "Aruá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 020, Descricao = "Asuriní do Tocantins", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 021, Descricao = "Asuriní do Xingu", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 022, Descricao = "Ava-Canoeiro/Avá-Canoeiro, Avá, Canoeiro", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 023, Descricao = "Awetí", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 024, Descricao = "Bakairí", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 025, Descricao = "Banawá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 026, Descricao = "Baníwa/Tapiira Tapuya, Kawa Tapuya", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 028, Descricao = "Bará", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 029, Descricao = "Baré", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 030, Descricao = "Boróro", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 031, Descricao = "Cinta Larga/Cinta-Larga", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 032, Descricao = "Dení", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 033, Descricao = "Desána", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 034, Descricao = "Dâw", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 035, Descricao = "Galibí do Oiapoque, Galibí (Ka’ríña)", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 036, Descricao = "Gavião (Ikõro, Digüt), Gavião de Rondônia/ Ikolen", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 037, Descricao = "Guajá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 038, Descricao = "Guaraní Kaiowá/Guarani Kayová", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 039, Descricao = "Guaraní Mbyá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 040, Descricao = "Guaraní Nhandéva", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 042, Descricao = "Guató", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 043, Descricao = "Hixkaryána", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 045, Descricao = "Ingarikó", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 046, Descricao = "Irántxe", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 047, Descricao = "Djeoromitxí/Jabotí/Jabutí", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 048, Descricao = "Jarawára", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 049, Descricao = "Yamináwa", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 050, Descricao = "Javaé", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 051, Descricao = "Jurúna/Yudjá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 052, Descricao = "Ka’apor/Urubu, Ka’apór", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 053, Descricao = "Kadiwéu", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 058, Descricao = "Kalapálo", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 059, Descricao = "Kamayurá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 060, Descricao = "Ashanínka/Axanínka", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 061, Descricao = "Kanamarí", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 062, Descricao = "Kanoé/Kanoê", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 063, Descricao = "Karajá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 064, Descricao = "Karapanã", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 065, Descricao = "Karitiána", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 066, Descricao = "Arara de Rondônia/Káro", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 067, Descricao = "Katawixí", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 068, Descricao = "Katukína do Acre", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 069, Descricao = "Katukína", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 076, Descricao = "Uru-Eu-Wau-Wau/Uruewawau", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 077, Descricao = "Kaxararí", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 078, Descricao = "Kaxinawá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 080, Descricao = "Kayabí", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 081, Descricao = "Gorotire (Kayapó)", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 082, Descricao = "Kararaô (Kayapó)", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 083, Descricao = "Kokraimoro (Kayapó)", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 084, Descricao = "Kubenkrngkegn (Kayapó)", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 085, Descricao = "Menkrangnoti (Kayapó)", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 086, Descricao = "Mentuktíre, Txukahamae (Kayapó)", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 087, Descricao = "Xikrin (Kayapó)", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 090, Descricao = "Kokáma", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 091, Descricao = "Korúbo", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 092, Descricao = "Krenák", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 093, Descricao = "Kubéo, Kubewa", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 094, Descricao = "Kuikúro", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 095, Descricao = "Kulína Madijá/Kulina, Kulína Madihá (Madija)", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 096, Descricao = "Kuruáya", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 099, Descricao = "Makuráp", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 100, Descricao = "Makuxí", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 102, Descricao = "Marúbo", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 103, Descricao = "Matipú", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 104, Descricao = "Matís", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 105, Descricao = "Matsés", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 106, Descricao = "Mawé", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 107, Descricao = "Maxakalí", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 108, Descricao = "Yekuána, Mayongong, Makiritáre,", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 109, Descricao = "Mehináku", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 110, Descricao = "Sakurabiat/Kampé", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 111, Descricao = "Mondé, Tupí-Mondé", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 112, Descricao = "Mundurukú", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 113, Descricao = "Múra", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 114, Descricao = "Mynky/Mynký, Meky, Menky, Menki", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 115, Descricao = "Nadëb", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 116, Descricao = "Nahukwá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 120, Descricao = "Negarotê/Negarote", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 126, Descricao = "Lingua Geral Amazônica, Nheengatu", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 127, Descricao = "Ninám", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 128, Descricao = "Nukiní", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 129, Descricao = "Ofayé", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 130, Descricao = "Oro Win", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 131, Descricao = "Palikúr", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 132, Descricao = "Panará, Krenakarôre/Kren-akarôre", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 133, Descricao = "Paresí", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 134, Descricao = "Karipúna do Amapá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 135, Descricao = "Galibí Marwórno/Galibi Marworno", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 136, Descricao = "Paumarí", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 137, Descricao = "Pirahã", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 138, Descricao = "Piratapúya", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 140, Descricao = "Poyanáwa", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 141, Descricao = "Puruborá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 142, Descricao = "Canoeiros/Rikbaktsá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 143, Descricao = "Sabanê", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 145, Descricao = "Enawenê-Nawê", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 146, Descricao = "Sanumá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 148, Descricao = "Zuruwahá, Suruahá (Zuruahá)", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 150, Descricao = "Suyá, Kisêdjê/Kisedjê", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 151, Descricao = "Tapirapé", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 152, Descricao = "Tariána", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 153, Descricao = "Taulipáng", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 155, Descricao = "Tembé", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 156, Descricao = "Teréna", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 157, Descricao = "Tikúna", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 167, Descricao = "Tiriyó/Tarona", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 168, Descricao = "Torá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 169, Descricao = "Trumái", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 171, Descricao = "Tuparí", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 172, Descricao = "Tuyúca /Tuyuca", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 173, Descricao = "Ikpeng/Ikpéng", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 174, Descricao = "Tsohom Djapa/Tsohondjapá (Tsohom Djapa)", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 175, Descricao = "Urupá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 176, Descricao = "Waimirí-Atroarí", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 177, Descricao = "Wái Wái/Waiwái", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 178, Descricao = "Wanána/Guanána", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 179, Descricao = "Wapixána", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 180, Descricao = "Warekéna", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 182, Descricao = "Wauja/Waurá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 183, Descricao = "Wayampí/Oyampi", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 184, Descricao = "Wayána", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 185, Descricao = "Xambioá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 186, Descricao = "Xetá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 187, Descricao = "Xipáya", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 188, Descricao = "Xokléng", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 190, Descricao = "Yanomám/Yanonmán", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 191, Descricao = "Yanomámi", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 192, Descricao = "Fulni-ô/Yathê", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 193, Descricao = "Yawalapití", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 194, Descricao = "Yawanawá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 196, Descricao = "Zo’é", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 197, Descricao = "Zoró", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 198, Descricao = "Akuntsú", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 199, Descricao = "Amondáwa", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 200, Descricao = "Arara do Aripuana/Arara do Aripuanã", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 201, Descricao = "Arara do Pará, Arara do Xingu", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 203, Descricao = "Barasána", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 204, Descricao = "Kambéba", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 205, Descricao = "Kanéla Rankocamekra/Canela Ramkokamekrã", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 206, Descricao = "Chamakóko/Samúko, Chamacoco", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 207, Descricao = "Chiquitáno/Chiquito", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 208, Descricao = "Diahói/Diahui", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 211, Descricao = "Guajajára", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 215, Descricao = "Júma/Juma", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 216, Descricao = "Yurutí, Juriti", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 218, Descricao = "Kapon Patamóna/Kapon Ptamóna", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 219, Descricao = "Karipúna", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 220, Descricao = "Kayapó", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 221, Descricao = "Mebengokré (Kayapó)", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 222, Descricao = "Kinikináu, Kinikinawa", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 223, Descricao = "Kreje/Krenjé", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 224, Descricao = "Krikatí/Krinkatí", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 225, Descricao = "Kujubím", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 226, Descricao = "Kuripáko", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 227, Descricao = "Kwazá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 228, Descricao = "Lakondê", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 229, Descricao = "Latundê", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 230, Descricao = "Mamaindê", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 231, Descricao = "Mandúka/Nambikwára do Campo", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 233, Descricao = "Miránha", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 234, Descricao = "Tukáno /Miriti-Tapuia", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 235, Descricao = "Kaingáng", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 236, Descricao = "Suruí de Rondônia", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 237, Descricao = "Parakanã", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 238, Descricao = "Parintintín", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 240, Descricao = "Gavião Pukobiyé/Gavião Pukobié", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 241, Descricao = "Tapayúna", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 242, Descricao = "Tawandê", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 243, Descricao = "Tenharím/Tenharim", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 244, Descricao = "Umutína", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 245, Descricao = "Pakaá Nóva/Migueleno, Miguelenho", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 246, Descricao = "Shanenáwa/Xanenáwa, Xawanawa", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 250, Descricao = "Tupí, Tupi Antigo", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 251, Descricao = "Canela", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 252, Descricao = "Kanéla Apaniekra/Canela Apaniekrã", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 253, Descricao = "Gavião Krikatêjê/Gavião Krinkatejé", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 254, Descricao = "Gavião Parkatêjê/Guató Parakatejé/Gavião do Pará", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 255, Descricao = "Krahô/Crao, Kraô", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 256, Descricao = "Krao Kanela", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 257, Descricao = "Kokuiregatêjê/Kokuiregatejje", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 258, Descricao = "Timbira", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 259, Descricao = "Xacriabá/Xakriabá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 260, Descricao = "Jê (não específico)", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 261, Descricao = "Pataxó", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 262, Descricao = "Pataxó Hã Hã Hãe/Pataxó Há-Há-Há", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 263, Descricao = "Salamãy", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 264, Descricao = "Ramaráma", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 265, Descricao = "Urucú/Urucu", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 266, Descricao = "Guaraní", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 267, Descricao = "Lingua De Sinais Ka’apor/Língua de Sinais Urubu-Kaapór", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 268, Descricao = "Kawahíb", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 269, Descricao = "Turiwára", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 270, Descricao = "Tupí-Guaraní", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 271, Descricao = "Kaixána/Kayuisiana", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 272, Descricao = "Machinéri", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 273, Descricao = "Mawayána", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 274, Descricao = "Aruák", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 275, Descricao = "Naravúte", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 276, Descricao = "Kaxuyána/Kahyána, Warikyána", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 277, Descricao = "Xikuyána/Sikiyána", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 278, Descricao = "Karib", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 279, Descricao = "Kulína Páno", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 280, Descricao = "Pano", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 281, Descricao = "Makúna, Yebá-masã", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 282, Descricao = "Siriáno/Suriana, Suriána", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 283, Descricao = "Arawá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 284, Descricao = "Himarimã/Hi-merimã, Mirimã, Himarimá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 285, Descricao = "Jamamadí-Kanamanti/Jamamadí", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 286, Descricao = "Hup, Húpda, Maku, Yuhupde, Yuhúp", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 287, Descricao = "Alaketesú", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 288, Descricao = "Alantesú", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 289, Descricao = "Hahaintesú", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 290, Descricao = "Halotesú", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 291, Descricao = "Kithaulú", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 292, Descricao = "Sararé", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 300, Descricao = "Sawentesú", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 301, Descricao = "Waikisú", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 302, Descricao = "Wakalitesú", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 303, Descricao = "Wasusú", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 304, Descricao = "Nambikwára", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 305, Descricao = "Miguelénho/Migueleno, Miguelenho", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 306, Descricao = "Txapakúra", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 307, Descricao = "Bóra", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 308, Descricao = "Guaikurú", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 309, Descricao = "Witóto", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 310, Descricao = "Acona/Akona", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 311, Descricao = "Aimoré", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 312, Descricao = "Anacé", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 313, Descricao = "Apolima – Arara", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 314, Descricao = "Arana", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 315, Descricao = "Arapiun", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 316, Descricao = "Arikén", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 317, Descricao = "Arikose", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 318, Descricao = "Atikum", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 319, Descricao = "Awi", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 320, Descricao = "Baenã", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 321, Descricao = "Borari", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 322, Descricao = "Botocudo", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 323, Descricao = "Catokin (Katukína)", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 324, Descricao = "Charrúa/Charrua", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 325, Descricao = "Coiupanka", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 326, Descricao = "Guara", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 327, Descricao = "Guarino", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 328, Descricao = "Guaru", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 329, Descricao = "Isse", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 330, Descricao = "Jaricuna", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 331, Descricao = "Jeripancó/Jeripankó", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 332, Descricao = "Kaete", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 333, Descricao = "Kaimbé", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 334, Descricao = "Kalabassa", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 335, Descricao = "Kalankó", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 336, Descricao = "Kamba/Kámba", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 337, Descricao = "Kambiwá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 338, Descricao = "Kambiwá Pipipã", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 339, Descricao = "Kanindé", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 340, Descricao = "Kantaruré", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 341, Descricao = "Kapinawá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 342, Descricao = "Karapoto/Karapotó", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 343, Descricao = "Karijo", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 344, Descricao = "Kariri/Karirí", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 345, Descricao = "Kariri – Xocó/Karirí-Xocó", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 346, Descricao = "Kaxixó", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 347, Descricao = "Kayuisiana -(Kaixána)", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 348, Descricao = "Kiriri", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 349, Descricao = "Kueskue", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 350, Descricao = "Manao/Manáo", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 351, Descricao = "Maragua", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 352, Descricao = "Maytapu", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 353, Descricao = "Mucurim", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 354, Descricao = "Nawa/Náwa", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 355, Descricao = "Paiaku", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 356, Descricao = "Pankará", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 357, Descricao = "Pankararé", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 358, Descricao = "Pankararú/Pankarú", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 359, Descricao = "Pankararú – Kalanko", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 360, Descricao = "Pankararú – Karuazu", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 361, Descricao = "Pankaru", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 362, Descricao = "Patxôhã/Patxoha", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 363, Descricao = "Paumelenho", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 364, Descricao = "Piri-Piri/Pirí-Pirí", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 365, Descricao = "Pitaguari/Pitaguarí", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 366, Descricao = "Potiguara/Potiguára", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 367, Descricao = "Puri/Purí", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 368, Descricao = "Sapará/Sapara", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 369, Descricao = "Tabajara", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 370, Descricao = "Tapajós", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 371, Descricao = "Tapeba", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 372, Descricao = "Tapiuns/Tapiun", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 373, Descricao = "Tapuía/Tapúya", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 374, Descricao = "Tingui Botó/Tinguí-Botó", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 375, Descricao = "Tremembé", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 376, Descricao = "Truká", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 377, Descricao = "Tumbalalá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 378, Descricao = "Tupinambá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 379, Descricao = "Tupinambaraná", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 380, Descricao = "Tupiniquim", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 381, Descricao = "Tuxá", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 382, Descricao = "Waira", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 383, Descricao = "Waiána-Apalaí", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 384, Descricao = "Wajuju/Wajujú", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 385, Descricao = "Wassú (Wasusú)", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 386, Descricao = "Xocó", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 387, Descricao = "Xucuru/Xukurú", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 388, Descricao = "Xucuru – Kariri/Xukurú-Karirí", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 389, Descricao = "Maya", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    lidao.Add(new LinguaIndigena() { Codigo = 999, Descricao = "Outras Línguas Indígenas", FlagIndigena = "S", FlagPadrao = "N", FlagAtivo = "S" });

                    PaisDAO pdao = new PaisDAO();
                    pdao.Add(new Pais() { Codigo = 004, Descricao = "Afeganistão", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 008, Descricao = "Albânia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 012, Descricao = "Argélia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 016, Descricao = "Samoa Americana", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 020, Descricao = "Andorra", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 024, Descricao = "Angola", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 028, Descricao = "Antígua e Barbuda", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 031, Descricao = "Azerbaijão", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 032, Descricao = "Argentina", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 036, Descricao = "Austrália", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 040, Descricao = "Áustria", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 044, Descricao = "Bahamas", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 048, Descricao = "Bahrein", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 050, Descricao = "Bangladesh", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 051, Descricao = "Armênia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 052, Descricao = "Barbados", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 056, Descricao = "Bélgica", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 060, Descricao = "Bermudas", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 064, Descricao = "Butão", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 068, Descricao = "Bolívia (Estado Plurinacional da)", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 070, Descricao = "Bósnia e Herzegovina", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 072, Descricao = "Botsuana", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 076, Descricao = "Brasil", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 084, Descricao = "Belize", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 090, Descricao = "Ilhas Salomão", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 092, Descricao = "Ilhas Virgens Britânicas", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 096, Descricao = "Brunei", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 100, Descricao = "Bulgária", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 104, Descricao = "Myanmar", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 108, Descricao = "Burundi", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 112, Descricao = "Bielorrússia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 116, Descricao = "Camboja", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 120, Descricao = "Camarões", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 124, Descricao = "Canadá", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 132, Descricao = "Cabo Verde", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 136, Descricao = "Ilhas Caiman", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 140, Descricao = "República Centro-Africana", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 144, Descricao = "Sri Lanka", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 148, Descricao = "Chade", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 152, Descricao = "Chile", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 156, Descricao = "China", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 170, Descricao = "Colômbia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 174, Descricao = "Comores", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 175, Descricao = "Mayotte", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 178, Descricao = "Congo", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 180, Descricao = "República Democrática do Congo", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 184, Descricao = "Ilhas Cook", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 188, Descricao = "Costa Rica", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 191, Descricao = "Croácia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 192, Descricao = "Cuba", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 196, Descricao = "Chipre", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 203, Descricao = "República Tcheca", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 204, Descricao = "Benin", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 208, Descricao = "Dinamarca", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 212, Descricao = "Dominica", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 214, Descricao = "República Dominicana", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 218, Descricao = "Equador", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 222, Descricao = "El Salvador", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 226, Descricao = "Guiné Equatorial", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 231, Descricao = "Etiópia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 232, Descricao = "Eritreia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 233, Descricao = "Estônia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 234, Descricao = "Ilhas Feroe", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 238, Descricao = "Ilhas Malvinas", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 242, Descricao = "Fiji", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 246, Descricao = "Finlândia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 248, Descricao = "Åland, Ilhas", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 250, Descricao = "França", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 254, Descricao = "Guiana Francesa", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 258, Descricao = "Polinésia Francesa", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 262, Descricao = "Djibuti", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 266, Descricao = "Gabão", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 268, Descricao = "Geórgia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 270, Descricao = "Gâmbia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 275, Descricao = "Palestina", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 276, Descricao = "Alemanha", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 288, Descricao = "Gana", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 292, Descricao = "Gibraltar", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 296, Descricao = "Quiribati", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 300, Descricao = "Grécia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 304, Descricao = "Groenlândia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 308, Descricao = "Granada", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 312, Descricao = "Guadalupe", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 316, Descricao = "Guam", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 320, Descricao = "Guatemala", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 324, Descricao = "Guiné", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 328, Descricao = "Guiana", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 332, Descricao = "Haiti", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 336, Descricao = "Vaticano", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 339, Descricao = "Apátrida", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 340, Descricao = "Honduras", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 344, Descricao = "China, Região Administrativa Especial de Hong Kong", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 348, Descricao = "Hungria", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 352, Descricao = "Islândia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 356, Descricao = "Índia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 360, Descricao = "Indonésia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 364, Descricao = "Irã (República Islâmica do)", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 368, Descricao = "Iraque", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 372, Descricao = "Irlanda", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 376, Descricao = "Israel", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 380, Descricao = "Itália", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 384, Descricao = "Costa do Marfi", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 388, Descricao = "Jamaica", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 392, Descricao = "Japão", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 398, Descricao = "Cazaquistão", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 400, Descricao = "Jordânia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 404, Descricao = "Quênia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 408, Descricao = "Coreia do Norte", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 410, Descricao = "Coreia do Sul", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 414, Descricao = "Kuwait", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 417, Descricao = "Quirguistão", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 418, Descricao = "Laos, República Popular Democrática", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 422, Descricao = "Líbano", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 426, Descricao = "Lesoto", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 428, Descricao = "Letônia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 430, Descricao = "Libéria", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 434, Descricao = "Líbia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 438, Descricao = "Liechtenstein", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 440, Descricao = "Lituânia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 442, Descricao = "Luxemburgo", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 446, Descricao = "China, Região Administrativa Especial de Macau", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 450, Descricao = "Madagáscar", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 454, Descricao = "Malawi", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 458, Descricao = "Malásia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 462, Descricao = "Maldivas (Ilhas)", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 466, Descricao = "Mali", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 470, Descricao = "Malta", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 474, Descricao = "Martinica", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 478, Descricao = "Mauritânia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 480, Descricao = "Maurício (Ilhas)", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 484, Descricao = "México", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 492, Descricao = "Mônaco", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 496, Descricao = "Mongólia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 498, Descricao = "Moldávia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 499, Descricao = "Montenegro", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 500, Descricao = "Montserrat", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 504, Descricao = "Marrocos", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 508, Descricao = "Moçambique", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 512, Descricao = "Oman", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 516, Descricao = "Namíbia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 520, Descricao = "Nauru", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 524, Descricao = "Nepal", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 528, Descricao = "Holanda", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 531, Descricao = "Curaçao", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 533, Descricao = "Aruba", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 534, Descricao = "Sint Maarten (parte holandesa)", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 535, Descricao = "Bonaire, Saint Eustatius e Saba", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 540, Descricao = "Nova Caledônia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 548, Descricao = "Vanuatu", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 554, Descricao = "Nova Zelândia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 558, Descricao = "Nicarágua", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 562, Descricao = "Níger", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 566, Descricao = "Nigéria", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 570, Descricao = "Niue", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 574, Descricao = "Ilha Norfolk", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 578, Descricao = "Noruega", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 580, Descricao = "Ilhas Mariana", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 583, Descricao = "Micronésia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 584, Descricao = "Ilhas Marshall", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 585, Descricao = "Palau", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 586, Descricao = "Paquistão", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 591, Descricao = "Panamá", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 598, Descricao = "Papua Nova Guiné", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 600, Descricao = "Paraguai", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 604, Descricao = "Peru", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 608, Descricao = "Filipinas", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 612, Descricao = "Pitcairin", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 616, Descricao = "Polônia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 620, Descricao = "Portugal", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 624, Descricao = "Guiné Bissau", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 626, Descricao = "Timor Leste", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 630, Descricao = "Porto Rico", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 634, Descricao = "Catar", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 638, Descricao = "Reunião", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 642, Descricao = "Romênia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 643, Descricao = "Rússia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 646, Descricao = "Ruanda", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 652, Descricao = "São Bartolomeu", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 654, Descricao = "Santa Helena", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 659, Descricao = "São Cristóvão e Nevis", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 660, Descricao = "Anguilla", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 662, Descricao = "Santa Lúcia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 663, Descricao = "Saint-Martin (parte francesa)", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 666, Descricao = "Saint Pierre e Miquelon", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 670, Descricao = "São Vicente e Granadinas", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 674, Descricao = "San Marino", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 678, Descricao = "São Tomé e Príncipe", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 680, Descricao = "Sark", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 682, Descricao = "Arábia Saudita", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 686, Descricao = "Senegal", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 688, Descricao = "Sérvia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 690, Descricao = "Seychelles", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 694, Descricao = "Serra Leoa", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 702, Descricao = "Cingapura", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 703, Descricao = "Eslováquia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 704, Descricao = "Vietnã", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 705, Descricao = "Eslovênia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 706, Descricao = "Somália", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 710, Descricao = "África do Sul", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 716, Descricao = "Zimbábue", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 724, Descricao = "Espanha", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 728, Descricao = "Sudão do Sul", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 729, Descricao = "Sudão", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 732, Descricao = "Saara Ocidental", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 740, Descricao = "Suriname", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 744, Descricao = "Svalbard e Jan Mayer", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 748, Descricao = "Suazilândia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 752, Descricao = "Suécia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 756, Descricao = "Suíça", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 760, Descricao = "Síria", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 762, Descricao = "Tajiquistão", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 764, Descricao = "Tailândia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 768, Descricao = "Togo", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 772, Descricao = "Tokelau", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 776, Descricao = "Tonga", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 780, Descricao = "Trindade e Tobago", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 784, Descricao = "Emirados Árabes Unidos", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 788, Descricao = "Tunísia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 792, Descricao = "Turquia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 795, Descricao = "Turquemenistão", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 796, Descricao = "Ilhas Turks e Caicos", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 798, Descricao = "Tuvalu", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 800, Descricao = "Uganda", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 804, Descricao = "Ucrânia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 807, Descricao = "Macedônia (República da)", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 818, Descricao = "Egito", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 826, Descricao = "Reino Unido", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 830, Descricao = "Ilhas do Canal", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 831, Descricao = "Guernsey", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 832, Descricao = "Jersey", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 833, Descricao = "Ilhas de Man", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 834, Descricao = "Tanzânia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 840, Descricao = "Estados Unidos da América", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 850, Descricao = "Ilhas Virgens Americanas", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 854, Descricao = "Burquina Faso", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 858, Descricao = "Uruguai", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 860, Descricao = "Uzbequistão", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 862, Descricao = "Venezuela", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 876, Descricao = "Ilhas Wallis e Futuna", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 882, Descricao = "Samoa", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 887, Descricao = "Iêmen", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 894, Descricao = "Zâmbia", FlagPadrao = "N", FlagAtivo = "S" });
                    pdao.Add(new Pais() { Codigo = 999, Descricao = "Outra acionalidade", FlagPadrao = "N", FlagAtivo = "S" });

                    SituacaoDocumentoDAO sdadao = new SituacaoDocumentoDAO();
                    sdadao.Add(new SituacaoDocumento() { Descricao = "Pessoa Possui documento", FlagPossui = "S", FlagPadrao = "N", FlagAtivo = "S" });
                    sdadao.Add(new SituacaoDocumento() { Descricao = "Pessoa não possui documento", FlagPossui = "N", FlagPadrao = "N", FlagAtivo = "S" });
                    sdadao.Add(new SituacaoDocumento() { Descricao = "Escola não possui informação de documento da pessoa", FlagPossui = "S", FlagPadrao = "N", FlagAtivo = "S" });

                    SituacaoFuncionamentoDAO sifudao = new SituacaoFuncionamentoDAO();
                    sifudao.Add(new SituacaoFuncionamento() { Descricao = "Em atividade", ValorEducacenso = 1, FlagPadrao = "S", FlagAtivo = "S" });
                    sifudao.Add(new SituacaoFuncionamento() { Descricao = "Paralizada", ValorEducacenso = 2, FlagPadrao = "N", FlagAtivo = "S" });
                    sifudao.Add(new SituacaoFuncionamento() { Descricao = "Extinta", ValorEducacenso = 3, FlagPadrao = "N", FlagAtivo = "S" });

                    ModalidadeDAO mdao = new ModalidadeDAO();

                    Modalidade modREG = new Modalidade() { Descricao = "Ensino Regular", Abreviatura = "REG", ValorEducacenso = 1, FlagAtivo = "S" };
                    Modalidade modESP = new Modalidade() { Descricao = "Educação Especial – Modalidade Substitutiva", Abreviatura = "ESP", ValorEducacenso = 2, FlagAtivo = "S" };
                    Modalidade modEJA = new Modalidade() { Descricao = "Educação de Jovens e Adultos", Abreviatura = "EJA", ValorEducacenso = 3, FlagAtivo = "S" };

                    mdao.Add(modREG);
                    mdao.Add(modESP);
                    mdao.Add(modEJA);

                    Serie sCreche = new Serie() { Descricao = "CRECHE (0 A 3 ANOS)", Abreviatura = "CRECHE", FlagAtivo = "S" };
                    Serie sPre = new Serie() { Descricao = "PRÉ ESCOLA (4 E 5 ANOS)", Abreviatura = "PRE", FlagAtivo = "S" };
                    Serie sUni = new Serie() { Descricao = "UNIFICADA (0 A 5 ANOS)", Abreviatura = "UNI", FlagAtivo = "S" };
                    Serie sMltEtp = new Serie() { Descricao = "MULTIETAPA", Abreviatura = "MLTETP", FlagAtivo = "S" };

                    Serie s1S = new Serie() { Descricao = "1a SÉRIE", Abreviatura = "1a SÉRIE", FlagAtivo = "S" };
                    Serie s2S = new Serie() { Descricao = "2a SÉRIE", Abreviatura = "2a SÉRIE", FlagAtivo = "S" };
                    Serie s3S = new Serie() { Descricao = "3a SÉRIE", Abreviatura = "3a SÉRIE", FlagAtivo = "S" };
                    Serie s4S = new Serie() { Descricao = "4a SÉRIE", Abreviatura = "4a SÉRIE", FlagAtivo = "S" };
                    Serie s5S = new Serie() { Descricao = "5a SÉRIE", Abreviatura = "5a SÉRIE", FlagAtivo = "S" };
                    Serie s6S = new Serie() { Descricao = "6a SÉRIE", Abreviatura = "6a SÉRIE", FlagAtivo = "S" };
                    Serie s7S = new Serie() { Descricao = "7a SÉRIE", Abreviatura = "7a SÉRIE", FlagAtivo = "S" };
                    Serie s8S = new Serie() { Descricao = "8a SÉRIE", Abreviatura = "8a SÉRIE", FlagAtivo = "S" };

                    Serie sMulti = new Serie() { Descricao = "MULTI", Abreviatura = "MULTI", FlagAtivo = "S" };
                    Serie sCorr = new Serie() { Descricao = "CORREÇÃO DE FLUXO", Abreviatura = "CORR", FlagAtivo = "S" };
                    Serie sMulti89 = new Serie() { Descricao = "MULTI 8 E 9 ANOS", Abreviatura = "MULTI 8 E 9", FlagAtivo = "S" };
                    Serie sNaoSer = new Serie() { Descricao = "NÃO SERIADA", Abreviatura = "NÃO SER", FlagAtivo = "S" };

                    Serie s1A = new Serie() { Descricao = "1o ANO", Abreviatura = "1o ANO", FlagAtivo = "S" };
                    Serie s2A = new Serie() { Descricao = "2o ANO", Abreviatura = "2o ANO", FlagAtivo = "S" };
                    Serie s3A = new Serie() { Descricao = "3o ANO", Abreviatura = "3o ANO", FlagAtivo = "S" };
                    Serie s4A = new Serie() { Descricao = "4o ANO", Abreviatura = "4o ANO", FlagAtivo = "S" };
                    Serie s5A = new Serie() { Descricao = "5o ANO", Abreviatura = "5o ANO", FlagAtivo = "S" };
                    Serie s6A = new Serie() { Descricao = "6o ANO", Abreviatura = "6o ANO", FlagAtivo = "S" };
                    Serie s7A = new Serie() { Descricao = "7o ANO", Abreviatura = "7o ANO", FlagAtivo = "S" };
                    Serie s8A = new Serie() { Descricao = "8o ANO", Abreviatura = "8o ANO", FlagAtivo = "S" };
                    Serie s9A = new Serie() { Descricao = "9o ANO", Abreviatura = "9o ANO", FlagAtivo = "S" };

                    Serie s1SI = new Serie() { Descricao = "INTEGRADO 1a SÉRIE", Abreviatura = "INTEG 1a", FlagAtivo = "S" };
                    Serie s2SI = new Serie() { Descricao = "INTEGRADO 2a SÉRIE", Abreviatura = "INTEG 2a", FlagAtivo = "S" };
                    Serie s3SI = new Serie() { Descricao = "INTEGRADO 3a SÉRIE", Abreviatura = "INTEG 3a", FlagAtivo = "S" };
                    Serie s4SI = new Serie() { Descricao = "INTEGRADO 4a SÉRIE", Abreviatura = "INTEG 4a", FlagAtivo = "S" };
                    Serie sINTNS = new Serie() { Descricao = "INTEGRADO NÃO SERIADA", Abreviatura = "INTEG NS", FlagAtivo = "S" };

                    Serie s1MAG = new Serie() { Descricao = "NORMAL/MAGISTÉRIO 1a SÉRIE", Abreviatura = "MAG 1a", FlagAtivo = "S" };
                    Serie s2MAG = new Serie() { Descricao = "NORMAL/MAGISTÉRIO 2a SÉRIE", Abreviatura = "MAG 2a", FlagAtivo = "S" };
                    Serie s3MAG = new Serie() { Descricao = "NORMAL/MAGISTÉRIO 3a SÉRIE", Abreviatura = "MAG 3a", FlagAtivo = "S" };
                    Serie s4MAG = new Serie() { Descricao = "NORMAL/MAGISTÉRIO 4a SÉRIE", Abreviatura = "MAG 4a", FlagAtivo = "S" };

                    Serie sConc = new Serie() { Descricao = "CONCOMITANTE", Abreviatura = "CONC", FlagAtivo = "S" };
                    Serie sSubseq = new Serie() { Descricao = "SUBSEQUENTE", Abreviatura = "SUBSEQ", FlagAtivo = "S" };
                    Serie sMista = new Serie() { Descricao = "MISTA - CONCOMITANTE E SUBSEQUENTE", Abreviatura = "MISTA", FlagAtivo = "S" };
                    Serie sSegInteg = new Serie() { Descricao = "SEGMENTO PROFISSIONAL DA EJA INTEGRADA", Abreviatura = "SEG INTEG", FlagAtivo = "S" };
                    
                    Serie sIni = new Serie() { Descricao = "ANOS INICIAIS", Abreviatura = "INICIAIS", FlagAtivo = "S" };
                    Serie sFim = new Serie() { Descricao = "ANOS FINAIS", Abreviatura = "FINAIS", FlagAtivo = "S" };
                    Serie sIniFim = new Serie() { Descricao = "ANOS INICIAIS E FINAIS", Abreviatura = "INI/FINAIS", FlagAtivo = "S" };
                    Serie sEnsMed = new Serie() { Descricao = "ENSINO MÉDIO", Abreviatura = "ENS MED", FlagAtivo = "S" };
                    Serie sIntProfFund = new Serie() { Descricao = "INTEGRADA EDUCAÇÃO PROFISIONAL NÍVEL FUNDAMENTAL (FIC)", Abreviatura = "INT PROF FUND", FlagAtivo = "S" };
                    Serie sIntProfMed = new Serie() { Descricao = "INTEGRADA EDUCAÇÃO PROFISIONAL NÍVEL MÉDIO", Abreviatura = "INT PROF MED", FlagAtivo = "S" };


                    SerieDAO sdao = new SerieDAO();

                    sdao.Add(sCreche);
                    sdao.Add(sPre);
                    sdao.Add(sUni);
                    sdao.Add(sMltEtp);

                    sdao.Add(s1S);
                    sdao.Add(s2S);
                    sdao.Add(s3S);
                    sdao.Add(s4S);
                    sdao.Add(s5S);
                    sdao.Add(s6S);
                    sdao.Add(s7S);
                    sdao.Add(s8S);

                    sdao.Add(sMulti);
                    sdao.Add(sCorr);
                    sdao.Add(sMulti89);
                    sdao.Add(sNaoSer);

                    sdao.Add(s1A);
                    sdao.Add(s2A);
                    sdao.Add(s3A);
                    sdao.Add(s4A);
                    sdao.Add(s5A);
                    sdao.Add(s6A);
                    sdao.Add(s7A);
                    sdao.Add(s8A);
                    sdao.Add(s9A);

                    sdao.Add(s1SI);
                    sdao.Add(s2SI);
                    sdao.Add(s3SI);
                    sdao.Add(s4SI);
                    sdao.Add(sINTNS);

                    sdao.Add(s1MAG);
                    sdao.Add(s2MAG);
                    sdao.Add(s3MAG);
                    sdao.Add(s4MAG);

                    sdao.Add(sConc);
                    sdao.Add(sSubseq);
                    sdao.Add(sMista);
                    sdao.Add(sSegInteg);

                    sdao.Add(sIni);
                    sdao.Add(sFim);
                    sdao.Add(sIniFim);
                    sdao.Add(sEnsMed);
                    sdao.Add(sIntProfFund);
                    sdao.Add(sIntProfMed);

                    TipoEnsinoDAO tedao = new TipoEnsinoDAO();
                    TipoEnsino tINF = new TipoEnsino() { Descricao = "EDUCAÇÃO INFANTIL", Abreviatura = "INF", FlagAtivo = "S" };
                    TipoEnsino tFUND8 = new TipoEnsino() { Descricao = "ENSINO FUNDAMENTAL (8 ANOS)", Abreviatura = "FUND8", FlagAtivo = "S" };
                    TipoEnsino tFUND9 = new TipoEnsino() { Descricao = "ENSINO FUNDAMENTAL (9 ANOS)", Abreviatura = "FUND9", FlagAtivo = "S" };
                    TipoEnsino tFUND89 = new TipoEnsino() { Descricao = "ENSINO FUNDAMENTAL (8 E 9 ANOS)", Abreviatura = "FUND8E9", FlagAtivo = "S" };
                    TipoEnsino tMED = new TipoEnsino() { Descricao = "ENSINO MÉDIO", Abreviatura = "MED", FlagAtivo = "S" };
                    TipoEnsino tPROF = new TipoEnsino() { Descricao = "EDUCAÇÃO PROFISSIONAL", Abreviatura = "PROF", FlagAtivo = "S" };

                    TipoEnsino tEJAPres = new TipoEnsino() { Descricao = "EJA PRESENCIAL", Abreviatura = "EJA PRES", FlagAtivo = "S" };
                    TipoEnsino tEJASemi = new TipoEnsino() { Descricao = "EJA SEMIPRESENCIAL", Abreviatura = "EJA SEMIPRES", FlagAtivo = "S" };

                    tedao.Add(tINF);
                    tedao.Add(tFUND8);
                    tedao.Add(tFUND9);
                    tedao.Add(tFUND89);
                    tedao.Add(tMED);
                    tedao.Add(tPROF);
                    tedao.Add(tEJAPres);
                    tedao.Add(tEJASemi);

                    EtapaDAO etpdao = new EtapaDAO();

                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tINF, Serie = sCreche, Sequencia = 0, ValorEducacenso = 1, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tINF, Serie = sPre, Sequencia = 0, ValorEducacenso = 2, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tINF, Serie = sUni, Sequencia = 0, ValorEducacenso = 3, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tINF, Serie = sMulti, Sequencia = 0, ValorEducacenso = 56, FlagAtivo = "S" });
                    
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tFUND8, Serie = s1S, Sequencia = 1, ValorEducacenso = 4, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tFUND8, Serie = s2S, Sequencia = 2, ValorEducacenso = 5, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tFUND8, Serie = s3S, Sequencia = 3, ValorEducacenso = 6, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tFUND8, Serie = s4S, Sequencia = 4, ValorEducacenso = 7, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tFUND8, Serie = s5S, Sequencia = 5, ValorEducacenso = 8, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tFUND8, Serie = s6S, Sequencia = 6, ValorEducacenso = 9, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tFUND8, Serie = s7S, Sequencia = 7, ValorEducacenso = 10, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tFUND8, Serie = s8S, Sequencia = 8, ValorEducacenso = 11, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tFUND8, Serie = sMulti, Sequencia = 0, ValorEducacenso = 12, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tFUND8, Serie = sCorr, Sequencia = 0, ValorEducacenso = 13, FlagAtivo = "S" });
                    
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tFUND9, Serie = s1A, Sequencia = 1, ValorEducacenso = 14, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tFUND9, Serie = s2A, Sequencia = 2, ValorEducacenso = 15, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tFUND9, Serie = s3A, Sequencia = 3, ValorEducacenso = 16, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tFUND9, Serie = s4A, Sequencia = 4, ValorEducacenso = 17, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tFUND9, Serie = s5A, Sequencia = 5, ValorEducacenso = 18, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tFUND9, Serie = s6A, Sequencia = 6, ValorEducacenso = 19, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tFUND9, Serie = s7A, Sequencia = 7, ValorEducacenso = 20, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tFUND9, Serie = s8A, Sequencia = 8, ValorEducacenso = 21, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tFUND9, Serie = s9A, Sequencia = 9, ValorEducacenso = 41, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tFUND9, Serie = sMulti, Sequencia = 0, ValorEducacenso = 22, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tFUND9, Serie = sCorr, Sequencia = 0, ValorEducacenso = 23, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tFUND89, Serie = sMulti89, Sequencia = 0, ValorEducacenso = 24, FlagAtivo = "S" });
                    
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tMED, Serie = s1S, Sequencia = 1, ValorEducacenso = 25, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tMED, Serie = s2S, Sequencia = 2, ValorEducacenso = 26, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tMED, Serie = s3S, Sequencia = 3, ValorEducacenso = 27, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tMED, Serie = s4S, Sequencia = 4, ValorEducacenso = 28, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tMED, Serie = sNaoSer, Sequencia = 0, ValorEducacenso = 29, FlagAtivo = "S" });
                    
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tMED, Serie = s1SI, Sequencia = 1, ValorEducacenso = 30, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tMED, Serie = s2SI, Sequencia = 2, ValorEducacenso = 31, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tMED, Serie = s3SI, Sequencia = 3, ValorEducacenso = 32, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tMED, Serie = s4SI, Sequencia = 4, ValorEducacenso = 33, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tMED, Serie = sINTNS, Sequencia = 0, ValorEducacenso = 34, FlagAtivo = "S" });

                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tMED, Serie = s1MAG, Sequencia = 1, ValorEducacenso = 35, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tMED, Serie = s2MAG, Sequencia = 2, ValorEducacenso = 36, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tMED, Serie = s3MAG, Sequencia = 3, ValorEducacenso = 37, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tMED, Serie = s4MAG, Sequencia = 4, ValorEducacenso = 38, FlagAtivo = "S" });

                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tPROF, Serie = sConc, Sequencia = 0, ValorEducacenso = 39, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tPROF, Serie = sSubseq, Sequencia = 0, ValorEducacenso = 40, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tPROF, Serie = sMista, Sequencia = 0, ValorEducacenso = 64, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modREG, TipoEnsino = tPROF, Serie = sSegInteg, Sequencia = 0, ValorEducacenso = 66, FlagAtivo = "S" });
                    
                    // MODALIDADE EDUCAÇÃO ESPECIAL

                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tINF, Serie = sCreche, Sequencia = 0, ValorEducacenso = 1, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tINF, Serie = sPre, Sequencia = 0, ValorEducacenso = 2, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tINF, Serie = sUni, Sequencia = 0, ValorEducacenso = 3, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tINF, Serie = sMulti, Sequencia = 0, ValorEducacenso = 56, FlagAtivo = "S" });

                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tFUND8, Serie = s1S, Sequencia = 1, ValorEducacenso = 4, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tFUND8, Serie = s2S, Sequencia = 2, ValorEducacenso = 5, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tFUND8, Serie = s3S, Sequencia = 3, ValorEducacenso = 6, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tFUND8, Serie = s4S, Sequencia = 4, ValorEducacenso = 7, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tFUND8, Serie = s5S, Sequencia = 5, ValorEducacenso = 8, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tFUND8, Serie = s6S, Sequencia = 6, ValorEducacenso = 9, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tFUND8, Serie = s7S, Sequencia = 7, ValorEducacenso = 10, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tFUND8, Serie = s8S, Sequencia = 8, ValorEducacenso = 11, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tFUND8, Serie = sMulti, Sequencia = 0, ValorEducacenso = 12, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tFUND8, Serie = sCorr, Sequencia = 0, ValorEducacenso = 13, FlagAtivo = "S" });

                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tFUND9, Serie = s1A, Sequencia = 1, ValorEducacenso = 14, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tFUND9, Serie = s2A, Sequencia = 2, ValorEducacenso = 15, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tFUND9, Serie = s3A, Sequencia = 3, ValorEducacenso = 16, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tFUND9, Serie = s4A, Sequencia = 4, ValorEducacenso = 17, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tFUND9, Serie = s5A, Sequencia = 5, ValorEducacenso = 18, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tFUND9, Serie = s6A, Sequencia = 6, ValorEducacenso = 19, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tFUND9, Serie = s7A, Sequencia = 7, ValorEducacenso = 20, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tFUND9, Serie = s8A, Sequencia = 8, ValorEducacenso = 21, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tFUND9, Serie = s9A, Sequencia = 9, ValorEducacenso = 41, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tFUND9, Serie = sMulti, Sequencia = 0, ValorEducacenso = 22, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tFUND9, Serie = sCorr, Sequencia = 0, ValorEducacenso = 23, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tFUND89, Serie = sMulti89, Sequencia = 0, ValorEducacenso = 24, FlagAtivo = "S" });

                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tMED, Serie = s1S, Sequencia = 1, ValorEducacenso = 25, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tMED, Serie = s2S, Sequencia = 2, ValorEducacenso = 26, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tMED, Serie = s3S, Sequencia = 3, ValorEducacenso = 27, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tMED, Serie = s4S, Sequencia = 4, ValorEducacenso = 28, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tMED, Serie = sNaoSer, Sequencia = 0, ValorEducacenso = 29, FlagAtivo = "S" });

                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tMED, Serie = s1SI, Sequencia = 1, ValorEducacenso = 30, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tMED, Serie = s2SI, Sequencia = 2, ValorEducacenso = 31, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tMED, Serie = s3SI, Sequencia = 3, ValorEducacenso = 32, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tMED, Serie = s4SI, Sequencia = 4, ValorEducacenso = 33, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tMED, Serie = sINTNS, Sequencia = 0, ValorEducacenso = 34, FlagAtivo = "S" });

                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tMED, Serie = s1MAG, Sequencia = 1, ValorEducacenso = 35, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tMED, Serie = s2MAG, Sequencia = 2, ValorEducacenso = 36, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tMED, Serie = s3MAG, Sequencia = 3, ValorEducacenso = 37, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tMED, Serie = s4MAG, Sequencia = 4, ValorEducacenso = 38, FlagAtivo = "S" });

                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tPROF, Serie = sConc, Sequencia = 0, ValorEducacenso = 39, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tPROF, Serie = sSubseq, Sequencia = 0, ValorEducacenso = 40, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tPROF, Serie = sMista, Sequencia = 0, ValorEducacenso = 64, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tPROF, Serie = sSegInteg, Sequencia = 0, ValorEducacenso = 66, FlagAtivo = "S" });

                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tEJAPres, Serie = sIni, Sequencia = 0, ValorEducacenso = 43, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tEJAPres, Serie = sFim, Sequencia = 0, ValorEducacenso = 44, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tEJAPres, Serie = sIniFim, Sequencia = 0, ValorEducacenso = 51, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tEJAPres, Serie = sEnsMed, Sequencia = 0, ValorEducacenso = 45, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tEJAPres, Serie = sIntProfFund, Sequencia = 0, ValorEducacenso = 60, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tEJAPres, Serie = sIntProfMed, Sequencia = 0, ValorEducacenso = 62, FlagAtivo = "S" });

                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tEJASemi, Serie = sIni, Sequencia = 0, ValorEducacenso = 46, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tEJASemi, Serie = sFim, Sequencia = 0, ValorEducacenso = 47, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tEJASemi, Serie = sIniFim, Sequencia = 0, ValorEducacenso = 58, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tEJASemi, Serie = sEnsMed, Sequencia = 0, ValorEducacenso = 48, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tEJASemi, Serie = sIntProfFund, Sequencia = 0, ValorEducacenso = 61, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modESP, TipoEnsino = tEJASemi, Serie = sIntProfMed, Sequencia = 0, ValorEducacenso = 63, FlagAtivo = "S" });

                    // EJA PRESENCIAL
                    etpdao.Add(new Etapa() { Modalidade = modEJA, TipoEnsino = tEJAPres, Serie = sIni, Sequencia = 0, ValorEducacenso = 43, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modEJA, TipoEnsino = tEJAPres, Serie = sFim, Sequencia = 0, ValorEducacenso = 44, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modEJA, TipoEnsino = tEJAPres, Serie = sIniFim, Sequencia = 0, ValorEducacenso = 51, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modEJA, TipoEnsino = tEJAPres, Serie = sEnsMed, Sequencia = 0, ValorEducacenso = 45, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modEJA, TipoEnsino = tEJAPres, Serie = sIntProfFund, Sequencia = 0, ValorEducacenso = 60, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modEJA, TipoEnsino = tEJAPres, Serie = sIntProfMed, Sequencia = 0, ValorEducacenso = 62, FlagAtivo = "S" });

                    etpdao.Add(new Etapa() { Modalidade = modEJA, TipoEnsino = tEJASemi, Serie = sIni, Sequencia = 0, ValorEducacenso = 46, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modEJA, TipoEnsino = tEJASemi, Serie = sFim, Sequencia = 0, ValorEducacenso = 47, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modEJA, TipoEnsino = tEJASemi, Serie = sIniFim, Sequencia = 0, ValorEducacenso = 58, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modEJA, TipoEnsino = tEJASemi, Serie = sEnsMed, Sequencia = 0, ValorEducacenso = 48, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modEJA, TipoEnsino = tEJASemi, Serie = sIntProfFund, Sequencia = 0, ValorEducacenso = 61, FlagAtivo = "S" });
                    etpdao.Add(new Etapa() { Modalidade = modEJA, TipoEnsino = tEJASemi, Serie = sIntProfMed, Sequencia = 0, ValorEducacenso = 63, FlagAtivo = "S" });


                    EtapaEscolaDAO etescdao = new EtapaEscolaDAO();
                    etescdao.Add(new EtapaEscola() { Descricao= "Educação Infantil - Creche (0 a 3 anos)", Modalidade=modREG, ValorEducacenso=98, FlagAtivo="S"});
                    etescdao.Add(new EtapaEscola() { Descricao = "Educação Infantil - Pré-escola (4 e 5 anos)", Modalidade = modREG, ValorEducacenso = 99, FlagAtivo = "S" });
                    etescdao.Add(new EtapaEscola() { Descricao= "Ensino Fundamental 8 anos", Modalidade=modREG, ValorEducacenso=100, FlagAtivo="S"});
                    etescdao.Add(new EtapaEscola() { Descricao= "Ensino Fundamental 9 anos", Modalidade=modREG, ValorEducacenso=101, FlagAtivo="S"});
                    etescdao.Add(new EtapaEscola() { Descricao= "Ensino Médio - Médio", Modalidade=modREG, ValorEducacenso=102, FlagAtivo="S"});
                    etescdao.Add(new EtapaEscola() { Descricao= "Ensino Médio - Integrado", Modalidade=modREG, ValorEducacenso=103, FlagAtivo="S"});
                    etescdao.Add(new EtapaEscola() { Descricao= "Ensino Médio - Normal/Magistério", Modalidade=modREG, ValorEducacenso=104, FlagAtivo="S"});
                    etescdao.Add(new EtapaEscola() { Descricao= "Ensino Médio - Educação Profissional", Modalidade=modREG, ValorEducacenso=105, FlagAtivo="S"});
                    
                    etescdao.Add(new EtapaEscola() { Descricao= "Educação Infantil - Creche (0 a 3 anos)", Modalidade=modESP, ValorEducacenso=106, FlagAtivo="S"});
                    etescdao.Add(new EtapaEscola() { Descricao= "Educação Infantil - Pré-escola (4 e 5 anos)", Modalidade=modESP, ValorEducacenso=107, FlagAtivo="S"});
                    etescdao.Add(new EtapaEscola() { Descricao= "Ensino Fundamental 8 anos", Modalidade=modESP, ValorEducacenso=108, FlagAtivo="S"});
                    etescdao.Add(new EtapaEscola() { Descricao= "Ensino Fundamental 9 anos", Modalidade=modESP, ValorEducacenso=109, FlagAtivo="S"});
                    etescdao.Add(new EtapaEscola() { Descricao= "Ensino Médio - Médio", Modalidade=modESP, ValorEducacenso=110, FlagAtivo="S"});
                    etescdao.Add(new EtapaEscola() { Descricao= "Ensino Médio - Integrado", Modalidade=modESP, ValorEducacenso=111, FlagAtivo="S"});
                    etescdao.Add(new EtapaEscola() { Descricao= "Ensino Médio - Normal/Magistério", Modalidade=modESP, ValorEducacenso=112, FlagAtivo="S"});
                    etescdao.Add(new EtapaEscola() { Descricao= "Educação Profissional", Modalidade=modESP, ValorEducacenso=113, FlagAtivo="S"});
                    etescdao.Add(new EtapaEscola() { Descricao= "EJA Ensino Fundamental", Modalidade=modESP, ValorEducacenso=114, FlagAtivo="S"});
                    etescdao.Add(new EtapaEscola() { Descricao= "EJA Ensino Médio", Modalidade=modESP, ValorEducacenso=115, FlagAtivo="S"});
                    
                    etescdao.Add(new EtapaEscola() { Descricao= "Ensino Fundamental", Modalidade=modEJA, ValorEducacenso=116, FlagAtivo="S"});
                    etescdao.Add(new EtapaEscola() { Descricao= "Ensino Fundamental - Projovem (urbano)", Modalidade=modEJA, ValorEducacenso=117, FlagAtivo="S"});
                    etescdao.Add(new EtapaEscola() { Descricao= "Ensino Médio", Modalidade=modEJA, ValorEducacenso=118, FlagAtivo="S"});


                    TipoAtendimentoDAO tatddao = new TipoAtendimentoDAO();
                    tatddao.Add(new TipoAtendimento() { Descricao = "Classe Hospitalar", ValorEducacenso = 1, FlagAtivo = "S" });
                    tatddao.Add(new TipoAtendimento() { Descricao = "Atividade Complementar", ValorEducacenso = 4, FlagAtivo = "S" });
                    tatddao.Add(new TipoAtendimento() { Descricao = "Unidade de Internação Socioeducativa", ValorEducacenso = 2, FlagAtivo = "S" });
                    tatddao.Add(new TipoAtendimento() { Descricao = "Atendimento Educacional Especializado (AEE)", ValorEducacenso = 5, FlagAtivo = "S" });
                    tatddao.Add(new TipoAtendimento() { Descricao = "Unidade Prisional", ValorEducacenso = 3, FlagAtivo = "S" });
                    tatddao.Add(new TipoAtendimento() { Descricao = "Não Se Aplica", ValorEducacenso = 0, FlagAtivo = "S" });
                    
                    TipoAEEDAO taeedao = new TipoAEEDAO();
                    taeedao.Add(new TipoAEE() { Descricao = "Ensino do Sistema Braille", ValorEducacenso = 25, FlagAtivo = "S" });
                    taeedao.Add(new TipoAEE() { Descricao = "Ensino do uso de recursos ópticos", ValorEducacenso = 26, FlagAtivo = "S" });
                    taeedao.Add(new TipoAEE() { Descricao = "Estratégias para o desenvolvimento de processos mentais", ValorEducacenso = 27, FlagAtivo = "S" });
                    taeedao.Add(new TipoAEE() { Descricao = "Técnicas de orientação e mobilidade", ValorEducacenso = 28, FlagAtivo = "S" });
                    taeedao.Add(new TipoAEE() { Descricao = "Ensino da Língua Brasileira de Sinais (Libras)", ValorEducacenso = 29, FlagAtivo = "S" });
                    taeedao.Add(new TipoAEE() { Descricao = "Ensino do uso da Comunicação Alternativa e Aumentativa (CAA)", ValorEducacenso = 30, FlagAtivo = "S" });
                    taeedao.Add(new TipoAEE() { Descricao = "Estratégias para enriquecimento curricular", ValorEducacenso = 31, FlagAtivo = "S" });
                    taeedao.Add(new TipoAEE() { Descricao = "Ensino do uso do Soroban", ValorEducacenso = 32, FlagAtivo = "S" });
                    taeedao.Add(new TipoAEE() { Descricao = "Ensino da usabilidade e da funcionalidade da informática acessível", ValorEducacenso = 33, FlagAtivo = "S" });
                    taeedao.Add(new TipoAEE() { Descricao = "Ensino da Língua Portuguesa na modalidade escrita", ValorEducacenso = 34, FlagAtivo = "S" });
                    taeedao.Add(new TipoAEE() { Descricao = "Estratégias para a autonomia no ambiente escolar", ValorEducacenso = 35, FlagAtivo = "S" });

                    TipoComplementarDAO tcmpdao = new TipoComplementarDAO();

                    tcmpdao.Add(new TipoComplementar() { Descricao = "História da Música e Teoria Musical", ValorEducacenso = "11001", FlagAtivo = "S" });
                    tcmpdao.Add(new TipoComplementar() { Descricao = "Canto Coral", ValorEducacenso = "11002", FlagAtivo = "S" });
                    tcmpdao.Add(new TipoComplementar() { Descricao = "Ensino Coletivo de Cordas (Piano, Violão, Guitarra, Violino), Flauta Doce, Trompete, etc.", ValorEducacenso = "11003", FlagAtivo = "S" });
                    tcmpdao.Add(new TipoComplementar() { Descricao = "Banda Fanfarra, Percussão", ValorEducacenso = "11004", FlagAtivo = "S" });
                    tcmpdao.Add(new TipoComplementar() { Descricao = "Hip Hop", ValorEducacenso = "11005", FlagAtivo = "S" });
                    tcmpdao.Add(new TipoComplementar() { Descricao = "História da Arte", ValorEducacenso = "12001", FlagAtivo = "S" });

                    transaction.Commit();
                } // end transaction
            } // end session
        } // end method
    }
}
