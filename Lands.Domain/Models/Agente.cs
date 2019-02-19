namespace Lands.Domain.Models
{
    public class Agente
    {
        public string ClaveAge { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public string Servidor { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }

        public Agente()
        {
            ClaveAge = "";
            Nombre = "";
            Password = "";
            Servidor = "";
            Status = false;
            Message = "";
        }
    }
}
