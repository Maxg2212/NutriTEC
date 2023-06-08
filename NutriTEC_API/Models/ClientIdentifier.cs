using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class ClientIdentifier
    {
        public string client_id { get; set; } = string.Empty;
    }
}
