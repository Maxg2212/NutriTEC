using System;
using System.Collections.Generic;

namespace NutriTEC_API.Models;

public partial class Eatingplan
{
    public string Eatplanid { get; set; } = null!;

    public string Nutritionistname { get; set; } = null!;

    public string Quantity { get; set; } = null!;

    public string? Eatingschedule { get; set; }

    public string? Startperiod { get; set; }

    public string? Endingperiod { get; set; }

    public virtual ICollection<Productdish> Barcodes { get; set; } = new List<Productdish>();

    public virtual ICollection<Nutritionist> Nutritionists { get; set; } = new List<Nutritionist>();
}
