namespace GestorDuctos.Models
{
    public class Oficio
    {
        public int Id { get; set; }
        public string Folio { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty; // Interno, Entrada, Salida
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Asunto { get; set; } = string.Empty;
        public string? DirigidoAtencion { get; set; } // Dirigido o en atención
        public string? TipoOficio { get; set; }      // Ej. "Oficio de la empresa" o "Seguimiento de oficio de PEMEX"
        public string? OficioSolicitudNo { get; set; } // Oficio de solicitud No.
        public DateTime FechaSolicitud { get; set; } = DateTime.Now; // Seguimiento de la fecha de la solicitud
        public string Remitente { get; set; } = string.Empty;
        public string Destinatario { get; set; } = string.Empty;
        public string Proyecto { get; set; } = string.Empty;
        public string Estado { get; set; } = "Registrado"; // Registrado, En revisión, Atendido

        // Campos para la gestión y descarga de archivos adjuntos
        public string? RutaArchivo { get; set; }
        public DateTime? FechaSubida { get; set; }
        public long? TamanoBytes { get; set; }

        public int? ProyectoId { get; set; }
        public Proyecto? ProyectoRelacionado { get; set; }
        public List<OficioRespuesta> Respuestas { get; set; } = new();
        public bool AprobadoPorJefe { get; set; } = false;
        public string? DocumentoValidado { get; set; }
    }
}