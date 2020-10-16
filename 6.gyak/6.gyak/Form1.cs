using _6.gyak.Entities;
using _6.gyak.MNBServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace _6.gyak
{
    public partial class Form1 : Form
    {
        new BindingList<Ratedata> rates = new BindingList<Ratedata>();

        public Form1()
        {
            InitializeComponent();
            GetExchangerate();
            dataGridView1.DataSource = rates;
            xmlfeldolgozas();
            diagram();
        }
        public void GetExchangerate()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };
            var response = mnbService.GetExchangeRates(request);
            var result = response.GetExchangeRatesResult;
        }
        private void xmlfeldolgozas()
        {
            var xml = new XmlDocument();
            xml.LoadXml(result);
            foreach (XmlElement element in xml.DocumentElement)
            {
                var rate = new Ratedata();
                rates.Add(rate);

                rate.date = DateTime.Parse(element.GetAttribute("date"));

                var childElement = (XmlElement)element.ChildNodes[0];
                rate.currency = childElement.GetAttribute("curr");

                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.value = value / unit;
            }
        }
        private void diagram()
        {
            chartratedata.DataSource = rates;

            var series = chartratedata.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartratedata.Legends[0];
            legend.Enabled = false;

            var chartArea = chartratedata.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }
    }
}
