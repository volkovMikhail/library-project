using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Библиотечный_фонд.Forms
{
    public partial class ForgotPassWord : Form// TODO: Поменять логику формы восстоновления пороля, т.к. входная инфа только E-Mail
    {
        SqlConnection conn;
        int secretCode;
        public ForgotPassWord()
        {
            InitializeComponent();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            try
            {
                MailAddress from = new MailAddress("volkov.electronics@gmail.com", "admin");
                MailAddress to = new MailAddress(textBoxEmail.Text);
                MailMessage mail = new MailMessage(from, to);
                mail.Subject = "Восстоновление пароля volkov.electronics";
                Random r = new Random((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
                secretCode = r.Next(10000, 99999);
                mail.Body = $"<h1>{secretCode}</h1>";
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("volkov.electronics@gmail.com", "volkov.electronics12345678");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                textBoxConfirm.Visible = true;
                labelConfirm.Visible = true;
                buttonConfirm.Visible = true;
            }
            catch (Exception)
            {
                if (textBoxEmail.Text == string.Empty)
                {
                    MessageBox.Show("Укажите ваш email, и повторите попытку");
                }
                else
                {
                    MessageBox.Show("Ошибка, проверьте соединение с интернетом,\nИли проверьте корректность введённый почты");
                }
            }
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (textBoxConfirm.Text == secretCode.ToString())
            {
                labelCheckNewPassword.Visible = true;
                labelNewPassword.Visible = true;
                textBoxNewPassword.Visible = true;
                textBoxCheckNewPassword.Visible = true;
            }
            else
            {
                MessageBox.Show("Код не совапал.\nПопробоуйте отправить письмо заного");
            }
        }

        private void textBoxNewPassword_TextChanged(object sender, EventArgs e)
        {
            if (textBoxNewPassword.Text.Length < 8)
            {
                labelErr.Text = "Пароль слишком маленький";
                labelErr.Visible = true;
            }
            else if (textBoxNewPassword.Text.Length > 255)
            {
                labelErr.Text = "Пароль слишком большой";
                labelErr.Visible = true;
            }
            else if (textBoxNewPassword.Text != textBoxCheckNewPassword.Text)
            {
                labelErr.Text = "Подтвердите пароль";
                labelErr.Visible = true;
            }
            else
            {
                labelErr.Text = "";
                labelErr.Visible = false;
                buttonApply.Visible = true;
            }
        }

        private void textBoxCheckNewPassword_TextChanged(object sender, EventArgs e)
        {
            if (textBoxNewPassword.Text.Length < 8)
            {
                labelErr.Text = "Пароль слишком маленький";
                labelErr.Visible = true;
            }
            else if (textBoxNewPassword.Text.Length > 255)
            {
                labelErr.Text = "Пароль слишком большой";
                labelErr.Visible = true;
            }
            else if (textBoxNewPassword.Text != textBoxCheckNewPassword.Text)
            {
                labelErr.Text = "Подтвердите пароль";
                labelErr.Visible = true;
            }
            else
            {
                labelErr.Text = "";
                labelErr.Visible = false;
                buttonApply.Visible = true;
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Users SET password = @pw WHERE phoneNumber = @phone", conn);
            cmd.Parameters.Add("@pw", SqlDbType.NVarChar).Value = textBoxNewPassword.Text;
            try
            {
                if (cmd.ExecuteNonQuery() == 0)
                {
                    throw new Exception("Ошибка" + "\nВероятно неправильно указан номер");
                }
                DialogResult = DialogResult.OK;
                MessageBox.Show("Пароль успешно изменён!","Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
