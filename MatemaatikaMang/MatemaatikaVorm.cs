using System;
using System.Drawing;
using System.Windows.Forms;

namespace MatemaatikaMang
{
    // Matemaatiline mäng: 4 ülesannet, ajapiirang, punktid, raskusaste
    public class MatemaatikaVorm : Form
    {
        private char[] tehted = { '+', '-', '*', '/' };
        private Ulesanne[] ulesanded = new Ulesanne[4];

        private Label[] aSildid = new Label[4];
        private Label[] tehteSildid = new Label[4];
        private Label[] bSildid = new Label[4];
        private NumericUpDown[] vastused = new NumericUpDown[4];

        private ComboBox raskusKast;
        private Label aegSilt, punktiSilt;
        private Button startNupp;
        private Timer taimer;

        private Random juhuslik = new Random();
        private int jaanudAeg;
        private int parimTulemus;
        private bool mangKaib;

        public MatemaatikaVorm()
        {
            Text = "Matemaatikamäng";
            ClientSize = new Size(560, 390);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            // Ülemine rida
            LooSilt("Raskusaste:", 10, 15, 10);

            raskusKast = new ComboBox();
            raskusKast.DropDownStyle = ComboBoxStyle.DropDownList;
            raskusKast.Items.Add("Lihtne");
            raskusKast.Items.Add("Keskmine");
            raskusKast.Items.Add("Raske");
            raskusKast.SelectedIndex = 0;
            raskusKast.Location = new Point(90, 12);
            raskusKast.Width = 90;
            Controls.Add(raskusKast);

            LooSilt("Aega jäänud:", 200, 15, 10);

            aegSilt = new Label();
            aegSilt.Location = new Point(290, 12);
            aegSilt.Size = new Size(80, 24);
            aegSilt.BorderStyle = BorderStyle.FixedSingle;
            aegSilt.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(aegSilt);

            LooSilt("Punktid:", 390, 15, 10);

            punktiSilt = new Label();
            punktiSilt.Location = new Point(460, 12);
            punktiSilt.Size = new Size(60, 24);
            punktiSilt.BorderStyle = BorderStyle.FixedSingle;
            punktiSilt.TextAlign = ContentAlignment.MiddleCenter;
            punktiSilt.Text = "0";
            Controls.Add(punktiSilt);

            // Neli ülesande rida
            for (int i = 0; i < 4; i++)
            {
                int y = 70 + i * 60;
                aSildid[i] = LooSilt("", 80, y, 18);
                tehteSildid[i] = LooSilt("", 160, y, 18);
                bSildid[i] = LooSilt("", 230, y, 18);
                LooSilt("=", 310, y, 18);

                vastused[i] = new NumericUpDown();
                vastused[i].Location = new Point(360, y);
                vastused[i].Width = 90;
                vastused[i].Font = new Font("Segoe UI", 16);
                vastused[i].Maximum = 10000;
                vastused[i].Enabled = false;
                vastused[i].ValueChanged += Vastus_ValueChanged;
                Controls.Add(vastused[i]);
            }

            // Start nupp
            startNupp = new Button();
            startNupp.Text = "Alusta mängu";
            startNupp.Location = new Point(180, 325);
            startNupp.Size = new Size(200, 42);
            startNupp.Click += StartNupp_Click;
            Controls.Add(startNupp);

            // Taimer (tiksub iga sekund)
            taimer = new Timer();
            taimer.Interval = 1000;
            taimer.Tick += Taimer_Tick;
        }

        // Teeb sildi ja lisab vormile
        private Label LooSilt(string tekst, int x, int y, int fondiSuurus)
        {
            Label silt = new Label();
            silt.Text = tekst;
            silt.AutoSize = true;
            silt.Font = new Font("Segoe UI", fondiSuurus);
            silt.Location = new Point(x, y);
            Controls.Add(silt);
            return silt;
        }

        private void StartNupp_Click(object sender, EventArgs e)
        {
            mangKaib = false;

            // Uued ülesanded
            for (int i = 0; i < 4; i++)
            {
                ulesanded[i] = new Ulesanne(tehted[i], raskusKast.SelectedIndex, juhuslik);
                aSildid[i].Text = ulesanded[i].A.ToString();
                tehteSildid[i].Text = ulesanded[i].Margi();
                bSildid[i].Text = ulesanded[i].B.ToString();
                vastused[i].Value = 0;
                vastused[i].BackColor = SystemColors.Window;
                vastused[i].Enabled = true;
            }

            // Mida raskem, seda vähem aega
            if (raskusKast.SelectedIndex == 0) jaanudAeg = 40;
            else if (raskusKast.SelectedIndex == 1) jaanudAeg = 30;
            else jaanudAeg = 20;

            aegSilt.Text = jaanudAeg + " sek";
            punktiSilt.Text = "0";
            startNupp.Enabled = false;
            raskusKast.Enabled = false;
            mangKaib = true;
            taimer.Start();
            vastused[0].Focus();
        }

        private void Taimer_Tick(object sender, EventArgs e)
        {
            jaanudAeg--;
            aegSilt.Text = jaanudAeg + " sek";
            if (jaanudAeg <= 0)
            {
                LoppMang(false);
            }
        }

        // Käivitub, kui mõnda vastust muudetakse
        private void Vastus_ValueChanged(object sender, EventArgs e)
        {
            if (!mangKaib) return;

            int oigeid = 0;
            for (int i = 0; i < 4; i++)
            {
                if (vastused[i].Value == ulesanded[i].Vastus)
                {
                    vastused[i].BackColor = Color.LightGreen;
                    oigeid++;
                }
                else
                {
                    vastused[i].BackColor = SystemColors.Window;
                }
            }

            if (oigeid == 4)
            {
                LoppMang(true);
            }
        }

        private void LoppMang(bool koikOiged)
        {
            taimer.Stop();
            mangKaib = false;

            int oigeid = 0;
            for (int i = 0; i < 4; i++)
            {
                if (vastused[i].Value == ulesanded[i].Vastus)
                    oigeid++;
                vastused[i].Enabled = false;
            }

            // 10 punkti iga õige eest + 2 punkti iga allesjäänud sekundi eest (kui kõik on õiged)
            int punktid = oigeid * 10;
            if (koikOiged)
                punktid = punktid + jaanudAeg * 2;

            punktiSilt.Text = punktid.ToString();
            if (punktid > parimTulemus)
                parimTulemus = punktid;

            startNupp.Enabled = true;
            startNupp.Text = "Mängi uuesti";
            raskusKast.Enabled = true;

            if (koikOiged)
                MessageBox.Show("Tubli! Kõik õiged.\nPunktid: " + punktid + "\nParim: " + parimTulemus);
            else
                MessageBox.Show("Aeg sai otsa! Õigeid: " + oigeid + "/4\nPunktid: " + punktid + "\nParim: " + parimTulemus);
        }
    }
}
