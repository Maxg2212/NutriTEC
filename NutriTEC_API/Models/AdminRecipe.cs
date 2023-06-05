using System;
using System.Collections.Generic;

namespace NutriTEC_API.Models;

public partial class AdminRecipe
{
    public string RecipeId { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual Admin EmailNavigation { get; set; } = null!;

    public virtual Recipe Recipe { get; set; } = null!;
}
