using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class NutriSearchProduct
    {
        public string barcode { get; set; } = string.Empty;
        public string vitamins { get; set; } = string.Empty;
        public string calcium { get; set; } = string.Empty;
        public string iron { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string portion_size { get; set; } = string.Empty;
        public string energy { get; set; } = string.Empty;
        public string fat { get; set; } = string.Empty;
        public string sodium { get; set; } = string.Empty;
        public string carbs { get; set; } = string.Empty;
        public string protein { get; set; } = string.Empty;
        public int sstate { get; set; } = 0;

    }
}
