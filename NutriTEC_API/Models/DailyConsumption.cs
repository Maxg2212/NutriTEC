using System;
using System.Collections.Generic;

namespace NutriTEC_API.Models;

public partial class DailyConsumption
{
    public string Barcode { get; set; } = null!;

    public string ClientId { get; set; } = null!;

    public string EatingTime { get; set; } = null!;

    public string Date { get; set; } = null!;
}
