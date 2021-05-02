using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Библиотечный_фонд.Model
{
    public class Booking
    {
        SqlConnection conn;
        public int id { get; private set; }
        public int userId { get; private set; }
        public int bookId { get; private set; }
        public DateTime takingDate { get; private set; }
        public DateTime returnDate { get; private set; }
        public bool status { get; private set; }
        
        public Booking()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            this.userId = -1;
            this.bookId = -1;
            this.takingDate = DateTime.MinValue;
            this.returnDate = DateTime.MinValue;
            this.status = false;
        }

        public static List<Booking> getUserBooking(int userId)
        {
            List<Booking> list = new List<Booking>();
            //TODO сделать выборку из бд с данными о транзакциях юзера
            return list;
        } 

        public Booking(int id)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Booking WHERE Id=@id", conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            SqlDataReader dataReader = null;
            conn.Open();
            try
            {
                dataReader = cmd.ExecuteReader();
                dataReader.Read();
                this.id = Convert.ToInt32(dataReader[0]);
                this.bookId = Convert.ToInt32(dataReader[1]);
                this.userId = Convert.ToInt32(dataReader[2]);
                this.takingDate = Convert.ToDateTime(dataReader[3]);
                this.returnDate = Convert.ToDateTime(dataReader[4]);
                this.status = Convert.ToBoolean(dataReader[5]);
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

    }
}
