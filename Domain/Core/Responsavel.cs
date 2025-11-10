using System;
using System.Collections.Generic;

namespace SalaReuniao.Api.Core
{
    public class Responsavel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public ICollection<SalaDeReuniao> Salas { get; set; } = new List<SalaDeReuniao>();
        public ICollection<Servico> ServicosCadastrados { get; set; } = new List<Servico>();

        public void CadastrarSala(SalaDeReuniao sala)
        {
            Salas.Add(sala);
        }

        public void CadastrarServico(Servico servico)
        {
            ServicosCadastrados.Add(servico);
        }
    }
}
