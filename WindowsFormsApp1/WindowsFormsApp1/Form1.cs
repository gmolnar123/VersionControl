﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Entities;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

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

            List<decimal> Nyereségek = new List<decimal>();
            int intervalum = 30;
            DateTime kezdőDátum = (from x in ticks select x.TradingDay).Min();
            DateTime záróDátum = new DateTime(2016, 12, 30);
            TimeSpan z = záróDátum - kezdőDátum;
            for (int i = 0; i < z.Days - intervalum; i++)
            {
                decimal ny = GetPortfolioValue(kezdőDátum.AddDays(i + intervalum))
                           - GetPortfolioValue(kezdőDátum.AddDays(i));
                Nyereségek.Add(ny);
                Console.WriteLine(i + " " + ny);
            }

            var nyereségekRendezve = (from x in Nyereségek
                                      orderby x
                                      select x)
                                        .ToList();
            MessageBox.Show(nyereségekRendezve[nyereségekRendezve.Count() / 5].ToString());

            SaveFileDialog s = new SaveFileDialog();
            s.ShowDialog();
            using (StreamWriter sw = new StreamWriter(s.FileName))
            {
                sw.WriteLine("Időszak" + " " + "Nyereség");
                int i = 0;
                foreach (var item in Nyereségek)
                {
                    sw.WriteLine(i.ToString() + " " + item.ToString());
                    i++;
                }
            }
            
        }

        private void CreatePortfolio()
        {
            pi.Add(new PortfolioItem() { Index = "OTP", Vollume = 10 });
            pi.Add(new PortfolioItem() { Index = "ZWACK", Vollume = 10 });
            pi.Add(new PortfolioItem() { Index = "ELMU", Vollume = 10 });
            dataGridView2.DataSource = pi;
        }

        private decimal GetPortfolioValue(DateTime date)
        {
            decimal value = 0;
            foreach (var item in pi)
            {
                var last = (from x in ticks
                            where item.Index == x.Index.Trim()
                               && date <= x.TradingDay
                            select x)
                            .First();
                value += (decimal)last.Price * item.Vollume;
            }
            return value;
        }
    }
}
