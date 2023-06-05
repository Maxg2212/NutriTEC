using System;
using System.Collections.Generic;

namespace NutriTEC_API.Models;

public partial class Nutritionist
{
    public string EmployeeId { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string SecondName { get; set; } = null!;

    public string Lname1 { get; set; } = null!;

    public string? Lname2 { get; set; }

    public string Password { get; set; } = null!;

    public string Bdate { get; set; } = null!;

    public string? ProfilePic { get; set; }

    public string CreditCard { get; set; } = null!;

    public string NutritionistCode { get; set; } = null!;

    public string Bmi { get; set; } = null!;

    public string Weight { get; set; } = null!;

    public string? Address { get; set; }

    public string PaymentType { get; set; } = null!;

    public decimal? Discount { get; set; }

    public virtual PaymentType PaymentTypeNavigation { get; set; } = null!;
}
