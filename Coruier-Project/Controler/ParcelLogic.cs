using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coruier_Project.Controler
{
    public class ParcelLogic
    {
        static string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=ParcelContext;Integrated Security=true";

        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public void AddParcel(string name, string description, decimal price, decimal weight, int typeId)
        {
            SqlConnection connect = GetConnection();
            if (connect.State == 0) connect.Open();
            string sql =
                "INSERT INTO Parcels (Name, Description, Price, Wieght, TypeId) " +
                "VALUES (@name, @description, @price, @weight, @typeId);";
            SqlCommand cmd = new SqlCommand(sql, connect);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@weight", weight);
            cmd.Parameters.AddWithValue("@typeId", typeId);
            cmd.ExecuteNonQuery();
            connect.Close();
        }

        public DataTable GetAllParcels()
        {
            SqlConnection connect = GetConnection();
            if (connect.State == 0) connect.Open();
            string sql =
                "SELECT p.Id, p.Name, p.Description, p.Price, p.Wieght, t.TypeName " +
                "FROM Parcels p " +
                "INNER JOIN ParcelTypes t ON p.TypeId = t.Id;";
            SqlCommand cmd = new SqlCommand(sql, connect);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connect.Close();
            return table;
        }

        public DataTable GetAllTypes()
        {
            SqlConnection connect = GetConnection();
            if (connect.State == 0) connect.Open();
            string sql = "SELECT Id, TypeName FROM ParcelTypes;";
            SqlCommand cmd = new SqlCommand(sql, connect);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connect.Close();
            return table;
        }

        public DataTable FindParcel(int id)
        {
            SqlConnection connect = GetConnection();
            if (connect.State == 0) connect.Open();
            string sql =
                "SELECT p.Id, p.Name, p.Description, p.Price, p.Wieght, t.TypeName " +
                "FROM Parcels p " +
                "INNER JOIN ParcelTypes t ON p.TypeId = t.Id " +
                "WHERE p.Id = @id;";
            SqlCommand cmd = new SqlCommand(sql, connect);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            connect.Close();
            return table;
        }

        public void UpdateParcel(int id, string name, string description, decimal price, decimal weight, int typeId)
        {
            SqlConnection connect = GetConnection();
            if (connect.State == 0) connect.Open();
            string sql =
                "UPDATE Parcels " +
                "SET Name = @name, Description = @description, Price = @price, Wieght = @weight, TypeId = @typeId " +
                "WHERE Id = @id;";
            SqlCommand cmd = new SqlCommand(sql, connect);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@weight", weight);
            cmd.Parameters.AddWithValue("@typeId", typeId);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            connect.Close();
        }

        public void DeleteParcel(int id)
        {
            SqlConnection connect = GetConnection();
            if (connect.State == 0) connect.Open();
            string sql = "DELETE FROM Parcels WHERE Id = @id;";
            SqlCommand cmd = new SqlCommand(sql, connect);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            connect.Close();
        }
    }
}
