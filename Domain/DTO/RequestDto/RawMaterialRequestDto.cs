namespace Domain.DTO.RequestDto
{
    public class RawMaterialRequestDto : TYPE
    {

        public RawMaterialRequestDto(int valor) : base(valor) { }

        public string guid { set; get; } = Guid.NewGuid().ToString();
        public string nombreProducto { set; get; } = null!;
        public string rutaImagen { set; get; } = null!;
        public DateTime fechaUltimaCompra { set; get; } = DateTime.Now;
        public double kgTotal { set; get; } = 0.0d;

    }

    public class TYPE
    {
        public STATUS status { get; set; }
        public string? color { set; get; }
        public string? backgroundColor { set; get; }

        public string? text { set; get; }

        public TYPE(int valor)
        {
            this.status = (STATUS)valor;

            procesar();
        }

        public void procesar()
        {
            switch (status)
            {
                case STATUS.Disponible:
                    this.color = "#4CAF50";
                    this.backgroundColor = "#9CC4F693";
                    this.text = "Disponible";
                    break;

                case STATUS.Bajo:
                    this.color = "#FF9800";
                    this.backgroundColor = "#9CF7DA8F";
                    this.text = "Stock Bajo";
                    break;

                case STATUS.Agotado:
                    this.color = "#F44336";
                    this.backgroundColor = "#9CF4A8B9";
                    this.text = "Agotado";
                    break;
            }
        }
    }


    public enum STATUS : int
    {
        Disponible,
        Bajo,
        Agotado
    }
}
