using Microsoft.EntityFrameworkCore;
using SubsistemaGerencialBackend.AppDbContexts;
using SubsistemaGerencialBackend.Enums.FormasPagamentos;
using SubsistemaGerencialBackend.Enums.Planos;
using SubsistemaGerencialBackend.Enums.SituacaoClientes;
using SubsistemaGerencialBackend.Enums.StatusLicencas;
using SubsistemaGerencialBackend.Enums.StatusPagamentos;
using System.Text.Json.Serialization;
using System.Text.Json;
using SubsistemaGerencialBackend.Enums.SituacaoContratos;

namespace SubsistemaGerencialBackend.Models.Dashboard
{
    public class DashboardRepository(AppDbContext context) : IDashboardRepository
    {
        public async Task<DashboardDados> GetDashboardDataAsync()
        {
            var dashboardDados = new DashboardDados();

            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;

            for (int ano = currentYear; ano >= currentYear - 6; ano--)
            {
                // Licenças por ano, considerando todas as licenças, agrupando por StatusLicenca
                var licencasPorAno = await context.Licencas
                    .Where(l => l.DataInico.HasValue && l.DataInico.Value.Year == ano)
                    .GroupBy(l => l.StatusLicenca) // Agrupar por StatusLicenca
                    .Select(g => new { StatusLicenca = g.Key, Count = g.Count() })
                    .ToListAsync();

                // Agora vamos preencher o dicionário de LicencasPorAno
                var statusLicencasAno = new Dictionary<int, int>();

                foreach (var licenca in licencasPorAno)
                {
                    statusLicencasAno[(int)licenca.StatusLicenca] = licenca.Count;
                }

                // Atribuindo os valores ao dicionário LicencasPorAno, com chave sendo o ano
                dashboardDados.LicencasPorAno[ano] = statusLicencasAno;
            }

            return dashboardDados;
        }
        

        public async Task<ClientesResumo> GetClientesResumo()
        {
            var resumo = new ClientesResumo
            {
                ClienteAtivos = await context.Clientes.CountAsync(c => c.Situacao == SituacaoCliente.Ativo),
                ClienteInativos = await context.Clientes.CountAsync(c => c.Situacao == SituacaoCliente.Inativo),
                ClienteInderteminados = await context.Clientes.CountAsync(c => c.Situacao == SituacaoCliente.Indefinido)
            };
            return resumo;
        }
        public async Task<FormasPagamentoResumo> GetFormasPagamentoResumo()
        {
            var ultimaLicencaPorCliente = await context.Licencas
                    .GroupBy(l => l.ClienteId)
                    .Select(group => new {
                        ClienteId = group.Key,
                        UltimaLicencaId = group.OrderByDescending(l => l.DataCriacao).FirstOrDefault()!.Id
                    })
                    .ToListAsync();

            // Usar 'AsEnumerable' para carregar os pagamentos e fazer a comparação na memória do cliente
            var ultimaLicencaIds = ultimaLicencaPorCliente.Select(l => l.UltimaLicencaId).ToList();

            var ultimaLicencaPagamentos = await context.DetalhesPagamentos
                .Where(dp => ultimaLicencaIds.Contains(dp.LicencaId))  // Filtra usando a lista carregada em memória
                .ToListAsync();


            var resumo = new FormasPagamentoResumo
            {
                TotalFormaPagamentoDinheiro = await context.DetalhesPagamentos.CountAsync(p => p.FormaPagamneto == FormaPagamento.Dinheiro),
                TotalFormaPagamentoCartaoCredito = await context.DetalhesPagamentos.CountAsync(p => p.FormaPagamneto == FormaPagamento.CartaoCredito),
                TotalFormaPagamentoPix = await context.DetalhesPagamentos.CountAsync(p => p.FormaPagamneto == FormaPagamento.Pix),
                TotalFormaPagamentoBoleto = await context.DetalhesPagamentos.CountAsync(p => p.FormaPagamneto == FormaPagamento.Boleto),
                TotalFormaPagamentoIndefinido = await context.DetalhesPagamentos.CountAsync(p => p.FormaPagamneto == FormaPagamento.Indefinido),
                FormaPagamentoDinheiro = ultimaLicencaPagamentos.Count(l => l.FormaPagamneto == FormaPagamento.Dinheiro),
                FormaPagamentoCartaoCredito = ultimaLicencaPagamentos.Count(l => l.FormaPagamneto == FormaPagamento.CartaoCredito),
                FormaPagamentoPix = ultimaLicencaPagamentos.Count(l => l.FormaPagamneto == FormaPagamento.Pix),
                FormaPagamentoBoleto = ultimaLicencaPagamentos.Count(l => l.FormaPagamneto == FormaPagamento.Boleto),
                FormaPagamentoIndefinido = ultimaLicencaPagamentos.Count(l => l.FormaPagamneto == FormaPagamento.Indefinido),
            };
            return resumo;
        }

