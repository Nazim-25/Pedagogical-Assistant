namespace Etudiant
{
    partial class EtudiantMain
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
            this.ConversationBox = new System.Windows.Forms.TextBox();
            this.MessageBox_inp = new System.Windows.Forms.TextBox();
            this.Send_btn = new System.Windows.Forms.Button();
            this.Browser_btn = new System.Windows.Forms.Button();
            this.SendFile_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ConversationBox
            // 
            this.ConversationBox.BackColor = System.Drawing.SystemColors.Window;
            this.ConversationBox.Location = new System.Drawing.Point(0, 16);
            this.ConversationBox.Multiline = true;
            this.ConversationBox.Name = "ConversationBox";
            this.ConversationBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ConversationBox.Size = new System.Drawing.Size(625, 329);
            this.ConversationBox.TabIndex = 0;
            // 
            // MessageBox_inp
            // 
            this.MessageBox_inp.Location = new System.Drawing.Point(0, 351);
            this.MessageBox_inp.Multiline = true;
            this.MessageBox_inp.Name = "MessageBox_inp";
            this.MessageBox_inp.Size = new System.Drawing.Size(520, 33);
            this.MessageBox_inp.TabIndex = 1;
            // 
            // Send_btn
            // 
            this.Send_btn.Location = new System.Drawing.Point(526, 351);
            this.Send_btn.Name = "Send_btn";
            this.Send_btn.Size = new System.Drawing.Size(99, 33);
            this.Send_btn.TabIndex = 2;
            this.Send_btn.Text = "Send";
            this.Send_btn.UseVisualStyleBackColor = true;
            this.Send_btn.Click += new System.EventHandler(this.Send_btn_Click);
            // 
            // Browser_btn
            // 
            this.Browser_btn.Location = new System.Drawing.Point(631, 16);
            this.Browser_btn.Name = "Browser_btn";
            this.Browser_btn.Size = new System.Drawing.Size(75, 23);
            this.Browser_btn.TabIndex = 3;
            this.Browser_btn.Text = "Browser";
            this.Browser_btn.UseVisualStyleBackColor = true;
            this.Browser_btn.Click += new System.EventHandler(this.Browser_btn_Click);
            // 
            // SendFile_btn
            // 
            this.SendFile_btn.Location = new System.Drawing.Point(631, 64);
            this.SendFile_btn.Name = "SendFile_btn";
            this.SendFile_btn.Size = new System.Drawing.Size(75, 23);
            this.SendFile_btn.TabIndex = 4;
            this.SendFile_btn.Text = "Send_File";
            this.SendFile_btn.UseVisualStyleBackColor = true;
            this.SendFile_btn.Click += new System.EventHandler(this.SendFile_btn_Click);
            // 
            // EtudiantMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 408);
            this.Controls.Add(this.SendFile_btn);
            this.Controls.Add(this.Browser_btn);
            this.Controls.Add(this.Send_btn);
            this.Controls.Add(this.MessageBox_inp);
            this.Controls.Add(this.ConversationBox);
            this.Name = "EtudiantMain";
            this.Text = "EtudiantMain";
            this.Load += new System.EventHandler(this.EtudiantMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ConversationBox;
        private System.Windows.Forms.TextBox MessageBox_inp;
        private System.Windows.Forms.Button Send_btn;
        private System.Windows.Forms.Button Browser_btn;
        private System.Windows.Forms.Button SendFile_btn;
    }
}