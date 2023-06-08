using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class UpdateRecipe
    {
        public int update_recipe { get; set; } = 0;
    }
}
