namespace VeterinariaApp.Models
{
    public class Mascota
    {
        public int IdMascota { get; set; }
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public string Raza { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public int IdCliente { get; set; }
        public bool Estado { get; set; }

    }
}
