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
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using WindowsFormsApp4.Entities;
using WindowsFormsApp4.MnbServiceReference;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        BindingList<RateData> rates = new BindingList<RateData>();
        BindingList<string> currencies = new BindingList<string>();

        string Webhivas()
        {
            MNBArfolyamServiceSoapClient mnService = new MNBArfolyamServiceSoapClient();
            GetExchangeRatesRequestBody request = new GetExchangeRatesRequestBody
            {
                currencyNames = comboBox1.SelectedItem.ToString(),
                startDate = dateTimePicker1.Value.ToString(),
                endDate = dateTimePicker2.Value.ToString(),
            };
            GetExchangeRatesResponseBody response = mnService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;
          //  StreamWriter sr = new StreamWriter("proba")
            {

            };
          //  var sorok = result.ToString().Split(new[] { '\r', '\n' });
          //  foreach (var item in sorok)
           //     sr.WriteLine(item);

            return result.ToString();
        }

        void XmlFeldolgoz(string s)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(s);

            foreach (XmlElement item in xml.DocumentElement)
            {
                if (item.ChildNodes[0] == null)
                {
                    continue;
                }
                RateData rd = new RateData();
                rates.Add(rd);
                rd.Date = Convert.ToDateTime(item.Attributes["date"].Value);

                rd.Currency = item.ChildNodes[0].Attributes["curr"].Value;
                rd.Value = decimal.Parse(item.ChildNodes[0].Attributes["unit"].Value);
                if (rd.Value != 0)
                {
                    rd.Value = Convert.ToDecimal(item.InnerText) / rd.Value;
                }

            }

        }

        void diagram()
        {
            chartRateData.DataSource = rates;
            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;
            var legends = chartRateData.Legends[0];
            legends.Enabled = false;
            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }
        public Form1()
        {
            InitializeComponent();
            // Webhivas();

            MNBArfolyamServiceSoapClient mnService = new MNBArfolyamServiceSoapClient();
            GetCurrenciesRequestBody request = new GetCurrenciesRequestBody
            {
                
                
            };
            GetCurrenciesResponseBody response = mnService.GetCurrencies(request);
            var result = response.GetCurrenciesResult;
            MessageBox.Show(result.ToString());
            XmlDocument xm = new XmlDocument();
            xm.LoadXml(result);
            int counter = 0;
            XmlElement item = xm.DocumentElement;
            while (item.ChildNodes[0].ChildNodes[counter] !=null)
            {
                //  string c = item.ChildNodes[counter].



                currencies.Add(item.ChildNodes[0].ChildNodes[counter].InnerText.ToString());
                    counter++;
                
                
            }


            comboBox1.DataSource = currencies;
            comboBox1.SelectedIndex = 0;
            RefreshData();

        }

        private void RefreshData()
        {
            rates.Clear();
          //  comboBox1.Text = "EUR";
            dataGridView1.DataSource = rates;
            XmlFeldolgoz(Webhivas());
            diagram();
            
        }

        private void chartRateData_Click(object sender, EventArgs e)
        {
            chartRateData.DataSource = rates;

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
