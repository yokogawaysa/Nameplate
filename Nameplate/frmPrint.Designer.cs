namespace Nameplate {
    partial class frmPrint {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrint));
            this.chkIntegral = new System.Windows.Forms.CheckBox();
            this.chkRemoteSensor = new System.Windows.Forms.CheckBox();
            this.chkTAG = new System.Windows.Forms.CheckBox();
            this.pnlProgressBar = new System.Windows.Forms.Panel();
            this.btnPrintCancel = new System.Windows.Forms.Button();
            this.btnPrintContinue = new System.Windows.Forms.Button();
            this.prbSend = new System.Windows.Forms.ProgressBar();
            this.lblMsg = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.chkOnlyUpload = new System.Windows.Forms.CheckBox();
            this.chkSimulate = new System.Windows.Forms.CheckBox();
            this.tmrProgressBar = new System.Windows.Forms.Timer(this.components);
            this.pnlVariables = new System.Windows.Forms.Panel();
            this.pnlIntegral = new System.Windows.Forms.Panel();
            this.pnlTAG = new System.Windows.Forms.Panel();
            this.textBox36 = new System.Windows.Forms.TextBox();
            this.textBox35 = new System.Windows.Forms.TextBox();
            this.pnlRemoteSensor = new System.Windows.Forms.Panel();
            this.textBox38 = new System.Windows.Forms.TextBox();
            this.textBox37 = new System.Windows.Forms.TextBox();
            this.textBox34 = new System.Windows.Forms.TextBox();
            this.textBox31 = new System.Windows.Forms.TextBox();
            this.textBox33 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox22 = new System.Windows.Forms.TextBox();
            this.textBox23 = new System.Windows.Forms.TextBox();
            this.textBox24 = new System.Windows.Forms.TextBox();
            this.textBox25 = new System.Windows.Forms.TextBox();
            this.textBox26 = new System.Windows.Forms.TextBox();
            this.textBox27 = new System.Windows.Forms.TextBox();
            this.textBox28 = new System.Windows.Forms.TextBox();
            this.textBox29 = new System.Windows.Forms.TextBox();
            this.textBox30 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.textBox19 = new System.Windows.Forms.TextBox();
            this.textBox21 = new System.Windows.Forms.TextBox();
            this.textBox20 = new System.Windows.Forms.TextBox();
            this.pnlProgressBar.SuspendLayout();
            this.pnlVariables.SuspendLayout();
            this.pnlIntegral.SuspendLayout();
            this.pnlTAG.SuspendLayout();
            this.pnlRemoteSensor.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkIntegral
            // 
            this.chkIntegral.AutoSize = true;
            this.chkIntegral.Location = new System.Drawing.Point(529, 333);
            this.chkIntegral.Name = "chkIntegral";
            this.chkIntegral.Size = new System.Drawing.Size(113, 20);
            this.chkIntegral.TabIndex = 0;
            this.chkIntegral.Text = "Print Integral";
            this.chkIntegral.UseVisualStyleBackColor = true;
            this.chkIntegral.CheckedChanged += new System.EventHandler(this.chkIntegral_CheckedChanged);
            // 
            // chkRemoteSensor
            // 
            this.chkRemoteSensor.AutoSize = true;
            this.chkRemoteSensor.Location = new System.Drawing.Point(529, 359);
            this.chkRemoteSensor.Name = "chkRemoteSensor";
            this.chkRemoteSensor.Size = new System.Drawing.Size(161, 20);
            this.chkRemoteSensor.TabIndex = 1;
            this.chkRemoteSensor.Text = "Print Remote Sensor";
            this.chkRemoteSensor.UseVisualStyleBackColor = true;
            this.chkRemoteSensor.CheckedChanged += new System.EventHandler(this.chkRemoteSensor_CheckedChanged);
            // 
            // chkTAG
            // 
            this.chkTAG.AutoSize = true;
            this.chkTAG.Location = new System.Drawing.Point(529, 385);
            this.chkTAG.Name = "chkTAG";
            this.chkTAG.Size = new System.Drawing.Size(88, 20);
            this.chkTAG.TabIndex = 2;
            this.chkTAG.Text = "Print TAG";
            this.chkTAG.UseVisualStyleBackColor = true;
            this.chkTAG.CheckedChanged += new System.EventHandler(this.chkTAG_CheckedChanged);
            // 
            // pnlProgressBar
            // 
            this.pnlProgressBar.BackColor = System.Drawing.Color.Moccasin;
            this.pnlProgressBar.Controls.Add(this.btnPrintCancel);
            this.pnlProgressBar.Controls.Add(this.btnPrintContinue);
            this.pnlProgressBar.Controls.Add(this.prbSend);
            this.pnlProgressBar.Controls.Add(this.lblMsg);
            this.pnlProgressBar.Location = new System.Drawing.Point(244, 121);
            this.pnlProgressBar.Name = "pnlProgressBar";
            this.pnlProgressBar.Size = new System.Drawing.Size(566, 100);
            this.pnlProgressBar.TabIndex = 18;
            this.pnlProgressBar.Visible = false;
            // 
            // btnPrintCancel
            // 
            this.btnPrintCancel.Location = new System.Drawing.Point(27, 62);
            this.btnPrintCancel.Name = "btnPrintCancel";
            this.btnPrintCancel.Size = new System.Drawing.Size(97, 25);
            this.btnPrintCancel.TabIndex = 3;
            this.btnPrintCancel.Text = "Cancel";
            this.btnPrintCancel.UseVisualStyleBackColor = true;
            this.btnPrintCancel.Click += new System.EventHandler(this.btnPrintCancel_Click);
            // 
            // btnPrintContinue
            // 
            this.btnPrintContinue.Location = new System.Drawing.Point(432, 62);
            this.btnPrintContinue.Name = "btnPrintContinue";
            this.btnPrintContinue.Size = new System.Drawing.Size(97, 25);
            this.btnPrintContinue.TabIndex = 2;
            this.btnPrintContinue.Text = "Continue";
            this.btnPrintContinue.UseVisualStyleBackColor = true;
            this.btnPrintContinue.Visible = false;
            this.btnPrintContinue.Click += new System.EventHandler(this.btnPrintContinue_Click);
            // 
            // prbSend
            // 
            this.prbSend.Location = new System.Drawing.Point(139, 61);
            this.prbSend.Name = "prbSend";
            this.prbSend.Size = new System.Drawing.Size(264, 26);
            this.prbSend.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.prbSend.TabIndex = 1;
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(22, 17);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(514, 25);
            this.lblMsg.TabIndex = 0;
            this.lblMsg.Text = "Wait.... sending data to the printer...";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(845, 368);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(150, 32);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // chkOnlyUpload
            // 
            this.chkOnlyUpload.AutoSize = true;
            this.chkOnlyUpload.Location = new System.Drawing.Point(824, 329);
            this.chkOnlyUpload.Name = "chkOnlyUpload";
            this.chkOnlyUpload.Size = new System.Drawing.Size(105, 20);
            this.chkOnlyUpload.TabIndex = 19;
            this.chkOnlyUpload.Text = "Only Upload";
            this.chkOnlyUpload.UseVisualStyleBackColor = true;
            // 
            // chkSimulate
            // 
            this.chkSimulate.AutoSize = true;
            this.chkSimulate.Location = new System.Drawing.Point(824, 346);
            this.chkSimulate.Name = "chkSimulate";
            this.chkSimulate.Size = new System.Drawing.Size(179, 20);
            this.chkSimulate.TabIndex = 20;
            this.chkSimulate.Text = "Simulate (do not mark)";
            this.chkSimulate.UseVisualStyleBackColor = true;
            // 
            // tmrProgressBar
            // 
            this.tmrProgressBar.Enabled = true;
            this.tmrProgressBar.Interval = 10;
            this.tmrProgressBar.Tick += new System.EventHandler(this.tmrProgressBar_Tick);
            // 
            // pnlVariables
            // 
            this.pnlVariables.BackgroundImage = global::Nameplate.Properties.Resources.plaquetas;
            this.pnlVariables.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlVariables.Controls.Add(this.pnlIntegral);
            this.pnlVariables.Controls.Add(this.pnlTAG);
            this.pnlVariables.Controls.Add(this.pnlRemoteSensor);
            this.pnlVariables.Enabled = false;
            this.pnlVariables.Location = new System.Drawing.Point(12, 12);
            this.pnlVariables.Name = "pnlVariables";
            this.pnlVariables.Size = new System.Drawing.Size(983, 315);
            this.pnlVariables.TabIndex = 9;
            // 
            // pnlIntegral
            // 
            this.pnlIntegral.BackColor = System.Drawing.Color.Transparent;
            this.pnlIntegral.BackgroundImage = global::Nameplate.Properties.Resources.transparent;
            this.pnlIntegral.Controls.Add(this.textBox20);
            this.pnlIntegral.Controls.Add(this.textBox1);
            this.pnlIntegral.Controls.Add(this.textBox21);
            this.pnlIntegral.Controls.Add(this.textBox2);
            this.pnlIntegral.Controls.Add(this.textBox19);
            this.pnlIntegral.Controls.Add(this.textBox4);
            this.pnlIntegral.Controls.Add(this.textBox18);
            this.pnlIntegral.Controls.Add(this.textBox5);
            this.pnlIntegral.Controls.Add(this.textBox17);
            this.pnlIntegral.Controls.Add(this.textBox6);
            this.pnlIntegral.Controls.Add(this.textBox16);
            this.pnlIntegral.Controls.Add(this.textBox7);
            this.pnlIntegral.Controls.Add(this.textBox15);
            this.pnlIntegral.Controls.Add(this.textBox8);
            this.pnlIntegral.Controls.Add(this.textBox14);
            this.pnlIntegral.Controls.Add(this.textBox9);
            this.pnlIntegral.Controls.Add(this.textBox12);
            this.pnlIntegral.Controls.Add(this.textBox10);
            this.pnlIntegral.Controls.Add(this.textBox13);
            this.pnlIntegral.Controls.Add(this.textBox11);
            this.pnlIntegral.Enabled = false;
            this.pnlIntegral.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlIntegral.Location = new System.Drawing.Point(-21, 3);
            this.pnlIntegral.Name = "pnlIntegral";
            this.pnlIntegral.Size = new System.Drawing.Size(922, 149);
            this.pnlIntegral.TabIndex = 0;
            // 
            // pnlTAG
            // 
            this.pnlTAG.BackColor = System.Drawing.Color.Transparent;
            this.pnlTAG.BackgroundImage = global::Nameplate.Properties.Resources.transparent;
            this.pnlTAG.Controls.Add(this.textBox36);
            this.pnlTAG.Controls.Add(this.textBox35);
            this.pnlTAG.Location = new System.Drawing.Point(703, 158);
            this.pnlTAG.Name = "pnlTAG";
            this.pnlTAG.Size = new System.Drawing.Size(286, 100);
            this.pnlTAG.TabIndex = 10;
            // 
            // textBox36
            // 
            this.textBox36.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox36.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox36.Location = new System.Drawing.Point(64, 50);
            this.textBox36.Name = "textBox36";
            this.textBox36.Size = new System.Drawing.Size(154, 16);
            this.textBox36.TabIndex = 1;
            this.textBox36.Tag = "TAG2";
            // 
            // textBox35
            // 
            this.textBox35.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox35.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox35.Location = new System.Drawing.Point(64, 33);
            this.textBox35.Name = "textBox35";
            this.textBox35.Size = new System.Drawing.Size(154, 16);
            this.textBox35.TabIndex = 0;
            this.textBox35.Tag = "TAG1";
            // 
            // pnlRemoteSensor
            // 
            this.pnlRemoteSensor.BackColor = System.Drawing.Color.Transparent;
            this.pnlRemoteSensor.BackgroundImage = global::Nameplate.Properties.Resources.transparent;
            this.pnlRemoteSensor.Controls.Add(this.textBox38);
            this.pnlRemoteSensor.Controls.Add(this.textBox37);
            this.pnlRemoteSensor.Controls.Add(this.textBox34);
            this.pnlRemoteSensor.Controls.Add(this.textBox31);
            this.pnlRemoteSensor.Controls.Add(this.textBox33);
            this.pnlRemoteSensor.Controls.Add(this.textBox3);
            this.pnlRemoteSensor.Controls.Add(this.textBox22);
            this.pnlRemoteSensor.Controls.Add(this.textBox23);
            this.pnlRemoteSensor.Controls.Add(this.textBox24);
            this.pnlRemoteSensor.Controls.Add(this.textBox25);
            this.pnlRemoteSensor.Controls.Add(this.textBox26);
            this.pnlRemoteSensor.Controls.Add(this.textBox27);
            this.pnlRemoteSensor.Controls.Add(this.textBox28);
            this.pnlRemoteSensor.Controls.Add(this.textBox29);
            this.pnlRemoteSensor.Controls.Add(this.textBox30);
            this.pnlRemoteSensor.Enabled = false;
            this.pnlRemoteSensor.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlRemoteSensor.Location = new System.Drawing.Point(-13, 155);
            this.pnlRemoteSensor.Name = "pnlRemoteSensor";
            this.pnlRemoteSensor.Size = new System.Drawing.Size(700, 154);
            this.pnlRemoteSensor.TabIndex = 10;
            // 
            // textBox38
            // 
            this.textBox38.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox38.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox38.Location = new System.Drawing.Point(607, 43);
            this.textBox38.Name = "textBox38";
            this.textBox38.Size = new System.Drawing.Size(25, 11);
            this.textBox38.TabIndex = 12;
            this.textBox38.Tag = "SERIAL2";
            // 
            // textBox37
            // 
            this.textBox37.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox37.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox37.Location = new System.Drawing.Point(83, 111);
            this.textBox37.Name = "textBox37";
            this.textBox37.Size = new System.Drawing.Size(187, 11);
            this.textBox37.TabIndex = 5;
            this.textBox37.Tag = "YEARMONTH";
            // 
            // textBox34
            // 
            this.textBox34.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox34.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox34.Location = new System.Drawing.Point(534, 125);
            this.textBox34.Name = "textBox34";
            this.textBox34.Size = new System.Drawing.Size(68, 11);
            this.textBox34.TabIndex = 14;
            this.textBox34.Tag = "COMBNO";
            // 
            // textBox31
            // 
            this.textBox31.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox31.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox31.Location = new System.Drawing.Point(502, 82);
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new System.Drawing.Size(130, 11);
            this.textBox31.TabIndex = 13;
            this.textBox31.Tag = "TAG1";
            // 
            // textBox33
            // 
            this.textBox33.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox33.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox33.Location = new System.Drawing.Point(534, 43);
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new System.Drawing.Size(71, 11);
            this.textBox33.TabIndex = 11;
            this.textBox33.Tag = "SERIAL";
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox3.Location = new System.Drawing.Point(379, 97);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(92, 11);
            this.textBox3.TabIndex = 10;
            this.textBox3.Tag = "AMBTEMP";
            // 
            // textBox22
            // 
            this.textBox22.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox22.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox22.Location = new System.Drawing.Point(379, 83);
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new System.Drawing.Size(92, 11);
            this.textBox22.TabIndex = 9;
            this.textBox22.Tag = "FLUIDTEMP";
            // 
            // textBox23
            // 
            this.textBox23.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox23.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox23.Location = new System.Drawing.Point(379, 69);
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new System.Drawing.Size(40, 11);
            this.textBox23.TabIndex = 8;
            this.textBox23.Tag = "FLUIDPRESS";
            // 
            // textBox24
            // 
            this.textBox24.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox24.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox24.Location = new System.Drawing.Point(420, 55);
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new System.Drawing.Size(62, 11);
            this.textBox24.TabIndex = 7;
            this.textBox24.Tag = "METERFACTORH";
            // 
            // textBox25
            // 
            this.textBox25.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox25.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox25.Location = new System.Drawing.Point(420, 41);
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new System.Drawing.Size(62, 11);
            this.textBox25.TabIndex = 6;
            this.textBox25.Tag = "METERFACTORL";
            // 
            // textBox26
            // 
            this.textBox26.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox26.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox26.Location = new System.Drawing.Point(83, 97);
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new System.Drawing.Size(62, 11);
            this.textBox26.TabIndex = 4;
            this.textBox26.Tag = "SIZE";
            // 
            // textBox27
            // 
            this.textBox27.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox27.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox27.Location = new System.Drawing.Point(83, 71);
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new System.Drawing.Size(205, 11);
            this.textBox27.TabIndex = 3;
            this.textBox27.Tag = "SUFFIX2";
            // 
            // textBox28
            // 
            this.textBox28.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox28.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox28.Location = new System.Drawing.Point(83, 57);
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new System.Drawing.Size(205, 11);
            this.textBox28.TabIndex = 2;
            this.textBox28.Tag = "SUFFIX1";
            // 
            // textBox29
            // 
            this.textBox29.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox29.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox29.Location = new System.Drawing.Point(245, 43);
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new System.Drawing.Size(43, 11);
            this.textBox29.TabIndex = 1;
            this.textBox29.Tag = "STYLE";
            // 
            // textBox30
            // 
            this.textBox30.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox30.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox30.Location = new System.Drawing.Point(83, 43);
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new System.Drawing.Size(112, 11);
            this.textBox30.TabIndex = 0;
            this.textBox30.Tag = "MODEL";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox1.Location = new System.Drawing.Point(91, 38);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(112, 11);
            this.textBox1.TabIndex = 0;
            this.textBox1.Tag = "MODEL";
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox2.Location = new System.Drawing.Point(253, 38);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(43, 11);
            this.textBox2.TabIndex = 1;
            this.textBox2.Tag = "STYLE";
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox4.Location = new System.Drawing.Point(91, 52);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(205, 11);
            this.textBox4.TabIndex = 2;
            this.textBox4.Tag = "SUFFIX1";
            // 
            // textBox5
            // 
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox5.Location = new System.Drawing.Point(91, 66);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(205, 11);
            this.textBox5.TabIndex = 3;
            this.textBox5.Tag = "SUFFIX2";
            // 
            // textBox6
            // 
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox6.Location = new System.Drawing.Point(91, 94);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(62, 11);
            this.textBox6.TabIndex = 4;
            this.textBox6.Tag = "SIZE";
            // 
            // textBox7
            // 
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox7.Location = new System.Drawing.Point(435, 38);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(62, 11);
            this.textBox7.TabIndex = 5;
            this.textBox7.Tag = "METERFACTORL";
            // 
            // textBox8
            // 
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox8.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox8.Location = new System.Drawing.Point(435, 52);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(62, 11);
            this.textBox8.TabIndex = 6;
            this.textBox8.Tag = "METERFACTORH";
            // 
            // textBox9
            // 
            this.textBox9.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox9.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox9.Location = new System.Drawing.Point(394, 66);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(40, 11);
            this.textBox9.TabIndex = 7;
            this.textBox9.Tag = "FLUIDPRESS";
            // 
            // textBox10
            // 
            this.textBox10.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox10.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox10.Location = new System.Drawing.Point(394, 80);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(92, 11);
            this.textBox10.TabIndex = 8;
            this.textBox10.Tag = "FLUIDTEMP";
            // 
            // textBox11
            // 
            this.textBox11.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox11.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox11.Location = new System.Drawing.Point(394, 94);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(92, 11);
            this.textBox11.TabIndex = 9;
            this.textBox11.Tag = "AMBTEMP";
            // 
            // textBox13
            // 
            this.textBox13.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox13.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox13.Location = new System.Drawing.Point(586, 36);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(58, 11);
            this.textBox13.TabIndex = 10;
            this.textBox13.Tag = "SUPPLY1";
            // 
            // textBox12
            // 
            this.textBox12.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox12.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox12.Location = new System.Drawing.Point(519, 51);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(58, 11);
            this.textBox12.TabIndex = 11;
            this.textBox12.Tag = "SUPPLY2";
            // 
            // textBox14
            // 
            this.textBox14.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox14.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox14.Location = new System.Drawing.Point(591, 66);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(157, 11);
            this.textBox14.TabIndex = 12;
            this.textBox14.Tag = "OUTPUT1";
            // 
            // textBox15
            // 
            this.textBox15.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox15.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox15.Location = new System.Drawing.Point(519, 80);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(229, 11);
            this.textBox15.TabIndex = 13;
            this.textBox15.Tag = "OUTPUT2";
            // 
            // textBox16
            // 
            this.textBox16.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox16.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox16.Location = new System.Drawing.Point(519, 94);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(229, 11);
            this.textBox16.TabIndex = 14;
            this.textBox16.Tag = "OUTPUT3";
            // 
            // textBox17
            // 
            this.textBox17.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox17.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox17.Location = new System.Drawing.Point(519, 108);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(229, 11);
            this.textBox17.TabIndex = 15;
            this.textBox17.Tag = "OUTPUT4";
            // 
            // textBox18
            // 
            this.textBox18.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox18.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox18.Location = new System.Drawing.Point(519, 124);
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new System.Drawing.Size(229, 11);
            this.textBox18.TabIndex = 16;
            this.textBox18.Tag = "YEARMONTH";
            // 
            // textBox19
            // 
            this.textBox19.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox19.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox19.Location = new System.Drawing.Point(792, 37);
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new System.Drawing.Size(71, 11);
            this.textBox19.TabIndex = 17;
            this.textBox19.Tag = "SERIAL";
            // 
            // textBox21
            // 
            this.textBox21.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox21.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox21.Location = new System.Drawing.Point(760, 76);
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new System.Drawing.Size(130, 11);
            this.textBox21.TabIndex = 19;
            this.textBox21.Tag = "TAG1";
            // 
            // textBox20
            // 
            this.textBox20.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox20.ForeColor = System.Drawing.Color.SlateBlue;
            this.textBox20.Location = new System.Drawing.Point(865, 37);
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new System.Drawing.Size(25, 11);
            this.textBox20.TabIndex = 18;
            this.textBox20.Tag = "SERIAL2";
            // 
            // frmPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 413);
            this.Controls.Add(this.chkSimulate);
            this.Controls.Add(this.chkOnlyUpload);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.chkTAG);
            this.Controls.Add(this.chkRemoteSensor);
            this.Controls.Add(this.chkIntegral);
            this.Controls.Add(this.pnlVariables);
            this.Controls.Add(this.pnlProgressBar);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "frmPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nameplate Flowmeter V.";
            this.Activated += new System.EventHandler(this.frmPrint_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPrint_FormClosing);
            this.Load += new System.EventHandler(this.frmPrint_Load);
            this.pnlProgressBar.ResumeLayout(false);
            this.pnlVariables.ResumeLayout(false);
            this.pnlIntegral.ResumeLayout(false);
            this.pnlIntegral.PerformLayout();
            this.pnlTAG.ResumeLayout(false);
            this.pnlTAG.PerformLayout();
            this.pnlRemoteSensor.ResumeLayout(false);
            this.pnlRemoteSensor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox chkIntegral;
        private System.Windows.Forms.CheckBox chkRemoteSensor;
        private System.Windows.Forms.CheckBox chkTAG;
        private System.Windows.Forms.Panel pnlVariables;
        private System.Windows.Forms.Panel pnlIntegral;
        private System.Windows.Forms.Panel pnlRemoteSensor;
        private System.Windows.Forms.TextBox textBox34;
        private System.Windows.Forms.TextBox textBox31;
        private System.Windows.Forms.TextBox textBox33;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox22;
        private System.Windows.Forms.TextBox textBox23;
        private System.Windows.Forms.TextBox textBox24;
        private System.Windows.Forms.TextBox textBox25;
        private System.Windows.Forms.TextBox textBox26;
        private System.Windows.Forms.TextBox textBox27;
        private System.Windows.Forms.TextBox textBox28;
        private System.Windows.Forms.TextBox textBox29;
        private System.Windows.Forms.TextBox textBox30;
        private System.Windows.Forms.Panel pnlTAG;
        private System.Windows.Forms.TextBox textBox36;
        private System.Windows.Forms.TextBox textBox35;
        private System.Windows.Forms.Panel pnlProgressBar;
        private System.Windows.Forms.ProgressBar prbSend;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.TextBox textBox37;
        private System.Windows.Forms.Button btnPrintCancel;
        private System.Windows.Forms.Button btnPrintContinue;
        private System.Windows.Forms.TextBox textBox38;
        private System.Windows.Forms.CheckBox chkOnlyUpload;
        private System.Windows.Forms.CheckBox chkSimulate;
        private System.Windows.Forms.Timer tmrProgressBar;
        private System.Windows.Forms.TextBox textBox20;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox21;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox19;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox18;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox17;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox textBox11;
    }
}