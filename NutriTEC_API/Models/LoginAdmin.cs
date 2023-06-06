using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class LoginAdmin
    {
        public string email { get; set; } = string.Empty;
    }
}
