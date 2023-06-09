using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class GetNutritionistPlan
    {
        public string nutritionist_id { get; set; } = string.Empty;
        public string eatplan_id { get; set; } = string.Empty;
    }
}
