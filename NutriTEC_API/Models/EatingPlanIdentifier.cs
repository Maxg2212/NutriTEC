using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class EatingPlanIdentifier
    {
        public string eatplan_id { get; set; } = string.Empty;
    }
}