        public async Task<PlanoResumo> GetPlanoResumo()
        {
            var ultimaLicencaPorCliente = await context.Licencas
                        .GroupBy(l => l.ClienteId)
                        .ToDictionaryAsync(
                            group => group.Key,
                            group => group.OrderByDescending(l => l.DataCriacao).FirstOrDefault()
                        );
            var resumo = new PlanoResumo
            {
                TotalPlanoMensal = await context.Licencas.CountAsync(p => p.Plano == Plano.Mensal),
                TotalPlanoAnual = await context.Licencas.CountAsync(p => p.Plano == Plano.Anual),
                TotalPlanoGratuito = await context.Licencas.CountAsync(p => p.Plano == Plano.Gratuito),
                TotalPlanoTeste = await context.Licencas.CountAsync(p => p.Plano == Plano.Teste),
                TotalPlanoIndefinido = await context.Licencas.CountAsync(p => p.Plano == Plano.Indefinido),
                PlanoMensal = ultimaLicencaPorCliente.Values
                    .Count(l => l != null && l.Plano == Plano.Mensal),
                PlanoAnual = ultimaLicencaPorCliente.Values
                    .Count(l => l != null && l.Plano == Plano.Anual),
                PlanoTeste = ultimaLicencaPorCliente.Values
                    .Count(l => l != null && l.Plano == Plano.Teste),
                PlanoGratuito = ultimaLicencaPorCliente.Values
                    .Count(l => l != null && l.Plano == Plano.Gratuito),
                PlanoIndefinido = ultimaLicencaPorCliente.Values
                    .Count(l => l != null && l.Plano == Plano.Indefinido),
            };

            return resumo;
        }

        public async Task<LicencaResumo> GetLicencaResumo()
        {
            var ultimaLicencaPorCliente = await context.Licencas
                        .GroupBy(l => l.ClienteId)
                        .ToDictionaryAsync(
                            group => group.Key,
                            group => group.OrderByDescending(l => l.DataCriacao).FirstOrDefault()
                        );
            var resumo = new LicencaResumo
            {
                TotalLicencaAtiva = await context.Licencas.CountAsync(l => l.StatusLicenca == StatusLicenca.Ativa),
                TotalLicencaExpirado = await context.Licencas.CountAsync(l => l.StatusLicenca == StatusLicenca.Expirado),
                TotalLicencaIndefinido = await context.Licencas.CountAsync(l => l.StatusLicenca == StatusLicenca.Indefinido),
                TotalLicencaSuspenco = await context.Licencas.CountAsync(l => l.StatusLicenca == StatusLicenca.Suspenco),
                LicencaIndefinido = ultimaLicencaPorCliente.Values
                    .Count(l => l != null && l.StatusLicenca == StatusLicenca.Indefinido),

                LicencaAtiva = ultimaLicencaPorCliente.Values
                    .Count(l => l != null && l.StatusLicenca == StatusLicenca.Ativa),

                LicencaExpirado = ultimaLicencaPorCliente.Values
                    .Count(l => l != null && l.StatusLicenca == StatusLicenca.Expirado),

                LicencaSuspenco = ultimaLicencaPorCliente.Values
                    .Count(l => l != null && l.StatusLicenca == StatusLicenca.Suspenco),
            };

            return resumo;
        }

