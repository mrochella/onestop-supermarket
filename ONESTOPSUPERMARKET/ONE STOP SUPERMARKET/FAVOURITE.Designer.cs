namespace ONE_STOP_SUPERMARKET
{
    partial class FAVOURITE
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
            this.panel_like = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panel_like
            // 
            this.panel_like.Location = new System.Drawing.Point(2, 1);
            this.panel_like.Name = "panel_like";
            this.panel_like.Size = new System.Drawing.Size(1196, 1076);
            this.panel_like.TabIndex = 0;
            this.panel_like.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_like_Paint);
            // 
            // FAVOURITE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.panel_like);
            this.Name = "FAVOURITE";
            this.Text = "FAVOURITE";
            this.Load += new System.EventHandler(this.FAVOURITE_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_like;
    }
}