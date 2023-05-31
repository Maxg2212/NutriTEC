using System;
using System.Collections.Generic;

namespace NutriTEC_API.Models;

public partial class ProductPlan
{
    public string Barcode { get; set; } = null!;

    public string EatPlanId { get; set; } = null!;
}
