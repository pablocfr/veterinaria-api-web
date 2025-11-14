namespace VeterinariaAPI.DTOs
{
    public class CitaActualizarDTO
    {
        public int IdCita { get; set; }
        public DateTime Fecha { get; set; }
        public string Motivo { get; set; }
        public int IdMascota { get; set; }
        public int IdVeterinario { get; set; }
    }
}
