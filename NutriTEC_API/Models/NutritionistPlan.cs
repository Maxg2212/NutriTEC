using System;
using System.Collections.Generic;

namespace NutriTEC_API.Models;

public partial class NutritionistPlan
{
    public string NutritionistId { get; set; } = null!;

    public string EatplanId { get; set; } = null!;

    public virtual EatingPlan Eatplan { get; set; } = null!;

    public virtual Nutritionist Nutritionist { get; set; } = null!;
}
