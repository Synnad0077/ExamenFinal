using System;
using System.Collections.Generic;

#nullable disable

namespace FinalDesarrollo.DbModels
{
    public partial class Curso
    {
        public Curso()
        {
            Asignacions = new HashSet<Asignacion>();
        }

        public int CursoId { get; set; }
        public string Nombre { get; set; }
        public int NotaMinima { get; set; }

        public virtual ICollection<Asignacion> Asignacions { get; set; }
    }
}
