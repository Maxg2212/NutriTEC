using System;
using System.Collections.Generic;

namespace NutriTEC_API.Models;

public partial class Dailyconsumption
{
    public string Barcode { get; set; } = null!;

    public string Clientid { get; set; } = null!;

    public string Eatingtime { get; set; } = null!;

    public DateOnly Datec { get; set; }

    public virtual Productdish BarcodeNavigation { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;
}
