using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class PaymentReport
    {
        public string payment_type { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string second_name { get; set; } = string.Empty;
        public string lname1 { get; set; } = string.Empty;
        public string lname2 { get; set; } = string.Empty;
        public string credit_card { get; set; } = string.Empty;
        public float discount { get; set; } = 0;
        public int total_amount { get; set; } = 0;
        public int final_amount { get; set; } = 0;
    }
}
