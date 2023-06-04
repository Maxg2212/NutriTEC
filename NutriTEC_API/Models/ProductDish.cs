using System;
using System.Collections.Generic;

namespace NutriTEC_API.Models;

public partial class Productdish
{
    public string Barcode { get; set; } = null!;

    public string Vitamins { get; set; } = null!;

    public string Calcium { get; set; } = null!;

    public string Iron { get; set; } = null!;

    public string? Description { get; set; }

    public string Portionsize { get; set; } = null!;

    public string Energy { get; set; } = null!;

    public string Fat { get; set; } = null!;

    public string Sodium { get; set; } = null!;

    public string Carbs { get; set; } = null!;

    public string Protein { get; set; } = null!;

    public int State { get; set; }

    public virtual ICollection<Dailyconsumption> Dailyconsumptions { get; set; } = new List<Dailyconsumption>();

    public virtual ICollection<Eatingplan> Eatplans { get; set; } = new List<Eatingplan>();

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
