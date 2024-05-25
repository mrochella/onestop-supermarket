namespace ONE_STOP_SUPERMARKET
{
    partial class MEAT_AND_FISH
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
            this.button_cart = new System.Windows.Forms.Button();
            this.button_like = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_cart
            // 
            this.button_cart.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button_cart.BackgroundImage = global::ONE_STOP_SUPERMARKET.Properties.Resources.image_removebg_preview;
            this.button_cart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_cart.Location = new System.Drawing.Point(641, 8);
            this.button_cart.Name = "button_cart";
            this.button_cart.Size = new System.Drawing.Size(57, 51);
            this.button_cart.TabIndex = 23;
            this.button_cart.UseVisualStyleBackColor = false;
            this.button_cart.Click += new System.EventHandler(this.button_cart_Click);
            // 
            // button_like
            // 
            this.button_like.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button_like.BackgroundImage = global::ONE_STOP_SUPERMARKET.Properties.Resources._8f79d824d93cccf9bd3917ba5451829a_removebg_preview;
            this.button_like.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_like.Location = new System.Drawing.Point(708, 8);
            this.button_like.Name = "button_like";
            this.button_like.Size = new System.Drawing.Size(57, 51);
            this.button_like.TabIndex = 22;
            this.button_like.UseVisualStyleBackColor = false;
            this.button_like.Click += new System.EventHandler(this.button_like_Click);
            // 
            // MEAT_AND_FISH
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button_cart);
            this.Controls.Add(this.button_like);
            this.Name = "MEAT_AND_FISH";
            this.Text = "MEAT_AND_FISH";
            this.Load += new System.EventHandler(this.MEAT_AND_FISH_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button_cart;
        private System.Windows.Forms.Button button_like;
    }
}