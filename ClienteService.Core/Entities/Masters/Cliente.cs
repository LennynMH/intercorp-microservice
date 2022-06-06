using ClienteService.Core.Base;

namespace ClienteService.Core.Entities.Masters
{
    public partial class Cliente : EntityBase
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string FechaNacimiento { get; set; }
    }
}
