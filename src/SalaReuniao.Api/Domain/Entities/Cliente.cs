using System;
using System.Collections.Generic;

namespace SalaReuniao.Api.Core
{
    public class Cliente : Usuario
    {
        public ICollection<ReuniaoAgendada> ReunioesAgendadas { get; set; } = new List<ReuniaoAgendada>();
    }
}
