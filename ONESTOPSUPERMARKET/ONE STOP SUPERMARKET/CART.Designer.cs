namespace ONE_STOP_SUPERMARKET
{
    partial class CART
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
            this.panelcartitem = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelcartitem.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelcartitem
            // 
            this.panelcartitem.AutoScroll = true;
            this.panelcartitem.Controls.Add(this.label1);
            this.panelcartitem.Location = new System.Drawing.Point(-4, 3);
            this.panelcartitem.Name = "panelcartitem";
            this.panelcartitem.Size = new System.Drawing.Size(804, 447);
            this.panelcartitem.TabIndex = 0;
            this.panelcartitem.Paint += new System.Windows.Forms.PaintEventHandler(this.panelcartitem_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(674, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // CART
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelcartitem);
            this.Name = "CART";
            this.Text = "CART";
            this.Load += new System.EventHandler(this.CART_Load);
            this.panelcartitem.ResumeLayout(false);
            this.panelcartitem.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelcartitem;
        private System.Windows.Forms.Label label1;
    }
}