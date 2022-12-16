using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Entities;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        List<Tick> ticks;
        PortfolioEntities pe = new PortfolioEntities();
        List<PortfolioItem> pi = new List<PortfolioItem>();


        public Form1()
        {
            InitializeComponent();
            ticks = pe.Tick.ToList();
            dataGridView1.DataSource = ticks;
            CreatePortfolio();
        }

        private void CreatePortfolio()
        {
            pi.Add(new PortfolioItem() { Index = "OTP", Vollume = 10 });
            pi.Add(new PortfolioItem() { Index = "ZWACK", Vollume = 10 });
            pi.Add(new PortfolioItem() { Index = "ELMU", Vollume = 10 });
            dataGridView2.DataSource = pi;
        }
    }
}
