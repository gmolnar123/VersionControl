using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp4
{
    

    public partial class Form1 : Form
    {
        private List<Country> orszagok = new List<Country>();
        List<Ramen> ramens = new List<Ramen>();
        List<Brand> brands = new List<Brand>();


        public Form1()
        {
            InitializeComponent();
            LoadData("ramen.csv");
            GetCountries();
            
        }

        void LoadData(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string[] sor = sr.ReadLine().Split(';');
                    string orszag = sor[2];
                    string brand = sor[0];
                    Country aktorszag = AddCountry(orszag);
                    Brand aktbrand = AddBrand(brand);
                    Ramen r = new Ramen
                    {
                        ID = ramens.Count,
                        Name = sor[1],
                        Brand = aktbrand,
                        CounrtyFK = aktorszag.ID,
                        Country = aktorszag,
                        Rating = Convert.ToDouble(sor[3])

                    } ;
                    ramens.Add(r);
                    Brand b = new Brand
                    {
                        ID = brands.Count,
                        Name = aktbrand.Name
                    };
                }
            }

            


        }

        void GetCountries()
        {
            //  var ered = (from c in orszagok where c.Name.Contains(textBox1.Text) orderby c.Name select c).ToList();
            var ered = orszagok.Where(i => i.Name.Contains(textBox1.Text)).OrderBy(i => i.Name).ToList();
            listBox1.DataSource = ered;
            listBox1.DisplayMember = "Name";
        }

        Country AddCountry(string orszag)
        {
            //  var ered = (from o in orszagok where o.Name == orszag select o).FirstOrDefault();
            var ered = orszagok.Where(i => i.Name.Equals(orszag)).FirstOrDefault();
            if (ered == null)
            {
                ered = new Country
                {
                    Name = orszag,
                    ID = orszagok.Count + 1
                };
                orszagok.Add(ered);
            }
            return ered;
        }

        Brand AddBrand(string brand)
        {
            var ered = (from o in brands where o.Name == brand select o).FirstOrDefault();
            if (ered == null)
            {
                ered = new Brand
                {
                    Name = brand,
                    ID = brands.Count + 1
                };
                brands.Add(ered);
            }
            return ered;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            GetCountries();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Country akt = (Country)listBox1.SelectedItem;
            var ered = (from r in ramens where r.CounrtyFK == akt.ID select r);
            var ered2 = (from r in ered group r.Rating by r.Brand.Name into g select new
            {
                b = g.Key,
                rate = g.Average()
            });
            var ered3 = from g in ered2 orderby g.rate descending select g;

            dataGridView1.DataSource = ered3.ToList();

        }
    }
}
