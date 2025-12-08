namespace ServicioApiBodegaBalanceado.Domain.DTO
{
    public class DataImageDto
    {
        public string Identificador { set; get; } = null!;
        public string Url { set; get; } = null!;
        public bool Estado { set; get; } = false;
        public string Tipo { set; get; } = "";
    }
}