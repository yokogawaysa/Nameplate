using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nameplate {
    public partial class frmPrint : Form {
        public frmMain mainForm;
        private string txtTML;
        private double plotX;
        private double plotY;
        private int blkCT;
        private bool cancelClicked;
        private bool continueClicked;
        private byte[] lastCommandPrepared = new byte[0];

        public frmPrint() {
            InitializeComponent();
        }

        private void frmPrint_Load(object sender, EventArgs e) {
            this.Text += Application.ProductVersion;
            Control ctrl;
            string txt;

            Model.refreshRestr(false);
            pnlProgressBar.SendToBack();

            RSComm.sErrorInfo sErr = Model.comm.openPort();
            if (sErr.hasError) {
                MessageBox.Show(sErr.errorMsg + "\r\n" + sErr.exceptionMsg, "Error opening serial port!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            clLog.openLog(Model.OP, Model.Serial.Substring(0, 9));
            clLog.doLog(string.Format("==============={0}===============", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")));
            clLog.doLog("---------------> DATABASE DATA");

            pnlIntegral.Enabled = chkIntegral.Checked = chkIntegral.Enabled = Model.hasIntegral;
            pnlRemoteSensor.Enabled = chkRemoteSensor.Checked = chkRemoteSensor.Enabled = Model.hasRemoteSensor;
            pnlTAG.Enabled = chkTAG.Checked = chkTAG.Enabled = Model.hasTag;

            if (Debugger.IsAttached) {
                chkIntegral.Enabled = true;
                chkRemoteSensor.Enabled = true;
                chkTAG.Enabled = true;
            }

            // check if user can fill or change fields manually
            txt = Model.RestrSoft.getProp("manualFill").CurrValue;
            if (txt.Equals("Sim")) {
                pnlVariables.Enabled = true;
                txt = Model.RestrSoft.getProp("manualFillDate").CurrValue;
                if (!txt.isEmpty()) {
                    DateTime dt;
                    if (DateTime.TryParse(txt, out dt)) {
                        pnlVariables.Enabled = ((DateTime.Now - dt).Minutes <= 0);
                    }
                }
                txt = Model.RestrSoft.getProp("manualFillOP").CurrValue;
                if (!txt.isEmpty()) {
                    pnlVariables.Enabled = (Model.OP.StartsWith(txt));
                }
            }
            if (Debugger.IsAttached) pnlVariables.Enabled = true;
            txt = Model.RestrSoft.getProp("onlyUpload").CurrValue;
            chkOnlyUpload.Visible = chkSimulate.Visible = txt.AsString().Equals("Sim") || Debugger.IsAttached;
            chkSimulate.Checked = Debugger.IsAttached;

            clLog.doLog("Integral enabled: " + Model.hasIntegral);
            clLog.doLog("Remote Sensor enabled: " + Model.hasRemoteSensor);
            clLog.doLog("Tag enabled: " + Model.hasTag);
            clLog.doLog("Only Tag:" + Model.hasOnlyTag);

            
            if (Model.hasIntegral) {
                ctrl = pnlIntegral;
                putText(ctrl, "MODEL", Model.PrintFieldData.FirstOrDefault(x=> x.Key.Equals("MODEL")).Value);
                putText(ctrl, "SUFFIX1", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("SUFFIX1")).Value);
                putText(ctrl, "SUFFIX2", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("SUFFIX2")).Value);
                putText(ctrl, "STYLE", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("STYLE")).Value);
                putText(ctrl, "SIZE", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("SIZE")).Value);
                putText(ctrl, "METERFACTORL", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("METERFACTORL")).Value);
                putText(ctrl, "METERFACTORH", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("METERFACTORH")).Value);
                putText(ctrl, "FLUIDPRESS", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("FLUIDPRESS")).Value);
                putText(ctrl, "FLUIDTEMP", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("FLUIDTEMP")).Value);
                putText(ctrl, "AMBTEMP", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("AMBTEMP")).Value);
                putText(ctrl, "SUPPLY1", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("SUPPLY1")).Value);
                putText(ctrl, "SUPPLY2", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("SUPPLY2")).Value);
                putText(ctrl, "OUTPUT1", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("OUTPUT1")).Value);
                putText(ctrl, "OUTPUT2", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("OUTPUT2")).Value);
                putText(ctrl, "OUTPUT3", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("OUTPUT3")).Value);
                putText(ctrl, "OUTPUT4", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("OUTPUT4")).Value);
                putText(ctrl, "YEARMONTH", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("YEARMONTH")).Value);
                putText(ctrl, "SERIAL", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("SERIAL")).Value);
                putText(ctrl, "SERIAL2", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("SERIAL2")).Value);
                putText(ctrl, "TAG1", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("TAG1")).Value);
                //putText(ctrl, "TAG2", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("TAG2")).Value);
            }
            if (Model.hasRemoteSensor) {
                ctrl = pnlRemoteSensor;
                putText(ctrl, "MODEL", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("MODEL")).Value);
                putText(ctrl, "SUFFIX1", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("SUFFIX1")).Value);
                putText(ctrl, "SUFFIX2", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("SUFFIX2")).Value);
                putText(ctrl, "STYLE", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("STYLE")).Value);
                putText(ctrl, "SIZE", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("SIZE")).Value);
                putText(ctrl, "METERFACTORL", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("METERFACTORL")).Value);
                putText(ctrl, "METERFACTORH", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("METERFACTORH")).Value);
                putText(ctrl, "FLUIDPRESS", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("FLUIDPRESS")).Value);
                putText(ctrl, "FLUIDTEMP", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("FLUIDTEMP")).Value);
                putText(ctrl, "AMBTEMP", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("AMBTEMP")).Value);
                putText(ctrl, "YEARMONTH", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("YEARMONTH")).Value);
                putText(ctrl, "SERIAL", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("SERIAL")).Value);
                putText(ctrl, "SERIAL2", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("SERIAL2")).Value);
                putText(ctrl, "TAG1", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("TAG1")).Value);
                //putText(ctrl, "TAG2", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("TAG2")).Value);
                putText(ctrl, "COMBNO", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("COMBNO")).Value);
            }
            if (Model.hasTag || Model.hasOnlyTag) {
                ctrl = pnlTAG;
                putText(ctrl, "TAG1", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("TAG_TAG1")).Value);
                putText(ctrl, "TAG2", Model.PrintFieldData.FirstOrDefault(x => x.Key.Equals("TAG_TAG2")).Value);
            }
        }


        private void putText(Control panel, string where, string value) {
            foreach (Control ctrl in panel.Controls) {
                if (ctrl is TextBox tb) {
                    if (tb.Tag != null && tb.Tag.Equals(where)) {
                        tb.Text = value;
                        if (value.Equals("##ERROR##")) {
                            tb.BackColor = Color.LightPink;
                            tb.ForeColor = Color.White;
                        } else {
                            tb.BackColor = SystemColors.Window;
                            tb.ForeColor = Color.Purple;
                        }
                        clLog.doLog(string.Format("{0} -> [{1}]: {2}", panel.Name.Substring(3), where, value));
                        return;
                    }
                }
            }
        }
        private string getText(Control panel, string from) {
            foreach (Control ctrl in panel.Controls) {
                if (ctrl is TextBox tb) {
                    if (tb.Tag != null && tb.Tag.Equals(from)) {
                        if (panel.Enabled) clLog.doLog(string.Format("{0} -> [{1}]: {2}", panel.Name.Substring(3), from, tb.Text));
                        return tb.Text;
                    }
                }
            }

            return "";
        }
        public static byte[] StringToByteArray(string hex) {
            hex = "1B" + ((hex.Length - 1) / 2).ToString("X6") + hex;
            Debug.WriteLine(hex);
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        private void readAndPrepareTML(string fileName) {
            Regex pattern;
            Match match;

            txtTML = clFacilities.readFileContents(fileName);
            if (!txtTML.Substring(txtTML.Length - 2, 2).Equals("\r\n")) {
                txtTML += "\r\n";
            }
            // a primeira linha do TML contém informações da posição X e Y base de tudo
            pattern = new Regex(@"X=(?<plotX>-?\d+\.*\d+),Y=(?<plotY>-?\d+\.*\d+),BLK=(?<blkCT>\d+)");
            match = pattern.Match(txtTML.Substring(0, txtTML.IndexOf("\r\n")));
            plotX = plotY = blkCT = 0;
            try { plotX = double.Parse(match.Groups["plotX"].Value, NumberStyles.Any, CultureInfo.InvariantCulture); } catch { }
            try { plotY = double.Parse(match.Groups["plotY"].Value, NumberStyles.Any, CultureInfo.InvariantCulture); } catch { }
            try { blkCT = int.Parse(match.Groups["blkCT"].Value); } catch { }
            if (blkCT == 0) blkCT = 1;

            prbSend.Maximum = 100;// blkCT + 1; // qty of fields to send
            prbSend.Value = 0;
        }
        private byte[] prepareTMLToSend(string TMLFileName) {
            // convert tml into hex raw
            txtTML = "EFBBBF" + clFacilities.convertToHexString(txtTML);

            // the end of string must be 0D0A
            if (!txtTML.Substring(txtTML.Length - 4, 4).Equals("0D0A")) {
                txtTML += "0D0A";
            }

            txtTML = "PF \"" + TMLFileName + "\" R " + txtTML;
            
            return Encoding.Default.GetBytes((char)0x1B + calcDataSize(txtTML.Length) + txtTML + (char)0x0D);
        }
        private string calcDataSize(int length) {
            string dataSize;

            // calc the data size of the total frame to transmit
            dataSize = length.ToString("X6");
            dataSize = Encoding.Default.GetString(new byte[] {
                byte.Parse(dataSize.Substring(0,2), NumberStyles.HexNumber),
                byte.Parse(dataSize.Substring(2,2), NumberStyles.HexNumber),
                byte.Parse(dataSize.Substring(4,2), NumberStyles.HexNumber)
            });
            return dataSize;
        }
        private byte[] prepareCommandToSend(string command) {
            lastCommandPrepared = Encoding.Default.GetBytes((char)0x1B + calcDataSize(command.Length) + command + (char)0x0D);

            return lastCommandPrepared;
        }
        private void btnPrint_Click(object sender, EventArgs e) {
            Control ctrl;
            byte[] bt = new byte[0];
            bool error = false;

            Model.comm.clearInputOutputBuffers();
            cancelClicked = false;
            btnPrintCancel.Visible = true;

            Model.comm.sendBytes(prepareCommandToSend("AM"), 0);
            wait(500, 3000, "AM 1", false);
            Model.comm.sendBytes(prepareCommandToSend("AD"), 0);
            wait(500, 1000, "AD 1", false);

            pnlProgressBar.Visible = true;
            pnlProgressBar.BringToFront();
            btnPrint.Enabled = false;

            clLog.doLog("---------------> PRINTING DATA");
            clLog.doLog(string.Format("Integral [{0}], Remote Sensor [{1}], TAG [{2}]", chkIntegral.Checked, chkRemoteSensor.Checked, chkTAG.Checked));
            if (!pnlVariables.Enabled) {
                clLog.doLog("---- Screen data is locked and is the same as database data ----");
            }

            #region INTEGRAL PART 1
            if (!error && chkIntegral.Checked) {
                clLog.doLog("Printing INTEGRAL PART 1");
                lblMsg.Text = "Sending INTEGRAL PART 1 data to the printer...";
                // get the file instructions
                readAndPrepareTML(Properties.Settings.Default.drvDatabase + "nameplate_integral_PART1.txt");

                // fill fields into file instructions
                ctrl = pnlIntegral;
                writeBlock("MODEL", getText(ctrl, "MODEL"));
                writeBlock("SUFFIX1", getText(ctrl, "SUFFIX1"));
                writeBlock("SUFFIX2", getText(ctrl, "SUFFIX2"));
                writeBlock("STYLE", getText(ctrl, "STYLE"));
                writeBlock("SIZE", getText(ctrl, "SIZE"));
                writeBlock("METERFACTORL", getText(ctrl, "METERFACTORL"));
                writeBlock("METERFACTORH", getText(ctrl, "METERFACTORH"));
                writeBlock("FLUIDPRESS", getText(ctrl, "FLUIDPRESS"));
                writeBlock("FLUIDTEMP", getText(ctrl, "FLUIDTEMP"));
                writeBlock("AMBTEMP", getText(ctrl, "AMBTEMP"));

                // prepare file instructions to be sent
                error = !sendAndStartTMLPrinter(prepareTMLToSend("autointegral1.tml"), "autointegral1.tml");
                if (Properties.Settings.Default.clearTML) {
                    Model.comm.sendBytes(prepareCommandToSend("RM \"autointegral1.tml\""), 0);
                    wait(500, 3000, "RM 1", false);
                }
            }
            #endregion

            #region REMOTE
            if (!error & chkRemoteSensor.Checked) {
                clLog.doLog("Printing REMOTE SENSOR");
                lblMsg.Text = "Sending REMOTE data to the printer...";
                // get the file instructions
                readAndPrepareTML(Properties.Settings.Default.drvDatabase + "nameplate_remote.txt");

                // fill fields into file instructions
                ctrl = pnlRemoteSensor;
                writeBlock("MODEL", getText(ctrl, "MODEL"));
                writeBlock("SUFFIX1", getText(ctrl, "SUFFIX1"));
                writeBlock("SUFFIX2", getText(ctrl, "SUFFIX2"));
                writeBlock("STYLE", getText(ctrl, "STYLE"));
                writeBlock("SIZE", getText(ctrl, "SIZE"));
                writeBlock("METERFACTORL", getText(ctrl, "METERFACTORL"));
                writeBlock("METERFACTORH", getText(ctrl, "METERFACTORH"));
                writeBlock("FLUIDPRESS", getText(ctrl, "FLUIDPRESS"));
                writeBlock("FLUIDTEMP", getText(ctrl, "FLUIDTEMP"));
                writeBlock("AMBTEMP", getText(ctrl, "AMBTEMP"));
                writeBlock("YEARMONTH", getText(ctrl, "YEARMONTH"));
                writeBlock("SERIAL", getText(ctrl, "SERIAL"));
                writeBlock("SERIAL2", getText(ctrl, "SERIAL2"));
                writeBlock("TAG1", getText(ctrl, "TAG1"));
                writeBlock("COMBNO", getText(ctrl, "COMBNO"));

                error = !sendAndStartTMLPrinter(prepareTMLToSend("autoremote.tml"), "autoremote.tml");
                if (Properties.Settings.Default.clearTML) {
                    Model.comm.sendBytes(prepareCommandToSend("RM \"autoremote.tml\""), 0);
                    wait(500, 3000, "RM 1", false);
                }

            }
            #endregion

            #region INTEGRAL PART 2
            if (!error && chkIntegral.Checked) {
                clLog.doLog("Printing INTEGRAL PART 2");

                // wait user change nameplate (invert it)
                btnPrintCancel.Visible = true;
                btnPrintContinue.Visible = true;
                btnPrintContinue.Focus();
                lblMsg.Text = "Click to print INTEGRAL part 2!";

                while (!cancelClicked && !continueClicked) {
                    Application.DoEvents();
                }
                btnPrintCancel.Visible = btnPrintContinue.Visible = false;
                

                if (continueClicked) {
                    lblMsg.Text = "Sending INTEGRAL PART 2 data to the printer...";
                    // get the file instructions
                    readAndPrepareTML(Properties.Settings.Default.drvDatabase + "nameplate_integral_PART2.txt");

                    // fill fields into file instructions
                    ctrl = pnlIntegral;
                    writeBlock("SUPPLY1", getText(ctrl, "SUPPLY1"));
                    writeBlock("SUPPLY2", getText(ctrl, "SUPPLY2"));
                    writeBlock("OUTPUT1", getText(ctrl, "OUTPUT1"));
                    writeBlock("OUTPUT2", getText(ctrl, "OUTPUT2"));
                    writeBlock("OUTPUT3", getText(ctrl, "OUTPUT3"));
                    writeBlock("OUTPUT4", getText(ctrl, "OUTPUT4"));
                    writeBlock("YEARMONTH", getText(ctrl, "YEARMONTH"));
                    writeBlock("SERIAL", getText(ctrl, "SERIAL"));
                    writeBlock("SERIAL2", getText(ctrl, "SERIAL2"));
                    writeBlock("TAG1", getText(ctrl, "TAG1"));

                    // prepare file instructions to be sent
                    error = !sendAndStartTMLPrinter(prepareTMLToSend("autointegral2.tml"), "autointegral2.tml");
                    if (Properties.Settings.Default.clearTML) {
                        Model.comm.sendBytes(prepareCommandToSend("RM \"autointegral2.tml\""), 0);
                        wait(500, 3000, "RM 1", false);
                    }
                }
            }
            #endregion


            if (cancelClicked) {
                lblMsg.Text = "Stopping the printer, wait a little...";
                Application.DoEvents();
                Model.comm.sendBytes(prepareCommandToSend("AM"), 0);
                wait(500, 1000, "AM 1", false);
                wait(1000);
                Model.comm.sendBytes(prepareCommandToSend("AD"), 0);
                wait(500, 1000, "AD 1", false);
                wait(1000);
            }
            Model.comm.sendBytes(prepareCommandToSend("OG"), 0);
            wait(500, 1000, "OG 1", false);


            clLog.closeLog();
            this.Close();
        }

        /// <summary>
        /// this procedure:
        /// 1. sends bt array
        /// 2. wait for printer response
        /// 3. send de load file command to load TML already charged
        /// 4. send the GO command to start marking
        /// </summary>
        /// <param name="bt">It conteins all TML instructions frame bytes to send</param>
        /// <param name="filename"></param>
        /// <returns></returns>
        private bool sendAndStartTMLPrinter(byte[] bt, string filename) {
            int tries = 0;
            RSComm.sErrorInfo sErr;

denovo:
            sErr = Model.comm.sendBytes(bt, 0);
            if (sErr.hasError) clFacilities.Report("Erro ao enviar pacote para serial", "frmPrint", "btnPrint", sErr.errorMsg + "\r\n" + sErr.exceptionMsg);
            bt = wait(200, 3000, "PF 1");
            if ((bt.Length == 0 || bt[0] != 6) && tries == 0) {
                tries++;
                goto denovo; // as x ela não recebe o primeiro comando, então repete uma vez
            }
            if (!cancelClicked && bt.Length > 0 && bt[0] == 6) { // 6 means ACK
                if (!chkOnlyUpload.Checked) {
                    // command do load the file already uploaded
                    sErr = Model.comm.sendBytes(prepareCommandToSend("LD \"" + filename + "\" 1 " + (chkSimulate.Checked ? "S" : "N")), 0);
                    bt = wait(200, 1000, "LD 1");
                    if (!sErr.hasError && bt.Length > 0 && bt[0] == 6) {
                        // start marking
                        sErr = Model.comm.sendBytes(prepareCommandToSend("GO"), 0);
                        lblMsg.Text = "Awaiting end of marking...";
                        bt = wait(200, -1, "GO F");
                        //Model.comm.sendBytes(prepareCommandToSend("AM"), 0);
                        //wait(500, 1000, "AM 1", false);
                        //wait(1000);
                        //Model.comm.sendBytes(prepareCommandToSend("AD"), 0);
                        //wait(500, 1000, "AD 1", false);
                        //// return to origin
                        //Model.comm.sendBytes(prepareCommandToSend("OG"), 0);
                        //bt = wait(200, 1000, "OG 1", false);
                        if (!cancelClicked && bt.Length > 0 && bt[0] == 6) {
                            return true;
                        }
                    } else {
                        clFacilities.Report("Erro ao enviar pacote para serial", "frmPrint", "btnPrint", sErr.hasError ? sErr.errorMsg + "\r\n" + sErr.exceptionMsg : "bytes: " + Encoding.ASCII.GetString(bt));
                        MessageBox.Show("An error occurred while sending commands to the printer!", "Printer error");
                    }
                }
            } else if (!cancelClicked && bt.Length > 0 && bt[0] == 21) { // NAK
                clFacilities.Report("Comando PF enviado mas retorno NAK", "frmPrint", "btnPrint_Click", Encoding.ASCII.GetString(bt));
                MessageBox.Show("Printer doesn't accept commands!", "Printer error");
            } else if (!cancelClicked) {
                clFacilities.Report("Comando PF enviado mas sem retorno", "frmPrint", "btnPrint_Click", "");
                MessageBox.Show("Printer not responding!", "Printer error");
            }


            return false;
        }

        private byte[] wait(int mili, int maxMili = -2, string waitFor = "", bool showErrorMsg = true) {
            var tmp = DateTime.Now;
            var tmp1 = DateTime.Now;
            bool goOut = false;
            byte[] bt = new byte[0];
            string txt;

            // if user don't pass maxMilli parameter and waitFor, just wait and go out
            if (maxMili == -2 && waitFor.isEmpty()) {
                while ((DateTime.Now - tmp).TotalMilliseconds < mili) {
                    Application.DoEvents();
                }
                return bt;
            }


            if (maxMili == -1) mili = int.MaxValue; // wait for printer answer or user cancel

            while (!goOut) {
                if ((DateTime.Now - tmp).TotalMilliseconds > mili && (waitFor.isEmpty() || Model.comm.getInputDataSize() == 0)) goOut = true;
                if (!waitFor.isEmpty() && Model.comm.getInputDataSize() > 0) {
                    bt = clFacilities.CombineByteArrays(bt, Model.comm.readInputBuffer());
                    if (bt.Length > 0 && bt[bt.Length - 1] == 0x0D) {
                        txt = bt.AsReadableString(); //Encoding.ASCII.GetString(bt);
                        if (txt.Contains(waitFor)) {
                            goOut = true;
                        } else if (txt.Contains("ER ")) {
                            goOut = true;
                            if (showErrorMsg) MessageBox.Show("Printer ERROR!", "Marking error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            clFacilities.Report("Printer answer with an error", "frmPrint", "wait", "Serial:" + Model.Serial + "\tCommand:" + lastCommandPrepared.AsReadableString() + "\twaitFor:" + waitFor + "\tResponse:" + txt);
                        } else if (txt.Contains("GO ")) {
                            if (!txt.Contains("GO M") && !txt.Contains("GO 1")) {
                                goOut = true;
                                if (showErrorMsg) MessageBox.Show("Printer ERROR!", "Marking error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                clFacilities.Report("Printer answer with an error", "frmPrint", "wait", "Serial:" + Model.Serial + "\tCommand:" + lastCommandPrepared.AsReadableString() + "\twaitFor:" + waitFor + "\tResponse:" + txt);
                            }
                        } else {
                            if (showErrorMsg) MessageBox.Show("Printer ERROR!", "Marking error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            clFacilities.Report("Printer answer not expected", "frmPrint", "wait", "Serial:" + Model.Serial + "\tCommand:" + lastCommandPrepared.AsReadableString() + "\twaitFor:" + waitFor + "\tResponse:" + txt);
                        }
                    }
                }

                Application.DoEvents();

                if (cancelClicked) goOut = true;
            }

            //if (!goOut && !cancelClicked && !waitFor.isEmpty() && Model.comm.getInputDataSize() < waitFor.Length + 6 && (DateTime.Now - tmp1).TotalMilliseconds < maxMili) goto denovo;

            return bt;
        }
        private void writeBlock(string field, string value) {
            string txt;
            string[] param;
            DataTable dt = new DataTable();

            int t = txtTML.IndexOf(",\"" + field + "\",");
            if (t > 0) {
                txt = getNextLine(t,"MV");
                param = txt.Substring(3).Replace(")","").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                param[0] = dt.Compute(param[0].Replace("X", plotX.ToString().Replace(",",".")), null).ToString().Replace(",",".");
                param[1] = dt.Compute(param[1].Replace("Y", plotY.ToString().Replace(",",".")), null).ToString().Replace(",", ".");
                substNextLine(t, "MV", "MV(" + param[0] + "," + param[1] + ")");
                txtTML = txtTML.Replace("<<" + field + ">>", value);
                clLog.doLog(string.Format("{0}={1}", field, value));
            } else {
                clFacilities.Report("Campo: " + field + " não encontrado no template", "frmPrint", "writeBlock", "");
            }
            Application.DoEvents();
        }
        private void substNextLine(int from, string searchValue, string replaceLineValue) {
            int to;

            from = txtTML.IndexOf("\r\n" + searchValue, from);
            if (from> 0) {
                from += 2;
                to = txtTML.IndexOf("\r\n", from);
                txtTML = txtTML.Substring(0, from) + replaceLineValue + txtTML.Substring(to);
            } else {
                clFacilities.Report("Valor não encontrado no arquivo de template: " + searchValue, "frmPrint", "getNextLine", "");
            }
        }
        private string getNextLine(int from, string searchValue) {
            from = txtTML.IndexOf("\r\n" + searchValue, from);
            if (from > 0) {
                from += 2;
                return txtTML.Substring(from, txtTML.IndexOf("\r\n", from)-from);
            }
            clFacilities.Report("Valor não encontrado no arquivo de template: " + searchValue, "frmPrint", "getNextLine", "");
            return "";
        }

        private void frmPrint_FormClosing(object sender, FormClosingEventArgs e) {
            if (clLog.isLogOpened()) clLog.doLog("Log didn't finished, window was forcibly closed.");
            clLog.closeLog();
            Model.comm.closePort();
        }

        private void frmPrint_Activated(object sender, EventArgs e) {
            mainForm.Hide();
        }

        private void chkIntegral_CheckedChanged(object sender, EventArgs e) {
            pnlIntegral.Enabled = ((CheckBox)sender).Checked;
            pnlIntegral.BackgroundImage = ((CheckBox)sender).Checked ? Properties.Resources.selected : Properties.Resources.transparent;
        }

        private void chkRemoteSensor_CheckedChanged(object sender, EventArgs e) {
            pnlRemoteSensor.Enabled = ((CheckBox)sender).Checked;
            pnlRemoteSensor.BackgroundImage = ((CheckBox)sender).Checked ? Properties.Resources.selected : Properties.Resources.transparent;
        }

        private void chkTAG_CheckedChanged(object sender, EventArgs e) {
            pnlTAG.Enabled = ((CheckBox)sender).Checked;
            pnlTAG.BackgroundImage = ((CheckBox)sender).Checked ? Properties.Resources.selected : Properties.Resources.transparent;
        }

        private void btnPrintCancel_Click(object sender, EventArgs e) {
            cancelClicked = true;
            clLog.doLog("Cancel button clicked, serial:" + Model.Serial);
        }

        private void btnPrintContinue_Click(object sender, EventArgs e) {
            continueClicked = true;
        }

        private void tmrProgressBar_Tick(object sender, EventArgs e) {
            try {
                prbSend.PerformStep();
                if (prbSend.Value >= prbSend.Maximum) {
                    prbSend.Value = 0;
                }
            } catch {
                prbSend.Value = 0;
            }
        }
    }
}