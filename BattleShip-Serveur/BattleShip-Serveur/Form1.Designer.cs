namespace BattleShip_Serveur
{
    partial class Form1
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
            this.LB_StatusServer = new System.Windows.Forms.Label();
            this.Btn_DémarrerServeur = new System.Windows.Forms.Button();
            this.Lb_JoueurConnecter = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LB_StatusServer
            // 
            this.LB_StatusServer.AutoSize = true;
            this.LB_StatusServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.LB_StatusServer.ForeColor = System.Drawing.Color.Red;
            this.LB_StatusServer.Location = new System.Drawing.Point(98, 28);
            this.LB_StatusServer.Name = "LB_StatusServer";
            this.LB_StatusServer.Size = new System.Drawing.Size(148, 18);
            this.LB_StatusServer.TabIndex = 0;
            this.LB_StatusServer.Text = "SERVEUR FERMER";
            // 
            // Btn_DémarrerServeur
            // 
            this.Btn_DémarrerServeur.Location = new System.Drawing.Point(119, 131);
            this.Btn_DémarrerServeur.Name = "Btn_DémarrerServeur";
            this.Btn_DémarrerServeur.Size = new System.Drawing.Size(94, 23);
            this.Btn_DémarrerServeur.TabIndex = 1;
            this.Btn_DémarrerServeur.Text = "Démarrer Serveur";
            this.Btn_DémarrerServeur.UseVisualStyleBackColor = true;
            this.Btn_DémarrerServeur.Click += new System.EventHandler(this.Btn_DémarrerServeur_Click);
            // 
            // Lb_JoueurConnecter
            // 
            this.Lb_JoueurConnecter.AutoSize = true;
            this.Lb_JoueurConnecter.Location = new System.Drawing.Point(65, 80);
            this.Lb_JoueurConnecter.Name = "Lb_JoueurConnecter";
            this.Lb_JoueurConnecter.Size = new System.Drawing.Size(0, 13);
            this.Lb_JoueurConnecter.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 170);
            this.Controls.Add(this.Lb_JoueurConnecter);
            this.Controls.Add(this.Btn_DémarrerServeur);
            this.Controls.Add(this.LB_StatusServer);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LB_StatusServer;
        private System.Windows.Forms.Button Btn_DémarrerServeur;
        private System.Windows.Forms.Label Lb_JoueurConnecter;
    }
}

