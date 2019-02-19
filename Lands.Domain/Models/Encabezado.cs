namespace Lands.Domain.Models
{
    using System;

    public class Encabezado
    {
        public string ClaveSuc { get; set; }
        public string ClaveAge { get; set; }
        public DateTime Fecha { get; set; }

        public Encabezado()
        {
            ClaveSuc = "";
            ClaveAge = "";
            Fecha = new DateTime();
        }
    }
}
