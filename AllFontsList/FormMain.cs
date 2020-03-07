using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AllFontsList
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            EnumerateInstalledFonts();
        }

        InstalledFontCollection ifc = new InstalledFontCollection();
        private void EnumerateInstalledFonts()
        {
            FontFamily[] families = ifc.Families;
            String strSample = "Jerry & Rachel";
            if (textBoxSample.Text.Length > 0)
                strSample = textBoxSample.Text;

            this.listViewFonts.BeginUpdate();
            this.listViewFonts.Items.Clear();

            // Use image to extend Item Height
            ImageList il = new ImageList();
            il.ImageSize = new Size(1, 30);
            listViewFonts.SmallImageList = il;

            for (int i = 0; i < ifc.Families.Length; i++)
            {
                if (ifc.Families[i].IsStyleAvailable(FontStyle.Regular)) 
                {
                    ListViewItem lvi = new ListViewItem();
                    Font f = new Font(ifc.Families[i], 20);
                    lvi.Font = f;
                    lvi.Text = strSample;
                    lvi.SubItems.Add(ifc.Families[i].Name);
                    this.listViewFonts.Items.Add(lvi);
                }
            }

            this.listViewFonts.EndUpdate();
        }

    }
}
