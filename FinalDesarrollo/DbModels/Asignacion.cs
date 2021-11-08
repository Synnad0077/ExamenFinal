using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FinalDesarrollo.DbModels
{
    public partial class Asignacion
    {
        public int AsignacionId { get; set; }
        public int AlumnoId { get; set; }
        public int CursoId { get; set; }
        public decimal? Nota { get; set; }
        public decimal? Zona { get; set; }
        public decimal? ExFinal { get; set; }

        public virtual Alumno Alumno { get; set; }
        public virtual Curso Curso { get; set; }

        [NotMapped]
        public decimal NotaFinal { get { return (Nota ?? 0) + (Zona ?? 0) + (ExFinal ?? 0); } }
    }
}
