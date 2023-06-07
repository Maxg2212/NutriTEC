using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class DailyConsumptionFunction
    {
        public string barcode { get; set; } = string.Empty;

        public string client_id { get; set; } = string.Empty;

        public string eating_time { get; set; } = string.Empty;

        public string datec { get; set; } = string.Empty;
    }
}
