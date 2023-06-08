using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class ProductToRecipeFunction
    {
        public string barcode { get; set; } = string.Empty;

        public string recipe_id { get; set; } = string.Empty;
    }
}
