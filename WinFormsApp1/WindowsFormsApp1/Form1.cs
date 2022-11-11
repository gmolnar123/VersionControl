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
        List<Ramen> ramens = new List<Ramen>();
        List<Brand > brands = new List<Brand>();

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
                    string brand = sor[0];
                    Country or = AddCountry(orszag);
                    Brand br = AddBrand(brand);
                    Ramen r = new Ramen
                    {
                        ID = ramens.Count,
                        CountryFK = or.ID,
                        Country = or,
                        Stars = Convert.ToDouble(sor[3]),
                        Brand = br,
                        Name = sor[1]
                    };
                    ramens.Add(r);


                }
            }

            Country AddCountry(string orszag)
            {
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
                return ered;
            }

            Brand AddBrand(string brand)
            {
                var ered = (from c in brands where c.Name.Equals(brand) select c).FirstOrDefault();
                if (ered == null)
                {
                    ered = new Brand()
                    {
                        ID = brands.Count + 1,
                        Name = brand
                    };
                    brands.Add(ered);
                }
                return ered;
            }
        }
    }
}
