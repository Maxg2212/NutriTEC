using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class EatingPlanToClient
    {
        public string client_id { get; set; } = string.Empty;
        public string nutritionist_id { get; set;} = string.Empty;
        public string eatplan_id { get; set; } = string.Empty;
    }
}
