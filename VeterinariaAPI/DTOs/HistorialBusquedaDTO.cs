namespace VeterinariaAPI.DTOs
{
    public class HistorialBusquedaDTO
    {
        public int IdHistorial { get; set; }
        public string NombreMascota { get; set; }
        public string EspecieMascota { get; set; }
        public string NombreCliente { get; set; }
        public DateTime FechaAtencion { get; set; }
        public string MotivoCita { get; set; }
        public string DiagnosticoResumen { get; set; }
    }
}