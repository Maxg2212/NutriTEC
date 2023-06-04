using System;
using System.Collections.Generic;

namespace NutriTEC_API.Models;

public partial class Nutritionist
{
    public string Employeeid { get; set; } = null!;

    public string Nutritionistid { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Secondname { get; set; } = null!;

    public string Lname1 { get; set; } = null!;

    public string? Lname2 { get; set; }

    public string Password { get; set; } = null!;

    public string Bdate { get; set; } = null!;

    public string? Profilepic { get; set; }

    public string Creditcard { get; set; } = null!;

    public string Nutritionistcode { get; set; } = null!;

    public string Bmi { get; set; } = null!;

    public string Weight { get; set; } = null!;

    public string? Address { get; set; }

    public string Paymenttype { get; set; } = null!;

    public virtual ICollection<Paymenttype> Paymenttypes { get; set; } = new List<Paymenttype>();

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Eatingplan> Eatplans { get; set; } = new List<Eatingplan>();
}
