using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class ClientMeasures
    {
        public string client_id { get; set; } = string.Empty;

        public string muslce_percentage { get; set; } = string.Empty;

        public string fat_percentage { get; set; } = string.Empty;

        public string hip_size { get; set; } = string.Empty;

        public string waist_size { get; set; } = string.Empty;

        public string neck_size { get; set; } = string.Empty;

        public string last_month_meas { get; set; }
    }
}
