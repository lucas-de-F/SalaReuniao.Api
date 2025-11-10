using System;
using System.Collections.Generic;

namespace SalaReuniao.Api.Infrastructure.Entities
{
    public abstract class UsuarioEntity
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
    }
}
