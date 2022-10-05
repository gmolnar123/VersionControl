using System.ComponentModel;
using UserMaintenance.Entities;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lblFirstName.Text = Resource1.FirstName;
            lblLastName.Text = Resource1.LastName;
            btnAdd.Text = Resource1.Add;

            listUser.DataSource = users;
            listUser.ValueMember = "ID";
            listUser.DisplayMember = "FullName";
        }

        BindingList<User> users = new BindingList<User>();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
                FirstName = textBox1.Text,
                LastName = textBox2.Text
            };

            users.Add(u);
        }
    }
}