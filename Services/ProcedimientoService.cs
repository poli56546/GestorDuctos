using Microsoft.EntityFrameworkCore;
using GestorDuctos.Data;
using GestorDuctos.Models;

namespace GestorDuctos.Services
{
    public class ProcedimientoService : IProcedimientosService
    {
        private readonly ApplicationDbContext _context;

        public ProcedimientoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Procedimiento>> ObtenerProcedimientosAsync()
        {
            return await _context.Procedimientos.ToListAsync();
        }

        public async Task<Procedimiento?> ObtenerProcedimientoPorIdAsync(int id)
        {
            return await _context.Procedimientos
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task GuardarProcedimientoAsync(Procedimiento procedimiento)
        {
            var total = await _context.Procedimientos.CountAsync();
            procedimiento.Codigo = $"PR-SGC-{(total + 1).ToString("D3")}";

            _context.Procedimientos.Add(procedimiento);
            await _context.SaveChangesAsync();
        }

        public async Task<int> ContarTotalAsync()
        {
            return await _context.Procedimientos.CountAsync();
        }

        public async Task AgregarRespuestaAsync(OficioRespuesta respuesta)
        {
            // Nota: Si usas otra entidad para el historial de procedimientos, ajústala aquí.
            // Si los procedimientos comparten la tabla de respuestas con oficios:
            _context.Add(respuesta);
            await _context.SaveChangesAsync();
        }
    }
}