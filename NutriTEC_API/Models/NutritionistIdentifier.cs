﻿using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class NutritionistIdentifier
    {
        public string nutritionist_id { get; set; } = string.Empty;
    }
}
