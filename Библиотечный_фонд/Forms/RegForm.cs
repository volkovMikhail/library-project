using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using Библиотечный_фонд.Model;

namespace Библиотечный_фонд
{
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();
        }

        TextBox[] props;
        private void RegForm_Load(object sender, EventArgs e)
        {
            props = new TextBox[4] {
                textBoxProperities1,
                textBoxProperities2,
                textBoxProperities3,
                textBoxProperities4
            };
            mailIsSand = false;
            nameIsOk = false;
            lastnameIsOk = false;
            passwordIsOk = false;
            emailIsOk = true;//TODO поменять на false
            phoneIsOk = false;
            propsIsOk = true;
        }

        bool nameIsOk;
        bool lastnameIsOk;
        bool passwordIsOk;
        bool emailIsOk;
        bool phoneIsOk;
        bool propsIsOk;

        int secretCode;
        bool mailIsSand;

        private void button1_Click(object sender, EventArgs e)
        {
            if (nameIsOk && lastnameIsOk && emailIsOk && passwordIsOk && propsIsOk && phoneIsOk && comboBox1.SelectedIndex != -1)
            {
                bool regIsOk = User.regUser(textBoxFirstName.Text,
                             textBoxLastName.Text,
                             textBoxEmail.Text,
                             textBoxPassword.Text,
                             maskedTextBoxPhone.Text,
                             comboBox1.SelectedIndex,
                             textBoxProperities1.Text,
                             textBoxProperities2.Text,
                             textBoxProperities3.Text,
                             textBoxProperities4.Text);
                if (regIsOk)
                {
                    MessageBox.Show("регистрация прошла успешно","Информация",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка, вероятно данные телефона или email уже существуют", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Проверьте правильность введённых данных", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            foreach (var item in props)
            {
                if (item.Visible)
                {
                    if (item.Text == string.Empty)
                    {
                        propsIsOk = false;
                        labelErr.Text = "Неверные данные";
                        labelErr.Visible = true;
                    }
                }
            }
            if (propsIsOk)
            {
                labelErr.Visible = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxProperities1.Visible = false;
            textBoxProperities2.Visible = false;
            textBoxProperities3.Visible = false;
            textBoxProperities4.Visible = false;
            labelProperities1.Visible = false;
            labelProperities2.Visible = false;
            labelProperities3.Visible = false;
            labelProperities4.Visible = false;
            switch (comboBox1.SelectedIndex)
            {
                case 0://никто

                    break;
                case 1://библиотекарь
                    labelProperities1.Text = "Название\nбиблиотеки";
                    textBoxProperities1.Visible = true;
                    labelProperities1.Visible = true;
                    break;
                case 2://школьник
                    labelProperities1.Text = "Ваше учебное\nзаведение";
                    labelProperities3.Text = "Класс";
                    textBoxProperities1.Visible = true;
                    textBoxProperities3.Visible = true;
                    labelProperities1.Visible = true;
                    labelProperities3.Visible = true;
                    break;
                case 3://студент
                    labelProperities1.Text = "Ваше учебное\nзаведение";
                    labelProperities2.Text = "Факультет";
                    labelProperities3.Text = "Курс";
                    labelProperities4.Text = "Номер группы";
                    textBoxProperities1.Visible = true;
                    textBoxProperities2.Visible = true;
                    textBoxProperities3.Visible = true;
                    textBoxProperities4.Visible = true;
                    labelProperities1.Visible = true;
                    labelProperities2.Visible = true;
                    labelProperities3.Visible = true;
                    labelProperities4.Visible = true;
                    break;
                case 4://препод
                    textBoxProperities1.Visible = true;
                    textBoxProperities2.Visible = true;
                    labelProperities1.Visible = true;
                    labelProperities2.Visible = true;
                    labelProperities1.Text = "Учебное\nзаведение";
                    labelProperities2.Text = "Кафедра";
                    break;
                case 5://научный работник
                    textBoxProperities1.Visible = true;
                    textBoxProperities2.Visible = true;
                    labelProperities1.Visible = true;
                    labelProperities2.Visible = true;
                    labelProperities1.Text = "Название\nорганизации";
                    labelProperities2.Text = "Научная облость";
                    break;
                case 6://пенсионер
                    textBoxProperities1.Visible = true;
                    labelProperities1.Visible = true;
                    labelProperities1.Text = "Номер\nпенсионной карты";
                    break;
                default:
                    break;
            }
        }

        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {
            if (textBoxFirstName.Text.Length < 2 || textBoxFirstName.Text.Length > 50)
            {
                labelErr.Text = "Неверный формат имени";
                labelErr.Visible = true;
                nameIsOk = false;
            }
            else
            {
                labelErr.Visible = false;
                nameIsOk = true;
            }
        }

        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {
            if (textBoxLastName.Text.Length < 2 || textBoxLastName.Text.Length > 50)
            {
                labelErr.Text = "Неверный формат фамилии";
                labelErr.Visible = true;
                lastnameIsOk = false;
            }
            else
            {
                labelErr.Visible = false;
                lastnameIsOk = true;
            }
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPassword.Text.Length < 8 || textBoxPassword.Text.Length > 50)
            {
                labelErr.Text = "Неверный формат пароля";
                labelErr.Visible = true;
                passwordIsOk = false;
            }
            else
            {
                if (textBoxPassword.Text == textBoxPasswordSubmit.Text)
                {
                    passwordIsOk = true;
                    labelErr.Visible = false;
                }
                else
                {
                    labelErr.Text = "Вы не подтвердили пароль";
                    passwordIsOk = false;
                    labelErr.Visible = true;
                }
            }
        }

        private void textBoxPasswordSubmit_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPassword.Text.Length < 8 || textBoxPassword.Text.Length > 50)
            {
                labelErr.Text = "Неверный формат пароля";
                labelErr.Visible = true;
                passwordIsOk = false;
            }
            else
            {
                if (textBoxPassword.Text == textBoxPasswordSubmit.Text)
                {
                    passwordIsOk = true;
                    labelErr.Visible = false;
                }
                else
                {
                    labelErr.Text = "Вы не подтвердили пароль";
                    passwordIsOk = false;
                    labelErr.Visible = true;
                }
            }
        }

        private void maskedTextBoxPhone_TextChanged(object sender, EventArgs e)
        {
            char[] phone = new char[18];
            bool isFull = true;
            for (int i = 5; i < 18; i++)
            {
                try
                {
                    phone[i] = maskedTextBoxPhone.Text[i];
                }
                catch (Exception)
                {
                    phone[i] = ' ';
                }
                if (phone[i] == ' ')
                {
                    isFull = false;
                }
            }
            if (isFull)
            {
                phoneIsOk = true;
                labelErr.Visible = false;
            }
            else
            {
                phoneIsOk = false;
                labelErr.Text = "Неверный номер телефона";
                labelErr.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                MailAddress from = new MailAddress("volkov.electronics@gmail.com", "admin");
                MailAddress to = new MailAddress(textBoxEmail.Text);
                MailMessage mail = new MailMessage(from, to);
                mail.Subject = "Библиотечный фонд, подтверждение почты";
                Random r = new Random((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
                secretCode = r.Next(10000, 99999);
                mail.Body = $"<h1>{secretCode}</h1>";
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("volkov.electronics@gmail.com", "volkov.electronics12345678");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                textBoxCode.Visible = true;
                labelCode.Visible = true;
                mailIsSand = true;
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

        private void textBoxCode_TextChanged(object sender, EventArgs e)
        {
            if (secretCode.ToString() == textBoxCode.Text)
            {
                labelErr.Visible = false;
                emailIsOk = true;
            }
            else
            {
                labelErr.Text = "Неверный код";
                labelErr.Visible = true;
            }
        }
    }
}
