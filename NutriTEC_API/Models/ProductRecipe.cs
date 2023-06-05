using System;
using System.Collections.Generic;

namespace NutriTEC_API.Models;

public partial class ProductRecipe
{
    public string Barcode { get; set; } = null!;

    public string RecipeId { get; set; } = null!;

    public virtual ProductDish BarcodeNavigation { get; set; } = null!;

    public virtual Recipe Recipe { get; set; } = null!;
}
