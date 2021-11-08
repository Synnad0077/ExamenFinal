using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalDesarrollo.DbModels;
using FinalDesarrollo.Models;

namespace FinalDesarrollo.Controllers.Api
{
    [ApiController]
    [Produces("application/json")]
    public class ReportesController : ControllerBase
    {
        private readonly ctrlalumnosContext _context;

        public ReportesController(ctrlalumnosContext context)
        {
            _context = context;
        }

        // GET: api/Alumno
        [HttpGet]
        [Route("api/Reportes/ObtenerAlumnosActivos")]
        public async Task<ActionResult<IEnumerable<Alumno>>> GetAlumnos()
        {
            return await _context.Alumnos.Where(x => x.Activo).ToListAsync();
        }

        [HttpGet]
        [Route("api/Reportes/ObtenerAlumnosAprobados")]
        public IEnumerable<ReporteNotas> GetAlumnosAprobados()
        {
            var test = (from nota in _context.Asignacions
                        join alumno in _context.Alumnos on nota.AlumnoId equals alumno.AlumnoId
                        where (nota.Nota + nota.Zona + nota.ExFinal) >= 61
                        select new ReporteNotas
                        {
                            Codigo = alumno.Codigo,
                            Nombre = alumno.Nombre,
                            Nota = nota.Nota ?? 0,
                            Zona = nota.Zona ?? 0,
                            ExamenFinal = nota.ExFinal ?? 0,
                            Total = (nota.Nota + nota.Zona + nota.ExFinal) ?? 0
                        }).ToList();

            return test;
        }

        [HttpGet]
        [Route("api/Reportes/ObtenerAlumnosReprobados")]
        public IEnumerable<ReporteNotas> GetAlumnosReprobados()
        {
            var test = (from nota in _context.Asignacions
                        join alumno in _context.Alumnos on nota.AlumnoId equals alumno.AlumnoId
                        where (nota.Nota + nota.Zona + nota.ExFinal) < 61
                        select new ReporteNotas
                        {
                            Codigo = alumno.Codigo,
                            Nombre = alumno.Nombre,
                            Nota = nota.Nota ?? 0,
                            Zona = nota.Zona ?? 0,
                            ExamenFinal = nota.ExFinal ?? 0,
                            Total = (nota.Nota + nota.Zona + nota.ExFinal) ?? 0
                        }).ToList();

            return test;
        }
    }
}