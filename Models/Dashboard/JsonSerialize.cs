//using SubsistemaGerencialBackend.AppDbContexts;
//using System.Text.Json;

//namespace SubsistemaGerencialBackend.Models.Dashboard
//{
//    public class JsonSerialize
//    {
//    private async Task SalvarDadosGeraisNoBanco(DadosGerais dadosGerais, AppDbContext context)
//        {
//            var mesAno = dadosGerais.MesAno;

//            var dadoExistente = await context.DadosMensais
//                .FirstOrDefaultAsync(d => d.MesAno == mesAno);

//            if (dadoExistente != null)
//            {
//                // Atualiza apenas se for o mês atual
//                if (mesAno == $"{DateTime.Now.Month:D2}/{DateTime.Now.Year}")
//                {
//                    dadoExistente.DistribuicaoLicencasPorAno = JsonSerializer.Serialize(dadosGerais.DistribuicaoLicencasPorAno);
//                    dadoExistente.ValoresAReceberPorMes = JsonSerializer.Serialize(dadosGerais.ValoresAReceberPorMes);
//                    dadoExistente.ReceitaPorMes = JsonSerializer.Serialize(dadosGerais.ReceitaPorMes);
//                    dadoExistente.DistribuicaoLicencasPorMes = JsonSerializer.Serialize(dadosGerais.DistribuicaoLicencasPorMes);
//                    dadoExistente.FormasPagamento = JsonSerializer.Serialize(dadosGerais.FormasPagamento);
//                    dadoExistente.Planos = JsonSerializer.Serialize(dadosGerais.Planos);
//                    dadoExistente.UltimaAtualizacao = DateTime.UtcNow;

//                    context.DadosMensais.Update(dadoExistente);
//                }
//            }
//            else
//            {
//                var novoDado = new DadosMensaisEntity
//                {
//                    DataReferencia = dadosGerais.DataReferencia,
//                    DistribuicaoLicencasPorAno = JsonSerializer.Serialize(dadosGerais.DistribuicaoLicencasPorAno),
//                    ValoresAReceberPorMes = JsonSerializer.Serialize(dadosGerais.ValoresAReceberPorMes),
//                    ReceitaPorMes = JsonSerializer.Serialize(dadosGerais.ReceitaPorMes),
//                    DistribuicaoLicencasPorMes = JsonSerializer.Serialize(dadosGerais.DistribuicaoLicencasPorMes),
//                    FormasPagamento = JsonSerializer.Serialize(dadosGerais.FormasPagamento),
//                    Planos = JsonSerializer.Serialize(dadosGerais.Planos)
//                };

//                await context.DadosMensais.AddAsync(novoDado);
//            }

//            await context.SaveChangesAsync();
//        }
//        }
//}
