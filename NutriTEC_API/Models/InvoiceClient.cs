using System;
using System.Collections.Generic;

namespace NutriTEC_API.Models;

public partial class InvoiceClient
{
    public string IdClient { get; set; } = null!;

    public string InvoiceIdc { get; set; } = null!;

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual Invoice InvoiceIdcNavigation { get; set; } = null!;
}
