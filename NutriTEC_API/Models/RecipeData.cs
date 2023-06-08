using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class RecipeData
    {
        public string recipe_id { get; set; } = string.Empty;

        public string portions { get; set; } = string.Empty;

        public string calories { get; set; } = string.Empty;

        public string ingredients { get; set; } = string.Empty;
    }
}
