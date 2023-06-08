using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class ProductIdentifier
    {
        public string barcode { get; set; } = string.Empty;
    }
}
