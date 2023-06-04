using System;
using System.Collections.Generic;

namespace NutriTEC_API.Models;

public partial class Admin
{
    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
