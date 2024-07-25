namespace FinanClickApi.Dtos
{
    public class DocumentoClienteReq
    {
        public int IdDocumentoCliente { get; set; }
        public string DocumentoBase64 { get; set; } = null!;
        public int Estatus { get; set; }
    }
}
