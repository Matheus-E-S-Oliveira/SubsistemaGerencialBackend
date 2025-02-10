using SubsistemaGerencialBackend.Enums.FormasPagamentos;
using SubsistemaGerencialBackend.Enums.Planos;
using System.ComponentModel.DataAnnotations;

namespace SubsistemaGerencialBackend.Models.Dashboard
{
    public class DashboardDados
    {
        public int ClienteAtivos { get; set; }

        public int ClienteInativos { get; set; }

        public int ClienteInderteminados { get; set; }

        public int FormaPagamentoIndefinido { get; set; }

        public int FormaPagamentoDinheiro { get; set; }

        public int FormaPagamentoCartaoCredito { get; set; }

        public int FormaPagamentoPix { get; set; }

        public int FormaPagamentoBoleto { get; set; }

        public int PlanoIndefinido { get; set; }

        public int PlanoMensal { get; set; }

        public int PlanoAnual { get; set; }

        public int PlanoGratuito { get; set; }

        public int PlanoTeste { get; set; }

        public int LicencaIndefinido { get; set; }

        public int LicencaAtiva { get; set; }

        public int LicencaExpirado { get; set; }

        public int LicencaSuspenco { get; set; }

        public Dictionary<int, Dictionary<int, int>> LicencasPorAno { get; set; } = new Dictionary<int, Dictionary<int, int>>();

        public Dictionary<KeyValuePair<int, int>, decimal> ValoresAReceberPorMes { get; set; } = new Dictionary<KeyValuePair<int, int>, decimal>();

        public Dictionary<KeyValuePair<int, int>, Dictionary<int, int>> LicencasCompradasPorMes { get; set; } = new Dictionary<KeyValuePair<int, int>, Dictionary<int, int>>();

        public Dictionary<KeyValuePair<int, int>, decimal> ReceitaPorMes { get; set; } = new Dictionary<KeyValuePair<int, int>, decimal>();
    
        public DashboardDados()
        {
            int anoAtual = DateTime.Now.Year;
            int mesAtual = DateTime.Now.Month;

            for (int ano = anoAtual; ano >= anoAtual - 6; ano--)
            {
                LicencasPorAno[ano] = new Dictionary<int, int>();

                for (int i = 0; i < 12; i++)
                {
                    int mes = mesAtual - i;
                    if (mes <= 0)
                    {
                        mes += 12; 
                        ano--; 
                    }

                    LicencasCompradasPorMes[new (ano, mes)] = new Dictionary<int, int>();
                    ReceitaPorMes[new (ano, mes)] = 0.0m;  
                    ValoresAReceberPorMes[new (ano, mes)] = 0.0m;  
                }
            }
        }
 
    }
    public class ClientesResumo
    {
        public int ClienteAtivos { get; set; }

        public int ClienteInativos { get; set; }

        public int ClienteInderteminados { get; set; }

    }
    public class FormasPagamentoResumo
    {
        public int FormaPagamentoIndefinido { get; set; }

        public int FormaPagamentoDinheiro { get; set; }

        public int FormaPagamentoCartaoCredito { get; set; }

        public int FormaPagamentoPix { get; set; }

        public int FormaPagamentoBoleto { get; set; }

        public int TotalFormaPagamentoIndefinido { get; set; }

        public int TotalFormaPagamentoDinheiro { get; set; }

        public int TotalFormaPagamentoCartaoCredito { get; set; }

        public int TotalFormaPagamentoPix { get; set; }

        public int TotalFormaPagamentoBoleto { get; set; }
    }
    public class PlanoResumo()
    {
        public int PlanoIndefinido { get; set; }

        public int PlanoMensal { get; set; }

        public int PlanoAnual { get; set; }

        public int PlanoGratuito { get; set; }

        public int PlanoTeste { get; set; }

        public int TotalPlanoIndefinido { get; set; }

        public int TotalPlanoMensal { get; set; }

        public int TotalPlanoAnual { get; set; }

        public int TotalPlanoGratuito { get; set; }

        public int TotalPlanoTeste { get; set; }
    }
    public class LicencaResumo()
    {
        public int TotalLicencaIndefinido { get; set; }

        public int TotalLicencaAtiva { get; set; }

        public int TotalLicencaExpirado { get; set; }

        public int TotalLicencaSuspenco { get; set; }

        public int LicencaIndefinido { get; set; }

        public int LicencaAtiva { get; set; }

        public int LicencaExpirado { get; set; }

        public int LicencaSuspenco { get; set; }
    }
    public class TotalAnoResumo()
    {
        public Dictionary<int, Dictionary<int, int>> LicencasPorAno { get; set; } = new Dictionary<int, Dictionary<int, int>>();
    }
    public class DadosMensais()
    {
        public Dictionary<string, decimal> ReceitaPorMes { get; set; } = new Dictionary<string, decimal>();

        public Dictionary<string, decimal> ValoresAReceberAcumulado { get; set; } = new Dictionary<string, decimal>();

        public Dictionary<string, decimal> ReceitaAcumulada { get; set; } = new Dictionary<string, decimal>();

        public Dictionary<string, decimal> ValoresAReceberPorMes { get; set; } = new Dictionary<string, decimal>();

        public Dictionary<string, Dictionary<int, int>> LicencasCompradasPorMes { get; set; } = new Dictionary<string, Dictionary<int, int>>();

    }

    public class DadosGerais
    {
        public DateTime DataReferencia { get; set; }

        public string MesAno => DataReferencia.ToString("MM/yyyy");

        public Dictionary<int, Dictionary<int, int>> DistribuicaoLicencasPorAno { get; set; } = new Dictionary<int, Dictionary<int, int>>();

        public Dictionary<string, decimal> ValoresAReceberPorMes { get; set; } = new Dictionary<string, decimal>();

        public Dictionary<string, decimal> ReceitaPorMes { get; set; } = new Dictionary<string, decimal>();

        public Dictionary<string, Dictionary<int, int>> DistribuicaoLicencasPorMes { get; set; } = new Dictionary<string, Dictionary<int, int>>();

        public Dictionary<int, int> FormasPagamento { get; set; } = new Dictionary<int, int>();

        public Dictionary<int, int> Planos { get; set; } = new Dictionary<int, int>();
    }

    public class DadosGeraisEntity : Base
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime DataReferencia { get; set; }

        public string MesAno { get; set; } = string.Empty;

            // JSON para armazenar os dados agregados sem criar tabelas separadas
        public string DistribuicaoLicencasPorAnoJson { get; set; } = string.Empty;
        public string ValoresAReceberPorMesJson { get; set; } = string.Empty;
        public string ReceitaPorMesJson { get; set; } = string.Empty;
        public string DistribuicaoLicencasPorMesJson { get; set; } = string.Empty;
        public string FormasPagamentoJson { get; set; } = string.Empty;
        public string PlanosJson { get; set; } = string.Empty;
  
    }
    public class DadosDiversos
    {
        public int BoletosEmitidosEsteMes { get; set; }

        public int FaturaPertoDoVencimento { get; set; }

        public int FaturaVencidas { get; set; }

        public int LicencaVencida { get; set; }

        public int LicencaPertoDeVencer {  get; set; }
    }
    public class Contratos
    {
        public int Assindo { get; set; }

        public int NAssinado { get; set; }

        public int Indefinido { get; set; }
        public int Ativo { get; set; }

        public int Inativo { get; set; }
        
        public int Gratuito { get; set; }
        
        public int Teste { get; set; }
    }




}
