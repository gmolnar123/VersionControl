using System.ComponentModel;
using UserMaintenance.Entities;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
          //  lblFirstName.Text = Resource1.FirstName;
            lblLastName.Text = Resource1.FullName;
            btnAdd.Text = Resource1.Add;
            button1.Text = Resource1.WriteToFile;

            listUser.DataSource = users;
            listUser.ValueMember = "ID";
            listUser.DisplayMember = "FullName";
        }

        BindingList<User> users = new BindingList<User>();

        private void btnAdd_Click(object sender, EventArgs e)
        {
           // var u = new User()
           // {
                //FirstName = textBox1.Text,
          //      FullName = textBox2.Text
         //   };
           User u = new User();
           u.FullName = textBox2.Text;

            users.Add(u);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
            {
                foreach (var item in users)
                {
                    sw.WriteLine(item.FullName+' '+item.ID);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listUser.SelectedIndex != -1)
            {
                users.Remove(users[listUser.SelectedIndex]);
            }
        }
    }
}