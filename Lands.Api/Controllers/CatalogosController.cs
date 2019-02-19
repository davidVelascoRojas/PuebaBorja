namespace Lands.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.Http;
    using Domain.Models;

    public class CatalogosController : ApiController
    {
        public Catalogos Post([FromBody]Catalogos cat)
        {
            Sucursal sucursal;
            Departamento departamento;
            Pregunta pregunta;
            Catalogos catalagos = new Catalogos();
            try
            {
                SqlConnection con = Connection.Conn();
                con.Open();

                string sqlSuc = "Select_Sucursal";
                string sqlDep = "Select_Departamento";
                string sqlPre = "Select_Pregunta";
                SqlCommand cmdSuc = new SqlCommand(sqlSuc, con);
                SqlCommand cmdDep = new SqlCommand(sqlDep, con);
                SqlCommand cmdPre = new SqlCommand(sqlPre, con);
                cmdSuc.CommandType = System.Data.CommandType.StoredProcedure;
                cmdDep.CommandType = System.Data.CommandType.StoredProcedure;
                cmdPre.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter adaptadorSuc = new SqlDataAdapter();
                SqlDataAdapter adaptadorDep = new SqlDataAdapter();
                SqlDataAdapter adaptadorPre = new SqlDataAdapter();
                adaptadorSuc.SelectCommand = cmdSuc;
                adaptadorDep.SelectCommand = cmdDep;
                adaptadorPre.SelectCommand = cmdPre;
                DataTable tablaSuc = new DataTable();
                DataTable tablaDep = new DataTable();
                DataTable tablaPre = new DataTable();
                adaptadorSuc.Fill(tablaSuc);
                adaptadorDep.Fill(tablaDep);
                adaptadorPre.Fill(tablaPre);
                
                for (int i = 0; i < tablaSuc.Rows.Count; i++)
                {
                    sucursal = new Sucursal();
                    sucursal.ClaveSuc = tablaSuc.Rows[i]["ClaveSuc"].ToString();
                    sucursal.Descripcion = tablaSuc.Rows[i]["Descripcion"].ToString();
                    catalagos.ListSucursal.Add(sucursal);
                }
                for (int i = 0; i < tablaDep.Rows.Count; i++)
                {
                    departamento = new Departamento();
                    departamento.ClaveSuc = tablaDep.Rows[i]["ClaveSuc"].ToString();
                    departamento.ClaveDep = tablaDep.Rows[i]["ClaveDep"].ToString();
                    departamento.Descripcion = tablaDep.Rows[i]["Descripcion"].ToString();
                    catalagos.ListDepartamento.Add(departamento);
                }
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
