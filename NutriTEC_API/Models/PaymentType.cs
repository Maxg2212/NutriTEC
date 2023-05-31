using System;
using System.Collections.Generic;

namespace NutriTEC_API.Models;

public partial class PaymentType
{
    public string PtypeId { get; set; } = null!;

    public string Description { get; set; } = null!;
}
