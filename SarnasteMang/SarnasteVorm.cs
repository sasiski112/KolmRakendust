using System;
using System.Drawing;
using System.Windows.Forms;

namespace SarnasteMang
{
    // Sarnaste piltide leidmise mäng.
    // Lisad: pildid sümbolite asemel, taimer + punktid, tasemed 4x4 ja 6x6.
    public class SarnasteVorm : Form
    {
        private string[] sumbolid = { "🍎", "🍌", "🍇", "🍒", "🍓", "🍉", "🍍", "🥝", "🍑",
                                      "🍋", "🥕", "🌽", "🍄", "🌸", "🐱", "🐶", "🐸", "🦊" };

        private Color[] varvid = { Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Purple, Color.Gold,
                                   Color.HotPink, Color.Brown, Color.Cyan, Color.Black, Color.Gray, Color.Lime,
                                   Color.Navy, Color.Teal, Color.Maroon, Color.Coral, Color.Violet, Color.Olive };

        private Panel paneel;
        private ComboBox taseKast, rezhiimKast;
        private Button uusNupp;
        private Label aegSilt, punktiSilt, kaikudeSilt;
        private Timer mangTaimer;     // loeb mänguaega
        private Timer peitmisTaimer;  // peidab valed kaardid

        private Kaart esimene, teine;  // hetkel avatud kaardid
        private bool ootab;            // true, kui valed kaardid on veel nähtaval
        private bool mangKaib;
        private int sekundid, punktid, kaigud, leitudPaare, paarideArv;
        private Random juhuslik = new Random();

        public SarnasteVorm()
        {
            Text = "Sarnaste piltide mäng";
            ClientSize = new Size(620, 670);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            // Ülemine rida
            taseKast = new ComboBox();
            taseKast.DropDownStyle = ComboBoxStyle.DropDownList;
            taseKast.Items.Add("4x4");
            taseKast.Items.Add("6x6");
            taseKast.SelectedIndex = 0;
            taseKast.Location = new Point(10, 12);
            taseKast.Width = 60;
            Controls.Add(taseKast);

            rezhiimKast = new ComboBox();
            rezhiimKast.DropDownStyle = ComboBoxStyle.DropDownList;
            rezhiimKast.Items.Add("Sümbolid");
            rezhiimKast.Items.Add("Pildid");
            rezhiimKast.SelectedIndex = 0;
            rezhiimKast.Location = new Point(80, 12);
            rezhiimKast.Width = 90;
            Controls.Add(rezhiimKast);

            uusNupp = new Button();
            uusNupp.Text = "Uus mäng";
            uusNupp.Location = new Point(180, 10);
            uusNupp.Size = new Size(90, 28);
            uusNupp.Click += UusNupp_Click;
            Controls.Add(uusNupp);

            aegSilt = LooSilt(290);
            punktiSilt = LooSilt(375);
            kaikudeSilt = LooSilt(490);

            // Paneel, kuhu kaardid pannakse
            paneel = new Panel();
            paneel.Location = new Point(10, 55);
            paneel.Size = new Size(600, 600);
            Controls.Add(paneel);

            // Taimerid
            mangTaimer = new Timer();
            mangTaimer.Interval = 1000;
            mangTaimer.Tick += MangTaimer_Tick;

            peitmisTaimer = new Timer();
            peitmisTaimer.Interval = 700;
            peitmisTaimer.Tick += PeitmisTaimer_Tick;

            UusMang();
        }

        private Label LooSilt(int x)
        {
            Label silt = new Label();
            silt.AutoSize = true;
            silt.Location = new Point(x, 17);
            Controls.Add(silt);
            return silt;
        }

        private void UusNupp_Click(object sender, EventArgs e)
        {
            UusMang();
        }

