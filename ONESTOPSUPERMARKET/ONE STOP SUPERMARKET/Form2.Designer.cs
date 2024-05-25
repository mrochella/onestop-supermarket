namespace ONE_STOP_SUPERMARKET
{
    partial class Form2
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
            this.components = new System.ComponentModel.Container();
            this.panel_main = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel_menustrip = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.produceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fFruitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vegetablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beverageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dairyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sodaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alcoholToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.meatAndFishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.snacksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.houseHoldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.personalCareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paperGoodsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_logo = new System.Windows.Forms.Panel();
            this.pictureBox_logo = new System.Windows.Forms.PictureBox();
            this.panel_kategori = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel_menustrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel_logo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_main
            // 
            this.panel_main.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_main.Location = new System.Drawing.Point(0, 137);
            this.panel_main.Name = "panel_main";
            this.panel_main.Size = new System.Drawing.Size(1160, 360);
            this.panel_main.TabIndex = 5;
            this.panel_main.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_main_Paint);
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(3, 92);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(796, 357);
            this.panel3.TabIndex = 2;
            // 
            // panel_menustrip
            // 
            this.panel_menustrip.Controls.Add(this.menuStrip1);
            this.panel_menustrip.Location = new System.Drawing.Point(155, 1);
            this.panel_menustrip.Name = "panel_menustrip";
            this.panel_menustrip.Size = new System.Drawing.Size(977, 101);
            this.panel_menustrip.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.Font = new System.Drawing.Font("Yu Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.produceToolStripMenuItem,
            this.beverageToolStripMenuItem,
            this.meatAndFishToolStripMenuItem,
            this.snacksToolStripMenuItem,
            this.houseHoldToolStripMenuItem,
            this.signInToolStripMenuItem,
            this.historyToolStripMenuItem,
            this.logOutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(977, 101);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // produceToolStripMenuItem
            // 
            this.produceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fFruitToolStripMenuItem,
            this.vegetablesToolStripMenuItem});
            this.produceToolStripMenuItem.Name = "produceToolStripMenuItem";
            this.produceToolStripMenuItem.Size = new System.Drawing.Size(73, 97);
            this.produceToolStripMenuItem.Text = "Produce";
            // 
            // fFruitToolStripMenuItem
            // 
            this.fFruitToolStripMenuItem.Name = "fFruitToolStripMenuItem";
            this.fFruitToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.fFruitToolStripMenuItem.Text = "Fruit";
            this.fFruitToolStripMenuItem.Click += new System.EventHandler(this.FruitToolStripMenuItem_Click);
            // 
            // vegetablesToolStripMenuItem
            // 
            this.vegetablesToolStripMenuItem.Name = "vegetablesToolStripMenuItem";
            this.vegetablesToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.vegetablesToolStripMenuItem.Text = "Vegetables";
            this.vegetablesToolStripMenuItem.Click += new System.EventHandler(this.VegetablesToolStripMenuItem_Click);
            // 
            // beverageToolStripMenuItem
            // 
            this.beverageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dairyToolStripMenuItem,
            this.sodaToolStripMenuItem,
            this.alcoholToolStripMenuItem});
            this.beverageToolStripMenuItem.Name = "beverageToolStripMenuItem";
            this.beverageToolStripMenuItem.Size = new System.Drawing.Size(81, 97);
            this.beverageToolStripMenuItem.Text = "Beverage";
            this.beverageToolStripMenuItem.Click += new System.EventHandler(this.beverageToolStripMenuItem_Click);
            // 
            // dairyToolStripMenuItem
            // 
            this.dairyToolStripMenuItem.Name = "dairyToolStripMenuItem";
            this.dairyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.dairyToolStripMenuItem.Text = "Dairy";
            this.dairyToolStripMenuItem.Click += new System.EventHandler(this.dairyToolStripMenuItem_Click);
            // 
            // sodaToolStripMenuItem
            // 
            this.sodaToolStripMenuItem.Name = "sodaToolStripMenuItem";
            this.sodaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sodaToolStripMenuItem.Text = "Soda";
            this.sodaToolStripMenuItem.Click += new System.EventHandler(this.sodaToolStripMenuItem_Click);
            // 
            // alcoholToolStripMenuItem
            // 
            this.alcoholToolStripMenuItem.Name = "alcoholToolStripMenuItem";
            this.alcoholToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.alcoholToolStripMenuItem.Text = "Alcohol";
            this.alcoholToolStripMenuItem.Click += new System.EventHandler(this.alcoholToolStripMenuItem_Click);
            // 
            // meatAndFishToolStripMenuItem
            // 
            this.meatAndFishToolStripMenuItem.Name = "meatAndFishToolStripMenuItem";
            this.meatAndFishToolStripMenuItem.Size = new System.Drawing.Size(112, 97);
            this.meatAndFishToolStripMenuItem.Text = "Meat and Fish";
            this.meatAndFishToolStripMenuItem.Click += new System.EventHandler(this.meatAndFishToolStripMenuItem_Click);
            // 
            // snacksToolStripMenuItem
            // 
            this.snacksToolStripMenuItem.Name = "snacksToolStripMenuItem";
            this.snacksToolStripMenuItem.Size = new System.Drawing.Size(66, 97);
            this.snacksToolStripMenuItem.Text = "Snacks";
            this.snacksToolStripMenuItem.Click += new System.EventHandler(this.snacksToolStripMenuItem_Click);
            // 
            // houseHoldToolStripMenuItem
            // 
            this.houseHoldToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cleanersToolStripMenuItem,
            this.personalCareToolStripMenuItem,
            this.paperGoodsToolStripMenuItem});
            this.houseHoldToolStripMenuItem.Name = "houseHoldToolStripMenuItem";
            this.houseHoldToolStripMenuItem.Size = new System.Drawing.Size(95, 97);
            this.houseHoldToolStripMenuItem.Text = "House Hold";
            // 
            // cleanersToolStripMenuItem
            // 
            this.cleanersToolStripMenuItem.Name = "cleanersToolStripMenuItem";
            this.cleanersToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.cleanersToolStripMenuItem.Text = "Cleaners";
            this.cleanersToolStripMenuItem.Click += new System.EventHandler(this.cleanersToolStripMenuItem_Click);
            // 
            // personalCareToolStripMenuItem
            // 
            this.personalCareToolStripMenuItem.Name = "personalCareToolStripMenuItem";
            this.personalCareToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.personalCareToolStripMenuItem.Text = "Personal Care";
            this.personalCareToolStripMenuItem.Click += new System.EventHandler(this.personalCareToolStripMenuItem_Click);
            // 
            // paperGoodsToolStripMenuItem
            // 
            this.paperGoodsToolStripMenuItem.Name = "paperGoodsToolStripMenuItem";
            this.paperGoodsToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.paperGoodsToolStripMenuItem.Text = "Paper Goods";
            this.paperGoodsToolStripMenuItem.Click += new System.EventHandler(this.paperGoodsToolStripMenuItem_Click);
            // 
            // signInToolStripMenuItem
            // 
            this.signInToolStripMenuItem.Name = "signInToolStripMenuItem";
            this.signInToolStripMenuItem.Size = new System.Drawing.Size(71, 97);
            this.signInToolStripMenuItem.Text = "Sign Up";
            this.signInToolStripMenuItem.Click += new System.EventHandler(this.signInToolStripMenuItem_Click);
            // 
            // historyToolStripMenuItem
            // 
            this.historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            this.historyToolStripMenuItem.Size = new System.Drawing.Size(66, 97);
            this.historyToolStripMenuItem.Text = "History";
            this.historyToolStripMenuItem.Click += new System.EventHandler(this.historyToolStripMenuItem_Click);
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(71, 97);
            this.logOutToolStripMenuItem.Text = "Log Out";
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);
            // 
            // panel_logo
            // 
            this.panel_logo.Controls.Add(this.pictureBox_logo);
            this.panel_logo.Controls.Add(this.panel3);
            this.panel_logo.Controls.Add(this.panel_kategori);
            this.panel_logo.Location = new System.Drawing.Point(1, 1);
            this.panel_logo.Name = "panel_logo";
            this.panel_logo.Size = new System.Drawing.Size(155, 101);
            this.panel_logo.TabIndex = 3;
            // 
            // pictureBox_logo
            // 
            this.pictureBox_logo.Image = global::ONE_STOP_SUPERMARKET.Properties.Resources.Screenshot_2023_05_05_184814;
            this.pictureBox_logo.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_logo.Name = "pictureBox_logo";
            this.pictureBox_logo.Size = new System.Drawing.Size(155, 101);
            this.pictureBox_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_logo.TabIndex = 3;
            this.pictureBox_logo.TabStop = false;
            this.pictureBox_logo.Click += new System.EventHandler(this.pictureBox_logo_Click);
            // 
            // panel_kategori
            // 
            this.panel_kategori.Location = new System.Drawing.Point(3, 3);
            this.panel_kategori.Name = "panel_kategori";
            this.panel_kategori.Size = new System.Drawing.Size(796, 434);
            this.panel_kategori.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Interval = 1500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form2
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1160, 497);
            this.Controls.Add(this.panel_main);
            this.Controls.Add(this.panel_menustrip);
            this.Controls.Add(this.panel_logo);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load_1);
            this.panel_menustrip.ResumeLayout(false);
            this.panel_menustrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel_logo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_main;
        private System.Windows.Forms.PictureBox pictureBox_logo;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel_menustrip;
        private System.Windows.Forms.Panel panel_logo;
        private System.Windows.Forms.Panel panel_kategori;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem produceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fFruitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vegetablesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beverageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dairyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sodaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alcoholToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem meatAndFishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem snacksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem houseHoldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cleanersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem personalCareToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paperGoodsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem historyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
    }
}