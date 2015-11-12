namespace Battleship
{
    partial class Menu
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.label1 = new System.Windows.Forms.Label();
            this.BTN_Start = new System.Windows.Forms.Button();
            this.BTN_Quit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Bernard MT Condensed", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Azure;
            this.label1.Location = new System.Drawing.Point(77, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(279, 76);
            this.label1.TabIndex = 0;
            this.label1.Text = "Battleship";
            // 
            // BTN_Start
            // 
            this.BTN_Start.BackColor = System.Drawing.Color.Transparent;
            this.BTN_Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_Start.Font = new System.Drawing.Font("Bernard MT Condensed", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_Start.ForeColor = System.Drawing.Color.Azure;
            this.BTN_Start.Location = new System.Drawing.Point(104, 156);
            this.BTN_Start.Name = "BTN_Start";
            this.BTN_Start.Size = new System.Drawing.Size(207, 60);
            this.BTN_Start.TabIndex = 3;
            this.BTN_Start.Text = "Démarrer";
            this.BTN_Start.UseVisualStyleBackColor = false;
            this.BTN_Start.Click += new System.EventHandler(this.BTN_Start_Click);
            this.BTN_Start.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BTN_Start_MouseDown);
            this.BTN_Start.MouseEnter += new System.EventHandler(this.BTN_Start_MouseEnter);
            this.BTN_Start.MouseLeave += new System.EventHandler(this.BTN_Start_MouseLeave);
            this.BTN_Start.MouseHover += new System.EventHandler(this.BTN_Start_MouseHover);
            this.BTN_Start.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BTN_Start_MouseMove);
            this.BTN_Start.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BTN_Start_MouseUp);
            // 
            // BTN_Quit
            // 
            this.BTN_Quit.BackColor = System.Drawing.Color.Transparent;
            this.BTN_Quit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTN_Quit.Font = new System.Drawing.Font("Bernard MT Condensed", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_Quit.ForeColor = System.Drawing.Color.Azure;
            this.BTN_Quit.Location = new System.Drawing.Point(104, 223);
            this.BTN_Quit.Name = "BTN_Quit";
            this.BTN_Quit.Size = new System.Drawing.Size(207, 60);
            this.BTN_Quit.TabIndex = 4;
            this.BTN_Quit.Text = "Quitter";
            this.BTN_Quit.UseVisualStyleBackColor = false;
            this.BTN_Quit.Click += new System.EventHandler(this.BTN_Quit_Click);
            this.BTN_Quit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BTN_Start_MouseDown);
            this.BTN_Quit.MouseEnter += new System.EventHandler(this.BTN_Quit_MouseEnter);
            this.BTN_Quit.MouseLeave += new System.EventHandler(this.BTN_Start_MouseLeave);
            this.BTN_Quit.MouseHover += new System.EventHandler(this.BTN_Start_MouseHover);
            this.BTN_Quit.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BTN_Start_MouseMove);
            this.BTN_Quit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BTN_Start_MouseUp);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(726, 429);
            this.Controls.Add(this.BTN_Quit);
            this.Controls.Add(this.BTN_Start);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Menu";
            this.Text = "Battleship";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BTN_Start;
        private System.Windows.Forms.Button BTN_Quit;
    }
}

