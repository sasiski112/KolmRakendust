using System;
using System.Drawing;
using System.Windows.Forms;
using MatemaatikaMang;
using PildiVaataja;
using SarnasteMang;

namespace Launcher
{
    public class PeaVorm : Form
    {
        private Button pildiNupp, matemaatikaNupp, sarnasteNupp, valjuNupp;

        public PeaVorm()
        {
            Text = "Kolm rakendust";
            ClientSize = new Size(300, 260);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            Label pealkiri = new Label();
            pealkiri.Text = "Vali rakendus";
            pealkiri.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            pealkiri.AutoSize = true;
            pealkiri.Location = new Point(55, 15);
            Controls.Add(pealkiri);

            pildiNupp = LooNupp("Pildi vaataja", 70);
            matemaatikaNupp = LooNupp("Matemaatiline mäng", 120);
            sarnasteNupp = LooNupp("Sarnaste piltide mäng", 170);
            valjuNupp = LooNupp("Välju", 220);

            pildiNupp.Click += PildiNupp_Click;
            matemaatikaNupp.Click += MatemaatikaNupp_Click;
            sarnasteNupp.Click += SarnasteNupp_Click;
            valjuNupp.Click += ValjuNupp_Click;
        }

        private Button LooNupp(string tekst, int y)
        {
            Button nupp = new Button();
            nupp.Text = tekst;
            nupp.Location = new Point(40, y);
            nupp.Size = new Size(220, 40);
            Controls.Add(nupp);
            return nupp;
        }

        private void PildiNupp_Click(object sender, EventArgs e)
        {
            PildiVaatajaVorm vorm = new PildiVaatajaVorm();
            vorm.ShowDialog();
        }

        private void MatemaatikaNupp_Click(object sender, EventArgs e)
        {
            MatemaatikaVorm vorm = new MatemaatikaVorm();
            vorm.ShowDialog();
        }

        private void SarnasteNupp_Click(object sender, EventArgs e)
        {
            SarnasteVorm vorm = new SarnasteVorm();
            vorm.ShowDialog();
        }

        private void ValjuNupp_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
