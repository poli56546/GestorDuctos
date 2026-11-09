namespace GestorDuctos.Models
{
    public class Procedimiento
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;     // Ej: SGC-PR-01
        public string Titulo { get; set; } = string.Empty;     // Ej: Procedimiento de inspección de soldadura
        public string Version { get; set; } = "V1";            // Versión del documento
        public string Area { get; set; } = string.Empty;       // Ej: Calidad, Construcción, Seguridad
        public string Responsable { get; set; } = string.Empty; // Quién lo elaboró o aprueba
        public DateTime FechaEmision { get; set; } = DateTime.Now;
        public int? ProyectoId { get; set; }
        public Proyecto? ProyectoRelacionado { get; set; }
    }
}