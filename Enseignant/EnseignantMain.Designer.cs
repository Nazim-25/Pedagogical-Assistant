namespace Enseignant
{
    partial class EnseignantMain
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
            this.ConnectList = new System.Windows.Forms.ListBox();
            this.AllStudents_btn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.AbsStudents_btn = new System.Windows.Forms.Button();
            this.ExcludedStudents_btn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._statusTextBox = new System.Windows.Forms.TextBox();
            this.ListType = new System.Windows.Forms.Label();
            this._MessageTextBox = new System.Windows.Forms.TextBox();
            this.SendMsg_btn = new System.Windows.Forms.Button();
            this.Browser = new System.Windows.Forms.Button();
            this.SendFile_btn = new System.Windows.Forms.Button();
            this.CompteRendusManquants = new System.Windows.Forms.Button();
            this.CompteRendusRecu = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConnectList
            // 
            this.ConnectList.FormattingEnabled = true;
            this.ConnectList.Location = new System.Drawing.Point(533, 147);
            this.ConnectList.Name = "ConnectList";
            this.ConnectList.Size = new System.Drawing.Size(278, 329);
            this.ConnectList.TabIndex = 0;
            // 
            // AllStudents_btn
            // 
            this.AllStudents_btn.ForeColor = System.Drawing.Color.Black;
            this.AllStudents_btn.Location = new System.Drawing.Point(533, 15);
            this.AllStudents_btn.Name = "AllStudents_btn";
            this.AllStudents_btn.Size = new System.Drawing.Size(153, 23);
            this.AllStudents_btn.TabIndex = 1;
            this.AllStudents_btn.Text = "ConnectedStudents";
            this.AllStudents_btn.UseVisualStyleBackColor = true;
            this.AllStudents_btn.Click += new System.EventHandler(this.AllStudents_btn_Click);
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(692, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "AllStudents";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AbsStudents_btn
            // 
            this.AbsStudents_btn.ForeColor = System.Drawing.Color.Black;
            this.AbsStudents_btn.Location = new System.Drawing.Point(692, 44);
            this.AbsStudents_btn.Name = "AbsStudents_btn";
            this.AbsStudents_btn.Size = new System.Drawing.Size(126, 23);
            this.AbsStudents_btn.TabIndex = 3;
            this.AbsStudents_btn.Text = "AbsStudents";
            this.AbsStudents_btn.UseVisualStyleBackColor = true;
            this.AbsStudents_btn.Click += new System.EventHandler(this.AbsStudents_btn_Click);
            // 
            // ExcludedStudents_btn
            // 
            this.ExcludedStudents_btn.ForeColor = System.Drawing.Color.Black;
            this.ExcludedStudents_btn.Location = new System.Drawing.Point(533, 44);
            this.ExcludedStudents_btn.Name = "ExcludedStudents_btn";
            this.ExcludedStudents_btn.Size = new System.Drawing.Size(153, 23);
            this.ExcludedStudents_btn.TabIndex = 4;
            this.ExcludedStudents_btn.Text = "ExcludedStudents";
            this.ExcludedStudents_btn.UseVisualStyleBackColor = true;
            this.ExcludedStudents_btn.Click += new System.EventHandler(this.ExcludedStudents_btn_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this._statusTextBox, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 389F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 389F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 389F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(526, 462);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // _statusTextBox
            // 
            this._statusTextBox.BackColor = System.Drawing.SystemColors.Control;
            this._statusTextBox.Location = new System.Drawing.Point(3, 3);
            this._statusTextBox.Multiline = true;
            this._statusTextBox.Name = "_statusTextBox";
            this._statusTextBox.ReadOnly = true;
            this._statusTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._statusTextBox.Size = new System.Drawing.Size(520, 456);
            this._statusTextBox.TabIndex = 0;
            // 
            // ListType
            // 
            this.ListType.AutoSize = true;
            this.ListType.Location = new System.Drawing.Point(555, 119);
            this.ListType.Name = "ListType";
            this.ListType.Size = new System.Drawing.Size(0, 13);
            this.ListType.TabIndex = 8;
            // 
            // _MessageTextBox
            // 
            this._MessageTextBox.Location = new System.Drawing.Point(4, 494);
            this._MessageTextBox.Name = "_MessageTextBox";
            this._MessageTextBox.Size = new System.Drawing.Size(421, 20);
            this._MessageTextBox.TabIndex = 9;
            // 
            // SendMsg_btn
            // 
            this.SendMsg_btn.ForeColor = System.Drawing.Color.Black;
            this.SendMsg_btn.Location = new System.Drawing.Point(431, 491);
            this.SendMsg_btn.Name = "SendMsg_btn";
            this.SendMsg_btn.Size = new System.Drawing.Size(96, 23);
            this.SendMsg_btn.TabIndex = 10;
            this.SendMsg_btn.Text = "Send";
            this.SendMsg_btn.UseVisualStyleBackColor = true;
            this.SendMsg_btn.Click += new System.EventHandler(this.button3_Click);
            // 
            // Browser
            // 
            this.Browser.ForeColor = System.Drawing.Color.Black;
            this.Browser.Location = new System.Drawing.Point(533, 491);
            this.Browser.Name = "Browser";
            this.Browser.Size = new System.Drawing.Size(136, 23);
            this.Browser.TabIndex = 11;
            this.Browser.Text = "Browser";
            this.Browser.UseVisualStyleBackColor = true;
            this.Browser.Click += new System.EventHandler(this.Browser_Click);
            // 
            // SendFile_btn
            // 
            this.SendFile_btn.ForeColor = System.Drawing.Color.Black;
            this.SendFile_btn.Location = new System.Drawing.Point(682, 491);
            this.SendFile_btn.Name = "SendFile_btn";
            this.SendFile_btn.Size = new System.Drawing.Size(136, 23);
            this.SendFile_btn.TabIndex = 12;
            this.SendFile_btn.Text = "SendFile";
            this.SendFile_btn.UseVisualStyleBackColor = true;
            this.SendFile_btn.Click += new System.EventHandler(this.SendFile_btn_Click);
            // 
            // CompteRendusManquants
            // 
            this.CompteRendusManquants.ForeColor = System.Drawing.Color.Black;
            this.CompteRendusManquants.Location = new System.Drawing.Point(533, 73);
            this.CompteRendusManquants.Name = "CompteRendusManquants";
            this.CompteRendusManquants.Size = new System.Drawing.Size(153, 23);
            this.CompteRendusManquants.TabIndex = 13;
            this.CompteRendusManquants.Text = "CompteRendusManquants";
            this.CompteRendusManquants.UseVisualStyleBackColor = true;
            this.CompteRendusManquants.Click += new System.EventHandler(this.CompteRendusManquants_Click);
            // 
            // CompteRendusRecu
            // 
            this.CompteRendusRecu.ForeColor = System.Drawing.Color.Black;
            this.CompteRendusRecu.Location = new System.Drawing.Point(692, 73);
            this.CompteRendusRecu.Name = "CompteRendusRecu";
            this.CompteRendusRecu.Size = new System.Drawing.Size(126, 23);
            this.CompteRendusRecu.TabIndex = 14;
            this.CompteRendusRecu.Text = "CompteRendusRecu";
            this.CompteRendusRecu.UseVisualStyleBackColor = true;
            this.CompteRendusRecu.Click += new System.EventHandler(this.CompteRendusRecu_Click);
            // 
            // EnseignantMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 545);
            this.Controls.Add(this.CompteRendusRecu);
            this.Controls.Add(this.CompteRendusManquants);
            this.Controls.Add(this.SendFile_btn);
            this.Controls.Add(this.Browser);
            this.Controls.Add(this.SendMsg_btn);
            this.Controls.Add(this._MessageTextBox);
            this.Controls.Add(this.ListType);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.ExcludedStudents_btn);
            this.Controls.Add(this.AbsStudents_btn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.AllStudents_btn);
            this.Controls.Add(this.ConnectList);
            this.ForeColor = System.Drawing.Color.Red;
            this.Name = "EnseignantMain";
            this.Text = "EnseignantMain";
            this.Load += new System.EventHandler(this.EnseignantMain_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ConnectList;
        private System.Windows.Forms.Button AllStudents_btn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button AbsStudents_btn;
        private System.Windows.Forms.Button ExcludedStudents_btn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox _statusTextBox;
        private System.Windows.Forms.Label ListType;
        private System.Windows.Forms.TextBox _MessageTextBox;
        private System.Windows.Forms.Button SendMsg_btn;
        private System.Windows.Forms.Button Browser;
        private System.Windows.Forms.Button SendFile_btn;
        private System.Windows.Forms.Button CompteRendusManquants;
        private System.Windows.Forms.Button CompteRendusRecu;

    }
}