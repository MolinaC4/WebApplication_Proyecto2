namespace WebApplication_Proyecto2.Models.Domain
{
    public class Lstock
    {
        public Guid Id { get; set; }

        public string Descripcion { get; set; }
        public string Precio { get; set; }

        public string Cod_Cliente { get; set; }

        public string Fec_Ingreso { get; set; }
    }
}
