namespace SalaReuniao.Api.Core.Dtos
{
    public class LocalidadeResult
    {
        public string Estado { get; set; } = string.Empty;
        public List<string> Municipios { get; set; } = new() { };
    }
}