        // Alustab uue mängu
        private void UusMang()
        {
            mangTaimer.Stop();
            peitmisTaimer.Stop();
            esimene = null;
            teine = null;
            ootab = false;
            mangKaib = false;
            sekundid = 0;
            punktid = 0;
            kaigud = 0;
            leitudPaare = 0;

            int suurus = 4;
            if (taseKast.SelectedIndex == 1)
                suurus = 6;
            bool pildidRezhiim = (rezhiimKast.SelectedIndex == 1);
            paarideArv = suurus * suurus / 2;

            // Iga paari jaoks kaks kaarti
            Kaart[] kaardid = new Kaart[suurus * suurus];
            for (int i = 0; i < paarideArv; i++)
            {
                kaardid[2 * i] = LooKaart(i, pildidRezhiim, suurus);
                kaardid[2 * i + 1] = LooKaart(i, pildidRezhiim, suurus);
            }

            Sega(kaardid);

            // Paneme kaardid ruudustikku
            paneel.Controls.Clear();
            int kaardiSuurus = 600 / suurus;
            for (int i = 0; i < kaardid.Length; i++)
            {
                int rida = i / suurus;
                int veerg = i % suurus;
                kaardid[i].Size = new Size(kaardiSuurus - 4, kaardiSuurus - 4);
                kaardid[i].Location = new Point(veerg * kaardiSuurus, rida * kaardiSuurus);
                paneel.Controls.Add(kaardid[i]);
            }

            UuendaSilte();
        }

        private Kaart LooKaart(int nr, bool pildidRezhiim, int suurus)
        {
            Image pilt = null;
            if (pildidRezhiim)
                pilt = LooPilt(nr);

            float fondiSuurus = 18;
            if (suurus == 4)
                fondiSuurus = 28;

            Kaart kaart = new Kaart(nr, sumbolid[nr], pilt, fondiSuurus);
            kaart.Click += Kaart_Click;
            return kaart;
        }

        private Bitmap LooPilt(int nr)
        {
            Bitmap pilt = new Bitmap(60, 60);
            Graphics g = Graphics.FromImage(pilt);
            g.Clear(Color.White);
            Brush pintsel = new SolidBrush(varvid[nr]);
            if (nr % 2 == 0)
                g.FillEllipse(pintsel, 5, 5, 50, 50);
            else
                g.FillRectangle(pintsel, 5, 5, 50, 50);
            g.Dispose();
            return pilt;
        }

        // Segab kaardid omavahel
        private void Sega(Kaart[] kaardid)
        {
            for (int i = kaardid.Length - 1; i > 0; i--)
            {
                int j = juhuslik.Next(i + 1);
                Kaart vahe = kaardid[i];
                kaardid[i] = kaardid[j];
                kaardid[j] = vahe;
            }
        }

        // Kaardile klõpsamine
        private void Kaart_Click(object sender, EventArgs e)
        {
            Kaart kaart = (Kaart)sender;

            if (ootab || kaart.Avatud || kaart.Leitud)
                return;

            // Aeg hakkab käima esimesest klõpsust
            if (!mangKaib)
            {
                mangKaib = true;
                mangTaimer.Start();
            }

            kaart.Ava();

            if (esimene == null)
            {
                esimene = kaart;
                return;
            }

            // Teine kaart avati
            kaigud++;
            if (esimene.ParId == kaart.ParId)
            {
                // Paar leitud
                esimene.MargiLeituks();
                kaart.MargiLeituks();
                esimene = null;
                leitudPaare++;
                punktid = punktid + 10;
                UuendaSilte();

                if (leitudPaare == paarideArv)
                    LoppMang();
            }
            else
            {
                // Vale paar: -2 punkti, kaardid peidetakse hetke pärast
                punktid = punktid - 2;
                if (punktid < 0)
                    punktid = 0;
                teine = kaart;
                ootab = true;
                UuendaSilte();
                peitmisTaimer.Start();
            }
        }

        private void PeitmisTaimer_Tick(object sender, EventArgs e)
        {
            peitmisTaimer.Stop();
            esimene.Sulge();
            teine.Sulge();
            esimene = null;
            teine = null;
            ootab = false;
        }

        private void MangTaimer_Tick(object sender, EventArgs e)
        {
            sekundid++;
            UuendaSilte();
        }

        private void LoppMang()
        {
            mangTaimer.Stop();
            mangKaib = false;
            MessageBox.Show("Leidsid kõik paarid!\nAeg: " + sekundid + " sek\nKäike: " + kaigud + "\nPunkte: " + punktid);
        }

        private void UuendaSilte()
        {
            aegSilt.Text = "Aeg: " + sekundid + " s";
            punktiSilt.Text = "Punktid: " + punktid;
            kaikudeSilt.Text = "Käigud: " + kaigud;
        }
    }
}
