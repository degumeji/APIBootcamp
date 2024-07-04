using System;
using System.Collections.Generic;

namespace ejemploEntity.Models;

public partial class Marca
{
    public double? MarcaId { get; set; }

    public string? MarcaNombre { get; set; }

    public string? Estado { get; set; }

    public DateTime? FechaHoraReg { get; set; }
}
