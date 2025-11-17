using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalaReuniao.Api.Domain.Filters;
using SalaReuniao.Api.Infrastructure;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Api.Infrastructure.Repositories;
using Xunit;

public class SalaDeReuniaoRepositoryTests
{
    private SalaDeReuniaoRepository CreateRepository(out AppDbContext context)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;

        context = new AppDbContext(options);
        return new SalaDeReuniaoRepository(context);
    }

    [Fact]
    public async Task ObterTodasAsync_Deve_Filtrar_Corretamente()
    {
        // Arrange
        var repo = CreateRepository(out var context);

        var sala1 = new SalaDeReuniaoEntity
        {
            Id = Guid.NewGuid(),
            Nome = "Sala Azul",
            IdResponsavel = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            Endereco = new EnderecoEntity { Estado = "SP", Municipio = "Campinas" },
            Disponibilidades =
            [
                new DisponibilidadeEntity
                {
                    DiaSemana = DayOfWeek.Monday,
                    Inicio = new TimeOnly(8, 0),
                    Fim = new TimeOnly(10, 0)
                }
            ],
            ReunioesAgendadas =
            [
                new ReuniaoAgendadaEntity
                {
                    Id = Guid.NewGuid(),
                    Data = new DateOnly(2025, 1, 10),
                    Inicio = new TimeOnly(9, 0),
                    Fim = new TimeOnly(11, 0)
                }
            ]
        };

        var sala2 = new SalaDeReuniaoEntity
        {
            Id = Guid.NewGuid(),
            Nome = "Sala Verde",
            IdResponsavel = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            Endereco = new EnderecoEntity { Estado = "SP", Municipio = "São Paulo" },
            Disponibilidades =
            [
                new DisponibilidadeEntity
                {
                    DiaSemana = DayOfWeek.Monday,
                    Inicio = new TimeOnly(14, 0),
                    Fim = new TimeOnly(18, 0)
                }
            ]
        };

        context.Salas.AddRange(sala1, sala2);
        await context.SaveChangesAsync();

        var filter = new FilterSalaReuniao
        {
            Page = 1,
            PageSize = 10,
            Estado = new[] { "SP" },
            Municipio = new[] { "São Paulo" },
            Data = new DateOnly(2025, 1, 10),
            HoraInicio = new TimeOnly(15, 0),
            Duracao = 2
        };

        // Act
        var result = await repo.ObterTodasAsync(filter);

        // Assert: sala1 deve ser eliminada por conflito de reunião
        Assert.DoesNotContain(result.Items, x => x.Id == sala1.Id);

        // Assert: sala2 deve aparecer
        var encontrada = result.Items.FirstOrDefault(x => x.Id == sala2.Id);
        Assert.NotNull(encontrada);

        // Assert: filtra município
        Assert.Equal("São Paulo", encontrada.Endereco.Municipio);

        // Assert: filtra disponibilidade compatível com horário
        Assert.True(encontrada.Disponibilidades.Any(d =>
            d.Inicio <= filter.HoraInicio.Value &&
            d.Fim >= filter.HoraInicio.Value.AddHours(filter.Duracao.Value)
        ));

        // Assert: paginação
        Assert.Equal(1, result.Page);
        Assert.Equal(10, result.PageSize);
    }
}