        public async Task<TotalAnoResumo> GetTotalLicencaAno()
        {
            var dashboardDados = new TotalAnoResumo
            {
                LicencasPorAno = new Dictionary<int, Dictionary<int, int>>() // Garante inicialização
            };

            var currentYear = DateTime.Now.Year;
            var minYear = currentYear - 5;

            // Busca todas as licenças de uma vez, filtrando apenas os anos necessários
            var licencas = await context.Licencas
                .Where(l => l.DataInico.HasValue && l.DataInico.Value.Year >= minYear)
                .GroupBy(l => new { l.DataInico!.Value.Year, l.Plano })
                .Select(g => new { g.Key.Year, Plano = (int)g.Key.Plano, Count = g.Count() })
                .ToListAsync();


            for (int ano = currentYear; ano >= minYear; ano--)
            {
                if (!dashboardDados.LicencasPorAno.ContainsKey(ano))
                {
                    dashboardDados.LicencasPorAno[ano] = new Dictionary<int, int>();
                }

                // Preenche todos os status de licença com 0, caso não existam dados para esse ano
                foreach (StatusLicenca status in Enum.GetValues(typeof(Plano)))
                {
                    var statusInt = (int)status;
                    if (!dashboardDados.LicencasPorAno[ano].ContainsKey(statusInt))
                    {
                        dashboardDados.LicencasPorAno[ano][statusInt] = 0; // Preenche com 0 se não houver dados para esse status
                    }
                }
            }

            // Processa os resultados agrupados
            foreach (var licenca in licencas)
            {
                dashboardDados.LicencasPorAno[licenca.Year][licenca.Plano] = licenca.Count;
            }

            return dashboardDados;
        }
        public async Task<DadosMensais> ObterDadosMensais()
        {
            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;

            var dashboardDados = new DadosMensais
            {
                ReceitaPorMes = new Dictionary<string, decimal>(),
                ValoresAReceberAcumulado = new Dictionary<string, decimal>(),
                ValoresAReceberPorMes = new Dictionary<string, decimal>(),
                LicencasCompradasPorMes = new Dictionary<string, Dictionary<int, int>>(),
                ReceitaAcumulada = new Dictionary<string, decimal>()
            };

            // Definindo o ano inicial e mês inicial (começando pelo mês atual e indo para trás)
            int anoInicial = currentYear;
            int mesInicial = currentMonth;
            decimal? valorAcumulado = 0.0m;
            decimal? receitaAcumulada = 0.0m;

            var receitas = new List<(string chaveMesAno, decimal somaValores)>();
            var valoresAReceber = new List<(string chaveMesAno, decimal somaValores)>();
            var licencasCompradas = new List<(string chaveMesAno, Dictionary<int, int> statusLicencas)>();
            var valorAReceberAcumulado = new List<(string chaveMesAno, decimal acumulado)>();
            var recitaAcumuladaList = new List<(string chaveMesAno, decimal acumulado)>();

            // Preencher para os últimos 12 meses
            for (int i = 0; i < 12; i++)
            {
                // Calcula o mês e ano para o intervalo de 12 meses
                int mes = mesInicial - i;
                int ano = anoInicial;

                // Se o mês for menor ou igual a 0 (dezembro do ano anterior), ajusta o mês e o ano
                if (mes <= 0)
                {
                    mes += 12;
                    ano--;
                }

                // Formato de chave MM/YYYY
                string chaveMesAno = $"{mes:D2}/{ano}";  // Exemplo: 01/2024, 02/2024

                // Receita por mês
                var receitasMes = await context.DetalhesPagamentos
                    .Where(r => r.DataLiquidacao.HasValue && r.DataLiquidacao.Value.Year == ano && r.DataLiquidacao.Value.Month == mes)
                    .GroupBy(r => new { r.DataLiquidacao!.Value.Year, r.DataLiquidacao.Value.Month })
                    .Select(g => new
                    {
                        Ano = g.Key.Year,
                        Mes = g.Key.Month,
                        SomaValores = g.Sum(r => r.ValorCobrado)
                    })
                    .FirstOrDefaultAsync(); // Busca uma única soma para o mês/ano

                // Se não encontrar valores, atribui 0
                receitas.Add((chaveMesAno, receitasMes?.SomaValores ?? 0.0m));

                // Valores a receber por mês
                var valoresAReceberMes = await context.DetalhesPagamentos
                    .Join(context.Licencas, pagamento => pagamento.LicencaId, outra => outra.Id, (pagamento, outra) => new { pagamento, outra })
                    .Where(p => p.pagamento.StatusPagamento != StatusPagamento.Pago
                                && p.outra.DataInico.HasValue
                                && p.outra.DataInico.Value.Year == ano && p.outra.DataInico.Value.Month == mes)
                    .GroupBy(p => new { p.outra.DataInico!.Value.Year, p.outra.DataInico.Value.Month })
                    .Select(g => new ValoresAReceberMes
                    {
                        Ano = g.Key.Year,
                        Mes = g.Key.Month,
                        SomaValores = g.Sum(p => p.pagamento.Valor)
                    })
                    .FirstOrDefaultAsync(); // Busca uma única soma para o mês/ano

                // Se não encontrar valores, atribui 0
                valoresAReceber.Add((chaveMesAno, valoresAReceberMes?.SomaValores ?? 0.0m));

                var todosStatus = new List<int> { 0, 1, 2, 3, 4 };
                // Licenças compradas por mês
                var licencasCompradasMes = await context.Licencas
                    .Where(l => l.DataInico.HasValue
                                && l.DataInico.Value.Year == ano && l.DataInico.Value.Month == mes)
                    .GroupBy(l => l.Plano)
                    .Select(g => new { Plano = g.Key, Count = g.Count() })
                    .ToListAsync();

                var statusLicencas = new Dictionary<int, int>();
                
                foreach (var status in todosStatus)
                {
                    statusLicencas[status] = 0;
                }

                foreach (var licenca in licencasCompradasMes)
                {
                    statusLicencas[(int)licenca.Plano] = licenca.Count;
                }

                licencasCompradas.Add((chaveMesAno, statusLicencas));
            }

            // Ordenando os dados por chaveMesAno
            var orderedReceitas = receitas.OrderBy(r => DateTime.ParseExact(r.chaveMesAno, "MM/yyyy", null)).ToList();
            var orderedValoresAReceber = valoresAReceber.OrderBy(r => DateTime.ParseExact(r.chaveMesAno, "MM/yyyy", null)).ToList();
            var orderedLicencas = licencasCompradas.OrderBy(r => DateTime.ParseExact(r.chaveMesAno, "MM/yyyy", null)).ToList();
            
            foreach (var item in orderedValoresAReceber)
            {
                var partes = item.chaveMesAno.Split('/');

                // Converter as partes para inteiros, se necessário
                int mes = int.Parse(partes[0]); // Mês
                int ano = int.Parse(partes[1]); // Ano
                valorAcumulado += item.somaValores;

                var calculaPagamento = await context.DetalhesPagamentos
                    .Where(dp => dp.StatusPagamento == StatusPagamento.PagamentoAtrasado
                        && dp.DataLiquidacao.HasValue
                        && dp.DataLiquidacao.Value.Month == mes
                        && dp.DataLiquidacao.Value.Year == ano)
                    .SumAsync(dp => dp.Valor);

                if (calculaPagamento > 0)
                {
                    valorAcumulado -= calculaPagamento;
                }

                valorAReceberAcumulado.Add((item.chaveMesAno, valorAcumulado ?? 0.0m));
            }
            foreach(var item in orderedReceitas)
            {
                receitaAcumulada += item.somaValores;

                recitaAcumuladaList.Add((item.chaveMesAno, receitaAcumulada ?? 0.0m));
            }

            var orderedValorAReceberAcumulado = valorAReceberAcumulado.OrderBy(r => DateTime.ParseExact(r.chaveMesAno, "MM/yyyy", null)).ToList();
            var orderedReceitaAcumulada = recitaAcumuladaList.OrderBy(r => DateTime.ParseExact(r.chaveMesAno, "MM/yyyy", null)).ToList();

            // Preenchendo os dicionários com os valores ordenados
            foreach (var receita in orderedReceitas)
            {
                dashboardDados.ReceitaPorMes[receita.chaveMesAno] = receita.somaValores;
            }

            foreach( var item in orderedReceitaAcumulada)
            {
                dashboardDados.ReceitaAcumulada[item.chaveMesAno] = item.acumulado;
            }

            foreach (var valores in orderedValoresAReceber)
            {
                dashboardDados.ValoresAReceberPorMes[valores.chaveMesAno] = valores.somaValores;
            }

            foreach (var licenca in orderedLicencas)
            {
                dashboardDados.LicencasCompradasPorMes[licenca.chaveMesAno] = licenca.statusLicencas;
            }

            foreach (var item in orderedValorAReceberAcumulado)
            {
                dashboardDados.ValoresAReceberAcumulado[item.chaveMesAno] = item.acumulado;
            }

            return dashboardDados;
        }

