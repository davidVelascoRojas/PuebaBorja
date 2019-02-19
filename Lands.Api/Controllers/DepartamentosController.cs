namespace Lands.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Web.Http;
    using Domain.Models;

    public class DepartamentosController : ApiController
    {
        public Catalogos Post([FromBody]Catalogos cat)
        {
            Departamento departamento;
            Catalogos catalogos = new Catalogos();
            Catalogos catalagos = new Catalogos();
            try
            {
                SqlConnection con = Connection.Conn();
                con.Open();

                string sqlDep = "Select_Departamento";
                SqlCommand cmdDep = new SqlCommand(sqlDep, con);
                cmdDep.Parameters.Add("@ClaveSuc", System.Data.SqlDbType.VarChar).Value = cat.Departamento.ClaveSuc;
                cmdDep.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter adaptadorDep = new SqlDataAdapter();
                adaptadorDep.SelectCommand = cmdDep;
                DataTable tablaDep = new DataTable();
                adaptadorDep.Fill(tablaDep);
                
                for (int i = 0; i < tablaDep.Rows.Count; i++)
                {
                    departamento = new Departamento();
                    departamento.ClaveSuc = tablaDep.Rows[i]["ClaveSuc"].ToString();
                    departamento.ClaveDep = tablaDep.Rows[i]["ClaveDep"].ToString();
                    departamento.Descripcion = tablaDep.Rows[i]["Descripcion"].ToString();
                    catalagos.ListDepartamento.Add(departamento);
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
