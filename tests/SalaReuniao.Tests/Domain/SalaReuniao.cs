using System;
using Xunit;
using FluentAssertions;
using SalaReuniao.Api.Core;
using SalaReuniao.Api.Core.Commands;
using SalaReuniao.Domain.ValueObjects;
using SalaReuniao.Domain.Exceptions;

namespace SalaReuniao.Tests.Domain
{
    public class SalaDeReuniaoTests
    {

        private Endereco CriarEnderecoValido()
        {
            return new Endereco(
                new DadosEndereco
                {
                    Bairro = "Bairro Atualizado",
                    Municipio = "Cidade Atualizada",
                    Rua = "Rua Atualizada",
                    CEP = "98765-001",
                    Estado = "Estado"
                },
                new DadosComplementaresEndereco
                {
                    Numero = 2,
                    Complemento = "Complemento"
                }
            );
        
        }
        private SalaDeReuniao CriarSalaDeReuniao(CriarSalaReuniaoCommand command)
        {
            return new SalaDeReuniao
            (
                Guid.NewGuid(),
                command.IdResponsavel,
                command.Nome.Trim(),
                command.Capacidade,
                command.ValorHora,
                CriarEnderecoValido(),
                command.Descricao,
                command.DisponibilidadeSemanal
            );
        }
        private SalaDeReuniao AtualizarSalaReuniao(SalaDeReuniao sala, AtualizarSalaReuniaoCommand command, Endereco endereco)
        {
            sala.Atualizar(
                command.Nome.Trim(),
                command.Capacidade,
                command.ValorHora,
                command.Descricao,
                command.DisponibilidadeSemanal
            );
            sala.AtualizarEndereco(
                new DadosEndereco
                {
                    Bairro = endereco.Bairro,
                    Municipio = endereco.Municipio,
                    Rua = endereco.Rua,
                    CEP = endereco.CEP,
                    Estado = endereco.Estado
                },
                new DadosComplementaresEndereco
                {
                    Numero = endereco.Numero,
                    Complemento = endereco.Complemento
                }
            );

            return sala;
        }
        private CriarSalaReuniaoCommand CriarCommandValido()
        {
            return new CriarSalaReuniaoCommand
            {
                IdResponsavel = Guid.NewGuid(),
                Nome = "Sala Teste",
                Capacidade = 10,
                ValorHora = 50,
                Descricao = "Uma sala de teste",
            };
        }

        // ----------------------------------------------------
        // TESTE 1: Criar sala com dados v�lidos
        // ----------------------------------------------------
        [Fact]
        public void Criar_Deve_Retornar_Sala_Valida()
        {
            var command = CriarCommandValido();

            var sala = CriarSalaDeReuniao(command);

            sala.Nome.Should().Be("Sala Teste");
            sala.Capacidade.Should().Be(10);
            sala.ValorHora.Should().Be(50);
            sala.Id.Should().NotBe(Guid.Empty);
        }

        // ----------------------------------------------------
        // TESTE 2: Criar deve falhar com nome vazio
        // ----------------------------------------------------
        [Fact]
        public void Criar_Sala_Deve_Falhar_Com_Nome_Invalido()
        {
            var command = CriarCommandValido();
            command.Nome = "   ";

            Action act = () => CriarSalaDeReuniao(command);

            act.Should().Throw<DomainException>()
                .WithMessage("O nome da sala é obrigatório.");
        }

        // ----------------------------------------------------
        // TESTE 2.1: Criar deve falhar com nome vazio
        // ----------------------------------------------------
        [Fact]
        public void Criar_Deve_Falhar_Com_Capacidade_Inferior_A_1()
        {
            var command = CriarCommandValido();
            command.Capacidade = 0;

            Action act = () => CriarSalaDeReuniao(command);

            act.Should().Throw<DomainException>()
                .WithMessage("A sala deve comportar ao menos uma pessoa.");
        }

        // ----------------------------------------------------
        // TESTE 2.3: Criar deve falhar com valor por hora inferior a 0
        // ----------------------------------------------------
        [Fact]
        public void Criar_Deve_Falhar_Com_Valor_Por_Hora_Inferior_A_0()
        {
            var command = CriarCommandValido();
            command.ValorHora = -1;

            Action act = () => CriarSalaDeReuniao(command);

            act.Should().Throw<DomainException>()
                .WithMessage("O valor por hora não pode ser negativo.");
        }

