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
using System.Xml.Serialization;
using System.Linq;
using System.Globalization;

namespace Xml
{
    public partial class Form1 : Form
    {
        public static bool IsValidDate(string value)
        {
            string dateFormats= "dd.MM.yyyy";
            DateTime tempDate;
            bool validDate = DateTime.TryParseExact(value, dateFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out tempDate);
            if (validDate)
                return true;
            else
                return false;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            XmlSerializer deserializer = new XmlSerializer(typeof(Popis_Imovine));
            TextReader reader = new StreamReader("Imovina_20200108_2.xml");
            object obj = deserializer.Deserialize(reader);
            Popis_Imovine XmlData = (Popis_Imovine)obj;

            listView1.Items.Clear();
            foreach (var objekt in XmlData.objekti)
            {
                listView1.Items.Add(objekt.sifra.ToString());
            }
            var ocisceno = XmlData.objekti.Distinct().ToList();
            
            
            var v = XmlData.objekti.Where(x => !ocisceno.Contains(x));
            var dupli = new List<Imovina>();
            for (int i = 0; i < XmlData.objekti.Count(); i++)
            {
                for (int j = i + 1; j < XmlData.objekti.Count() - i; j++)
                {
                    if (XmlData.objekti[i].Equals(XmlData.objekti[j]))
                    {
                        dupli.Add(XmlData.objekti[j]);
                    }
                }
            }

            using (var n = new StreamWriter("IN\\Neispravna_Imovina.csv"))
            {

                foreach (var objekt in dupli)
                {

                    var prvi = objekt.sifra.ToString();
                    var drugi = objekt.naziv.ToString();
                    var treci = objekt.mjesto_troška.ToString();
                    var cetvrti = objekt.datum_aktivacije.ToString();
                    var peti = objekt.smjestaj.ToString();
                    var line = string.Format("{0};{1};{2};{3};{4}", prvi, drugi, treci, cetvrti, peti);
                    n.WriteLine(line);
                    n.Flush();
                }


                DateTime dDate;


                using (var w = new StreamWriter("OUT\\rezultat.csv"))
                {
                    foreach (var objekt in ocisceno)
                    {
                        if (IsValidDate(objekt.datum_aktivacije.ToString()))
                        {
                            var prvi = objekt.sifra.ToString();
                            var drugi = objekt.naziv.ToString();
                            var treci = objekt.mjesto_troška.ToString();
                            var cetvrti = objekt.datum_aktivacije.ToString();
                            var peti = objekt.smjestaj.ToString();
                            var line = string.Format("{0};{1};{2};{3};{4}", prvi, drugi, treci, cetvrti, peti);
                            w.WriteLine(line);
                            w.Flush();
                        }
                        else
                        {
                            var prvi = objekt.sifra.ToString();
                            var drugi = objekt.naziv.ToString();
                            var treci = objekt.mjesto_troška.ToString();
                            var cetvrti = objekt.datum_aktivacije.ToString();
                            var peti = objekt.smjestaj.ToString();
                            var line = string.Format("{0};{1};{2};{3};{4}", prvi, drugi, treci, cetvrti, peti);
                            n.WriteLine(line);
                            n.Flush();
                        }
                    }
                }
            }
        }
    }
}
