using BasicAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BasicAPI.Data
{
    public class dbContext
    {
        string CS = ConfigurationManager.ConnectionStrings["connectionAPI"].ConnectionString;


        public DataTable getBooks()
        {
            SqlConnection connection = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("select * from Book", connection) { CommandType = CommandType.Text };

            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            return dt;
        }
        public DataTable getBookById(int id)
        {
            SqlConnection connection = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("select id,Name,Author from Book where id=@id", connection) { CommandType = CommandType.Text };
            cmd.Parameters.AddWithValue("id", id);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            return dt;
        }
        public int postBook(Book book)
        {
            try
            {
                SqlConnection connection = new SqlConnection(CS);
                SqlCommand cmd = new SqlCommand("Insert into Book (Name,Author) values (@name,@author)", connection) { CommandType = CommandType.Text };
                cmd.Parameters.AddWithValue("name", book.Name);
                cmd.Parameters.AddWithValue("author", book.Author);
                connection.Open();
                int row = cmd.ExecuteNonQuery();
                return row;
            }
            catch (Exception ex)
            {
                return 0;
               // throw ex;
            }
           
        }
        //
        public int updateBook(Book book)
        {
            try
            {
                SqlConnection connection = new SqlConnection(CS);
                SqlCommand cmd = new SqlCommand("update Book set Name=@name,Author=@author where id=@Id", connection) { CommandType = CommandType.Text };
                cmd.Parameters.AddWithValue("Id", book.Id);
                cmd.Parameters.AddWithValue("name", book.Name);
                cmd.Parameters.AddWithValue("author", book.Author);
                connection.Open();
                int row = cmd.ExecuteNonQuery();
                return row;
            }
            catch (Exception ex)
            {
                return 0;
                // throw ex;
            }
        }
        public int deleteBook(int Id)
        {
            try
            {
                SqlConnection connection = new SqlConnection(CS);
                SqlCommand cmd = new SqlCommand("delete from Book where id=@Id", connection) { CommandType = CommandType.Text };
                cmd.Parameters.AddWithValue("Id", Id);
                connection.Open();
                int row = cmd.ExecuteNonQuery();
                return row;
            }
            catch (Exception ex)
            {
                return 0;
                // throw ex;
            }
        }
        //
    }
}