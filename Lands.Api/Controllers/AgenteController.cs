namespace Lands.Api.Controllers
{
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.Http;
    using Domain.Models;

    public class AgenteController : ApiController
    {
        // POST api/values
        public Agente Post([FromBody]Agente Agente)
        {
            try
            {
                SqlConnection con = Connection.Conn();
                con.Open();

                string sql = "Select_Agente";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@ClaveAge", System.Data.SqlDbType.VarChar).Value = Agente.ClaveAge;
                cmd.Parameters.Add("@Password", System.Data.SqlDbType.VarChar).Value = Agente.Password;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = cmd;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);

                if (tabla.Rows.Count < 1)
                {
                    Agente.Status = false;
                    Agente.Message = "Agente no Encontrado";
                    return Agente;
                }

                Agente.ClaveAge = tabla.Rows[0]["ClaveAge"].ToString();
                Agente.Nombre = tabla.Rows[0]["Nombre"].ToString();
                Agente.Password = tabla.Rows[0]["Password"].ToString();
                Agente.Message = "";
                Agente.Status = true;
            }
            catch (System.Exception e)
            {
                Agente.Status = false;
                Agente.Message = e.Message.ToString();
            }

            return Agente;
        }
    }
}