        // ----------------------------------------------------
        // TESTE 3: Atualizar sala corretamente
        // ----------------------------------------------------
        [Fact]
        public void Atualizar_Deve_Modificar_Valores()
        {
            var sala = CriarSalaDeReuniao(CriarCommandValido());

            var atualizar = new AtualizarSalaReuniaoCommand
            {
                Nome = "Nova Sala",
                Capacidade = 20,
                ValorHora = 100,
                Descricao = "Atualizada",
            };
            var endereco = new Endereco(
                new DadosEndereco
                {
                    Bairro = "Bairro Atualizado",
                    Municipio = "Cidade Atualizada",
                    Rua = "Rua Atualizada",
                    CEP = "98765-001",
                    Estado = "Estado Atualizado"
                },
                new DadosComplementaresEndereco
                {
                    Numero = 2,
                    Complemento = "Complemento"
                }
            );
            
            sala = AtualizarSalaReuniao(sala, atualizar, endereco);

            sala.Nome.Should().Be("Nova Sala");
            sala.Capacidade.Should().Be(20);
            sala.ValorHora.Should().Be(100);
            sala.Descricao.Should().Be("Atualizada");
            sala.Endereco.Bairro.Should().Be("Bairro Atualizado");
            sala.Endereco.Municipio.Should().Be("Cidade Atualizada");
            sala.Endereco.Rua.Should().Be("Rua Atualizada");
            sala.Endereco.Complemento.Should().Be("Complemento");
            sala.Endereco.Numero.Should().Be(2);
            sala.Endereco.CEP.Should().Be("98765-001");
            sala.Endereco.Estado.Should().Be("Estado Atualizado");
        }

        // ----------------------------------------------------
        // TESTE 4: AgendaDisponivel sem conflitos deve retornar true
        // ----------------------------------------------------
        [Fact]
        public void AgendaDisponivel_Deve_Retornar_Verdadeiro_Quando_Livre()
        {
            var sala = CriarSalaDeReuniao(CriarCommandValido());

            var inicio = DateTime.Today.AddHours(9);
            var fim = DateTime.Today.AddHours(10);

            var disponivel = sala.AgendaDisponivel(inicio, fim);

            disponivel.Should().BeTrue();
        }

        // ----------------------------------------------------
        // TESTE 5: AgendaDisponivel retorna false em caso de conflito
        // ----------------------------------------------------
        [Fact]
        public void AgendaDisponivel_Deve_Falhar_Quando_Ha_Conflito()
        {
            var sala = CriarSalaDeReuniao(CriarCommandValido());

            var existente = new ReuniaoAgendada
            {
                Id = Guid.NewGuid(),
                IdSalaReuniao = sala.Id,
                IdCliente = Guid.NewGuid(),
                Inicio = DateTime.Today.AddHours(9),
                Fim = DateTime.Today.AddHours(10)
            };

            sala.ReunioesAgendadas.Add(existente);

            var inicio = DateTime.Today.AddHours(9.5);
            var fim = DateTime.Today.AddHours(11);

            var disponivel = sala.AgendaDisponivel(inicio, fim);

            disponivel.Should().BeFalse();
        }

        // ----------------------------------------------------
        // TESTE 6: AgendaReuniao quando disponível
        // ----------------------------------------------------
        [Fact]
        public void AgendaReuniao_Deve_Adicionar_Reuniao()
        {
            var sala = CriarSalaDeReuniao(CriarCommandValido());

            var inicio = DateTime.Today.AddHours(13);
            var fim = DateTime.Today.AddHours(14);

            sala.AgendaReuniao(Guid.NewGuid(), inicio, fim);

            sala.ReunioesAgendadas.Should().HaveCount(1);
            sala.ReunioesAgendadas.First().Inicio.Should().Be(inicio);
        }

        // ----------------------------------------------------
        // TESTE 7: AgendaReuniao deve falhar quando não disponível
        // ----------------------------------------------------
        [Fact]
        public void AgendaReuniao_Deve_Lancar_Erro_Quando_Indisponivel()
        {
            var sala = CriarSalaDeReuniao(CriarCommandValido());

            sala.ReunioesAgendadas.Add(new ReuniaoAgendada
            {
                Inicio = DateTime.Today.AddHours(15),
                Fim = DateTime.Today.AddHours(16)
            });

            Action act = () =>
                sala.AgendaReuniao(Guid.NewGuid(), DateTime.Today.AddHours(15), DateTime.Today.AddHours(16));

            act.Should().Throw<DomainException>()
               .WithMessage("A sala não está disponível no horário e dia solicitado.");
        }
    }
}
