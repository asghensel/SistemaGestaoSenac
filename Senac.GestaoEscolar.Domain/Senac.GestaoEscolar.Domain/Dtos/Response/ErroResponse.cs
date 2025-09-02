using System.Text.Json.Serialization;

namespace Senac.GestaoEscolar.Domain.Dtos.Response
{
    public class ErroResponse
    {
        public string Mensagem { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Codigo { get; set; }
    }
}
