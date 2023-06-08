using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class NutritionistIdentifier
    {
        public int nutritionist_id { get; set; } = 0;
    }
}
