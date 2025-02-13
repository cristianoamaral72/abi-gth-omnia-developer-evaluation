using Ambev.DeveloperEvaluation.ORM;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Integration;

public class SqlServerTestFixture : IAsyncLifetime
{
    public MsSqlContainer SqlServerContainer { get; private set; }
    public DefaultContext DbContext { get; private set; }

    public SqlServerTestFixture()
    {
        try
        {
            Console.WriteLine("🔄 Iniciando o container do SQL Server...");

            SqlServerContainer = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                .WithPassword("YourStrong!Passw0rd") // Senha obrigatória para o SQL Server
                .WithCleanUp(true) // Limpa o container após execução dos testes
                .Build();

            Console.WriteLine("✅ Container configurado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erro ao configurar o Testcontainers: {ex.Message}");
            throw;
        }
    }

    public async Task InitializeAsync()
    {
        try
        {
            Console.WriteLine("🚀 Iniciando o container do SQL Server...");
            await SqlServerContainer.StartAsync();
            Console.WriteLine("✅ Container iniciado com sucesso!");

            var options = new DbContextOptionsBuilder<DefaultContext>()
                .UseSqlServer(SqlServerContainer.GetConnectionString())
                .Options;

            DbContext = new DefaultContext(options);
            await DbContext.Database.EnsureCreatedAsync();
            Console.WriteLine("✅ Banco de dados criado com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erro ao iniciar o container: {ex.Message}");
            throw;
        }
    }

    public async Task DisposeAsync()
    {
        Console.WriteLine("🛑 Finalizando o container do SQL Server...");
        await DbContext.DisposeAsync();
        await SqlServerContainer.DisposeAsync();
        Console.WriteLine("✅ Container finalizado com sucesso!");
    }
}