namespace OptionCalculator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.pageSrc = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.tbIterations = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbDaysInYear = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbHv = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbSide = new System.Windows.Forms.ComboBox();
            this.tbPremium = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbDetrend = new System.Windows.Forms.CheckBox();
            this.btnCalc = new System.Windows.Forms.Button();
            this.tbTerm = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbVolume = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbStrike = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPrice = new System.Windows.Forms.TextBox();
            this.panelPath = new System.Windows.Forms.Panel();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btnExplore = new System.Windows.Forms.Button();
            this.tabPgTestSeries = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbTestDailyDeltaPercent = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbTestStart = new System.Windows.Forms.TextBox();
            this.tbTestTerm = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnMakeSeries = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.tabControl.SuspendLayout();
            this.pageSrc.SuspendLayout();
            this.panelPath.SuspendLayout();
            this.tabPgTestSeries.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.pageSrc);
            this.tabControl.Controls.Add(this.tabPgTestSeries);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(513, 288);
            this.tabControl.TabIndex = 0;
            // 
            // pageSrc
            // 
            this.pageSrc.Controls.Add(this.label13);
            this.pageSrc.Controls.Add(this.tbIterations);
            this.pageSrc.Controls.Add(this.label12);
            this.pageSrc.Controls.Add(this.tbDaysInYear);
            this.pageSrc.Controls.Add(this.label11);
            this.pageSrc.Controls.Add(this.tbHv);
            this.pageSrc.Controls.Add(this.label10);
            this.pageSrc.Controls.Add(this.cbSide);
            this.pageSrc.Controls.Add(this.tbPremium);
            this.pageSrc.Controls.Add(this.label5);
            this.pageSrc.Controls.Add(this.cbDetrend);
            this.pageSrc.Controls.Add(this.btnCalc);
            this.pageSrc.Controls.Add(this.tbTerm);
            this.pageSrc.Controls.Add(this.label4);
            this.pageSrc.Controls.Add(this.label3);
            this.pageSrc.Controls.Add(this.tbVolume);
            this.pageSrc.Controls.Add(this.label2);
            this.pageSrc.Controls.Add(this.tbStrike);
            this.pageSrc.Controls.Add(this.label1);
            this.pageSrc.Controls.Add(this.tbPrice);
            this.pageSrc.Controls.Add(this.panelPath);
            this.pageSrc.Location = new System.Drawing.Point(4, 22);
            this.pageSrc.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pageSrc.Name = "pageSrc";
            this.pageSrc.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pageSrc.Size = new System.Drawing.Size(505, 262);
            this.pageSrc.TabIndex = 0;
            this.pageSrc.Text = "Source";
            this.pageSrc.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(166, 72);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(49, 13);
            this.label13.TabIndex = 21;
            this.label13.Text = "iterations";
            // 
            // tbIterations
            // 
            this.tbIterations.Location = new System.Drawing.Point(218, 70);
            this.tbIterations.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbIterations.Name = "tbIterations";
            this.tbIterations.Size = new System.Drawing.Size(76, 20);
            this.tbIterations.TabIndex = 20;
            this.tbIterations.Text = "10000";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(2, 70);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(27, 13);
            this.label12.TabIndex = 19;
            this.label12.Text = "year";
            // 
            // tbDaysInYear
            // 
            this.tbDaysInYear.Location = new System.Drawing.Point(53, 67);
            this.tbDaysInYear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbDaysInYear.Name = "tbDaysInYear";
            this.tbDaysInYear.Size = new System.Drawing.Size(76, 20);
            this.tbDaysInYear.TabIndex = 18;
            this.tbDaysInYear.Text = "365";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(133, 166);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "%";
            // 
            // tbHv
            // 
            this.tbHv.Location = new System.Drawing.Point(53, 163);
            this.tbHv.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbHv.Name = "tbHv";
            this.tbHv.Size = new System.Drawing.Size(76, 20);
            this.tbHv.TabIndex = 16;
            this.tbHv.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 166);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(22, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "HV";
            // 
            // cbSide
            // 
            this.cbSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSide.FormattingEnabled = true;
            this.cbSide.Items.AddRange(new object[] {
            "PUT",
            "CALL"});
            this.cbSide.Location = new System.Drawing.Point(312, 43);
            this.cbSide.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbSide.Name = "cbSide";
            this.cbSide.Size = new System.Drawing.Size(80, 21);
            this.cbSide.TabIndex = 14;
            // 
            // tbPremium
            // 
            this.tbPremium.Location = new System.Drawing.Point(53, 141);
            this.tbPremium.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbPremium.Name = "tbPremium";
            this.tbPremium.Size = new System.Drawing.Size(76, 20);
            this.tbPremium.TabIndex = 13;
            this.tbPremium.Text = "1.0000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 143);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "premium";
            // 
            // cbDetrend
            // 
            this.cbDetrend.AutoSize = true;
            this.cbDetrend.Location = new System.Drawing.Point(312, 24);
            this.cbDetrend.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbDetrend.Name = "cbDetrend";
            this.cbDetrend.Size = new System.Drawing.Size(62, 17);
            this.cbDetrend.TabIndex = 11;
            this.cbDetrend.Text = "detrend";
            this.cbDetrend.UseVisualStyleBackColor = true;
            // 
            // btnCalc
            // 
            this.btnCalc.Location = new System.Drawing.Point(4, 101);
            this.btnCalc.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(124, 25);
            this.btnCalc.TabIndex = 10;
            this.btnCalc.Text = "Calculate premium";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // tbTerm
            // 
            this.tbTerm.Location = new System.Drawing.Point(218, 22);
            this.tbTerm.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbTerm.Name = "tbTerm";
            this.tbTerm.Size = new System.Drawing.Size(76, 20);
            this.tbTerm.TabIndex = 9;
            this.tbTerm.Text = "30.00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(151, 24);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "term, days";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(166, 47);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "volume";
            // 
            // tbVolume
            // 
            this.tbVolume.Location = new System.Drawing.Point(218, 45);
            this.tbVolume.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbVolume.Name = "tbVolume";
            this.tbVolume.Size = new System.Drawing.Size(76, 20);
            this.tbVolume.TabIndex = 6;
            this.tbVolume.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 47);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "strike";
            // 
            // tbStrike
            // 
            this.tbStrike.Location = new System.Drawing.Point(53, 45);
            this.tbStrike.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbStrike.Name = "tbStrike";
            this.tbStrike.Size = new System.Drawing.Size(76, 20);
            this.tbStrike.TabIndex = 4;
            this.tbStrike.Text = "1000.0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "current";
            // 
            // tbPrice
            // 
            this.tbPrice.Location = new System.Drawing.Point(53, 22);
            this.tbPrice.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbPrice.Name = "tbPrice";
            this.tbPrice.Size = new System.Drawing.Size(76, 20);
            this.tbPrice.TabIndex = 2;
            this.tbPrice.Text = "1000.0";
            // 
            // panelPath
            // 
            this.panelPath.Controls.Add(this.tbPath);
            this.panelPath.Controls.Add(this.btnExplore);
            this.panelPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPath.Location = new System.Drawing.Point(2, 2);
            this.panelPath.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelPath.Name = "panelPath";
            this.panelPath.Size = new System.Drawing.Size(501, 20);
            this.panelPath.TabIndex = 1;
            // 
            // tbPath
            // 
            this.tbPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPath.Location = new System.Drawing.Point(0, 0);
            this.tbPath.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(463, 20);
            this.tbPath.TabIndex = 1;
            // 
            // btnExplore
            // 
            this.btnExplore.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExplore.Location = new System.Drawing.Point(463, 0);
            this.btnExplore.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExplore.Name = "btnExplore";
            this.btnExplore.Size = new System.Drawing.Size(38, 20);
            this.btnExplore.TabIndex = 0;
            this.btnExplore.Text = "...";
            this.btnExplore.UseVisualStyleBackColor = true;
            this.btnExplore.Click += new System.EventHandler(this.btnExplore_Click);
            // 
            // tabPgTestSeries
            // 
            this.tabPgTestSeries.Controls.Add(this.label9);
            this.tabPgTestSeries.Controls.Add(this.label8);
            this.tabPgTestSeries.Controls.Add(this.tbTestDailyDeltaPercent);
            this.tabPgTestSeries.Controls.Add(this.label7);
            this.tabPgTestSeries.Controls.Add(this.tbTestStart);
            this.tabPgTestSeries.Controls.Add(this.tbTestTerm);
            this.tabPgTestSeries.Controls.Add(this.label6);
            this.tabPgTestSeries.Controls.Add(this.btnMakeSeries);
            this.tabPgTestSeries.Location = new System.Drawing.Point(4, 22);
            this.tabPgTestSeries.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPgTestSeries.Name = "tabPgTestSeries";
            this.tabPgTestSeries.Size = new System.Drawing.Size(505, 262);
            this.tabPgTestSeries.TabIndex = 2;
            this.tabPgTestSeries.Text = "Test Series";
            this.tabPgTestSeries.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(153, 59);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "%";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 59);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "daily delta";
            // 
            // tbTestDailyDeltaPercent
            // 
            this.tbTestDailyDeltaPercent.Location = new System.Drawing.Point(74, 57);
            this.tbTestDailyDeltaPercent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbTestDailyDeltaPercent.Name = "tbTestDailyDeltaPercent";
            this.tbTestDailyDeltaPercent.Size = new System.Drawing.Size(76, 20);
            this.tbTestDailyDeltaPercent.TabIndex = 16;
            this.tbTestDailyDeltaPercent.Text = "0.5";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 37);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "initial";
            // 
            // tbTestStart
            // 
            this.tbTestStart.Location = new System.Drawing.Point(74, 34);
            this.tbTestStart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbTestStart.Name = "tbTestStart";
            this.tbTestStart.Size = new System.Drawing.Size(76, 20);
            this.tbTestStart.TabIndex = 14;
            this.tbTestStart.Text = "1000.0";
            // 
            // tbTestTerm
            // 
            this.tbTestTerm.Location = new System.Drawing.Point(74, 11);
            this.tbTestTerm.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbTestTerm.Name = "tbTestTerm";
            this.tbTestTerm.Size = new System.Drawing.Size(76, 20);
            this.tbTestTerm.TabIndex = 13;
            this.tbTestTerm.Text = "730";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 14);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "term, days";
            // 
            // btnMakeSeries
            // 
            this.btnMakeSeries.Location = new System.Drawing.Point(8, 98);
            this.btnMakeSeries.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnMakeSeries.Name = "btnMakeSeries";
            this.btnMakeSeries.Size = new System.Drawing.Size(140, 25);
            this.btnMakeSeries.TabIndex = 11;
            this.btnMakeSeries.Text = "Make Series";
            this.btnMakeSeries.UseVisualStyleBackColor = true;
            this.btnMakeSeries.Click += new System.EventHandler(this.btnMakeSeries_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "*.csv;*.txt|*.txt;*.csv|*.*|*.*";
            this.openFileDialog.FilterIndex = 0;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "csv";
            this.saveFileDialog.Filter = "*.csv;*.txt|*.txt;*.csv|*.*|*.*";
            this.saveFileDialog.FilterIndex = 0;
            this.saveFileDialog.Title = "Save Time Series";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 288);
            this.Controls.Add(this.tabControl);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainForm";
            this.Text = "Option Calculator";
            this.tabControl.ResumeLayout(false);
            this.pageSrc.ResumeLayout(false);
            this.pageSrc.PerformLayout();
            this.panelPath.ResumeLayout(false);
            this.panelPath.PerformLayout();
            this.tabPgTestSeries.ResumeLayout(false);
            this.tabPgTestSeries.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage pageSrc;
        private System.Windows.Forms.Panel panelPath;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Button btnExplore;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbStrike;
        private System.Windows.Forms.TextBox tbTerm;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbVolume;
        private System.Windows.Forms.TextBox tbPremium;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbDetrend;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ComboBox cbSide;
        private System.Windows.Forms.TabPage tabPgTestSeries;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbTestDailyDeltaPercent;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbTestStart;
        private System.Windows.Forms.TextBox tbTestTerm;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnMakeSeries;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbHv;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbIterations;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbDaysInYear;
    }
}

