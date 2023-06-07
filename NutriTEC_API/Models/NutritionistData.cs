using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class NutritionistData
    {
        public string employee_id { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;

        public string name { get; set; } = string.Empty;

        public string second_name { get; set; } = string.Empty;

        public string lname1 { get; set; } = string.Empty;

        public string? lname2 { get; set; }

        public string password { get; set; } = string.Empty;

        public string bdate { get; set; } = string.Empty;

        public string? profile_pic { get; set; }

        public string credit_card { get; set; } = string.Empty;

        public string nutritionist_code { get; set; } = string.Empty;

        public string bmi { get; set; } = string.Empty;

        public string weight { get; set; } = string.Empty;

        public string? address { get; set; }

        public string payment_type { get; set; } = string.Empty;

    }

}
