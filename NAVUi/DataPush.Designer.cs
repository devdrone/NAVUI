namespace NAVUi
{
    partial class DataPush
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
            this.processes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.path = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pushNav = new System.Windows.Forms.Button();
            this.xmlDisplayer = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // processes
            // 
            this.processes.AllowDrop = true;
            this.processes.FormattingEnabled = true;
            this.processes.Items.AddRange(new object[] {
            "Customer",
            "Simple Item",
            "Non Simple Item",
            "Sales Order",
            "Sales Invoice",
            "Sales Shipment"});
            this.processes.Location = new System.Drawing.Point(96, 28);
            this.processes.Name = "processes";
            this.processes.Size = new System.Drawing.Size(121, 21);
            this.processes.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Process";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Select File";
            // 
            // path
            // 
            this.path.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.path.Location = new System.Drawing.Point(96, 63);
            this.path.Name = "path";
            this.path.ReadOnly = true;
            this.path.Size = new System.Drawing.Size(449, 20);
            this.path.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(470, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 20);
            this.button1.TabIndex = 4;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pushNav
            // 
            this.pushNav.Location = new System.Drawing.Point(230, 104);
            this.pushNav.Name = "pushNav";
            this.pushNav.Size = new System.Drawing.Size(115, 23);
            this.pushNav.TabIndex = 5;
            this.pushNav.Text = "Push To Navision";
            this.pushNav.UseVisualStyleBackColor = true;
            this.pushNav.Click += new System.EventHandler(this.pushNav_Click);
            // 
            // xmlDisplayer
            // 
            this.xmlDisplayer.Location = new System.Drawing.Point(15, 133);
            this.xmlDisplayer.Name = "xmlDisplayer";
            this.xmlDisplayer.Size = new System.Drawing.Size(530, 223);
            this.xmlDisplayer.TabIndex = 6;
            this.xmlDisplayer.Text = "";
            this.xmlDisplayer.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(15, 104);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Reload XML";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(393, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "label3";
            this.label3.Visible = false;
            // 
            // DataPush
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 368);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.xmlDisplayer);
            this.Controls.Add(this.pushNav);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.path);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.processes);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataPush";
            this.Text = "DataPush";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox processes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox path;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button pushNav;
        private System.Windows.Forms.RichTextBox xmlDisplayer;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
    }
}