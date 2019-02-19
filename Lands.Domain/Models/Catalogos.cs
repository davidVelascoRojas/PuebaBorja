namespace Lands.Domain.Models
{
    using System.Collections.Generic;
    public class Catalogos
    {
        public List<Sucursal> ListSucursal { get; set; }
        public List<Departamento> ListDepartamento { get; set; }
        public List<Pregunta> ListPregunta { get; set; }
        public Sucursal Sucursal { get; set; }
        public Departamento Departamento { get; set; }
        public Pregunta Pregunta { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }

        public Catalogos()
        {
            ListSucursal = new List<Sucursal>();
            ListDepartamento = new List<Departamento>();
            ListPregunta = new List<Pregunta>();
            Sucursal = new Sucursal();
            Departamento = new Departamento();
            Pregunta = new Pregunta();
            Status = false;
            Message = "";
        }
    }
}
