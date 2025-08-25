namespace VeterinariaAPI.DTOs
{
    public class MascotaRegistrarDTO
    {
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public string Raza { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public int IdCliente { get; set; }    // FK, but user must provide 
    }
}
