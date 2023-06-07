using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class ClientData
    {
        public string client_id { get; set; } = string.Empty;

        public string name { get; set; } = string.Empty;

        public string second_name { get; set; }

        public string lname1 { get; set; } = string.Empty;

        public string lname2 { get; set; }

        public string weight { get; set; } = string.Empty;

        public string bmi { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;

        public string bdate { get; set; } = string.Empty;

        public string muslce_percentage { get; set; } = string.Empty;

        public string fat_percentage { get; set; } = string.Empty;

        public string hip_size { get; set; } = string.Empty;

        public string waist_size { get; set; } = string.Empty;

        public string neck_size { get; set; } = string.Empty;

        public string last_month_meas { get; set; }
    }
}
