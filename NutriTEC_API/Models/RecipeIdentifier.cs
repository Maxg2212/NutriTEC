﻿using Microsoft.EntityFrameworkCore;

namespace NutriTEC_API.Models
{
    [Keyless]
    public class RecipeIdentifier
    {
        public string recipe_id { get; set; } = string.Empty;
    }
}
