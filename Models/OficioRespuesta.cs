public class OficioRespuesta
{
    public int Id { get; set; }
    public int OficioId { get; set; }
    public string Comentario { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public DateTime FechaRespuesta { get; set; } = DateTime.Now;
}