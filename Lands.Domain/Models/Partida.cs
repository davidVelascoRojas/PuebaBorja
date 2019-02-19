namespace Lands.Domain.Models
{
    public class Partida
    {
        public int IdEnc { get; set; }
        public string ClaveDep { get; set; }
        public string ClavePre { get; set; }
        public string Respuesta { get; set; }

        public Partida()
        {
            IdEnc = 0;
            ClaveDep = "";
            ClavePre = "";
            Respuesta = "";
        }
    }
}
