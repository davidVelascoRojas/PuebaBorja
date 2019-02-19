namespace Lands.Domain.Models
{
    public class Sucursal
    {
        public string ClaveSuc { get; set; }
        public string Descripcion { get; set; }

        public Sucursal()
        {
            ClaveSuc = "";
            Descripcion = "";
        }
    }
}
