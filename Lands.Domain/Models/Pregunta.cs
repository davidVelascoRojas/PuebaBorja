namespace Lands.Domain.Models
{
    public class Pregunta
    {
        public string ClavePre { get; set; }
        public string ClaveSuc { get; set; }
        public string ClaveDep { get; set; }
        public string Descripcion { get; set; }
        public bool Respuesta { get; set; }

        public Pregunta()
        {
            ClavePre = "";
            ClaveSuc = "";
            ClaveDep = "";
            Descripcion = "";
            Respuesta = false;
        }
    }
}
