namespace FinanClickApi.Dtos
{
    public class DocumentoClienteDto
    {
        public int IdCliente { get; set; }
        public List<int> IdsDocumentos { get; set; } = new List<int>();

    }
}
