using Microsoft.EntityFrameworkCore;
using SalaReuniao.Api.Infrastructure;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Api.Infrastructure.Repositories;
using SalaReuniao.Api.Domain.Filters;

public class LocalidadeEndereco
{
    public string Estado { get; set; } = string.Empty;
    public string Municipio { get; set; } = string.Empty;
}
public class LocalidadesSalaReuniaoRepositoryTests
{
    private LocalidadesSalaReuniaoRepository CreateRepository(out AppDbContext context)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        context = new AppDbContext(options);
        return new LocalidadesSalaReuniaoRepository(context);
    }

    [Fact]
    public async Task Deve_Retornar_Estado_Com_Municipios()
    {
        // Arrange
        var repo = CreateRepository(out var context);

        context.Salas.Add(new SalaDeReuniaoEntity
        {
            Id = Guid.NewGuid(),
            Endereco = new EnderecoEntity { Estado = "SP", Municipio = "São Paulo" }
        });

        context.Salas.Add(new SalaDeReuniaoEntity
        {
            Id = Guid.NewGuid(),
            Endereco = new EnderecoEntity { Estado = "SP", Municipio = "Campinas" }
        });

        context.Salas.Add(new SalaDeReuniaoEntity
        {
            Id = Guid.NewGuid(),
            Endereco = new EnderecoEntity { Estado = "RJ", Municipio = "Niterói" }
        });

        context.Salas.Add(new SalaDeReuniaoEntity
        {
            Id = Guid.NewGuid(),
            Endereco = new EnderecoEntity { Estado = "RJ", Municipio = "Nova Iguaçu" }
        });

        context.Salas.Add(new SalaDeReuniaoEntity
        {
            Id = Guid.NewGuid(),
            Endereco = new EnderecoEntity { Estado = "RJ", Municipio = "Belford Roxo" }
        });

        await context.SaveChangesAsync();

        var filter = new FilterLocalidade { Page = 1, PageSize = 10 };

        // Act
        var result = await repo.ObterFiltrosLocalidade(filter);

        // Assert
        Assert.Equal(5, result.Items.Count);

        var sp = result.Items.First(x => x.Estado == "SP");
        Assert.Equal(2, sp.Municipios.Count);
        Assert.Contains("São Paulo", sp.Municipios);
        Assert.Contains("Campinas", sp.Municipios);

        var rj = result.Items.First(x => x.Estado == "RJ");
        Assert.Equal(3, rj.Municipios.Count);
        Assert.Contains("Niterói", rj.Municipios);
        Assert.Contains("Nova Iguaçu", rj.Municipios);
        Assert.Contains("Belford Roxo", rj.Municipios);
    }
}