        public async Task<DadosDiversos> GetDadosDiversos()
        {
            var hoje = DateTime.Today;
            var dadosDiversos = new DadosDiversos
            {
                BoletosEmitidosEsteMes = await context.Boletos
                    .Where(b => b.DataEntrada.HasValue &&
                    (b.DataEntrada.Value.Month == DateTime.Now.Month) &&
                    (b.DataEntrada.Value.Year == DateTime.Now.Year))
                    .CountAsync(),
                FaturaPertoDoVencimento = await context.Boletos
                    .Where(b => b.DataVencimento.HasValue &&
                    (b.DataVencimento >= hoje) &&
                    (b.DataVencimento < hoje.AddDays(30)))
                    .CountAsync(),
                FaturaVencidas = await context.Boletos
                    .Where(b => b.DataVencimento.HasValue &&
                    (b.DataVencimento >= hoje.AddDays(-90)) &&
                    (b.DataVencimento < hoje))
                    .CountAsync(),
                LicencaVencida = await context.Licencas
                    .Where(l => l.DataVencimento.HasValue &&
                    (l.DataVencimento >= hoje.AddDays(-90)) &&
                    (l.DataVencimento < hoje))
                    .CountAsync(),

                LicencaPertoDeVencer = await context.Licencas
                    .Where(l => l.DataVencimento.HasValue &&
                    (l.DataVencimento >= hoje) &&
                    (l.DataVencimento < hoje.AddDays(30)))
                    .CountAsync(),
            };

            //DateTime hoje = DateTime.Today;
            //DateTime limite = hoje.AddDays(30);

            //var registros = await context.Tabela
            //    .Where(t => t.Data >= hoje && t.Data <= limite)
            //    .ToListAsync();

            //DateTime hoje = DateTime.Today;
            //DateTime limite = hoje.AddDays(-30);

            //var registros = await context.Tabela
            //    .Where(t => t.Data >= limite && t.Data <= hoje)
            //    .ToListAsync();



            return dadosDiversos;
        }

