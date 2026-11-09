namespace GestorDuctos.Models
{
    public class Proyecto
    {
        public int Id { get; set; }
        public string Clave { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? ContratoCliente { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}