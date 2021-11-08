using System;
namespace FinalDesarrollo.Models
{
    public class ReporteNotas
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal Nota { get; set; }
        public decimal Zona { get; set; }
        public decimal ExamenFinal { get; set; }
        public decimal Total { get; set; }
    }
}
