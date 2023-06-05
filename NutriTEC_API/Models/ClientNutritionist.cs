using System;
using System.Collections.Generic;

namespace NutriTEC_API.Models;

public partial class ClientNutritionist
{
    public string ClientId { get; set; } = null!;

    public string NutritionistId { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;

    public virtual Nutritionist Nutritionist { get; set; } = null!;
}
