using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class GetEatplanByNutritionist
    {
        public string eatplan_id { get; set; } = string.Empty;
        public string quantity { get; set; } = string.Empty;
        public string eating_schedule { get; set; } = string.Empty;
        public string start_period { get; set; } = string.Empty;
        public string ending_period { get; set; } = string.Empty;
    }
}
