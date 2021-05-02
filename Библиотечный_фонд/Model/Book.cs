using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Библиотечный_фонд.Utils;

namespace Библиотечный_фонд.Model
{
    public class Book
    {
        SqlConnection conn;
        public int id { get; private set; }
        public string name { get; private set; }
        public string author { get; private set; }
        public string discription { get; private set; }
        public string genre { get; private set; }
        public int year { get; private set; }
        public int category { get; private set; }
        public int popularity { get; private set; }
        public int quantity { get; private set; }
        public string image { get; private set; }
        public bool permission { get; private set; }

        public Book()
        {
            this.id = -1;
            this.name = string.Empty;
            this.author = string.Empty;
            this.discription = string.Empty;
            this.genre = string.Empty;
            this.year = -1;
            this.category = -1;
            this.popularity = 0;
            this.quantity = 0;
            this.image = string.Empty;
            this.permission = false;
        }

        public Book(int id)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Books WHERE Id=@id", conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            SqlDataReader dataReader = null;
            conn.Open();
            try
            {
                dataReader = cmd.ExecuteReader();
                dataReader.Read();
                this.id = Convert.ToInt32(dataReader[0]);
                this.name = Convert.ToString(dataReader[1]);
                this.author = Convert.ToString(dataReader[2]);
                this.discription = Convert.ToString(dataReader[3]);
                this.genre = Convert.ToString(dataReader[4]);
                this.year = Convert.ToInt16(dataReader[5]);
                this.category = Convert.ToInt16(dataReader[6]);
                this.popularity = Convert.ToInt32(dataReader[7]);
                this.quantity = Convert.ToInt32(dataReader[8]);
                this.image = Convert.ToString(dataReader[9]);
                this.permission = Convert.ToBoolean(dataReader[10]);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                this.id = -1;
            }
            finally
            {
                conn.Close();
                if (dataReader != null)
                {
                    dataReader.Close();
                }
            }
        }

        public static List<Book> getFilteredBooks(SearchBookData searchData)
        {
            List<Book> list = new List<Book>();
            string query = $"SELECT * FROM Books WHERE name LIKE N'%{searchData.name}%'";
            if (searchData.author != string.Empty)
            {
                query += $" AND author = N'{searchData.author}'";
            }
            if (searchData.genre != string.Empty)
            {
                query += $" AND genre = N'{searchData.genre}'";
            }
            if (searchData.year != -1)
            {
                query += $" AND year = {searchData.year}";
            }
            if (searchData.category != -1)
            {
                query += $" AND category = {searchData.category}";
            }
            if (searchData.popularity)
            {
                query += $" ORDER BY popularity DESC";
            }
            if (searchData.newBooks)
            {
                query += $" ORDER BY year DESC";
            }
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dataReader = null;
            conn.Open();
            try
            {
                Book book = new Book();
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    book.id = Convert.ToInt32(dataReader[0]);
                    book.name = Convert.ToString(dataReader[1]);
                    book.author = Convert.ToString(dataReader[2]);
                    book.discription = Convert.ToString(dataReader[3]);
                    book.genre = Convert.ToString(dataReader[4]);
                    book.year = Convert.ToInt16(dataReader[5]);
                    book.category = Convert.ToInt16(dataReader[6]);
                    book.popularity = Convert.ToInt32(dataReader[7]);
                    book.quantity = Convert.ToInt32(dataReader[8]);
                    book.image = Convert.ToString(dataReader[9]);
                    book.permission = Convert.ToBoolean(dataReader[10]);
                    list.Add(book);
                    book = new Book();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                list.Clear();
            }
            finally
            {
                conn.Close();
                if (dataReader != null)
                {
                    dataReader.Close();
                }
            }
            return list;
        }

        public static List<Book> getAllBooks()
        {
            List<Book> list = new List<Book>();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Books", conn);
            SqlDataReader dataReader = null;
            conn.Open();
            try
            {
                Book book = new Book();
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    book.id = Convert.ToInt32(dataReader[0]);
                    book.name = Convert.ToString(dataReader[1]);
                    book.author = Convert.ToString(dataReader[2]);
                    book.discription = Convert.ToString(dataReader[3]);
                    book.genre = Convert.ToString(dataReader[4]);
                    book.year = Convert.ToInt16(dataReader[5]);
                    book.category = Convert.ToInt16(dataReader[6]);
                    book.popularity = Convert.ToInt32(dataReader[7]);
                    book.quantity = Convert.ToInt32(dataReader[8]);
                    book.image = Convert.ToString(dataReader[9]);
                    book.permission = Convert.ToBoolean(dataReader[10]);
                    list.Add(book);
                    book = new Book();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                list.Clear();
            }
            finally
            {
                conn.Close();
                if (dataReader != null)
                {
                    dataReader.Close();
                }
            }
            return list;
        }

        public override string ToString()
        {
            return $"id : {this.id},\nname : {this.name},\nauthor : {this.author},\ngenre : {this.genre},\nyear : {this.year},\ncategory : {this.category},\npopularity : {this.popularity}";
        }
    }
}
