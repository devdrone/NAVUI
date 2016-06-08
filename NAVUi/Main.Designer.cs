namespace NAVUi
{
    partial class Main
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
            this.WebServiceCombo = new System.Windows.Forms.ComboBox();
            this.requestBox = new System.Windows.Forms.RichTextBox();
            this.responseBox = new System.Windows.Forms.RichTextBox();
            this.operationBox = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // WebServiceCombo
            // 
            this.WebServiceCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WebServiceCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WebServiceCombo.FormattingEnabled = true;
            this.WebServiceCombo.Location = new System.Drawing.Point(287, 29);
            this.WebServiceCombo.Name = "WebServiceCombo";
            this.WebServiceCombo.Size = new System.Drawing.Size(550, 21);
            this.WebServiceCombo.TabIndex = 0;
            this.WebServiceCombo.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // requestBox
            // 
            this.requestBox.Location = new System.Drawing.Point(12, 98);
            this.requestBox.Name = "requestBox";
            this.requestBox.Size = new System.Drawing.Size(519, 508);
            this.requestBox.TabIndex = 1;
            this.requestBox.Text = "";
            // 
            // responseBox
            // 
            this.responseBox.Location = new System.Drawing.Point(618, 98);
            this.responseBox.Name = "responseBox";
            this.responseBox.Size = new System.Drawing.Size(498, 508);
            this.responseBox.TabIndex = 2;
            this.responseBox.Text = "";
            // 
            // operationBox
            // 
            this.operationBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.operationBox.FormattingEnabled = true;
            this.operationBox.Location = new System.Drawing.Point(287, 56);
            this.operationBox.Name = "operationBox";
            this.operationBox.Size = new System.Drawing.Size(550, 21);
            this.operationBox.TabIndex = 3;
            this.operationBox.SelectedIndexChanged += new System.EventHandler(this.operationBox_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(537, 302);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 58);
            this.button1.TabIndex = 4;
            this.button1.Text = "=>";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(207, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Request";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(867, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Response";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1128, 618);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.operationBox);
            this.Controls.Add(this.responseBox);
            this.Controls.Add(this.requestBox);
            this.Controls.Add(this.WebServiceCombo);
            this.Name = "Main";
            this.Text = "Main";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox WebServiceCombo;
        private System.Windows.Forms.RichTextBox requestBox;
        private System.Windows.Forms.RichTextBox responseBox;
        private System.Windows.Forms.ComboBox operationBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}