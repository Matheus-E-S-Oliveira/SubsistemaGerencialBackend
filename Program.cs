using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using SubsistemaGerencialBackend.AppDbContexts;
using SubsistemaGerencialBackend.Enums.SituacaoClientes;
using SubsistemaGerencialBackend.Enums.SituacaoContratos;
using SubsistemaGerencialBackend.Models.ClienteContratos;
using SubsistemaGerencialBackend.Models.Clientes;
using SubsistemaGerencialBackend.Models.EnderecoClientes;
using SubsistemaGerencialBackend.Models.EnderecoFazendas;
using SubsistemaGerencialBackend.Models.Fazendas;
using System.Globalization;
using Microsoft.OpenApi.Models;



internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Adicionar serviços ao contêiner.
        builder.Services.AddRazorPages();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder => builder.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader());
        });
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        });

        builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))), ServiceLifetime.Scoped);

        var app = builder.Build();

        app.UseRouting();
        app.UseCors("AllowAllOrigins");
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
        }

        //using (var scope = app.Services.CreateScope())
        //{
        //    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        //    context.Database.EnsureDeleted();
        //}

        //ImportarDadosExcel(app.Services);

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAuthorization();

        app.MapControllers();

        app.MapRazorPages();
        app.Run();


        void ImportarDadosExcel(IServiceProvider services)
        {
            string filePath = @"E:\GitHub\RERUM\Pasta1.xlsx"; // Atualize este caminho conforme necessário
                                                              //"E:\GitHub\RERUM\Pasta1.xlsx"

            try
            {
                using (var scope = services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    using (var package = new ExcelPackage(new FileInfo(filePath)))
                    {
                        var worksheet = package.Workbook.Worksheets.First();
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 1; row <= 850; row++) // Supondo que a primeira linha é o cabeçalho
                        {
                            // Obtém valores do worksheet
                            var nome = worksheet.Cells[row, 23].Text.ToUpper();
                            var cpf = ValidateCpf(worksheet.Cells[row, 5].Text);
                            // Define os formatos esperados para as datas
                            string[] formatos = { "dd/MM/yyyy HH:mm:ss" };
                            CultureInfo cultura = CultureInfo.InvariantCulture;

                            // Função para converter uma data a partir de uma string
                            DateTime? ConverterData(string dataString)
                            {
                                if (DateTime.TryParseExact(dataString, formatos, cultura, DateTimeStyles.None, out DateTime dataConvertida))
                                {
                                    return dataConvertida;
                                }
                                else
                                {
                                    return null; // Retorna null para datas inválidas
                                }
                            }

                            // Validar DataInicioPagamento
                            DateTime? dataInicioPagamento = ConverterData(worksheet.Cells[row, 8].Text) ?? DateTime.MinValue;

                            // Validar DataFimTryal
                            DateTime? dataFimTryal = ConverterData(worksheet.Cells[row, 7].Text);


                            // Verifica se um cliente com o mesmo nome e CPF já existe
                            var clienteExistente = context.Clientes.FirstOrDefault(c => c.Nome == nome && c.Cpf == cpf);

                            Cliente cliente;

                            if (clienteExistente == null)
                            {
                                // Cria um novo cliente, caso ele não exista
                                cliente = new Cliente(
                                    nome: nome, // Nome (coluna W)
                                    cpf: cpf,   // CPF (coluna E, aceita null)
                                    situacao: SituacaoCliente.Indefinido, // Situação (valor fixo 0, que equivale a Indefinido, coluna AE)
                                    email: string.IsNullOrWhiteSpace(worksheet.Cells[row, 9].Text)
                                        ? null
                                        : worksheet.Cells[row, 9].Text, // Email (coluna I, aceita null)
                                    telefone: string.IsNullOrWhiteSpace(worksheet.Cells[row, 30].Text)
                                        ? string.Empty
                                        : FormatPhoneNumber(worksheet.Cells[row, 30].Text) // Telefone (coluna AD, aceita null)
                                );

                                // Adiciona o cliente ao contexto e salva as alterações
                                context.Clientes.Add(cliente);
                                context.SaveChanges();
                            }
                            else
                            {
                                // Caso o cliente já exista, utiliza o cliente encontrado
                                cliente = clienteExistente;
                            }

                            DateTime? dataCriacaoFazenda = ConverterData(worksheet.Cells[row, 6].Text);
                            var codigoFazenda = worksheet.Cells[row, 2].Text.ToUpper();
                            var nomeFazenda = worksheet.Cells[row, 21].Text.ToUpper();

                            var fazendaExistente = context.Fazendas.FirstOrDefault(c => c.Nome == nomeFazenda && c.CodigoFazenda == codigoFazenda);

                            Fazenda fazenda;

                            if (fazendaExistente == null)
                            {

                                fazenda = new Fazenda(
                                    cliente.Id, // Usar o Id do cliente recém salvo
                                    codigoFazenda: codigoFazenda, // CodigoFazenda (coluna B, não nulo)
                                    dataCriacaoFazenda: dataCriacaoFazenda.HasValue ? dataCriacaoFazenda.Value : DateTime.MinValue, // DataCriacaoFazenda (coluna F)
                                    nome: nomeFazenda, // Nome (coluna U)
                                    quantidadeAnimais: int.Parse(worksheet.Cells[row, 28].Text) // QuantidadeAnimais (coluna AB)
                                );

                                // Adicionar a fazenda ao contexto e salvar as alterações
                                context.Fazendas.Add(fazenda);
                                context.SaveChanges();
                            }
                            else
                            {
                                fazenda = fazendaExistente;
                            }

                            var clienteContrato = new ClienteContrato
                            (
                                clienteId: cliente.Id,
                                fazendaId: fazenda.Id,
                                codigoContrato: worksheet.Cells[row, 3].Text, // CodigoContrato (coluna C, não aceita null)
                                dataInicopagamento: dataInicioPagamento.HasValue ? dataInicioPagamento.Value : null,// DataInicioPagamento (coluna H, valor específico tratado como null)
                                assinadoPeloPortal: worksheet.Cells[row, 1].Text.ToLower() == "sim" ? true : worksheet.Cells[row, 1].Text.ToLower() == "não" ? false : null, // AssinadoPeloPortal (coluna A, sim/true e não/false, aceita null)
                                dataFimTryal: dataFimTryal.HasValue ? dataFimTryal.Value : null, // DataFimTryal (coluna G)
                                situacao: MapSituacao(worksheet.Cells[row, 29].Text),
                                codigoObjetoFazenda: worksheet.Cells[row, 4].Text // CodigoObjetoFazenda (coluna D)
                            );

                            context.ClienteContratos.Add(clienteContrato);
                            context.SaveChanges();


                            var enderecoCliente = new EnderecoCliente
                            (
                                clienteId: cliente.Id,
                                uf: string.IsNullOrWhiteSpace(worksheet.Cells[row, 27].Text) ? null : worksheet.Cells[row, 27].Text,        // UF (coluna AA)
                                cidade: string.IsNullOrWhiteSpace(worksheet.Cells[row, 22].Text) ? null : worksheet.Cells[row, 22].Text,    // Cidade (coluna V)
                                cep: string.IsNullOrWhiteSpace(worksheet.Cells[row, 25].Text) ? null : worksheet.Cells[row, 25].Text,       // CEP (coluna Y)
                                rua: string.IsNullOrWhiteSpace(worksheet.Cells[row, 26].Text) ? null : worksheet.Cells[row, 26].Text,       // Rua (coluna Z)
                                bairro: string.IsNullOrWhiteSpace(worksheet.Cells[row, 24].Text) ? null : worksheet.Cells[row, 24].Text,    // Bairro (coluna X)
                                numero: null,                                                                                               // Numero (não presente no Excel)
                                complemento: null                                                                                           // Complemento (não presente no Excel)
                            );

                            // Adicionar o endereço do cliente ao contexto e salvar as alterações
                            context.EnderecoClientes.Add(enderecoCliente);
                            context.SaveChanges();

                            var enderecoFazenda = new EnderecoFazenda
                            (
                                fazendaId: fazenda.Id,

                                // Endereço comercial
                                comercialUf: string.IsNullOrWhiteSpace(worksheet.Cells[row, 14].Text) ? null : worksheet.Cells[row, 14].Text,        // comercialUf (coluna N)
                                comercialCidade: string.IsNullOrWhiteSpace(worksheet.Cells[row, 12].Text) ? null : worksheet.Cells[row, 12].Text,    // comercialCidade (coluna L)
                                comercialCep: string.IsNullOrWhiteSpace(worksheet.Cells[row, 11].Text) ? null : worksheet.Cells[row, 11].Text,       // comercialCep (coluna K)
                                comercialRua: null,                                                                                                 // comercialRua (não presente no Excel)
                                comercialBairro: string.IsNullOrWhiteSpace(worksheet.Cells[row, 10].Text) ? null : worksheet.Cells[row, 10].Text,    // comercialBairro (coluna J)
                                comercialNumero: string.IsNullOrWhiteSpace(worksheet.Cells[row, 13].Text) ? null : worksheet.Cells[row, 13].Text,    // comercialNumero (coluna M)
                                comercialComplemento: null,                                                                                        // comercialComplemento (não presente no Excel)

                                // Endereço de faturamento
                                faturaUf: string.IsNullOrWhiteSpace(worksheet.Cells[row, 20].Text) ? null : worksheet.Cells[row, 20].Text,           // faturaUf (coluna T)
                                faturaCidade: string.IsNullOrWhiteSpace(worksheet.Cells[row, 17].Text) ? null : worksheet.Cells[row, 17].Text,       // faturaCidade (coluna Q)
                                faturaCep: string.IsNullOrWhiteSpace(worksheet.Cells[row, 16].Text) ? null : worksheet.Cells[row, 16].Text,          // faturaCep (coluna P)
                                faturaRua: string.IsNullOrWhiteSpace(worksheet.Cells[row, 19].Text) ? null : worksheet.Cells[row, 19].Text,          // faturaRua (coluna S)
                                faturaBairro: string.IsNullOrWhiteSpace(worksheet.Cells[row, 15].Text) ? null : worksheet.Cells[row, 15].Text,       // faturaBairro (coluna O)
                                faturaNumero: string.IsNullOrWhiteSpace(worksheet.Cells[row, 18].Text) ? null : worksheet.Cells[row, 18].Text,                                                                                                 // faturaNumero (não presente no Excel)
                                faturaComplemento: null                                                                                            // faturaComplemento (não presente no Excel)
                            );

                            // Adicionar o endereço da fazenda ao contexto e salvar as alterações
                            context.EnderecoFazendas.Add(enderecoFazenda);
                            context.SaveChanges();




                            string ValidateCpf(string cpf)
                            {
                                if (string.IsNullOrWhiteSpace(cpf))
                                    return string.Empty;

                                // Remover caracteres não numéricos
                                cpf = new string(cpf.Where(char.IsDigit).ToArray());

                                if (cpf.Length == 11)
                                    return cpf;

                                // Retorna null se o CPF não tiver exatamente 11 dígitos
                                return string.Empty;
                            }

                            string FormatPhoneNumber(string phoneNumber)
                            {
                                if (string.IsNullOrWhiteSpace(phoneNumber))
                                    return string.Empty;

                                // Remover caracteres não numéricos
                                phoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

                                if (phoneNumber.Length == 11)
                                { // Formato de telefone brasileiro esperado
                                    return $"+55 ({phoneNumber.Substring(0, 2)}) {phoneNumber.Substring(2, 1)} {phoneNumber.Substring(3, 4)}-{phoneNumber.Substring(7, 4)}";
                                }
                                else
                                {
                                    return string.Empty;
                                }
                            }

                            SituacaoContrato MapSituacao(string situacaoTexto)
                            {
                                if (string.IsNullOrWhiteSpace(situacaoTexto))
                                    return SituacaoContrato.Indefinido;

                                situacaoTexto = situacaoTexto.Trim().ToLower(); // Remover espaços e padronizar para minúsculas

                                return situacaoTexto switch
                                {
                                    "ativo" => SituacaoContrato.Ativo,
                                    "teste" => SituacaoContrato.Teste,
                                    "versao_gratuita" => SituacaoContrato.Gratuito,
                                    _ => SituacaoContrato.Indefinido // Valor padrão caso não seja nenhum dos três
                                };
                            }

                        }

                        context.SaveChanges();
                    }

                    Console.WriteLine("Dados importados com sucesso!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao importar os dados: {ex.Message}");
            }
        }
    }
}