using System;
using System.Collections.Generic;

namespace NutriTEC_API.Models;

public partial class Client
{
    public string Clientid { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Secondname { get; set; }

    public string Lname1 { get; set; } = null!;

    public string? Lname2 { get; set; }

    public string Weight { get; set; } = null!;

    public string Bmi { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Bdate { get; set; } = null!;

    public string Muslcepercentage { get; set; } = null!;

    public string Fatpercentage { get; set; } = null!;

    public string Hipsize { get; set; } = null!;

    public string Waistsize { get; set; } = null!;

    public string Necksize { get; set; } = null!;

    public string? Lastmonthmeas { get; set; }

    public virtual ICollection<Dailyconsumption> Dailyconsumptions { get; set; } = new List<Dailyconsumption>();

    public virtual ICollection<Invoice> Invoiceidcs { get; set; } = new List<Invoice>();

    public virtual ICollection<Nutritionist> Nutritionists { get; set; } = new List<Nutritionist>();
}
