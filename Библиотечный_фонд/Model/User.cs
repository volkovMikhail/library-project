using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace Библиотечный_фонд.Model
{
    public class User
    {
        SqlConnection conn;
        public int id { get; private set; }
        public string name { get; private set; }
        public string lastName { get; private set; }
        public string Email { get; private set; }
        public string phoneNumber { get; private set; }
        public short category { get; private set; }
        public DateTime lastOnline { get; private set; }
        public User()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            this.id = -1;
            this.name = string.Empty;
            this.lastName = string.Empty;
            this.Email = string.Empty;
            this.phoneNumber = string.Empty;
            this.category = -1;
            this.lastOnline = DateTime.MinValue;
        }

        public User(int ID)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT name,lastName,Email,phoneNumber,category,lastOnline FROM Users WHERE Id=@id",conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = ID;
            SqlDataReader dataReader = null;
            conn.Open();
            try
            {
                dataReader = cmd.ExecuteReader();
                dataReader.Read();
                this.id = ID;
                this.name = Convert.ToString(dataReader[0]);
                this.lastName = Convert.ToString(dataReader[1]);
                this.Email = Convert.ToString(dataReader[2]);
                this.phoneNumber = Convert.ToString(dataReader[3]);
                this.category = Convert.ToInt16(dataReader[4]);
                this.lastOnline = Convert.ToDateTime(dataReader[5]);
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

        public User(string email)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT Id,name,lastName,Email,phoneNumber,category,lastOnline FROM Users WHERE Email=@email", conn);
            cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
            SqlDataReader dataReader = null;
            conn.Open();
            try
            {
                dataReader = cmd.ExecuteReader();
                dataReader.Read();
                this.id = Convert.ToInt32(dataReader[0]);
                this.name = Convert.ToString(dataReader[1]);
                this.lastName = Convert.ToString(dataReader[2]);
                this.Email = Convert.ToString(dataReader[3]);
                this.phoneNumber = Convert.ToString(dataReader[4]);
                this.category = Convert.ToInt16(dataReader[5]);
                this.lastOnline = Convert.ToDateTime(dataReader[6]);
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

        public User(string email,string pw)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Email=@email", conn);
            cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
            SqlDataReader dataReader = null;
            conn.Open();
            try
            {
                if (email == string.Empty || pw == string.Empty)
                {
                    throw new Exception("а где? данные...");
                }

                dataReader = cmd.ExecuteReader();
                if (!dataReader.Read())
                {
                    throw new Exception("Пользователь не найден");
                }
                this.id = Convert.ToInt32(dataReader[0]);
                this.name = Convert.ToString(dataReader[1]);
                this.lastName = Convert.ToString(dataReader[2]);
                this.Email = Convert.ToString(dataReader[3]);
                this.phoneNumber = Convert.ToString(dataReader[4]);
                this.category = Convert.ToInt16(dataReader[6]);
                this.lastOnline = Convert.ToDateTime(dataReader[7]);

                string password = Convert.ToString(dataReader[5]);
                if (password != pw)
                {
                    throw new Exception("Неверный пароль");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message,"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
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

        public void setLastOnlineNow()
        {
            DateTime timeNow = DateTime.Now;
            SqlCommand cmd = new SqlCommand("UPDATE Users SET lastOnline=@now WHERE Id=@id", conn);
            cmd.Parameters.Add("@now", SqlDbType.DateTime).Value = timeNow;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = this.id;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                this.lastOnline = timeNow;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void UpdatePassword(string newpassword)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Users SET passWord=@pw WHERE Id=@id", conn);
            cmd.Parameters.Add("@pw", SqlDbType.NVarChar).Value = newpassword;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = this.id;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void Update(string name,string lastname,string phoneNumber)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Users SET name=@name,lastName=@lastname,phoneNumber=@phone WHERE Id=@id", conn);
            cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
            cmd.Parameters.Add("@lastname", SqlDbType.NVarChar).Value = lastname;
            cmd.Parameters.Add("@phone", SqlDbType.NVarChar).Value = phoneNumber;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = this.id;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                this.name = name;
                this.lastName = lastName;
                this.phoneNumber = phoneNumber;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<User> getAllUsers()
        {
            List<User> list = new List<User>();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT Id,name,lastName,Email,phoneNumber,category,lastOnline FROM Users", conn);
            SqlDataReader dataReader = null;
            try
            {
                conn.Open();
                User user = new User();
                dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    user.id =Convert.ToInt32(dataReader[0]);
                    user.name = Convert.ToString(dataReader[1]);
                    user.lastName = Convert.ToString(dataReader[2]);
                    user.Email = Convert.ToString(dataReader[3]);
                    user.phoneNumber = Convert.ToString(dataReader[4]);
                    user.category = Convert.ToInt16(dataReader[5]);
                    user.lastOnline = Convert.ToDateTime(dataReader[6]);
                    list.Add(user);
                    user = new User();
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

        public static void DeleteUser(int id)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE Id=@id", conn);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            
        }

        public static bool regUser(string name, string lastname,string email,string password, string phone, int category, string props1, string props2, string props3, string props4)
        {
            User user = new User();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO Users VALUES(@name,@lastname,@email,@phone,@password,@category,@lastonline)", conn);
            cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
            cmd.Parameters.Add("@lastname", SqlDbType.NVarChar).Value = lastname;
            cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
            cmd.Parameters.Add("@phone", SqlDbType.NVarChar).Value = phone;
            cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;
            cmd.Parameters.Add("@category", SqlDbType.Int).Value = category;
            cmd.Parameters.Add("@lastonline", SqlDbType.DateTime).Value = DateTime.Now;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                user = new User(email);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            bool execute = false;
            switch (category) //TODO занесение данных в разные таблицы
            {
                case 0://просто
                    execute = false;
                    break;
                case 1://библиотекарь (1)
                    command.CommandText = "INSERT INTO librarian VALUES(@id,@library)";
                    command.Parameters.Add("@id", SqlDbType.Int).Value = user.id;
                    command.Parameters.Add("@library", SqlDbType.NVarChar).Value = props1;
                    execute = true;
                    break;
                case 2://школьник (1,3)
                    command.CommandText = "INSERT INTO schoolchild VALUES(@id,@eduPlace,@grade)";
                    command.Parameters.Add("@id", SqlDbType.Int).Value = user.id;
                    command.Parameters.Add("@eduPlace", SqlDbType.NVarChar).Value = props1;
                    command.Parameters.Add("@grade", SqlDbType.NVarChar).Value = props3;
                    execute = true;
                    break;
                case 3://студент (все)
                    command.CommandText = "INSERT INTO student VALUES(@id,@eduPlace,@fac,@course,@group)";
                    command.Parameters.Add("@id", SqlDbType.Int).Value = user.id;
                    command.Parameters.Add("@eduPlace", SqlDbType.NVarChar).Value = props1;
                    command.Parameters.Add("@fac", SqlDbType.NVarChar).Value = props2;
                    command.Parameters.Add("@course", SqlDbType.NVarChar).Value = props3;
                    command.Parameters.Add("@group", SqlDbType.NVarChar).Value = props4;
                    execute = true;
                    break;
                case 4://преподователь (1,2)
                    command.CommandText = "INSERT INTO teacher VALUES(@id,@eduPlace,@chair)";
                    command.Parameters.Add("@id", SqlDbType.Int).Value = user.id;
                    command.Parameters.Add("@eduPlace", SqlDbType.NVarChar).Value = props1;
                    command.Parameters.Add("@chair", SqlDbType.NVarChar).Value = props2;
                    execute = true;
                    break;
                case 5://научный работник (1,2)
                    command.CommandText = "INSERT INTO scientist VALUES(@id,@org,@shpere)";
                    command.Parameters.Add("@id", SqlDbType.Int).Value = user.id;
                    command.Parameters.Add("@org", SqlDbType.NVarChar).Value = props1;
                    command.Parameters.Add("@shpere", SqlDbType.NVarChar).Value = props2;
                    execute = true;
                    break;
                case 6://пенсионер (1)
                    command.CommandText = "INSERT INTO retiree VALUES(@id,@card)";
                    command.Parameters.Add("@id", SqlDbType.Int).Value = user.id;
                    command.Parameters.Add("@card", SqlDbType.NVarChar).Value = props1;
                    execute = true;
                    break;
                default:
                    System.Windows.Forms.MessageBox.Show("category error");
                    execute = false;
                    break;
            }

            if (execute)
            {
                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return true;
        }

        public override string ToString()
        {
            return $"id : {id},\nname : {name},\nlastName : {lastName},\nEmail : {Email},\nphoneNumber : {phoneNumber},\ncategory : {category},\nlastOnline : {lastOnline.ToString()}";
        }

    }
}
