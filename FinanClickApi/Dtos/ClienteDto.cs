using FinanClickApi.Models;

namespace FinanClickApi.Dtos
{
    public class ClienteDto
    {
        public Cliente Cliente { get; set; }
        public Persona? Persona { get; set; }
        public PersonaMoral? PersonaMoral { get; set; }

        public DatosClienteMoral? DatosClienteMoral { get; private set; }
    }
}
