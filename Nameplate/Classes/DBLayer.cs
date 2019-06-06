using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nameplate {
    class DBLayer {
        private static OleDbConnection con = null;

        public static bool Connect(string filename) {
            try {
                //con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Persist Security Info=True;");
                con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename + ";");
                con.Open();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString(), "Error opening database");
                clFacilities.Report("Error opening database: " + filename, "DBLayer", "Connect", ex.Message.ToString());
                return false;
            }

            return true;
        }
        public static void Disconnect() {
            if (con != null) {
                con.Close();
                con.Dispose();
            }
            con = null;
        }

        public static bool getFirstInfo(string OPSerial) {
            string filename;

            if (OPSerial.isEmpty()) return false;
            if (OPSerial.Length == 9) {
                filename = "???????????_" + OPSerial + ".txt";
            } else {
                filename = OPSerial + "_?????????.txt";
            }

            string[] files = Directory.GetFiles(Properties.Settings.Default.drvMicrosigaTXT, filename, SearchOption.TopDirectoryOnly);
            if (files.Length != 1) {
                clFacilities.Report("Arquivo TXT não encontrado: " + filename, "DBLayer", "getFirstInfo", "");
                return false;
            }

            files[0] = Path.GetFileName(files[0]);
            Model.OP = files[0].Substring(0, 11);
            Model.Serial = files[0].Substring(12, 9);

            using (StreamReader sr = new StreamReader(Properties.Settings.Default.drvMicrosigaTXT + files[0])) {
                Model.contents = sr.ReadToEnd() + "$";
            }

            Model.MSCODE = getFieldContent("$MS_CODE");
            Model.CCODE = getFieldContent("$ORD_INST_CONTECT1_X76");

            if (Model.MSCODE.isEmpty()) {
                clFacilities.Report("$MS_CODE field not found in TXT or empty!", "DBLayer", "getFirstInfo", "contents: " + Model.contents);
                return false;
            }

            return true;
        }

        public static string getFieldContent(string field) {
            int t;
            string txt;

            try {
                txt = "";
                t = Model.contents.IndexOf(field);
                if (t > -1) {
                    txt = Model.contents.Substring(t + field.Length, Model.contents.IndexOf("$", t + 1) - t - field.Length);
                }
            } catch {
                txt = "";
            }

            return txt;
        }

        public static bool getDBInfo() {
            string table = null;

            if (con == null || con.State != System.Data.ConnectionState.Open) {
                return false;
            }

            table = getAccessTable();

            if (table == null) {
                clFacilities.Report("Não foi possível determinar qual tabela usar do banco pelo mscode", "DBLayer", "getDBInfo", Model.MSCODE);
                return false;
            }

            try {
                OleDbCommand command = new OleDbCommand("select top 1 * from " + table + " where SERIALNO=@serial", con);
                OleDbParameter param0 = new OleDbParameter("@serial", OleDbType.VarChar);

                param0.Value = Model.Serial;
                command.Parameters.Add(param0);

                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataSet dset = new DataSet();
                da.Fill(dset);

                if (dset.Tables.Count == 0 || dset.Tables[0].Rows.Count == 0) {
                    dset.Dispose();
                    da.Dispose();
                    command.Dispose();
                    clFacilities.Report("Número de série não encontrado na tabela: " + table, "DBLayer", "getDBInfo", Model.MSCODE);
                    return false;
                } else {
                    for (int col = 0; col < dset.Tables[0].Columns.Count; col++) {
                        // mesmo tendo a informação no TXT, a que vale é a do ACCESS, então faz a releitura
                        switch (dset.Tables[0].Columns[col].ColumnName.ToUpper()) {
                            case "MODEL":
                                Model.MSCODE = dset.Tables[0].Rows[0][col].AsString();
                                break;
                            case "SERIALNO":
                                Model.Serial = dset.Tables[0].Rows[0][col].AsString();
                                break;
                        }
                        Model.DBFieldData.Add(new KeyValuePair<string, string>(dset.Tables[0].Columns[col].ColumnName.ToUpper(), dset.Tables[0].Rows[0][col].AsString()));
                    }
                }
            } catch (Exception ex) {
                clFacilities.Report(ex.Message.ToString(), "DBLayer", "getDBInfo", Model.Serial);
                return false;
            }


            return true;
        }

        public static string getAccessTable() {
            switch (Model.MSCODE.Substring(8, 1)) {
                case "A":
                    switch (Model.MSCODE.Substring(23, 1)) {
                        case "D":
                            return "AXW_Brain_AXWA";
                        case "J":
                            return "AXW_Hart_AXWA";
                    }
                    break;
                case "D":
                    return "AXF_Brain_Hart";
                case "W":
                    switch (Model.CCODE.Substring(13, 1)) {
                        case "D":
                            return "AXW_Brain_AXWA";
                        case "J":
                            return "AXW_Hart_AXWA";
                    }
                    break;
            }

            return null;
        }

        public static void fillInfo() {
            string[] textos;
            
            txtTablesLib.sErrorInfo sErr = new txtTablesLib.sErrorInfo();
            txtTablesLib.simpleTableTests table = new txtTablesLib.simpleTableTests(Properties.Settings.Default.drvDatabase + "dlls");
            txtTablesLib.textRules rules = new txtTablesLib.textRules();
            List<Dictionary<string, string>> result;

            rules.ColumnSeparator = "\t";
            rules.ORSeparator = ",";
            rules.ResultColumnsCount = 1;
            rules.RowSeparator = "\r\n";
            rules.SearchCriteria = txtTablesLib.textRules.eSearchCriteria.getOnlyFirstOccurrencyLine;
            rules.useWildcards = true;

            Model.PrintFieldData.Clear();

            addPrintData("MODEL", Model.MSCODE.Substring(0, Model.MSCODE.IndexOf("-")));
            addPrintData("STYLE", Model.DBFieldData.FirstOrDefault(x => x.Key.Equals("SCODE")).Value);
            textos = clFacilities.divideTexto(Model.MSCODE.Substring(Model.MSCODE.IndexOf("-")), clFacilities.eDivideTexto.CODE, true, Properties.Settings.Default.limitIntegralSUFIX1, Properties.Settings.Default.limitIntegralSUFIX2);
            addPrintData("SUFFIX1", textos[0]);
            addPrintData("SUFFIX2", textos.Length > 1 ? textos[1] : "");
            if (!getAccessTable().ToUpper().Equals("AXF_Brain_Hart".ToUpper())) {
                addPrintData("SIZE", Model.DBFieldData.FirstOrDefault(x => x.Key.Equals("C27_NOMINAL_SIZE")).Value);
                addPrintData("METERFACTORL", Model.DBFieldData.FirstOrDefault(x => x.Key.Equals("C20_LOWMF")).Value);
                addPrintData("METERFACTORH", Model.DBFieldData.FirstOrDefault(x => x.Key.Equals("C21_HIGHMF")).Value);
            } else {
                addPrintData("SIZE", Model.DBFieldData.FirstOrDefault(x => x.Key.Equals("NOMISIZE")).Value);
                addPrintData("METERFACTORL", Model.DBFieldData.FirstOrDefault(x => x.Key.Equals("LOWMF")).Value);
                addPrintData("METERFACTORH", Model.DBFieldData.FirstOrDefault(x => x.Key.Equals("HIGHMF")).Value);
            }
            addPrintData("FLUIDPRESS", table.getStringTableResult(Properties.Settings.Default.drvDatabase + "FluidPress.txt", Model.MSCODE, rules));
            addPrintData("FLUIDTEMP", table.getStringTableResult(Properties.Settings.Default.drvDatabase + "FluidTemp.txt", Model.MSCODE, rules));
            addPrintData("AMBTEMP", table.getStringTableResult(Properties.Settings.Default.drvDatabase + "AmbTemp.txt", Model.MSCODE, rules));
            rules.ResultColumnsCount = 2;
            result = table.getTableResult(Properties.Settings.Default.drvDatabase + "PowerSupply.txt", Model.MSCODE, rules);
            addPrintData("SUPPLY1", result.Count > 0 ? result[0]["POWERSUPPLY11"].ToString() : "");
            addPrintData("SUPPLY2", result.Count > 0 ? result[0]["POWERSUPPLY12"].ToString() : "");
            rules.ResultColumnsCount = 1;
            addPrintData("OUTPUT1", table.getStringTableResult(Properties.Settings.Default.drvDatabase + "OutputType.txt", Model.MSCODE, rules));
            addPrintData("OUTPUT2", getOUTPUTRATE(table, rules));
            addPrintData("OUTPUT3", ""); // BLANK
            rules.ResultColumnsCount = 1;
            addPrintData("OUTPUT4", table.getStringTableResult(Properties.Settings.Default.drvDatabase + "Output.txt", Model.MSCODE, rules));
            addPrintData("YEARMONTH", table.getStringTableResult(Properties.Settings.Default.drvDatabase + "ProdYear.txt", Model.MSCODE, rules));
            addPrintData("SERIAL", Model.DBFieldData.FirstOrDefault(x => x.Key.Equals("SERIALNO")).Value);
            addPrintData("SERIAL2", Model.DBFieldData.FirstOrDefault(x => x.Key.Equals("WEEKNO")).Value);
            addPrintData("COMBNO", Model.DBFieldData.FirstOrDefault(x => x.Key.Equals("CSERIAL")).Value);
            textos = clFacilities.divideTexto(Model.DBFieldData.FirstOrDefault(x => x.Key.Equals("TAGNO")).Value, clFacilities.eDivideTexto.SPACES, true, Properties.Settings.Default.limitIntegralTAG1, Properties.Settings.Default.limitIntegralTAG2);
            addPrintData("TAG1", textos[0]);
            //addPrintData("TAG2", textos[1]);
            textos = clFacilities.divideTexto(Model.DBFieldData.FirstOrDefault(x => x.Key.Equals("TAGNO")).Value, clFacilities.eDivideTexto.SPACES, true, Properties.Settings.Default.limitTagTAG1, Properties.Settings.Default.limitTagTAG2);
            addPrintData("TAG_TAG1", textos[0]);
            addPrintData("TAG_TAG2", textos[1]);

            Model.hasIntegral = Model.MSCODE.Substring(8, 1).Equals("A");
            Model.hasRemoteSensor = "DW".Contains(Model.MSCODE.Substring(8, 1));
            Model.hasTag = Model.MSCODE.Contains("/SCT");
        }
        private static void addPrintData(string key, string value) {
            string outvalue = value;
            string[] param;
            string[] format;
            DateTime dt;
            string txt1;
            string[] dateParts;

            if (value == null) {
                outvalue = "##ERROR##";
            } else if (value.Contains("[")) {
                outvalue = "";
                // there are database fields here
                param = value.Split('+');
                txt1 = "";
                foreach (string par in param) {
                    if (par.Contains('[')) { // it is a database field
                        format = par.Replace("[", "").Replace("]", "").Split(':');
                        txt1 = Model.DBFieldData.FirstOrDefault(x => x.Key.Equals(format[0])).Value;
                        if (format.Length > 1) { // need to be formated
                            switch (format[1].Substring(0, 2)) {
                                case "DT": // date format
                                    try {
                                        dateParts = txt1.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                                        dt = new DateTime(2000 + int.Parse(dateParts[0]), int.Parse(dateParts[1]), int.Parse(dateParts[2].Substring(0, 2)));
                                        outvalue = dt.ToString(format[1].Substring(2));
                                    } catch {
                                        clFacilities.Report("Erro ao formatar campo em data", "DBLayer", "addPrintData", value + " -> " + format[1]);
                                        outvalue = "##ERROR##";
                                    }
                                    break;
                            }
                        } else {
                            outvalue += txt1;
                        }
                    } else {
                        // it is a string
                        outvalue += par;
                    }
                }
            }

            Model.PrintFieldData.Add(new KeyValuePair<string, string>(key, outvalue));
        }

        private static string getOUTPUTRATE(txtTablesLib.simpleTableTests table, txtTablesLib.textRules rules) {
            string ret = "";
            List<Dictionary<string, string>> result;
            string Spanunit;
            string CPLSout;
            string RATE_VAL;
            string RATE_UNIT;
            string FLOW_SEL;
            string VOL_F_UNIT;
            string MASS_F_UNIT;
            string OUT_MODE;

            RATE_VAL = Model.DBFieldData.FirstOrDefault(x => x.Key.Equals("E14_P1_RATE_VAL")).Value.AsString();
            RATE_UNIT = Model.DBFieldData.FirstOrDefault(x => x.Key.Equals("E13_P1_RATE_UNIT")).Value.AsString();
            FLOW_SEL = Model.DBFieldData.FirstOrDefault(x => x.Key.Equals("C30_PV_FLOW_SEL")).Value.AsString();
            VOL_F_UNIT = Model.DBFieldData.FirstOrDefault(x => x.Key.Equals("C32_VOL_F_UNIT")).Value.AsString();
            MASS_F_UNIT = Model.DBFieldData.FirstOrDefault(x => x.Key.Equals("C33_MASS_F_UNIT")).Value.AsString();
            OUT_MODE = Model.DBFieldData.FirstOrDefault(x => x.Key.Equals("E10_P1_OUT_MODE")).Value.AsString();

            rules.ResultColumnsCount = 3;
            CPLSout = "P/SOUT1" + RATE_VAL;

            switch (FLOW_SEL) {
                case "1":
                    result = table.getTableResult(Properties.Settings.Default.drvDatabase + "units.txt", VOL_F_UNIT, rules);
                    Spanunit = result[0]["VOL_F_UNIT"].ToString();
                    break;
                case "2":
                    result = table.getTableResult(Properties.Settings.Default.drvDatabase + "units.txt", MASS_F_UNIT, rules);
                    Spanunit = result[0]["MASS_F_UNIT"].ToString();
                    break;
                default:
                    Spanunit = "";
                    break;
            }

            result = table.getTableResult(Properties.Settings.Default.drvDatabase + "units.txt", RATE_UNIT, rules);
            if (result.Count > 0) CPLSout += result[0]["RATE_UNIT"].ToString();
            switch (OUT_MODE) {
                case "": // don't print
                    CPLSout = "";
                    break;
                case "0": // no function
                    break;
                case "1": // fixed pulse output
                    switch (RATE_UNIT) {
                        case "0":
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                            CPLSout += Spanunit + "/P";
                            break;
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                        case "10":
                        case "11":
                            CPLSout += "P/" + Spanunit;
                            break;
                    }
                    break;
                case "2": // frequency output
                case "3": // status output
                    break;
            }

            return CPLSout;
        }


    }
}
