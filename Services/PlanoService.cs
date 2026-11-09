using Microsoft.EntityFrameworkCore;
using GestorDuctos.Data;
using GestorDuctos.Models;

namespace GestorDuctos.Services
{
    public class PlanoService
    {
        private readonly ApplicationDbContext _context;

        public PlanoService(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. CORREGIDO: Este método debe devolver toda la lista
        public async Task<List<Plano>> ObtenerPlanosAsync()
        {
            return await _context.Planos.ToListAsync();
        }

        // 2. AGREGADO: Este es el método que busca por ID para la página Detalle.razor
        public async Task<Plano?> ObtenerPlanoPorIdAsync(int id)
        {
            return await _context.Planos.FirstOrDefaultAsync(p => p.Id == id);
        }

        // 3. TUS MÉTODOS ORIGINALES SE QUEDAN IGUAL
        public async Task GuardarPlanoAsync(Plano plano)
        {
            var planosAnteriores = await _context.Planos
                .Where(p => p.CodigoPlano == plano.CodigoPlano && p.EsVigente)
                .ToListAsync();

            foreach (var p in planosAnteriores)
            {
                p.EsVigente = false;
            }

            plano.EsVigente = true;
            _context.Planos.Add(plano);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Plano>> ObtenerHistorialVersionesAsync(string codigoPlano)
        {
            return await _context.Planos
                .Include(p => p.ProyectoRelacionado)
                .Where(p => p.CodigoPlano == codigoPlano)
                .OrderByDescending(p => p.Id) // De la más reciente a la más antigua
                .ToListAsync();
        }

        internal async Task<List<Plano>?> ObtenerHistorialVersionesAsync(object codigo)
        {
            throw new NotImplementedException();
        }
    }
}