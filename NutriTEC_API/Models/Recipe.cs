using System;
using System.Collections.Generic;

namespace NutriTEC_API.Models;

public partial class Recipe
{
    public string Recipeid { get; set; } = null!;

    public string Portions { get; set; } = null!;

    public string Calories { get; set; } = null!;

    public string Ingredients { get; set; } = null!;

    public virtual ICollection<Productdish> Barcodes { get; set; } = new List<Productdish>();

    public virtual ICollection<Admin> Emails { get; set; } = new List<Admin>();
}
