using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class ProductDishState
    {
        public int change_product_state { get; set; } = 0;
    }
}
