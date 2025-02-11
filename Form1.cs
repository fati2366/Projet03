using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet03
{
    public partial class FrmPrincipale : Form
    {
        //on récupère ici le bonbon que l'utilisateur a séléctionné une fois la selection est valide.
        private static string Nom_bonbonChoisi;
        private static decimal Prix_bonbonChoisi;
        private static int Stock_bonbonChoisi;
        private static decimal monnaiePercue = 0;

        public FrmPrincipale()
        {
            InitializeComponent();
            //initialisation des textbox
            TxtChoix.Text = "0";
            TxtMonnaie.Text = "0";
            //on désactive les composants mentionnés dans les étapes (voir énoncé)
            //exemple
            cmdAjouter.Enabled = false;
            lblMessage.Visible = false;
            cmdAcheter.Enabled = false;
            lblBonbon.Visible = false;
            //...

        }

        private void cmdAnnuler_Click(object sender, EventArgs e)
        {
            
        }

        private void cmdAjouter_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(TxtMonnaie.Text, out decimal montant) && (montant == 0.05m || montant == 0.10m || montant == 0.25m || montant == 1m || montant == 2m))
            {
                monnaiePercue += montant;
                lblPercu.Text = monnaiePercue.ToString("C2");
                TxtMonnaie.Clear();
            }
            else
            {
                MessageBox.Show("Pièce non valide. Utilisez uniquement 0.05, 0.10, 0.25, 1, 2.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cmdAcheter_Click(object sender, EventArgs e)
        {
            if (monnaiePercue >= Prix_bonbonChoisi)
            {
                lblMessage.Text = "Prenez votre friandise";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblBonbon.Text = Nom_bonbonChoisi.ToUpper();
                lblRemis.Text = (monnaiePercue - Prix_bonbonChoisi).ToString("C2");
                monnaiePercue = 0;
            }
            else
            {
                MessageBox.Show("Montant insuffisant, ajoutez plus de monnaie.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TxtChoix_TextChanged(object sender, EventArgs e)
        {
          
        }
        //bouton vérifier stock: fonction de vérification de la selection puis le stock
        private void cmdVerifierStock_Click(object sender, EventArgs e)
        {
            if ((int.Parse(TxtChoix.Text) < 1) || (int.Parse(TxtChoix.Text) > 25))
            {
                MessageBox.Show("selection du bonbon invalide, il faut choisir de 1 à 25","Erreur");
            }
            else
            {
                int selection = int.Parse(TxtChoix.Text);
                Nom_bonbonChoisi = Program.GetCandyName(selection);
                Prix_bonbonChoisi = Program.GetCandyPrice(selection);
                Stock_bonbonChoisi = Program.GetCandyStock(selection);
                //on vérifie si le bonbon choisi est en stock, sinon on affiche un MessageBox
                if (Stock_bonbonChoisi <= 0)
                {
                    MessageBox.Show("Bonbon en rupture de stock.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //si oui, on active les autres composants (voir énoncé)
                else
                {
                    lblChoix.Text = Nom_bonbonChoisi;
                    lblPrix.Text = Prix_bonbonChoisi.ToString("C2");
                    TxtMonnaie.Enabled = true;
                    cmdAjouter.Enabled = true;
                    cmdAcheter.Enabled = true;
                }
            }
            
        }


    

        private void cmdRecommencer_Click(object sender, EventArgs e)
        {
            //remettre la machine à zéro
            if (monnaiePercue > 0)
            {
                MessageBox.Show($"Monnaie remboursée : {monnaiePercue:C2}", "Remboursement", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            monnaiePercue = 0;
            Prix_bonbonChoisi = 0;
            Stock_bonbonChoisi = -1;
            Nom_bonbonChoisi = "";
            InitializeComponent();
        }

        private void cmdQuitter_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez vous quitter la machine à bonbon?",
                          "Message de confirmation",
                          MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void FrmPrincipale_Load(object sender, EventArgs e)
        {

        }

        private void lblBonbon_Click(object sender, EventArgs e)
        {
            lblBonbon.Text = Nom_bonbonChoisi.ToUpper();
        }

        private void lblMessage_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "Prenez votre friandise";
            lblMessage.ForeColor = System.Drawing.Color.Red;
           
        }
    }
}
