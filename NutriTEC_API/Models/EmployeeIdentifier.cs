using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class EmployeeIdentifier
    {
        public string employee_id { get; set; } = string.Empty;
    }
}
