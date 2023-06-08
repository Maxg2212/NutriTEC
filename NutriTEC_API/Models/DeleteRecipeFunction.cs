using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class DeleteRecipeFunction
    {
        public int delete_recipe { get; set; } = 0;
    }
}
