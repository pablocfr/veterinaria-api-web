namespace VeterinariaAPI.DTOs
{
    public class ClienteActualizarDTO
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string DNI { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public bool Estado { get; set; }
    }
}
