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
            _context.Set<OficioRespuesta>().Add(respuesta);
            await _context.SaveChangesAsync();
        }

        public async Task GuardarOficioAsync(Oficio oficio)
        {
            // Formato: OF-2026-001, OF-2026-002... (Incluye el año actual)
            var total = await _context.Oficios.CountAsync();
            oficio.Folio = $"OF-{DateTime.Now.Year}-{(total + 1).ToString("D3")}";

            _context.Oficios.Add(oficio);
            await _context.SaveChangesAsync();
        }
    }
}