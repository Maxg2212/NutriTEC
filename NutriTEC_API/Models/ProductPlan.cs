using System;
using System.Collections.Generic;

namespace NutriTEC_API.Models;

public partial class ProductPlan
{
    public string Barcode { get; set; } = null!;

    public string EatplanId { get; set; } = null!;

    public virtual ProductDish BarcodeNavigation { get; set; } = null!;

    public virtual EatingPlan Eatplan { get; set; } = null!;
}
