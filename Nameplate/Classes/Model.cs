using LM25AuthSystem.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Nameplate {
    class Model {
        public static string OP;
        public static string Serial;
        public static string MSCODE;
        public static string CCODE;
        public static bool hasIntegral; // print integral nameplate?
        public static bool hasRemoteSensor; // print remote sensor nameplate?
        public static bool hasTag; // print tag nameplate?
        public static bool hasOnlyTag; // print only tag nameplate
        public static string contents;
        public static List<KeyValuePair<string, string>> DBFieldData = new List<KeyValuePair<string, string>>();
        public static List<KeyValuePair<string, string>> PrintFieldData = new List<KeyValuePair<string, string>>();
        public static RSComm.cRSComm comm;
        public static clRestrConfig RestrConfig;
        public static clRestrSoft RestrSoft;

        public static void refreshRestr(bool displayErrorMessage) {
            try {
                RestrConfig = new clRestrConfig();
                StreamReader sr = new StreamReader(Properties.Settings.Default.drvDatabase + "restrictions.xml");
                XmlSerializer serializer = new XmlSerializer(typeof(clRestrConfig));
                RestrConfig = (clRestrConfig)serializer.Deserialize(sr);
                RestrSoft = RestrConfig.getSoftwareProps("Plaquetas");
                sr.Close();
                sr.Dispose();
            } catch (Exception ex) {
                if (displayErrorMessage) MessageBox.Show(ex.Message.ToString());
            }
        }
        public static void clearData() {
            OP = Serial = MSCODE = CCODE = contents = "";
            DBFieldData.Clear();
            PrintFieldData.Clear();
        }

    }
}

