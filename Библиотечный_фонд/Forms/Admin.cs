﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Библиотечный_фонд.Forms
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Admin_Load(object sender, EventArgs e)
        {
            AdminBookInfo bookInfo = new AdminBookInfo();
            bookInfo.ShowDialog();
            AdminUserInfo userInfo = new AdminUserInfo();
            userInfo.ShowDialog();
        }
    }
}
