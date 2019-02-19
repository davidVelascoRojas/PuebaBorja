namespace Lands.Domain.Models
{
    public class Departamento
    {
        public string ClaveSuc { get; set; }
        public string ClaveDep { get; set; }
        public string Descripcion { get; set; }

        public Departamento()
        {
            ClaveSuc = "";
            ClaveDep = "";
            Descripcion = "";

        }
    }
}
