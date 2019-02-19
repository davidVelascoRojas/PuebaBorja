namespace Lands.Api.Controllers
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.Http;
    using Domain.Models;

    public class PreguntasController : ApiController
    {
        public Catalogos Post([FromBody]Catalogos cat)
        {
            Pregunta pregunta;
            Catalogos catalagos = new Catalogos();
            try
            {
                SqlConnection con = Connection.Conn();
                con.Open();

                string sqlPre = "Select_Pregunta";
                SqlCommand cmdPre = new SqlCommand(sqlPre, con);
                cmdPre.Parameters.Add("@ClaveSuc", System.Data.SqlDbType.VarChar).Value = cat.Pregunta.ClaveSuc;
                cmdPre.Parameters.Add("@ClaveDep", System.Data.SqlDbType.VarChar).Value = cat.Pregunta.ClaveDep;
                cmdPre.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter adaptadorPre = new SqlDataAdapter();
                adaptadorPre.SelectCommand = cmdPre;
                DataTable tablaPre = new DataTable();
                adaptadorPre.Fill(tablaPre);

                for (int i = 0; i < tablaPre.Rows.Count; i++)
                {
                    pregunta = new Pregunta();
                    pregunta.ClavePre = tablaPre.Rows[i]["ClavePre"].ToString();
                    pregunta.ClaveSuc = tablaPre.Rows[i]["ClaveSuc"].ToString();
                    pregunta.ClaveDep = tablaPre.Rows[i]["ClaveDep"].ToString();
                    pregunta.Descripcion = tablaPre.Rows[i]["Descripcion"].ToString();
                    catalagos.ListPregunta.Add(pregunta);
                }
                catalagos.Status = true;
                catalagos.Message = "";
            }
            catch (Exception e)
            {
                catalagos.Status = false;
                catalagos.Message = e.Message.ToString();
            }

            return catalagos;
        }
    }
}
