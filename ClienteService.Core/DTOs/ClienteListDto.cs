using System;

namespace ClienteService.Core.DTOs
{
    public class ClienteListDto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string FechaNacimiento { get; set; }
        public string FechaProbableMuerte { get; set; }
    }
}
