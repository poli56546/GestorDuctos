namespace GestorDuctos.Models
{
    public class Plano
    {
        public int Id { get; set; }
        public string CodigoPlano { get; set; } = string.Empty; 
        public string Nombre { get; set; } = string.Empty;      
        public string Disciplina { get; set; } = string.Empty;   
        public string Version { get; set; } = "V1";              
        public string AutorizacionModificacion { get; set; } = string.Empty; 
        public bool EsVigente { get; set; } = true;              
        
        public int? ProyectoId { get; set; }
        public Proyecto? ProyectoRelacionado { get; set; }

        public string? RutaArchivo { get; set; }

        public bool EsAsBuilt { get; set; } = false; 
        public DateTime? FechaSubida { get; set; }
    public long? TamanoBytes { get; set; }
        
    }
    
}
