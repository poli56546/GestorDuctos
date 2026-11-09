using GestorDuctos.Models;

namespace GestorDuctos.Services
{
    public interface IProcedimientosService
    {
        Task<List<Procedimiento>> ObtenerProcedimientosAsync();
        Task<Procedimiento?> ObtenerProcedimientoPorIdAsync(int id);
        Task GuardarProcedimientoAsync(Procedimiento procedimiento);
        Task<int> ContarTotalAsync();
        Task AgregarRespuestaAsync(OficioRespuesta respuesta); // Método añadido para el Historial y Versiones SGC
    }
}