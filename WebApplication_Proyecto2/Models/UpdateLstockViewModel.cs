namespace WebApplication_Proyecto2.Models
{
    public class UpdateLstockViewModel
    {
        public Guid Id { get; set; }

        public string Descripcion { get; set; }
        public string Precio { get; set; }

        public string Cod_Cliente { get; set; }

        public string Fec_Ingreso { get; set; }
    }
}
