namespace Lands.Domain.Models
{
    using System.Collections.Generic;

    public class Encuesta
    {
        public Encabezado Encabezado { get; set; }
        public List<Partida> Partidas { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }

        public Encuesta()
        {
            Encabezado = new Encabezado();
            Partidas = new List<Partida>();
            Status = false;
            Message = "";
        }
    }
}
