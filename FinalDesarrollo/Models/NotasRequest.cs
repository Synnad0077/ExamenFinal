using System;
namespace FinalDesarrollo.Models
{
    public class NotasRequest
    {
        public int AlumnoId { get; set; }
        public int CursoId { get; set; }
        public decimal Nota { get; set; }
        public decimal Zona { get; set; }
        public decimal ExamenFinal { get; set; }
    }
}
