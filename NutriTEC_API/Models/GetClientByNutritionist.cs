using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class GetClientByNutritionist
    {
        public string client_id { get; set; } = string.Empty;
        public string c_name { get; set; } = string.Empty;
        public string c_lname { get; set; } = string.Empty;
    }
}
