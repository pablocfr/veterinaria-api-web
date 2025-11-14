namespace VeterinariaAPI.DTOs
{
    public class MascotaActualizarDTO
    {
        public int IdMascota { get; set; }
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public string Raza { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        //IdCliente y Estado no se incluiran porque no se mudan de dueño.
    }
}
