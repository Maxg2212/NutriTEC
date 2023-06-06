using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class LoginNutritionist
    {
        public string employee_id { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;

        public string name { get; set; } = string.Empty;

        public string second_name { get; set; } = string.Empty;

        public string lname1 { get; set; } = string.Empty;

        public string lname2 { get; set; } = string.Empty;

        public string credit_card { get; set; } = string.Empty;

        public string profile_pic { get; set; } = string.Empty;

        public string nutritionist_code { get; set; } = string.Empty;

    }
}
