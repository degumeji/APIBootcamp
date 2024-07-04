using System;
using System.Collections.Generic;

namespace ejemploEntity.Models;

public partial class Venta
{
    public double? IdFactura { get; set; }

    public string? NumFact { get; set; }

    public DateTime? FechaHora { get; set; }

    public double? ClienteId { get; set; }

    public double? ProductoId { get; set; }

    public double? ModeloId { get; set; }

    public double? CategId { get; set; }

    public double? MarcaId { get; set; }

    public double? SucursalId { get; set; }

    public string? Caja { get; set; }

    public string? Vendedor { get; set; }

    public double? Precio { get; set; }

    public double? Unidades { get; set; }

    public string? Estado { get; set; }
}
