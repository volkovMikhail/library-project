using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Библиотечный_фонд.Model;
using Библиотечный_фонд.Utils;

namespace Библиотечный_фонд.Forms
{
    public partial class BookForm : Form
    {
        Book book;
        User user;
        public BookForm(Book book,User user)
        {
            InitializeComponent();
            this.book = book;
            this.user = user;
        }

        private void BookForm_Load(object sender, EventArgs e)
        {
            this.Text = book.name;
            labelBookName.Text = book.name;
            labelCategory.Text = functions.categoryToString(book.category);
            labelDiscription.Text = book.discription;
            labelGenre.Text = book.genre;
            labelQuantity.Text = book.quantity.ToString();
            if (book.permission)
            {
                labelPermission.Text = "Разрешено";
            }
            else
            {
                labelPermission.Text = "Запрещено";
            }
            pictureBox.Image = functions.stratchImage(Image.FromFile(@"resources/images/" + book.image));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (book.permission)
            {
                //TODO some logic to db bookinglog
            }
            else
            {
                MessageBox.Show("Данную книгу можно читать только в зале.\nОбратитесь к библиотекарю","Информация",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
    }
}
