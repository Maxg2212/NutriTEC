using System;
using System.Collections.Generic;

namespace NutriTEC_API.Models;

public partial class Invoice
{
    public string InvoiceId { get; set; } = null!;

    public string PayAmount { get; set; } = null!;

    public DateOnly InvoiceDate { get; set; }

    public string InDescription { get; set; } = null!;
}
