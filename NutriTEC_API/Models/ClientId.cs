using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class ClientId
    {
        public string client_id { get; set; } =string.Empty;
    }
}
