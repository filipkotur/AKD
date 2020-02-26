using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Linq;

namespace Xml
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            
        }
    }
    [XmlRoot("Popis_Imovine")]
    public class Popis_Imovine
    {
        [XmlElement("Imovina")]
        public List<Imovina> objekti = new List<Imovina>();
        
    }

    [XmlRoot("Imovina")]
    public class Imovina : IEquatable<Imovina>
    {
        [XmlElement("datum_aktivacije")]
        public string datum_aktivacije { get; set; }
        [XmlElement("smještaj")]
        public int smjestaj { get; set; }
        [XmlElement("naziv")]
        public string naziv { get; set; }
        [XmlElement("mjesto_troška")]
        public int mjesto_troška { get; set; }
        [XmlElement("šifra")]
        public int sifra { get; set; }
        public bool Equals(Imovina obj)
        {
            if (Object.ReferenceEquals(obj, null)) return false;
            if (Object.ReferenceEquals(this, obj)) return true;

            return this.sifra.Equals(obj.sifra);
        }
        public override int GetHashCode()
        {
            //Get hash code for the Code field. 
            int hashProductCode = sifra.GetHashCode();

            //Calculate the hash code for the product. 
            return hashProductCode;
        }
    }
}
