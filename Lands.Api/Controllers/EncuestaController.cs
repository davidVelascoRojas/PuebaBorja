namespace Lands.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.Http;
    using Domain.Models;

    public class EncuestaController : ApiController
    {
        public Encuesta Post([FromBody]Encuesta valurObject)
        {
            try
            {
                SqlConnection con = Connection.Conn();
                con.Open();

                string sql = "insert_Encabezado";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@ClaveSuc", System.Data.SqlDbType.VarChar).Value = valurObject.Encabezado.ClaveSuc;
                cmd.Parameters.Add("@ClaveAge", System.Data.SqlDbType.VarChar).Value = valurObject.Encabezado.ClaveAge;
                cmd.Parameters.Add("@Fecha", System.Data.SqlDbType.DateTime).Value = valurObject.Encabezado.Fecha;
                cmd.Parameters.Add("@resp", System.Data.SqlDbType.Int).Value = 0;
                cmd.Parameters["@resp"].Direction = ParameterDirection.Output;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                var valor = cmd.Parameters["@resp"].Value.ToString();

                var dt = new DataTable();
                dt.Columns.Add("IdEnc", typeof(int));
                dt.Columns.Add("ClaveDep", typeof(string));
                dt.Columns.Add("ClavePre", typeof(string));
                dt.Columns.Add("Respuesta", typeof(string));

                foreach (var entidad in valurObject.Partidas)
                {
                    dt.Rows.Add(new object[] {
                    Convert.ToInt16(valor),
                    entidad.ClaveDep,
                    entidad.ClavePre,
                    entidad.Respuesta
                    });
                }

                var miDataTable = dt;

                using (var transaction = con.BeginTransaction())
                {
                    using (var bulkCopy = new SqlBulkCopy(con, SqlBulkCopyOptions.Default, transaction))
                    {
                        bulkCopy.DestinationTableName = "Partida";
                        bulkCopy.ColumnMappings.Clear();
                        var columnMappings = GetColumnMappings(miDataTable);
                        foreach (var mapping in columnMappings) bulkCopy.ColumnMappings.Add(mapping);
                        bulkCopy.WriteToServer(miDataTable);
                    }
                    transaction.Commit();
                }
                valurObject.Status = true;
                valurObject.Message = "Encuesta Guardada";
            }
            catch (Exception e)
            {
                valurObject.Status = false;
                valurObject.Message = e.Message;
            }

            return valurObject;
        }

        private static List<SqlBulkCopyColumnMapping> GetColumnMappings(DataTable dt)
        {
            var columnsMapping = new List<SqlBulkCopyColumnMapping>();

            foreach (DataColumn column in dt.Columns)
                columnsMapping.Add(new SqlBulkCopyColumnMapping(column.ColumnName, column.ColumnName));

            return columnsMapping;
        }
    }
}
