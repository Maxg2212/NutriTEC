using System;
using System.Collections.Generic;

namespace NutriTEC_API.Models;

public partial class EatingPlan
{
    public string EatPlanId { get; set; } = null!;

    public string NutritionistName { get; set; } = null!;

    public string Quantity { get; set; } = null!;

    public string? EatingSchedule { get; set; }

    public string? StartPeriod { get; set; }

    public string? EndingPeriod { get; set; }
}
