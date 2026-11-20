using Microsoft.EntityFrameworkCore;
using GestorDuctos.Data;
using GestorDuctos.Models;

namespace GestorDuctos.Services
{
    public class ProyectoService
    {
        private readonly ApplicationDbContext _context;

        public ProyectoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Proyecto>> ObtenerProyectosAsync()
        {
            return await _context.Proyectos.ToListAsync();
        }

        public async Task GuardarProyectoAsync(Proyecto proyecto)
        {
            // Asignar la fecha actual obligatoriamente para cumplir con la restricción de la base de datos
            proyecto.FechaCreacion = DateTime.UtcNow;

            // Cuenta cuántos hay y le suma 1. Formato: PRJ-001, PRJ-002...
            var total = await _context.Proyectos.CountAsync();
            proyecto.Clave = $"PRJ-{(total + 1).ToString("D3")}";

            _context.Proyectos.Add(proyecto);
            await _context.SaveChangesAsync();
        }
        public async Task AgregarRespuestaAsync(OficioRespuesta respuesta)
        {
            if (respuesta.FechaRespuesta == default)
            {
                respuesta.FechaRespuesta = DateTime.UtcNow;
            }
            else if (respuesta.FechaRespuesta.Kind != DateTimeKind.Utc)
            {
                respuesta.FechaRespuesta = DateTime.SpecifyKind(respuesta.FechaRespuesta, DateTimeKind.Utc);
            }

            _context.Add(respuesta);
            await _context.SaveChangesAsync();
        }
    }
}
