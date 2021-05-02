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
using Библиотечный_фонд.Utils;

namespace Библиотечный_фонд
{
    public partial class Main : Form
    {
        User user;
        public Main(User usr)
        {
            InitializeComponent();
            this.user = usr;
        }
        private void Main_Load(object sender, EventArgs e)
        {
            textBoxFirstName.Text = user.name;
            textBoxLastName.Text = user.lastName;
            maskedTextBoxPhone.Text = user.phoneNumber;
            labelID.Text = user.id.ToString();
            List<Book> books = Book.getAllBooks();
            List<int> years = new List<int>();
            List<string> authors = new List<string>();
            List<string> genres = new List<string>();
            List<string> category = new List<string>();
            listViewCatalog.Items.Clear();
            foreach (var book in books)
            {
                ListViewItem viewItem = new ListViewItem(new string[] {
                    book.name,
                    book.author,
                    book.genre,
                    book.year.ToString()
                });
                viewItem.Tag = book.id;
                listViewCatalog.Items.Add(viewItem);
                years.Add(book.year);
                authors.Add(book.author);
                genres.Add(book.genre);
                category.Add(functions.categoryToString(book.category));
            }

            foreach (var item in years.Distinct())
            {
                comboBoxYear.Items.Add(item);
            }

            foreach (var item in genres.Distinct())
            {
                comboBoxGenre.Items.Add(item);
            }

            foreach (var item in authors.Distinct())
            {
                comboBoxAuthor.Items.Add(item);
            }
            comboBoxCategory.SelectedIndex = 0;
            comboBoxGenre.SelectedIndex = 0;
            comboBoxYear.SelectedIndex = 0;
            comboBoxAuthor.SelectedIndex = 0;
            radioButtonPopularity.Checked = true;
            GC.Collect();
        }

        private void updateCatalog()
        {
            listViewCatalog.Items.Clear();
            SearchBookData searchData;
            if (comboBoxAuthor.SelectedIndex - 1 >= 0)
            {
                searchData.author = comboBoxAuthor.SelectedItem.ToString();
            }
            else
            {
                searchData.author = string.Empty;
            }
            if (comboBoxGenre.SelectedIndex-1 >= 0)
            {
                searchData.genre = comboBoxGenre.SelectedItem.ToString();
            }
            else
            {
                searchData.genre = string.Empty;
            }
            searchData.name = textBoxBookNameSearch.Text;
            if (comboBoxCategory.SelectedIndex - 1 >= 0)
            {
                searchData.category = Convert.ToInt16(comboBoxCategory.SelectedIndex - 1);
            }
            else
            {
                searchData.category = -1;
            }
            
            if (comboBoxYear.SelectedIndex-1 >= 0)
            {
                searchData.year = Convert.ToInt16(comboBoxYear.SelectedItem);
            }
            else
            {
                searchData.year = -1;
            }
            searchData.popularity = radioButtonPopularity.Checked;
            searchData.newBooks = radioButtonNew.Checked;
            List<Book> list = Book.getFilteredBooks(searchData);
            foreach (var book in list)
            {
                ListViewItem viewItem = new ListViewItem(new string[] {
                    book.name,
                    book.author,
                    book.genre,
                    book.year.ToString()
                });
                viewItem.Tag = book.id;
                listViewCatalog.Items.Add(viewItem);
            }
            GC.Collect();
        }

        private void buttonChangePW_Click(object sender, EventArgs e)
        {

        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            //about box
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (buttonEdit.Text == "Редактировать")
            {
                buttonEdit.Text = "Сохранить";
                textBoxFirstName.Enabled = true;
                textBoxLastName.Enabled = true;
                maskedTextBoxPhone.Enabled = true;
            }
            else
            {
                buttonEdit.Text = "Редактировать";
                textBoxFirstName.Enabled = false;
                textBoxLastName.Enabled = false;
                maskedTextBoxPhone.Enabled = false;
                user.Update(textBoxFirstName.Text, textBoxLastName.Text, maskedTextBoxPhone.Text);
            }
        }

        private void listViewCatalog_ItemActivate(object sender, EventArgs e)
        {
            Book book = new Book(Convert.ToInt32(listViewCatalog.SelectedItems[0].Tag));
            BookForm bookForm = new BookForm(book, user);
            bookForm.ShowDialog();
        }

        private void buttonAdmin_Click(object sender, EventArgs e)
        {
            //TODO убрать это на кнопке редактирваоть личные данные -клиент
            Admin adminForm = new Admin();
            this.Hide();
            adminForm.ShowDialog();
            this.Show();
        }

        private void comboBoxGenre_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateCatalog();
        }

        private void radioButtonPopularity_CheckedChanged(object sender, EventArgs e)
        {
            updateCatalog();
        }

        private void radioButtonNew_CheckedChanged(object sender, EventArgs e)
        {
            updateCatalog();
        }

        private void textBoxBookNameSearch_TextChanged(object sender, EventArgs e)
        {
            updateCatalog();
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateCatalog();
        }

        private void comboBoxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateCatalog();
        }
        private void comboBoxAuthor_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateCatalog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = "Библиотечный фонд города " + DateTime.Now.ToLongTimeString();
        }
    }
}
