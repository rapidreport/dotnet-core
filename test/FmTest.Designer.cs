namespace test
{
    partial class FmTest
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
            this.CmbTest = new System.Windows.Forms.ComboBox();
            this.BtnRun = new System.Windows.Forms.Button();
            this.BtnOpenOut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CmbTest
            // 
            this.CmbTest.FormattingEnabled = true;
            this.CmbTest.Location = new System.Drawing.Point(12, 12);
            this.CmbTest.Name = "CmbTest";
            this.CmbTest.Size = new System.Drawing.Size(260, 23);
            this.CmbTest.TabIndex = 0;
            // 
            // BtnRun
            // 
            this.BtnRun.Location = new System.Drawing.Point(147, 41);
            this.BtnRun.Name = "BtnRun";
            this.BtnRun.Size = new System.Drawing.Size(125, 23);
            this.BtnRun.TabIndex = 1;
            this.BtnRun.Text = "実行";
            this.BtnRun.UseVisualStyleBackColor = true;
            this.BtnRun.Click += new System.EventHandler(this.BtnRun_Click);
            // 
            // BtnOpenOut
            // 
            this.BtnOpenOut.Location = new System.Drawing.Point(147, 70);
            this.BtnOpenOut.Name = "BtnOpenOut";
            this.BtnOpenOut.Size = new System.Drawing.Size(125, 23);
            this.BtnOpenOut.TabIndex = 2;
            this.BtnOpenOut.Text = "出力フォルダを開く";
            this.BtnOpenOut.UseVisualStyleBackColor = true;
            this.BtnOpenOut.Click += new System.EventHandler(this.BtnOpenOut_Click);
            // 
            // FmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 97);
            this.Controls.Add(this.BtnOpenOut);
            this.Controls.Add(this.BtnRun);
            this.Controls.Add(this.CmbTest);
            this.Name = "FmTest";
            this.Text = "test";
            this.Load += new System.EventHandler(this.FmTest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox CmbTest;
        private System.Windows.Forms.Button BtnRun;
        private System.Windows.Forms.Button BtnOpenOut;
    }
}