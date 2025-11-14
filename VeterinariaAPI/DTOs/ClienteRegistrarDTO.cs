namespace VeterinariaAPI.DTOs
{
    public class ClienteRegistrarDTO
    {
        public string Nombre { get; set; }
        public string DNI { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        // Estado no se envía al crear (SP lo asume); si quieres, agrega bool Estado = true;
    }
}
