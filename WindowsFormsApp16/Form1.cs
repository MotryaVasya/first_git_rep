using QRCoder;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp16
{
    public partial class Form1 : Form
    {
        Color color = Color.Black;
        Color backcolor = Color.White;
        public Image logo;
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string inputvalue = textBox1.Text;
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrData = qrGenerator.CreateQrCode(inputvalue, QRCodeGenerator.ECCLevel.Q);

                using (QRCode qrCode = new QRCode(qrData))
                {
                    Bitmap qrCodeImage = qrCode.GetGraphic(20, color, backcolor , true);
                    if (logo!=null)
                    {
                        logo = AddLogoToQr(qrCodeImage, logo);
                    }
                    pictureBox1.Image = qrCodeImage;

                }
            }
        }
        public Bitmap AddLogoToQr(Bitmap qr, Image logo)
        {
            int size = qr.Width / 5;
            Bitmap logoBitmap = new Bitmap(logo,new Size(size,size));
            Graphics qrGraphics = Graphics.FromImage(qr);
            int x = ((qr.Height - size) / 2);
            int y = ((qr.Height - size) / 2);
            qrGraphics.DrawImage(logoBitmap, x, y ,size, size);
            qrGraphics.Save();
            return qr;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            color = colorDialog1.Color;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            backcolor = colorDialog1.Color;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                logo = Image.FromFile(openFileDialog1.FileName);
            }
        }
    }
}