namespace Mini_games
{
    partial class Form4
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.NwGm = new System.Windows.Forms.ToolStripMenuItem();
            this.Prop = new System.Windows.Forms.ToolStripMenuItem();
            this.ThisNewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.NewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.повернутисяДоМенюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.статистикаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.контрольГриToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.правийКлікПоставитиПозначкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.лівийклікВідкритиПолеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.натиснутиНаСмайликПочатиГруToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NwGm,
            this.повернутисяДоМенюToolStripMenuItem,
            this.статистикаToolStripMenuItem,
            this.контрольГриToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(844, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // NwGm
            // 
            this.NwGm.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Prop,
            this.ThisNewGame,
            this.NewGame});
            this.NwGm.Name = "NwGm";
            this.NwGm.Size = new System.Drawing.Size(68, 20);
            this.NwGm.Text = "Нова гра";
            this.NwGm.Click += new System.EventHandler(this.новаГраToolStripMenuItem_Click);
            // 
            // Prop
            // 
            this.Prop.Name = "Prop";
            this.Prop.Size = new System.Drawing.Size(211, 22);
            this.Prop.Text = "Налаштування";
            this.Prop.Click += new System.EventHandler(this.Prop_Click);
            // 
            // ThisNewGame
            // 
            this.ThisNewGame.Name = "ThisNewGame";
            this.ThisNewGame.Size = new System.Drawing.Size(211, 22);
            this.ThisNewGame.Text = "Почати цю гру з початку";
            this.ThisNewGame.Click += new System.EventHandler(this.ThisNewGame_Click);
            // 
            // NewGame
            // 
            this.NewGame.Name = "NewGame";
            this.NewGame.Size = new System.Drawing.Size(211, 22);
            this.NewGame.Text = "Почати нову гру";
            this.NewGame.Click += new System.EventHandler(this.NewGame_Click);
            // 
            // повернутисяДоМенюToolStripMenuItem
            // 
            this.повернутисяДоМенюToolStripMenuItem.Name = "повернутисяДоМенюToolStripMenuItem";
            this.повернутисяДоМенюToolStripMenuItem.Size = new System.Drawing.Size(142, 20);
            this.повернутисяДоМенюToolStripMenuItem.Text = "Повернутися до меню";
            this.повернутисяДоМенюToolStripMenuItem.Click += new System.EventHandler(this.повернутисяДоМенюToolStripMenuItem_Click);
            // 
            // статистикаToolStripMenuItem
            // 
            this.статистикаToolStripMenuItem.Name = "статистикаToolStripMenuItem";
            this.статистикаToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.статистикаToolStripMenuItem.Text = "Статистика";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(844, 687);
            this.panel1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(10, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = " ";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(799, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = " ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pictureBox2.BackgroundImage = global::Mini_games.Properties.Resources.Image1;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Location = new System.Drawing.Point(412, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 24);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(10, 47);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(820, 628);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // контрольГриToolStripMenuItem
            // 
            this.контрольГриToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.правийКлікПоставитиПозначкуToolStripMenuItem,
            this.лівийклікВідкритиПолеToolStripMenuItem,
            this.натиснутиНаСмайликПочатиГруToolStripMenuItem});
            this.контрольГриToolStripMenuItem.Name = "контрольГриToolStripMenuItem";
            this.контрольГриToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.контрольГриToolStripMenuItem.Text = "Контроль гри";
            // 
            // правийКлікПоставитиПозначкуToolStripMenuItem
            // 
            this.правийКлікПоставитиПозначкуToolStripMenuItem.Name = "правийКлікПоставитиПозначкуToolStripMenuItem";
            this.правийКлікПоставитиПозначкуToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.правийКлікПоставитиПозначкуToolStripMenuItem.Text = "Правий клік - поставити позначку";
            // 
            // лівийклікВідкритиПолеToolStripMenuItem
            // 
            this.лівийклікВідкритиПолеToolStripMenuItem.Name = "лівийклікВідкритиПолеToolStripMenuItem";
            this.лівийклікВідкритиПолеToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.лівийклікВідкритиПолеToolStripMenuItem.Text = "Лівийклік - відкрити поле";
            // 
            // натиснутиНаСмайликПочатиГруToolStripMenuItem
            // 
            this.натиснутиНаСмайликПочатиГруToolStripMenuItem.Name = "натиснутиНаСмайликПочатиГруToolStripMenuItem";
            this.натиснутиНаСмайликПочатиГруToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.натиснутиНаСмайликПочатиГруToolStripMenuItem.Text = "Натиснути на смайлик - почати гру";
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 711);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(860, 750);
            this.Name = "Form4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Мінівник";
            this.Load += new System.EventHandler(this.Form4_Load);
            this.SizeChanged += new System.EventHandler(this.Form4_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem NwGm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem Prop;
        private System.Windows.Forms.ToolStripMenuItem ThisNewGame;
        private System.Windows.Forms.ToolStripMenuItem NewGame;
        private System.Windows.Forms.ToolStripMenuItem повернутисяДоМенюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem статистикаToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem контрольГриToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem правийКлікПоставитиПозначкуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem лівийклікВідкритиПолеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem натиснутиНаСмайликПочатиГруToolStripMenuItem;
    }
}