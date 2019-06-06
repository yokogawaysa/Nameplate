using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nameplate {
    class clLog {
        private static StreamWriter sw;

        public static bool openLog(string OP, string serial) {
            Directory.CreateDirectory(Properties.Settings.Default.drvProgramData + "log");
            return openLog(Properties.Settings.Default.drvProgramData + "log\\" + string.Format("{0} {1} {2}.log", OP, serial, DateTime.Now.ToString("ddMMyyyy HHmmss")));
        }
        public static bool openLog(string fileName) {
            try {
                sw = new StreamWriter(fileName);
            } catch (Exception e) {
                clFacilities.Report("Erro ao criar arquivo de log", "clLog", "openLog", e.Message.ToString());
                return false;
            }

            return true;
        }
        public static bool isLogOpened() { return sw != null; }

        public static void closeLog() {
            try {
                if (sw != null) {
                    sw.Close();
                    sw.Dispose();
                    sw = null;
                }
            } catch { }
        }

        public static bool doLog(string info) {
            try {
                sw.WriteLine(info);
            } catch (Exception e) {
                clFacilities.Report("Erro ao escrever no arquivo de log", "clLog", "doLog", e.Message.ToString());
                return false;
            }

            return true;
        }

    }
}

