using Microsoft.EntityFrameworkCore;
using GestorDuctos.Data;
using GestorDuctos.Models;

namespace GestorDuctos.Services
{
    public class OficioService
    {
        private readonly ApplicationDbContext _context;

        public OficioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Oficio>> ObtenerOficiosAsync()
        {
            return await _context.Oficios.ToListAsync();
        }

        public async Task<Oficio?> ObtenerOficioPorIdAsync(int id)
        {
            return await _context.Oficios
                .Include(o => o.Respuestas)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AgregarRespuestaAsync(OficioRespuesta respuesta)
        {
            if (respuesta.FechaRespuesta == default)
            {
                respuesta.FechaRespuesta = DateTime.UtcNow;
            }
            else if (respuesta.FechaRespuesta.Kind != DateTimeKind.Utc)
            {
                respuesta.FechaRespuesta = respuesta.FechaRespuesta.ToUniversalTime();
            }

            _context.Set<OficioRespuesta>().Add(respuesta);
            await _context.SaveChangesAsync();
        }

        public async Task GuardarOficioAsync(Oficio oficio)
        {
            // Formato: OF-2026-001, OF-2026-002... (Incluye el año actual)
            var total = await _context.Oficios.CountAsync();
            oficio.Folio = $"OF-{DateTime.UtcNow.Year}-{(total + 1).ToString("D3")}";

            if (oficio.Fecha == default)
            {
                oficio.Fecha = DateTime.UtcNow;
            }
            else if (oficio.Fecha.Kind != DateTimeKind.Utc)
            {
                oficio.Fecha = DateTime.SpecifyKind(oficio.Fecha, DateTimeKind.Utc);
            }

            if (oficio.FechaSolicitud == default)
            {
                oficio.FechaSolicitud = DateTime.UtcNow;
            }
            else if (oficio.FechaSolicitud.Kind != DateTimeKind.Utc)
            {
                oficio.FechaSolicitud = DateTime.SpecifyKind(oficio.FechaSolicitud, DateTimeKind.Utc);
            }

            if (oficio.FechaSubida.HasValue && oficio.FechaSubida.Value.Kind != DateTimeKind.Utc)
            {
                oficio.FechaSubida = DateTime.SpecifyKind(oficio.FechaSubida.Value, DateTimeKind.Utc);
            }

            if (string.IsNullOrWhiteSpace(oficio.Estado))
            {
                oficio.Estado = "Abierto";
            }

            _context.Oficios.Add(oficio);
            await _context.SaveChangesAsync();
        }
    }
}