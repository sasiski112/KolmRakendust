using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace PildiVaataja
{
    // Pildi vaatamise programm.
    // Lisad: taustavärv (ColorDialog), slaidishow, salvestamine teise formaati.
    public class    PildiVaatajaVorm : Form
    {
        private PictureBox pildikast;
        private CheckBox venitaMark;
        private Button naitaNupp, varvNupp, slaidiNupp, salvestaNupp, tuhjendaNupp, sulgeNupp;
        private Timer slaidiTaimer;

        private string[] failid;   // valitud failid
        private int indeks;        // mitmendat pilti praegu näidatakse

        public PildiVaatajaVorm()
        {
            Text = "Pildi vaataja";
            ClientSize = new Size(800, 550);
            MinimumSize = new Size(800, 400);
            StartPosition = FormStartPosition.CenterScreen;

            // Pildikast
            pildikast = new PictureBox();
            pildikast.Location = new Point(10, 10);
            pildikast.Size = new Size(780, 440);
            pildikast.BorderStyle = BorderStyle.FixedSingle;
            pildikast.SizeMode = PictureBoxSizeMode.CenterImage;
            pildikast.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(pildikast);

            // Märkeruut
            venitaMark = new CheckBox();
            venitaMark.Text = "Sobita pilt aknasse";
            venitaMark.AutoSize = true;
            venitaMark.Location = new Point(10, 465);
            venitaMark.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            venitaMark.CheckedChanged += VenitaMark_CheckedChanged;
            Controls.Add(venitaMark);

            // Nupud
            naitaNupp = LooNupp("Näita pilti", 10);
            varvNupp = LooNupp("Vali taustavärv", 140);
            slaidiNupp = LooNupp("Alusta slaidishow'd", 270);
            salvestaNupp = LooNupp("Salvesta kui...", 400);
            tuhjendaNupp = LooNupp("Tühjenda pilt", 530);
            sulgeNupp = LooNupp("Sulge", 660);

            naitaNupp.Click += NaitaNupp_Click;
            varvNupp.Click += VarvNupp_Click;
            slaidiNupp.Click += SlaidiNupp_Click;
            salvestaNupp.Click += SalvestaNupp_Click;
            tuhjendaNupp.Click += TuhjendaNupp_Click;
            sulgeNupp.Click += SulgeNupp_Click;

            // Taimer slaidishow jaoks (vahetab pilti iga 3 sekundi järel)
            slaidiTaimer = new Timer();
            slaidiTaimer.Interval = 3000;
            slaidiTaimer.Tick += SlaidiTaimer_Tick;
        }

        private Button LooNupp(string tekst, int x)
        {
            Button nupp = new Button();
            nupp.Text = tekst;
            nupp.Location = new Point(x, 505);
            nupp.Size = new Size(120, 32);
            nupp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Controls.Add(nupp);
            return nupp;
        }

        // Näitab pilti (valida saab mitu faili)
        private void NaitaNupp_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialoog = new OpenFileDialog();
            dialoog.Filter = "Pildid|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            dialoog.Multiselect = true;

            if (dialoog.ShowDialog() == DialogResult.OK)
            {
                failid = dialoog.FileNames;
                indeks = 0;
                pildikast.Load(failid[indeks]);
            }
        }

        // Taustavärvi valik
        private void VarvNupp_Click(object sender, EventArgs e)
        {
            ColorDialog dialoog = new ColorDialog();
            if (dialoog.ShowDialog() == DialogResult.OK)
            {
                pildikast.BackColor = dialoog.Color;
            }
        }

        // Märkeruut: sobitab pildi aknasse või näitab originaalsuuruses
        private void VenitaMark_CheckedChanged(object sender, EventArgs e)
        {
            if (venitaMark.Checked)
                pildikast.SizeMode = PictureBoxSizeMode.Zoom;
            else
                pildikast.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        // Slaidishow käivitamine / peatamine
        private void SlaidiNupp_Click(object sender, EventArgs e)
        {
            if (slaidiTaimer.Enabled)
            {
                slaidiTaimer.Stop();
                slaidiNupp.Text = "Alusta slaidishow'd";
            }
            else
            {
                if (failid == null || failid.Length < 2)
                {
                    MessageBox.Show("Vali vähemalt kaks pilti.");
                    return;
                }
                slaidiTaimer.Start();
                slaidiNupp.Text = "Peata slaidishow";
            }
        }

        // Iga 3 sekundi järel järgmine pilt
        private void SlaidiTaimer_Tick(object sender, EventArgs e)
        {
            indeks++;
            if (indeks >= failid.Length)
                indeks = 0;
            pildikast.Load(failid[indeks]);
        }

        // Salvestab pildi valitud formaadis
        private void SalvestaNupp_Click(object sender, EventArgs e)
        {
            if (pildikast.Image == null)
            {
                MessageBox.Show("Kõigepealt ava pilt.");
                return;
            }

            SaveFileDialog dialoog = new SaveFileDialog();
            dialoog.Filter = "PNG|*.png|JPEG|*.jpg|BMP|*.bmp";

            if (dialoog.ShowDialog() == DialogResult.OK)
            {
                string laiend = Path.GetExtension(dialoog.FileName).ToLower();
                if (laiend == ".jpg")
                    pildikast.Image.Save(dialoog.FileName, ImageFormat.Jpeg);
                else if (laiend == ".bmp")
                    pildikast.Image.Save(dialoog.FileName, ImageFormat.Bmp);
                else
                    pildikast.Image.Save(dialoog.FileName, ImageFormat.Png);
            }
        }

        // Eemaldab pildi
        private void TuhjendaNupp_Click(object sender, EventArgs e)
        {
            slaidiTaimer.Stop();
            slaidiNupp.Text = "Alusta slaidishow'd";
            pildikast.Image = null;
            failid = null;
        }

        private void SulgeNupp_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
