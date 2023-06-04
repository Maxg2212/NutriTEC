using System;
using System.Collections.Generic;

namespace NutriTEC_API.Models;

public partial class Paymenttype
{
    public string Ptypeid { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual Nutritionist Ptype { get; set; } = null!;
}
