using System.Drawing;
using System.Windows.Forms;

namespace SarnasteMang
{
    // Üks mängukaart (nupp, mis teab oma paari numbrit)
    public class Kaart : Button
    {
        public int ParId { get; private set; }
        public bool Avatud { get; private set; }
        public bool Leitud { get; private set; }

        private string sumbol;
        private Image pilt;

        public Kaart(int parId, string sumbol, Image pilt, float fondiSuurus)
        {
            ParId = parId;
            this.sumbol = sumbol;
            this.pilt = pilt;
            Font = new Font("Segoe UI Emoji", fondiSuurus);
            FlatStyle = FlatStyle.Flat;
            Sulge();
        }

        // Näitab kaardi sisu
        public void Ava()
        {
            Avatud = true;
            BackColor = Color.White;
            if (pilt != null)
                Image = pilt;
            else
                Text = sumbol;
        }

        // Peidab kaardi sisu
        public void Sulge()
        {
            Avatud = false;
            BackColor = Color.CornflowerBlue;
            Text = "";
            Image = null;
        }

        public void MargiLeituks()
        {
            Leitud = true;
            BackColor = Color.LightGreen;
        }
    }
}
