namespace Battleship
{
    partial class PlancheJeu
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
            this.PN_Joueur = new System.Windows.Forms.Panel();
            this.PN_Ennemi = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // PN_Joueur
            // 
            this.PN_Joueur.Location = new System.Drawing.Point(53, 109);
            this.PN_Joueur.Name = "PN_Joueur";
            this.PN_Joueur.Size = new System.Drawing.Size(350, 350);
            this.PN_Joueur.TabIndex = 0;
            // 
            // PN_Ennemi
            // 
            this.PN_Ennemi.Location = new System.Drawing.Point(452, 109);
            this.PN_Ennemi.Name = "PN_Ennemi";
            this.PN_Ennemi.Size = new System.Drawing.Size(350, 350);
            this.PN_Ennemi.TabIndex = 1;
            // 
            // PlancheJeu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Battleship.Properties.Resources.Dark_water_wallpaper_2560x1600;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(855, 486);
            this.Controls.Add(this.PN_Ennemi);
            this.Controls.Add(this.PN_Joueur);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PlancheJeu";
            this.Text = "PlancheJeu";
            this.Load += new System.EventHandler(this.PlancheJeu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PN_Joueur;
        private System.Windows.Forms.Panel PN_Ennemi;

    }
}