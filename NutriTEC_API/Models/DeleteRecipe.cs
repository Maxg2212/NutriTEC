using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class DeleteRecipe
    {
        public int delete_recipe { get; set; } = 0;
    }
}
