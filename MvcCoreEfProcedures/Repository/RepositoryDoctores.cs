using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCoreEfProcedures.Data;
using MvcCoreEfProcedures.Models;
using System.Data.Common;

#region
//CREATE PROCEDURE SP_TODOS_DOCTORES
//AS
//	SELECT * FROM DOCTOR
//GO    



//CREATE PROCEDURE SP_INCREMENTAR_DOCTORES
//(@ESPECIALIDAD NVARCHAR(50), @INCREMENTO INT)
//AS
//    UPDATE DOCTOR SET SALARIO = SALARIO + @INCREMENTO
//    WHERE ESPECIALIDAD=@ESPECIALIDAD
//GO

#endregion

namespace MvcCoreEfProcedures.Repository
{
    public class RepositoryDoctores
    {
        private HospitalContext context;
        public RepositoryDoctores(HospitalContext context)
        {
            this.context = context;
        }

        public List<Doctor> GetDoctores()
        {

            using (DbCommand com =
                this.context.Database.GetDbConnection().CreateCommand())
            {
                string sql = "SP_TODOS_DOCTORES";
               var consulta = this.context.Doctores.FromSqlRaw(sql);
                List<Doctor> doctores = consulta.AsEnumerable().ToList();
                return doctores;

            }

        }

        public List<string> GetEspecialidades()
        {
            using (DbCommand com =
               this.context.Database.GetDbConnection().CreateCommand())
            {
                string sql = "SP_ESPECIALIDADES";
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = sql;
                com.Connection.Open();
                DbDataReader reader = com.ExecuteReader();
                List<string> especialidades = new List<string>();
                while (reader.Read())
                {
                    string espe = reader["ESPECIALIDAD"].ToString();
                    especialidades.Add(espe);
                }
                reader.Close();
                com.Connection.Close();
                return especialidades;

            }

        }

        public List<Doctor> GetDoctoresEspecialidad(string especialidad)
        {
            string sql = "SP_DOCTORES_ESPECIALIDAD @ESPECIALIDAD";
            SqlParameter pamespe = new SqlParameter("@ESPECIALIDAD", especialidad);
            var consulta = this.context.Doctores.FromSqlRaw(sql, pamespe);
            List<Doctor> doctores = consulta.AsEnumerable().ToList();
            return doctores;

        }

        public async Task IncrementarSalarioDoctoresAsync
            (string especialidad, int incremento)
        {
            string sql = "SP_INCREMENTAR_SALARIO_DOCTORES_ESPECIALIDAD @ESPECIALIDAD, @INCREMENTO";
            SqlParameter pamespe = new SqlParameter("@ESPECIALIDAD", especialidad);
            SqlParameter pamincremento = new SqlParameter("@INCREMENTO", incremento);
            await this.context.Database.ExecuteSqlRawAsync(sql, pamespe, pamincremento);

        }



    }
}
