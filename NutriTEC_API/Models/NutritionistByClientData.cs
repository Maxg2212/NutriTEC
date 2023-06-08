using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class NutritionistByClientData
    {
        public string employee_id { get; set; } = string.Empty;

        public string n_name { get; set; } = string.Empty;

        public string n_lname { get; set; } = string.Empty;
    }
}
