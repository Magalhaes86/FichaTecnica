using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using csharp_Sqlite.Models;
using System.Data;
using System.Data.SQLite;

namespace FichaTecnica
{
    public class DalHelper
    {
        private static SQLiteConnection sqliteConnection;
        public DalHelper()
        { }
        private static SQLiteConnection DbConnection()
        {
            sqliteConnection = new SQLiteConnection("Data Source=C:\\FichaTecnicaSoft\\BaseDados\\FichaTecnica.sqlite; Version=3;");
            sqliteConnection.Open();
            return sqliteConnection;
        }
        public static void CriarBancoSQLite()
        {
            try
            {
                SQLiteConnection.CreateFile(@"C:\FichaTecnicaSoft\BaseDados\FichaTecnica.sqlite");
            }
            catch
            {
                throw;
            }
        }
        public static void CriarTabelaSQlite()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Clientes(id INTEGER NOT NULL UNIQUE,CodSage Varchar(50), Nome Varchar(100), email VarChar(80),Tlm VarChar(80))";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetClientes()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Clientes";
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GetCliente(int id)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Clientes Where Id=" + id;
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void Add(Cliente cliente)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    //   cmd.CommandText = "INSERT INTO Clientes(id,CodSage, Nome, email ) values (@id,@CodSage, @nome, @email)";
                    cmd.CommandText = "INSERT INTO Clientes(id,CodSage, Nome, email,Tlm ) values (@id,@CodSage, @nome, @email,@Tlm)";
                    cmd.Parameters.AddWithValue("@Id", cliente.Id);
                    cmd.Parameters.AddWithValue("@CodSage", cliente.CodSage);
                    cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                    cmd.Parameters.AddWithValue("@Email", cliente.Email);
                    cmd.Parameters.AddWithValue("@Tlm", cliente.Tlm);
       
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void Add2 (AddFichaTecnica AddFichaTecnica)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    //   cmd.CommandText = "INSERT INTO Clientes(id,CodSage, Nome, email ) values (@id,@CodSage, @nome, @email)";
                    cmd.CommandText = "INSERT INTO FichaTecnica(CodCliente,CodClienteSage, NomeCliente,TlmCliente) values (@CodCliente,@CodClienteSage, @NomeCliente,@TlmCliente)";
                    cmd.Parameters.AddWithValue("@CodCliente", AddFichaTecnica.textcodcliente);
                    cmd.Parameters.AddWithValue("@CodClienteSage", AddFichaTecnica.textcodsage);
                    cmd.Parameters.AddWithValue("@NomeCliente", AddFichaTecnica.textNomeCliente);
                    cmd.Parameters.AddWithValue("@TlmCliente", AddFichaTecnica.tbtlm);


                


                   // cmd.Parameters.AddWithValue("@TlmCliente", AddFichaTecnica.);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        


        public static void Update(Cliente cliente)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    if (cliente.Id != null)
                    {
                        cmd.CommandText = "UPDATE Clientes SET CodSage=@CodSage, Nome=@Nome, Email=@Email , Tlm=@Tlm WHERE Id=@Id";
                        cmd.Parameters.AddWithValue("@Id", cliente.Id);
                        cmd.Parameters.AddWithValue("@CodSage", cliente.CodSage);
                        cmd.Parameters.AddWithValue("@Nome", cliente.Nome);
                        cmd.Parameters.AddWithValue("@Email", cliente.Email);
                        cmd.Parameters.AddWithValue("@Tlm", cliente.Tlm);
                        cmd.ExecuteNonQuery();
                    }
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void Delete(int Id)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    cmd.CommandText = "DELETE FROM Clientes Where Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
