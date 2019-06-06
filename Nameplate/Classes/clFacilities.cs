using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nameplate {
    class clFacilities {
        public enum eDivideTexto {
            CODE,
            SPACES
        }

        public static string PreparaDrv(string path) {
            if (path.Length < 1) return "";
            if (path.IndexOf("\\bin\\") > 0)
                path = path.Remove(path.IndexOf("\\bin\\"));
            if (path.IndexOf("\\obj\\") > 0)
                path = path.Remove(path.IndexOf("\\obj\\"));
            if (path.Substring(path.Length - 1, 1) != "\\")
                path += "\\";
            return path;
        }


        public static string getDrvServer(string FileName) {

            try {
                StreamReader RD = new StreamReader(FileName);
                string Texto = RD.ReadToEnd();
                RD.Close();
                RD.Dispose();

                int t = Texto.IndexOf("<drvServer>");
                if (t > -1) {
                    return PreparaDrv(Texto.Substring(t + "<drvServer>".Length, Texto.IndexOf("</drvServer>") - t - "</drvServer>".Length + 1));
                }
            } catch { }

            return "";
        }

        #region REPORT LOG
        static StringBuilder l_Report = new StringBuilder("Sequencia\tHora\tClasse\tProcedimento\tErro\tDetalhe\r\n"); // contém os erros encontrados ao gerar o relatório
        static StringBuilder prg_Report = new StringBuilder("Sequencia\tHora\tClasse\tProcedimento\tDetalhe\r\n"); // contém a lista dos programas por onde o código passou
        public static bool disableReport = false;
        private static long ctErros = 0;

        /// <summary>
        /// registra em arquivo cada linha de erro que ocorre para em caso de crash do programa poder analisar o log
        /// </summary>
        private static void insertReportLog(string Texto) {
            // grava o log de erros para manter histórico quando der um crash
            string nomeArq = Application.ProductName;
            nomeArq = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + nomeArq + "\\Report" + DateTime.Now.DayOfWeek.ToString() + ".log";
            try {
                StreamWriter sw = new StreamWriter(nomeArq, true);
                sw.Write(Texto);
                sw.Close();
            } catch (Exception) { }
            apagarLogAntigo();
        }

        /// <summary>
        /// registra em arquivo cada linha de rastreamento dos passos para em caso de crash do programa poder analisar o log
        /// </summary>
        private static void insertReportprgLog(string Texto) {
            // grava o log de rastreamento de rotinas para manter histórico quando der um crash
            string nomeArq = Application.ProductName;
            nomeArq = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + nomeArq + "\\Reportprg" + DateTime.Now.DayOfWeek.ToString() + ".log";
            try {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + Application.ProductName);
                StreamWriter sw = new StreamWriter(nomeArq, true);
                sw.Write(Texto);
                sw.Close();
            } catch { }
            apagarLogAntigo();
        }

        /// <summary>
        /// para evitar crescimento exagerado do log, apaga log de 2 dias atrás
        /// </summary>
        private static void apagarLogAntigo() {
            // apaga o log de 2 dias atrás
            string nomeArq = System.AppDomain.CurrentDomain.FriendlyName + ".";
            nomeArq = nomeArq.Substring(0, nomeArq.IndexOf("."));
            nomeArq = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + nomeArq + "\\Reportprg" + DateTime.Now.AddDays(-2).DayOfWeek.ToString() + ".log";

            if (File.Exists(nomeArq)) {
                try {
                    File.Delete(nomeArq);
                } catch (Exception ex) {
                    Report("Erro ao zerar log do programa do dia " + DateTime.Now.AddDays(-2).DayOfWeek.ToString(), "clFacilidades", "apagarLogAntigo", ex.Message.ToString());
                }
            }
        }

        // rotinas referentes à relatórios
        /// <summary>
        /// Usado para registrar erros ocorridos no programa para permitir rastreamento do motivo do erro
        /// </summary>
        public static void Report(string msg, string Classe, string Rotina, string Descr) {
            if (disableReport) return;
            // monta o relatório de erros com as colunas:
            // mensagem amigável
            // rotina onde o erro ocorreu
            // Descrição do erro quando disponível
            string log;

            if (l_Report.Length > 1024 * 1024 * 35) {
                l_Report = null;
                l_Report = new StringBuilder("Sequencia\tHora\tClasse\tProcedimento\tErro\tDetalhe\r\n");
            }

            try {
                ctErros += 1;
                log = ctErros + "\t" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") + "\t<" + Classe + ">\t<" + Rotina + ">\t" + msg + "\t" + Descr + "\r\n";
                l_Report.Append(log);
            } catch (Exception) {
                // provavelmente encheu o buffer.....
                ctErros = 1;
                l_Report = null;
                l_Report = new StringBuilder("Sequencia\tHora\tClasse\tProcedimento\tErro\tDetalhe\r\n");
                return;
            }
            if (ctErros == 1)
                log = "\r\n=====================================================\r\n" + log;

            insertReportLog(log);
        }
        /// <summary>
        /// Usado para registrar por onde o programa passou, guardando a ordem e alguns detalhes para ajudar a identificar problemas
        /// </summary>
        public static void Reportprg(string Classe, string Rotina, string Descr) {
            if (disableReport) return;
            // monta o relatório contendo qual rotina entrou para usar em debug
            string log;

            if (prg_Report.Length > 1024 * 1024 * 35) {
                prg_Report = null;
                prg_Report = new StringBuilder("Sequencia\tHora\tClasse\tProcedimento\tDetalhe\r\n");
            }

            try {
                ctErros += 1;
                log = ctErros + "\t" + DateTime.Now.ToString("HH:mm:ss.fff") + "\t" + Classe + "\t" + Rotina + "\t" + Descr + "\r\n";
                prg_Report.Append(log);
            } catch (Exception) {
                ctErros = 1;
                prg_Report = null;
                prg_Report = new StringBuilder("Sequencia\tHora\tClasse\tProcedimento\tDetalhe\r\n");
                return;
            }
            if (ctErros == 1)
                log = "\r\n=====================================================\r\n" + log;

            insertReportprgLog(log);
        }

        /// <summary>
        /// informa se existem erros no log
        /// </summary>
        /// <returns>
        /// True = tem erro
        /// False = não tem erro
        /// </returns>
        public static bool temReport() {
            try {
                if (l_Report.ToString().Substring(l_Report.ToString().IndexOf("\r\n") + 2) != "")
                    return true;
            } catch (Exception) {
                return false;
            }
            return false;
        }


        /// <summary>
        /// Retorna o relatório de erros registrados pelas rotinas que usaram a função Report
        /// </summary>
        /// <returns>Relatório de erros</returns>
        public static string getReport() {
            return l_Report.ToString();
        }
        /// <summary>
        /// Retorna o relatório de passagens por rotinas e algumas informações de retorno das rotinas que usaram a função ReportPrg
        /// </summary>
        /// <returns>Retorna o relatório de rastreamento por onde o programa passou</returns>
        public static string getReportprg() {
            return prg_Report.ToString();
        }
        /// <summary>
        /// Apaga os relatórios de Erro e de Programa registrados até o momento
        /// </summary>
        public static void EraseAllReports() {
            ctErros = 0;
            prg_Report = new StringBuilder("Sequencia\tHora\tClasse\tProcedimento\tDetalhe\r\n");
            l_Report = new StringBuilder("Sequencia\tHora\tClasse\tProcedimento\tErro\tDetalhe\r\n");
        }
        #endregion

        public static string[] divideTexto(string texto, eDivideTexto criterio, bool forceDivision, params int[] maxSize) {
            int t;
            List<string> ret = new List<string>();
            int paramIndex = 0;
            bool divide;

            switch (criterio) {
                case eDivideTexto.CODE:
                    while (texto.Length > maxSize[paramIndex]) {
                        t = texto.Length - 1;
                        while (texto.Length > 0 && t > 0) {
                            divide = false;
                            switch (texto.Substring(t, 1)) {
                                case "/":
                                case "+":
                                case "-":
                                    if (t <= maxSize[paramIndex]) {
                                        divide = true;
                                        break;
                                    }
                                    break;
                            }
                            if (divide) {
                                ret.Add(texto.Substring(0, t));
                                texto = texto.Substring(t);
                                if (paramIndex < maxSize.Length - 1) paramIndex++;
                                break;
                            }
                            t--;
                        }
                    }
                    if (!texto.isEmpty()) {
                        if (forceDivision && texto.Length > maxSize[paramIndex]) {
                            ret.Add(texto.Substring(0, maxSize[paramIndex]));
                        } else {
                            ret.Add(texto);
                        }
                    }
                    
                    break;
                case eDivideTexto.SPACES:
                    while (texto.Length > 0) {
                        t = texto.Length - 1;
                        if (t > maxSize[paramIndex]) t = maxSize[paramIndex] - 1;
                        while (t > 1 && !texto.Substring(t, 1).Equals(" ")) {
                            t--;
                        }
                        if (t == 1) {
                            // there is no spaces, no way to divide
                            if (forceDivision && texto.Length > maxSize[paramIndex]) {
                                ret.Add(texto.Substring(0, maxSize[paramIndex]));
                                texto = texto.Substring(maxSize[paramIndex]).Trim();
                                if (paramIndex < maxSize.Length - 1) paramIndex++;
                            } else {
                                ret.Add(texto);
                                texto = "";
                            }                            
                        } else {
                            ret.Add(texto.Substring(0, t).Trim());
                            texto = texto.Substring(t).Trim();
                            if (paramIndex < maxSize.Length - 1) paramIndex++;
                        }
                    }
                    break;
            }

            while (ret.Count < maxSize.Length) ret.Add("");

            return ret.ToArray();
        }

        public static string readFileContents(string filename) {
            string txt = "";

            try {
                using (StreamReader RD = new StreamReader(filename)) {
                    txt = RD.ReadToEnd();
                }
            } catch (Exception ex) {
                Report("Erro ao abrir arquivo", "clFacilities", "readFileContents", ex.Message.ToString());
            }

            return txt;
        }

        public static string convertToHexString(string text) {
            byte[] chars;
            string output = "";

            chars = Encoding.ASCII.GetBytes(text);

            for (int i = 0; i < chars.Length; i++) {
                output += chars[i].ToString("X2");
            }

            return output;
        }

        public static byte[] CombineByteArrays(byte[] first, byte[] second) {
            byte[] ret = new byte[first.Length + second.Length];

            if (first.Length == 0) {
                ret = second;
            } else {
                Buffer.BlockCopy(first, 0, ret, 0, first.Length);
                Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            }
            return ret;
        }

    }
}
