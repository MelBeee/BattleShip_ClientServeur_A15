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
            this.flashButton1 = new FlashButton.FlashButton();
            this.flashButton2 = new FlashButton.FlashButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Matura MT Script Capitals", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(49, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(341, 85);
            this.label1.TabIndex = 0;
            this.label1.Text = "Battleship";
            // 
            // flashButton1
            // 
            this.flashButton1.BackgroundImage = global::Battleship.Properties.Resources.Demarrer_Normal;
            this.flashButton1.ImageClick = global::Battleship.Properties.Resources.Demarrer_Click;
            this.flashButton1.ImageDisable = global::Battleship.Properties.Resources.Demarrer_Disable;
            this.flashButton1.ImageNeutral = global::Battleship.Properties.Resources.Demarrer_Normal;
            this.flashButton1.ImageOver = global::Battleship.Properties.Resources.Demarrer_Over;
            this.flashButton1.Location = new System.Drawing.Point(77, 138);
            this.flashButton1.Name = "flashButton1";
            this.flashButton1.Size = new System.Drawing.Size(286, 56);
            this.flashButton1.TabIndex = 1;
            // 
            // flashButton2
            // 
            this.flashButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flashButton2.ImageClick = null;
            this.flashButton2.ImageDisable = null;
            this.flashButton2.ImageNeutral = null;
            this.flashButton2.ImageOver = null;
            this.flashButton2.Location = new System.Drawing.Point(696, 7);
            this.flashButton2.Name = "flashButton2";
            this.flashButton2.Size = new System.Drawing.Size(23, 24);
            this.flashButton2.TabIndex = 2;
            this.flashButton2.Click += new System.EventHandler(this.flashButton2_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(726, 463);
            this.Controls.Add(this.flashButton2);
            this.Controls.Add(this.flashButton1);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Menu";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private FlashButton.FlashButton flashButton1;
        private FlashButton.FlashButton flashButton2;
    }
}

