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
    public class NotasController : ControllerBase
    {
        private readonly ctrlalumnosContext _context;

        public NotasController(ctrlalumnosContext context)
        {
            _context = context;
        }

        // PUT: api/Notas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        [Route("api/Notas/ActualizarNota/{id}")]
        public async Task<IActionResult> PutAsignacion(int id, NotasRequest asignacionRequest)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var asignacion = _context.Asignacions.First(x => x.AsignacionId == id);
            asignacion.AlumnoId = asignacionRequest.AlumnoId;
            asignacion.CursoId = asignacionRequest.CursoId;
            asignacion.Nota = asignacionRequest.Nota;
            asignacion.Zona = asignacionRequest.Zona;
            asignacion.ExFinal = asignacionRequest.ExamenFinal;

            _context.Asignacions.Update(asignacion);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsignacionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Notas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("api/Notas/IngresarNota")]
        public async Task<ActionResult<NotasRequest>> PostAsignacion(NotasRequest asignacion)
        {
            var newAsignacion = new Asignacion
            {
                Alumno = _context.Alumnos.First(x => x.AlumnoId == asignacion.AlumnoId),
                AlumnoId = asignacion.AlumnoId,
                Curso = _context.Cursos.First(x => x.CursoId == asignacion.CursoId),
                CursoId = asignacion.CursoId,
                Nota = asignacion.Nota,
                Zona = asignacion.Zona,
                ExFinal = asignacion.ExamenFinal
            };

            _context.Asignacions.Add(newAsignacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAsignacion", new { id = newAsignacion.AsignacionId }, asignacion);
        }

        private bool AsignacionExists(int id)
        {
            return _context.Asignacions.Any(e => e.AsignacionId == id);
        }
    }
}
