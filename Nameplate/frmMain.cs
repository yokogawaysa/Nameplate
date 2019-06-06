using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RWInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nameplate {
    public partial class frmMain : Form {
        private bool WebApiReady = false;
        public frmMain() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            string[] Param;
            this.Text += Application.ProductVersion;

            Properties.Settings.Default.drv = clFacilities.PreparaDrv(Application.StartupPath);
            Properties.Settings.Default.drvProgramData = clFacilities.PreparaDrv(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + Application.ProductName);

            string drvServer;
            drvServer = clFacilities.getDrvServer(Properties.Settings.Default.drv + "Config.xml");
            if (drvServer.Equals("")) drvServer = clFacilities.PreparaDrv(clFacilities.getDrvServer(Properties.Settings.Default.drvProgramData + "Config.xml"));

            #region CONFIG.XML
            clXML xml = new clXML(drvServer + "database\\dlls");
            clXML xml2 = new clXML(drvServer + "database\\dlls");
            string txt1, txt2 = "";
            
            // verifica se a data do config.xml instalado é <> do que o do usuário, se sim, copia por cima o arquivo
            txt1 = xml2.getValue("Comum", "ATUALIZADOEM", Properties.Settings.Default.drv + "Config.xml");
            txt2 = xml.getValue("Comum", "ATUALIZADOEM", Properties.Settings.Default.drvProgramData + "Config.xml");
            if (txt1 != txt2 || txt2.Equals("")) {
                // cria a pasta
                try {
                    Directory.CreateDirectory(Properties.Settings.Default.drvProgramData);
                    // se o arquivo ainda não existe, então só copia o novo
                    if (!File.Exists(Properties.Settings.Default.drvProgramData + "Config.xml")) {
                        File.Copy(Properties.Settings.Default.drv + "Config.xml", Properties.Settings.Default.drvProgramData + "Config.xml", true);
                    } else {
                        // como o arquivo já existe, então copia somente as chaves dentro de comum que contiverem o atributo de obrigatório
                        string Key = "ATUALIZADOEM";
                        string Value = "";
                        List<KeyValuePair<string, string>> Attribs;
                        // já grava a primeira informação que é a data
                        xml.setValue("Comum", Key, txt1);
                        xml.setKeyAttrib("Comum", Key, "required", "1");
                        while (xml2.getNextNodeValueAndAttribs("Comum", ref Key, out Value, out Attribs)) {
                            try {
                                // don't change value if exists
                                xml.getValue("Comum", Key);
                                if (xml.getLastErrorInfo.hasError) {
                                    xml.setValue("Comum", Key, Value);
                                }
                                // change value even if it exists but only if has attributes
                                foreach (KeyValuePair<string, string> item in Attribs) {
                                    xml.setValue("Comum", Key, Value);
                                    xml.setKeyAttrib("Comum", Key, item.Key, item.Value);
                                }
                            } catch { }
                        }
                        xml.saveFile("");
                    }

                } catch (Exception ex) {
                    MessageBox.Show(ex.Message.ToString(), "Erro ao copiar arquivo de definições!");
                    this.Dispose();
                    return;
                }
            }
            xml2.Dispose();
            #endregion

            // pega a localização do servidor de banco de dados
            Properties.Settings.Default.drvServer = clFacilities.PreparaDrv(xml.getValue("Comum", "drvServer", Properties.Settings.Default.drvProgramData + "Config.xml"));
            Properties.Settings.Default.drvDB = clFacilities.PreparaDrv(xml.getValue("Comum", "drvDB", Properties.Settings.Default.drvProgramData + "Config.xml"));
            Properties.Settings.Default.drvDatabase = clFacilities.PreparaDrv(xml.getValue("Comum", "drvDataBase", Properties.Settings.Default.drvProgramData + "Config.xml"));
            Properties.Settings.Default.drvMicrosigaTXT = clFacilities.PreparaDrv(xml.getValue("Comum", "drvMicrosigaTXT", Properties.Settings.Default.drvProgramData + "Config.xml"));

            Properties.Settings.Default.commPort = xml.getValue("Comum", "CommPort", Properties.Settings.Default.drvProgramData + "Config.xml");
            Properties.Settings.Default.commSettings = xml.getValue("Comum", "CommSettings", Properties.Settings.Default.drvProgramData + "Config.xml");
            if (Properties.Settings.Default.commPort.isEmpty()) Properties.Settings.Default.commPort = "COM1";
            if (Properties.Settings.Default.commSettings.isEmpty()) Properties.Settings.Default.commSettings = "9600,n,8,1";

            if (!(txt1 = xml.getValue("Comum", "limitIntegralSUFIX1")).isEmpty()) Properties.Settings.Default.limitIntegralSUFIX1 = int.Parse(txt1);
            if (!(txt1 = xml.getValue("Comum", "limitIntegralSUFIX2")).isEmpty()) Properties.Settings.Default.limitIntegralSUFIX2 = int.Parse(txt1);
            if (!(txt1 = xml.getValue("Comum", "limitIntegralTAG1")).isEmpty()) Properties.Settings.Default.limitIntegralTAG1 = int.Parse(txt1);
            if (!(txt1 = xml.getValue("Comum", "limitIntegralTAG2")).isEmpty()) Properties.Settings.Default.limitIntegralTAG2 = int.Parse(txt1);
            if (!(txt1 = xml.getValue("Comum", "limitTagTAG1")).isEmpty()) Properties.Settings.Default.limitTagTAG1 = int.Parse(txt1);
            if (!(txt1 = xml.getValue("Comum", "limitTagTAG2")).isEmpty()) Properties.Settings.Default.limitTagTAG2 = int.Parse(txt1);
            //Properties.Settings.Default.clearTML = bool.Parse(xml.getValue("Comum", "clearTML"));

            xml.Dispose();
            getConfigData(); // get all restrictions and remote configurations for this software

            txtBarCode.Enabled = DBLayer.Connect(Properties.Settings.Default.drvDB + "PRODUCT.mdb");

            Model.comm = new RSComm.cRSComm(Properties.Settings.Default.drvDatabase + "dlls");
            Model.comm.dataReceived += Comm_dataReceived;
            Model.comm.timeOutEvent += Comm_timeOutEvent;
            Param = Properties.Settings.Default.commSettings.Split(',');
            System.IO.Ports.StopBits sb = System.IO.Ports.StopBits.None;
            System.IO.Ports.Parity pt = System.IO.Ports.Parity.None;
            switch (Param[3]) {
                case "0": sb = System.IO.Ports.StopBits.None; break;
                case "1": sb = System.IO.Ports.StopBits.One; break;
                case "2": sb = System.IO.Ports.StopBits.Two; break;
            }
            switch (Param[1]) {
                case "n": pt = System.IO.Ports.Parity.None; break;
                case "e": pt = System.IO.Ports.Parity.Even; break;
                case "o": pt = System.IO.Ports.Parity.Odd; break;
            }
            RSComm.sErrorInfo sError = Model.comm.openPort(Properties.Settings.Default.commPort, int.Parse(Param[0]), int.Parse(Param[2]), sb, 5000, false, false, Encoding.UTF8, System.IO.Ports.Handshake.None, pt);
            if (sError.hasError) {
                MessageBox.Show(sError.errorMsg + "\r\n" + sError.exceptionMsg, "Serial port invalid!" + Properties.Settings.Default.commPort);
                //Application.Exit();
                //return;
            }
            Model.comm.closePort(); // keep it closed and free to use by another applications

            txtBarCode.Tag = "";

            Model.refreshRestr(true);

            // examples to test
            // B1V201531, B1V290001   INTEGRAL
            // B1V201963    REMOTE SENSOR
        }

        private async void getConfigData() {
            // get parameters from webapi
            try {
                using (HttpClient client = new HttpClient()) {
                    string uri = "http://ferramentasprodutividade/MSCODEAPI/api/Software?Software=Nameplate";
                    using (HttpResponseMessage response = await client.GetAsync(uri)) {
                        if (response.IsSuccessStatusCode) {
                            string jsonstring = await response.Content.ReadAsStringAsync();

                            JObject json = JsonConvert.DeserializeObject<dynamic>(jsonstring);

                            WebApiReady = true;
                            try { Properties.Settings.Default.clearTML = json["clearTML"].Value<bool>(); } catch { Properties.Settings.Default.clearTML = true; }
                        } else {
                            clFacilities.Report("Error getting some parameters from YSA Server", "frmMain", "getConfigData"," Cod: " + response.StatusCode);
                        }
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString(), "Error getting some parameters from YSA Server");
            }
        }

        private void txtBarCode_TextChanged(object sender, EventArgs e) {
            if (txtBarCode.Text.Trim().Length == 11 || (txtBarCode.Text.Trim().Length == 9 && txtBarCode.Text.Substring(0, 2).Equals("B1"))) {
                readInput();
            }
        }

        private void readInput() {
            Model.clearData();

            if (DBLayer.getFirstInfo(txtBarCode.Text)) {
                if (DBLayer.Connect(Properties.Settings.Default.drvDB + "PRODUCT.mdb")) {
                    if (DBLayer.getDBInfo()) {
                        // fill up more info about the product
                        DBLayer.fillInfo();
                        frmPrint print = new frmPrint();
                        print.mainForm = this;
                        print.ShowDialog(this);
                        txtBarCode.Focus();
                        print.Close();
                        print.Dispose();
                        print = null;
                        this.Show();
                        this.Focus();
                    } else {
                        MessageBox.Show("It was not possible to get information from database!", "Error getting information");
                    }
                } else {
                    MessageBox.Show("It was not possible to connect to database!", "Error connectiong to database");
                }
            } else {
                MessageBox.Show("Error getting information from Microsiga TXT file!", "Error getting information");
            }

            DBLayer.Disconnect();
            txtBarCode.Clear();
        }

        private void Comm_dataReceived(byte[] buffer) {
            //MessageBox.Show("********RECEIVED:\r\n" + Encoding.Default.GetString(buffer));
        }

        private void Comm_timeOutEvent(byte[] buffer) {
            //MessageBox.Show("********TIMEOUT:\r\n" + Encoding.Default.GetString(buffer));
        }

        private void timer1_Tick(object sender, EventArgs e) {
            if (WebApiReady) {
                imgWebApi.Visible = false;
                timer1.Enabled = false;
            } 
        }

    }
}
