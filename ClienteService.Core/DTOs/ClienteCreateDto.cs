namespace ClienteService.Core.DTOs
{
    public class ClienteCreateDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string FechaNacimiento { get; set; }
    }
}
