using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nameplate {
    public static class Extensions {
        public static bool isEmpty(this string value) {
            return value == null || value.Equals("");
        }
        public static bool isNumber(this object obj) {
            float a;

            try {
                if (obj == null) return false;
                a = int.Parse(obj.ToString());
                a = float.Parse(obj.ToString());
                return true;
            } catch {
                return false;
            }
        }
        public static bool isBlank(this object obj) {
            return (obj == DBNull.Value || obj == null || obj.ToString().Trim().Equals(""));
        }
        public static string AsString(this object obj) {
            if (obj == DBNull.Value || obj == null) return "";
            return obj.ToString().Trim();
        }

        public static string AsReadableString(this byte[] bt, string emptyArrayString = "empty") {
            string txt = "";

            for (int i = 0; i < bt.Length; i++) {
                if (bt[i] < 31 || bt[i] > 126) {
                    txt += "[" + bt[i].ToString("X2") + "]";
                } else {
                    txt += Encoding.ASCII.GetString(bt, i, 1);
                }
            }

            return txt.isEmpty() ? emptyArrayString : txt;
        }
    }
}
