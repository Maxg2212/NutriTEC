using System;
using System.Collections.Generic;

namespace NutriTEC_API.Models;

public partial class Client
{
    public string ClientId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? SecondName { get; set; }

    public string Lname1 { get; set; } = null!;

    public string? Lname2 { get; set; }

    public string Weight { get; set; } = null!;

    public string Bmi { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Bdate { get; set; } = null!;

    public string MuslcePercentage { get; set; } = null!;

    public string FatPercentage { get; set; } = null!;

    public string HipSize { get; set; } = null!;

    public string WaistSize { get; set; } = null!;

    public string NeckSize { get; set; } = null!;

    public string? LastMonthMeas { get; set; }
}
