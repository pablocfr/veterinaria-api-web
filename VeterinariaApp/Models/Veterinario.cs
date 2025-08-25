namespace VeterinariaApp.Models
{
    public class Veterinario
    {
        public int IDVeterinario { get; set; }
        public string NombreCompleto { get; set; }
        public string DNI { get; set; }
        public string Telefono { get; set; }
        public string Especialidad { get; set; }
        public string Correo { get; set; }
        public bool Estado { get; set; }
    }
}
