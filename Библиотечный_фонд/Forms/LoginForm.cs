using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Библиотечный_фонд.Forms;
using Библиотечный_фонд.Model;

namespace Библиотечный_фонд
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            //место для проверки в консоле
            //TODO поменять на вход по почте
        }
        
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            User user = new User(textBoxEmail.Text,textBoxPassword.Text);
            if (user.id != -1)
            {
                user.setLastOnlineNow();
                Main main = new Main(user);
                this.Hide();
                main.ShowDialog();
                this.Show();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegForm regForm = new RegForm();
            Visible = false;
            regForm.ShowDialog();
            Visible = true;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgotPassWord forgotPass = new ForgotPassWord();
            forgotPass.ShowDialog();
        }
    }
}
