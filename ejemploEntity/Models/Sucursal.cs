using System;
using System.Collections.Generic;

namespace ejemploEntity.Models;

public partial class Sucursal
{
    public double? SucursalId { get; set; }

    public string? SucursalNombre { get; set; }

    public string? Estado { get; set; }

    public DateTime? FechaHoraReg { get; set; }

    public double? CiudadId { get; set; }
}
