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
    public class AlumnoController : ControllerBase
    {
        private readonly ctrlalumnosContext _context;

        public AlumnoController(ctrlalumnosContext context)
        {
            _context = context;
        }

        // GET: api/Alumno/5
        [HttpGet]
        [Route("api/Alumnos/ObtenerAlumno/{id}")]
        public async Task<ActionResult<Alumno>> GetAlumno(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);

            if (alumno == null)
            {
                return NotFound();
            }

            return alumno;
        }

        // PUT: api/Alumno/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        [Route("api/Alumnos/ActualizarAlumno/{id}")]
        public async Task<IActionResult> PutAlumno(int id, AlumnoRequest alumno)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var alumn = _context.Alumnos.First(x => x.AlumnoId == id);
            alumn.Nombre = alumno.Nombre;
            alumn.Direccion = alumno.Direccion;
            alumn.Telefono = alumno.Telefono;
            alumn.Codigo = alumno.Codigo;

            _context.Alumnos.Update(alumn);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlumnoExists(id))
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

        // POST: api/Alumno
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Route("api/Alumnos/CrearAlumno")]
        public async Task<ActionResult<AlumnoRequest>> PostAlumno(AlumnoRequest alumno)
        {
            var alumn = new Alumno
            {
                Nombre = alumno.Nombre,
                Direccion = alumno.Direccion,
                Telefono = alumno.Telefono,
                Codigo = alumno.Codigo
            };
            _context.Alumnos.Add(alumn);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlumno", new { id = alumn.AlumnoId }, alumno);
        }

        // DELETE: api/Alumno/5
        [HttpDelete]
        [Route("api/Alumnos/EliminarAlumno/{id}")]
        public async Task<ActionResult<Alumno>> DeleteAlumno(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }
            alumno.Activo = false;
            _context.Alumnos.Update(alumno);
            await _context.SaveChangesAsync();

            return alumno;
        }

        private bool AlumnoExists(int id)
        {
            return _context.Alumnos.Any(e => e.AlumnoId == id);
        }
    }
}
