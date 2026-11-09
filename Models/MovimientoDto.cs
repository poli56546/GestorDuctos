namespace GestorDuctos.Models // Asegúrate de que coincida con tu estructura
{
    public class MovimientoDto
    {
        public string Descripcion { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string FechaTexto { get; set; } = string.Empty;
    }
}