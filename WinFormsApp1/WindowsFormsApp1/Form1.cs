using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        List<Country> countries = new List<Country>();
        public Form1()
        {
            InitializeComponent();
            LoadData("Ramen.csv");
        }

        public void LoadData(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName)) 
            { 
              sr.ReadLine();
              while (!sr.EndOfStream)
            {
                string[] sor = sr.ReadLine().Split(';');
                string orszag = sor[2];
                var ered = (from c in countries where c.Name.Equals(orszag) select c).FirstOrDefault();
                if (ered == null)
                    {
                        ered = new Country()
                        {
                            ID = countries.Count + 1,
                            Name = orszag
                        };
                        countries.Add(ered);
                    }

            }
        }
        }
    }
}
