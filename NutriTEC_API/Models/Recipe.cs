using System;
using System.Collections.Generic;

namespace NutriTEC_API.Models;

public partial class Recipe
{
    public string RecipeId { get; set; } = null!;

    public string Portions { get; set; } = null!;

    public string Calories { get; set; } = null!;

    public string Ingredients { get; set; } = null!;
}
