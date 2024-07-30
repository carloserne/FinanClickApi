using FinanClickApi.Models;

namespace FinanClickApi.Dtos
{
    public class UsuarioClienteDto
    {
        public UsuarioCliente UsuarioCliente { get; set; }
        public Cliente Cliente { get; set; }

        // Persona Moral
        public PersonaMoral? PersonaMoral { get; set; }
        public DatosClienteMoral? DatosClienteMoral { get; set; }

        // Persona Fisica
        public Persona? Persona { get; set; }
        public DatosClienteFisica? DatosClienteFisica { get; set; }

        public Empresa Empresa { get; set; }

    }
}