        public async Task<Contratos> GetContratos()
        {
            var resumo = new Contratos
            {
                Assindo = await context.ClienteContratos
                    .Where(cc => cc.AssinadoPeloPortal == true)
                    .CountAsync(),
                NAssinado = await context.ClienteContratos
                    .Where(cc => cc.AssinadoPeloPortal == false)
                    .CountAsync(),
                Ativo = await context.ClienteContratos
                    .Where(cc => cc.Situacao == SituacaoContrato.Ativo)
                    .CountAsync(),
                Inativo = await context.ClienteContratos
                    .Where(cc => cc.Situacao == SituacaoContrato.Inativo)
                    .CountAsync(),
                Gratuito = await context.ClienteContratos
                    .Where(cc => cc.Situacao == SituacaoContrato.Gratuito)
                    .CountAsync(),
                Teste = await context.ClienteContratos
                    .Where(cc => cc.Situacao == SituacaoContrato.Teste)
                    .CountAsync(),
                Indefinido = await context.ClienteContratos
                    .Where(cc => cc.Situacao == SituacaoContrato.Indefinido)
                    .CountAsync(),
            };

            return resumo;
        }
    }
}
public class ValoresAReceberMes
{
    public int Ano { get; set; }
    public int Mes { get; set; }
    public decimal? SomaValores { get; set; }
}
