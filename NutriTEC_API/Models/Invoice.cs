using System;
using System.Collections.Generic;

namespace NutriTEC_API.Models;

public partial class Invoice
{
    public string Invoiceid { get; set; } = null!;

    public string Payamount { get; set; } = null!;

    public DateOnly Invoicedate { get; set; }

    public string Indescription { get; set; } = null!;

    public virtual ICollection<Client> Idclients { get; set; } = new List<Client>();
}
