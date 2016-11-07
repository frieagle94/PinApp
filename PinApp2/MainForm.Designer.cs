namespace PinApp
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toCloseButton = new System.Windows.Forms.Button();
            this.messageTextBox = new System.Windows.Forms.RichTextBox();
            this.tabS1 = new System.Windows.Forms.TabPage();
            this.zedGraphControl2_1 = new ZedGraph.ZedGraphControl();
            this.zedGraphControl0_1 = new ZedGraph.ZedGraphControl();
            this.zedGraphControl1_1 = new ZedGraph.ZedGraphControl();
            this.tabS0 = new System.Windows.Forms.TabPage();
            this.zedGraphControl2_0 = new ZedGraph.ZedGraphControl();
            this.zedGraphControl0_0 = new ZedGraph.ZedGraphControl();
            this.zedGraphControl1_0 = new ZedGraph.ZedGraphControl();
            this.tabSensori = new System.Windows.Forms.TabControl();
            this.tabS2 = new System.Windows.Forms.TabPage();
            this.zedGraphControl2_2 = new ZedGraph.ZedGraphControl();
            this.zedGraphControl0_2 = new ZedGraph.ZedGraphControl();
            this.zedGraphControl1_2 = new ZedGraph.ZedGraphControl();
            this.tabS3 = new System.Windows.Forms.TabPage();
            this.zedGraphControl2_3 = new ZedGraph.ZedGraphControl();
            this.zedGraphControl0_3 = new ZedGraph.ZedGraphControl();
            this.zedGraphControl1_3 = new ZedGraph.ZedGraphControl();
            this.tabS4 = new System.Windows.Forms.TabPage();
            this.zedGraphControl2_4 = new ZedGraph.ZedGraphControl();
            this.zedGraphControl0_4 = new ZedGraph.ZedGraphControl();
            this.zedGraphControl1_4 = new ZedGraph.ZedGraphControl();
            this.zedGraphControlDeadReckoning = new ZedGraph.ZedGraphControl();
            this.tabS1.SuspendLayout();
            this.tabS0.SuspendLayout();
            this.tabSensori.SuspendLayout();
            this.tabS2.SuspendLayout();
            this.tabS3.SuspendLayout();
            this.tabS4.SuspendLayout();
            this.SuspendLayout();
            // 
            // toCloseButton
            // 
            this.toCloseButton.Location = new System.Drawing.Point(275, 652);
            this.toCloseButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.toCloseButton.Name = "toCloseButton";
            this.toCloseButton.Size = new System.Drawing.Size(126, 35);
            this.toCloseButton.TabIndex = 1;
            this.toCloseButton.Text = "Chiudi";
            this.toCloseButton.UseVisualStyleBackColor = true;
            this.toCloseButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // messageTextBox
            // 
            this.messageTextBox.Location = new System.Drawing.Point(-3, 332);
            this.messageTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.ReadOnly = true;
            this.messageTextBox.Size = new System.Drawing.Size(662, 301);
            this.messageTextBox.TabIndex = 8;
            this.messageTextBox.Text = "";
            // 
            // tabS1
            // 
            this.tabS1.Controls.Add(this.zedGraphControl2_1);
            this.tabS1.Controls.Add(this.zedGraphControl0_1);
            this.tabS1.Controls.Add(this.zedGraphControl1_1);
            this.tabS1.Location = new System.Drawing.Point(4, 29);
            this.tabS1.Name = "tabS1";
            this.tabS1.Padding = new System.Windows.Forms.Padding(3);
            this.tabS1.Size = new System.Drawing.Size(1268, 297);
            this.tabS1.TabIndex = 1;
            this.tabS1.Text = "Sensore 1";
            this.tabS1.UseVisualStyleBackColor = true;
            // 
            // zedGraphControl2_1
            // 
            this.zedGraphControl2_1.Location = new System.Drawing.Point(853, 11);
            this.zedGraphControl2_1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.zedGraphControl2_1.Name = "zedGraphControl2_1";
            this.zedGraphControl2_1.ScrollGrace = 0D;
            this.zedGraphControl2_1.ScrollMaxX = 0D;
            this.zedGraphControl2_1.ScrollMaxY = 0D;
            this.zedGraphControl2_1.ScrollMaxY2 = 0D;
            this.zedGraphControl2_1.ScrollMinX = 0D;
            this.zedGraphControl2_1.ScrollMinY = 0D;
            this.zedGraphControl2_1.ScrollMinY2 = 0D;
            this.zedGraphControl2_1.Size = new System.Drawing.Size(406, 278);
            this.zedGraphControl2_1.TabIndex = 11;
            // 
            // zedGraphControl0_1
            // 
            this.zedGraphControl0_1.Location = new System.Drawing.Point(9, 11);
            this.zedGraphControl0_1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.zedGraphControl0_1.Name = "zedGraphControl0_1";
            this.zedGraphControl0_1.ScrollGrace = 0D;
            this.zedGraphControl0_1.ScrollMaxX = 0D;
            this.zedGraphControl0_1.ScrollMaxY = 0D;
            this.zedGraphControl0_1.ScrollMaxY2 = 0D;
            this.zedGraphControl0_1.ScrollMinX = 0D;
            this.zedGraphControl0_1.ScrollMinY = 0D;
            this.zedGraphControl0_1.ScrollMinY2 = 0D;
            this.zedGraphControl0_1.Size = new System.Drawing.Size(406, 278);
            this.zedGraphControl0_1.TabIndex = 9;
            // 
            // zedGraphControl1_1
            // 
            this.zedGraphControl1_1.Location = new System.Drawing.Point(435, 11);
            this.zedGraphControl1_1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.zedGraphControl1_1.Name = "zedGraphControl1_1";
            this.zedGraphControl1_1.ScrollGrace = 0D;
            this.zedGraphControl1_1.ScrollMaxX = 0D;
            this.zedGraphControl1_1.ScrollMaxY = 0D;
            this.zedGraphControl1_1.ScrollMaxY2 = 0D;
            this.zedGraphControl1_1.ScrollMinX = 0D;
            this.zedGraphControl1_1.ScrollMinY = 0D;
            this.zedGraphControl1_1.ScrollMinY2 = 0D;
            this.zedGraphControl1_1.Size = new System.Drawing.Size(406, 278);
            this.zedGraphControl1_1.TabIndex = 10;
            // 
            // tabS0
            // 
            this.tabS0.Controls.Add(this.zedGraphControl2_0);
            this.tabS0.Controls.Add(this.zedGraphControl0_0);
            this.tabS0.Controls.Add(this.zedGraphControl1_0);
            this.tabS0.Location = new System.Drawing.Point(4, 29);
            this.tabS0.Name = "tabS0";
            this.tabS0.Padding = new System.Windows.Forms.Padding(3);
            this.tabS0.Size = new System.Drawing.Size(1268, 297);
            this.tabS0.TabIndex = 0;
            this.tabS0.Text = "Sensore 0";
            this.tabS0.UseVisualStyleBackColor = true;
            // 
            // zedGraphControl2_0
            // 
            this.zedGraphControl2_0.Location = new System.Drawing.Point(853, 11);
            this.zedGraphControl2_0.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.zedGraphControl2_0.Name = "zedGraphControl2_0";
            this.zedGraphControl2_0.ScrollGrace = 0D;
            this.zedGraphControl2_0.ScrollMaxX = 0D;
            this.zedGraphControl2_0.ScrollMaxY = 0D;
            this.zedGraphControl2_0.ScrollMaxY2 = 0D;
            this.zedGraphControl2_0.ScrollMinX = 0D;
            this.zedGraphControl2_0.ScrollMinY = 0D;
            this.zedGraphControl2_0.ScrollMinY2 = 0D;
            this.zedGraphControl2_0.Size = new System.Drawing.Size(406, 278);
            this.zedGraphControl2_0.TabIndex = 8;
            // 
            // zedGraphControl0_0
            // 
            this.zedGraphControl0_0.Location = new System.Drawing.Point(9, 11);
            this.zedGraphControl0_0.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.zedGraphControl0_0.Name = "zedGraphControl0_0";
            this.zedGraphControl0_0.ScrollGrace = 0D;
            this.zedGraphControl0_0.ScrollMaxX = 0D;
            this.zedGraphControl0_0.ScrollMaxY = 0D;
            this.zedGraphControl0_0.ScrollMaxY2 = 0D;
            this.zedGraphControl0_0.ScrollMinX = 0D;
            this.zedGraphControl0_0.ScrollMinY = 0D;
            this.zedGraphControl0_0.ScrollMinY2 = 0D;
            this.zedGraphControl0_0.Size = new System.Drawing.Size(406, 278);
            this.zedGraphControl0_0.TabIndex = 3;
            // 
            // zedGraphControl1_0
            // 
            this.zedGraphControl1_0.Location = new System.Drawing.Point(435, 11);
            this.zedGraphControl1_0.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.zedGraphControl1_0.Name = "zedGraphControl1_0";
            this.zedGraphControl1_0.ScrollGrace = 0D;
            this.zedGraphControl1_0.ScrollMaxX = 0D;
            this.zedGraphControl1_0.ScrollMaxY = 0D;
            this.zedGraphControl1_0.ScrollMaxY2 = 0D;
            this.zedGraphControl1_0.ScrollMinX = 0D;
            this.zedGraphControl1_0.ScrollMinY = 0D;
            this.zedGraphControl1_0.ScrollMinY2 = 0D;
            this.zedGraphControl1_0.Size = new System.Drawing.Size(406, 278);
            this.zedGraphControl1_0.TabIndex = 6;
            // 
            // tabSensori
            // 
            this.tabSensori.Controls.Add(this.tabS0);
            this.tabSensori.Controls.Add(this.tabS1);
            this.tabSensori.Controls.Add(this.tabS2);
            this.tabSensori.Controls.Add(this.tabS3);
            this.tabSensori.Controls.Add(this.tabS4);
            this.tabSensori.Location = new System.Drawing.Point(3, 1);
            this.tabSensori.Name = "tabSensori";
            this.tabSensori.SelectedIndex = 0;
            this.tabSensori.Size = new System.Drawing.Size(1276, 330);
            this.tabSensori.TabIndex = 9;
            // 
            // tabS2
            // 
            this.tabS2.Controls.Add(this.zedGraphControl2_2);
            this.tabS2.Controls.Add(this.zedGraphControl0_2);
            this.tabS2.Controls.Add(this.zedGraphControl1_2);
            this.tabS2.Location = new System.Drawing.Point(4, 29);
            this.tabS2.Name = "tabS2";
            this.tabS2.Padding = new System.Windows.Forms.Padding(3);
            this.tabS2.Size = new System.Drawing.Size(1268, 297);
            this.tabS2.TabIndex = 2;
            this.tabS2.Text = "Sensore 2";
            this.tabS2.UseVisualStyleBackColor = true;
            // 
            // zedGraphControl2_2
            // 
            this.zedGraphControl2_2.Location = new System.Drawing.Point(853, 11);
            this.zedGraphControl2_2.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.zedGraphControl2_2.Name = "zedGraphControl2_2";
            this.zedGraphControl2_2.ScrollGrace = 0D;
            this.zedGraphControl2_2.ScrollMaxX = 0D;
            this.zedGraphControl2_2.ScrollMaxY = 0D;
            this.zedGraphControl2_2.ScrollMaxY2 = 0D;
            this.zedGraphControl2_2.ScrollMinX = 0D;
            this.zedGraphControl2_2.ScrollMinY = 0D;
            this.zedGraphControl2_2.ScrollMinY2 = 0D;
            this.zedGraphControl2_2.Size = new System.Drawing.Size(406, 278);
            this.zedGraphControl2_2.TabIndex = 11;
            // 
            // zedGraphControl0_2
            // 
            this.zedGraphControl0_2.Location = new System.Drawing.Point(9, 11);
            this.zedGraphControl0_2.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.zedGraphControl0_2.Name = "zedGraphControl0_2";
            this.zedGraphControl0_2.ScrollGrace = 0D;
            this.zedGraphControl0_2.ScrollMaxX = 0D;
            this.zedGraphControl0_2.ScrollMaxY = 0D;
            this.zedGraphControl0_2.ScrollMaxY2 = 0D;
            this.zedGraphControl0_2.ScrollMinX = 0D;
            this.zedGraphControl0_2.ScrollMinY = 0D;
            this.zedGraphControl0_2.ScrollMinY2 = 0D;
            this.zedGraphControl0_2.Size = new System.Drawing.Size(406, 278);
            this.zedGraphControl0_2.TabIndex = 9;
            // 
            // zedGraphControl1_2
            // 
            this.zedGraphControl1_2.Location = new System.Drawing.Point(435, 11);
            this.zedGraphControl1_2.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.zedGraphControl1_2.Name = "zedGraphControl1_2";
            this.zedGraphControl1_2.ScrollGrace = 0D;
            this.zedGraphControl1_2.ScrollMaxX = 0D;
            this.zedGraphControl1_2.ScrollMaxY = 0D;
            this.zedGraphControl1_2.ScrollMaxY2 = 0D;
            this.zedGraphControl1_2.ScrollMinX = 0D;
            this.zedGraphControl1_2.ScrollMinY = 0D;
            this.zedGraphControl1_2.ScrollMinY2 = 0D;
            this.zedGraphControl1_2.Size = new System.Drawing.Size(406, 278);
            this.zedGraphControl1_2.TabIndex = 10;
            // 
            // tabS3
            // 
            this.tabS3.Controls.Add(this.zedGraphControl2_3);
            this.tabS3.Controls.Add(this.zedGraphControl0_3);
            this.tabS3.Controls.Add(this.zedGraphControl1_3);
            this.tabS3.Location = new System.Drawing.Point(4, 29);
            this.tabS3.Name = "tabS3";
            this.tabS3.Padding = new System.Windows.Forms.Padding(3);
            this.tabS3.Size = new System.Drawing.Size(1268, 297);
            this.tabS3.TabIndex = 3;
            this.tabS3.Text = "Sensore 3";
            this.tabS3.UseVisualStyleBackColor = true;
            // 
            // zedGraphControl2_3
            // 
            this.zedGraphControl2_3.Location = new System.Drawing.Point(853, 11);
            this.zedGraphControl2_3.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.zedGraphControl2_3.Name = "zedGraphControl2_3";
            this.zedGraphControl2_3.ScrollGrace = 0D;
            this.zedGraphControl2_3.ScrollMaxX = 0D;
            this.zedGraphControl2_3.ScrollMaxY = 0D;
            this.zedGraphControl2_3.ScrollMaxY2 = 0D;
            this.zedGraphControl2_3.ScrollMinX = 0D;
            this.zedGraphControl2_3.ScrollMinY = 0D;
            this.zedGraphControl2_3.ScrollMinY2 = 0D;
            this.zedGraphControl2_3.Size = new System.Drawing.Size(406, 278);
            this.zedGraphControl2_3.TabIndex = 11;
            // 
            // zedGraphControl0_3
            // 
            this.zedGraphControl0_3.Location = new System.Drawing.Point(9, 11);
            this.zedGraphControl0_3.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.zedGraphControl0_3.Name = "zedGraphControl0_3";
            this.zedGraphControl0_3.ScrollGrace = 0D;
            this.zedGraphControl0_3.ScrollMaxX = 0D;
            this.zedGraphControl0_3.ScrollMaxY = 0D;
            this.zedGraphControl0_3.ScrollMaxY2 = 0D;
            this.zedGraphControl0_3.ScrollMinX = 0D;
            this.zedGraphControl0_3.ScrollMinY = 0D;
            this.zedGraphControl0_3.ScrollMinY2 = 0D;
            this.zedGraphControl0_3.Size = new System.Drawing.Size(406, 278);
            this.zedGraphControl0_3.TabIndex = 9;
            // 
            // zedGraphControl1_3
            // 
            this.zedGraphControl1_3.Location = new System.Drawing.Point(435, 11);
            this.zedGraphControl1_3.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.zedGraphControl1_3.Name = "zedGraphControl1_3";
            this.zedGraphControl1_3.ScrollGrace = 0D;
            this.zedGraphControl1_3.ScrollMaxX = 0D;
            this.zedGraphControl1_3.ScrollMaxY = 0D;
            this.zedGraphControl1_3.ScrollMaxY2 = 0D;
            this.zedGraphControl1_3.ScrollMinX = 0D;
            this.zedGraphControl1_3.ScrollMinY = 0D;
            this.zedGraphControl1_3.ScrollMinY2 = 0D;
            this.zedGraphControl1_3.Size = new System.Drawing.Size(406, 278);
            this.zedGraphControl1_3.TabIndex = 10;
            // 
            // tabS4
            // 
            this.tabS4.Controls.Add(this.zedGraphControl2_4);
            this.tabS4.Controls.Add(this.zedGraphControl0_4);
            this.tabS4.Controls.Add(this.zedGraphControl1_4);
            this.tabS4.Location = new System.Drawing.Point(4, 29);
            this.tabS4.Name = "tabS4";
            this.tabS4.Padding = new System.Windows.Forms.Padding(3);
            this.tabS4.Size = new System.Drawing.Size(1268, 297);
            this.tabS4.TabIndex = 4;
            this.tabS4.Text = "Sensore 4";
            this.tabS4.UseVisualStyleBackColor = true;
            // 
            // zedGraphControl2_4
            // 
            this.zedGraphControl2_4.Location = new System.Drawing.Point(853, 11);
            this.zedGraphControl2_4.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.zedGraphControl2_4.Name = "zedGraphControl2_4";
            this.zedGraphControl2_4.ScrollGrace = 0D;
            this.zedGraphControl2_4.ScrollMaxX = 0D;
            this.zedGraphControl2_4.ScrollMaxY = 0D;
            this.zedGraphControl2_4.ScrollMaxY2 = 0D;
            this.zedGraphControl2_4.ScrollMinX = 0D;
            this.zedGraphControl2_4.ScrollMinY = 0D;
            this.zedGraphControl2_4.ScrollMinY2 = 0D;
            this.zedGraphControl2_4.Size = new System.Drawing.Size(406, 278);
            this.zedGraphControl2_4.TabIndex = 11;
            // 
            // zedGraphControl0_4
            // 
            this.zedGraphControl0_4.Location = new System.Drawing.Point(9, 11);
            this.zedGraphControl0_4.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.zedGraphControl0_4.Name = "zedGraphControl0_4";
            this.zedGraphControl0_4.ScrollGrace = 0D;
            this.zedGraphControl0_4.ScrollMaxX = 0D;
            this.zedGraphControl0_4.ScrollMaxY = 0D;
            this.zedGraphControl0_4.ScrollMaxY2 = 0D;
            this.zedGraphControl0_4.ScrollMinX = 0D;
            this.zedGraphControl0_4.ScrollMinY = 0D;
            this.zedGraphControl0_4.ScrollMinY2 = 0D;
            this.zedGraphControl0_4.Size = new System.Drawing.Size(406, 278);
            this.zedGraphControl0_4.TabIndex = 9;
            // 
            // zedGraphControl1_4
            // 
            this.zedGraphControl1_4.Location = new System.Drawing.Point(435, 11);
            this.zedGraphControl1_4.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.zedGraphControl1_4.Name = "zedGraphControl1_4";
            this.zedGraphControl1_4.ScrollGrace = 0D;
            this.zedGraphControl1_4.ScrollMaxX = 0D;
            this.zedGraphControl1_4.ScrollMaxY = 0D;
            this.zedGraphControl1_4.ScrollMaxY2 = 0D;
            this.zedGraphControl1_4.ScrollMinX = 0D;
            this.zedGraphControl1_4.ScrollMinY = 0D;
            this.zedGraphControl1_4.ScrollMinY2 = 0D;
            this.zedGraphControl1_4.Size = new System.Drawing.Size(406, 278);
            this.zedGraphControl1_4.TabIndex = 10;
            // 
            // zedGraphControlDeadReckoning
            // 
            this.zedGraphControlDeadReckoning.Location = new System.Drawing.Point(661, 332);
            this.zedGraphControlDeadReckoning.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.zedGraphControlDeadReckoning.Name = "zedGraphControlDeadReckoning";
            this.zedGraphControlDeadReckoning.ScrollGrace = 0D;
            this.zedGraphControlDeadReckoning.ScrollMaxX = 0D;
            this.zedGraphControlDeadReckoning.ScrollMaxY = 0D;
            this.zedGraphControlDeadReckoning.ScrollMaxY2 = 0D;
            this.zedGraphControlDeadReckoning.ScrollMinX = 0D;
            this.zedGraphControlDeadReckoning.ScrollMinY = 0D;
            this.zedGraphControlDeadReckoning.ScrollMinY2 = 0D;
            this.zedGraphControlDeadReckoning.Size = new System.Drawing.Size(614, 355);
            this.zedGraphControlDeadReckoning.TabIndex = 10;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1438, 775);
            this.Controls.Add(this.zedGraphControlDeadReckoning);
            this.Controls.Add(this.tabSensori);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.toCloseButton);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "PinApp";
            this.tabS1.ResumeLayout(false);
            this.tabS0.ResumeLayout(false);
            this.tabSensori.ResumeLayout(false);
            this.tabS2.ResumeLayout(false);
            this.tabS3.ResumeLayout(false);
            this.tabS4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        #region Elementi form

        // Elementi interazione utente
        private System.Windows.Forms.Button toCloseButton;
        private System.Windows.Forms.RichTextBox messageTextBox;

        // Elementi tab
        private System.Windows.Forms.TabControl tabSensori;
        private System.Windows.Forms.TabPage tabS0;
        private System.Windows.Forms.TabPage tabS1;
        private System.Windows.Forms.TabPage tabS2;
        private System.Windows.Forms.TabPage tabS3;
        private System.Windows.Forms.TabPage tabS4;

        // Controlli ZedGraph
        public ZedGraph.ZedGraphControl zedGraphControl0_0;
        public ZedGraph.ZedGraphControl zedGraphControl1_0;
        public ZedGraph.ZedGraphControl zedGraphControl2_0;
        public ZedGraph.ZedGraphControl zedGraphControl0_1;
        public ZedGraph.ZedGraphControl zedGraphControl1_1;
        public ZedGraph.ZedGraphControl zedGraphControl2_2;
        public ZedGraph.ZedGraphControl zedGraphControl0_2;
        public ZedGraph.ZedGraphControl zedGraphControl1_2;
        public ZedGraph.ZedGraphControl zedGraphControl2_3;
        public ZedGraph.ZedGraphControl zedGraphControl0_3;
        public ZedGraph.ZedGraphControl zedGraphControl1_3;
        public ZedGraph.ZedGraphControl zedGraphControl2_4;
        public ZedGraph.ZedGraphControl zedGraphControl0_4;
        public ZedGraph.ZedGraphControl zedGraphControl1_4;
        public ZedGraph.ZedGraphControl zedGraphControl2_1;
        public ZedGraph.ZedGraphControl zedGraphControlDeadReckoning;

        #endregion Elementi form
    }
}