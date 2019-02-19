namespace Lands.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.Http;
    using Domain.Models;

    public class SucursalesController : ApiController
    {
        public Catalogos Post([FromBody]Sucursal cat)
        {
            Sucursal sucursal;
            Catalogos catalagos = new Catalogos();
            try
            {
                SqlConnection con = Connection.Conn();
                con.Open();

                string sqlSuc = "Select_Sucursal";
                SqlCommand cmdSuc = new SqlCommand(sqlSuc, con);
                cmdSuc.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataAdapter adaptadorSuc = new SqlDataAdapter();
                adaptadorSuc.SelectCommand = cmdSuc;
                DataTable tablaSuc = new DataTable();
                adaptadorSuc.Fill(tablaSuc);

                for (int i = 0; i < tablaSuc.Rows.Count; i++)
                {
                    sucursal = new Sucursal();
                    sucursal.ClaveSuc = tablaSuc.Rows[i]["ClaveSuc"].ToString();
                    sucursal.Descripcion = tablaSuc.Rows[i]["Descripcion"].ToString();
                    catalagos.ListSucursal.Add(sucursal);
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
