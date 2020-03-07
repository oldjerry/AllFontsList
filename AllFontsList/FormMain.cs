using System;
using System.Collections;
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

        private void listViewFonts_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            string Asc = ((char)0x25bc).ToString().PadLeft(4, ' ');
            string Des = ((char)0x25b2).ToString().PadLeft(4, ' ');

            if (sort == false)
            {
                sort = true;
                string oldStr = this.listViewFonts.Columns[e.Column].Text.TrimEnd((char)0x25bc, (char)0x25b2, ' ');
                this.listViewFonts.Columns[e.Column].Text = oldStr + Des;
            }
            else if (sort == true)
            {
                sort = false;
                string oldStr = this.listViewFonts.Columns[e.Column].Text.TrimEnd((char)0x25bc, (char)0x25b2, ' ');
                this.listViewFonts.Columns[e.Column].Text = oldStr + Asc;
            }

            listViewFonts.ListViewItemSorter = new ListViewItemComparer(e.Column, sort);
            this.listViewFonts.Sort();
            int rowCount = this.listViewFonts.Items.Count;
            if (currentCol != -1)
            {
                for (int i = 0; i < rowCount; i++)
                {
                    this.listViewFonts.Items[i].UseItemStyleForSubItems = false;
                    this.listViewFonts.Items[i].SubItems[currentCol].BackColor = Color.White;

                    if (e.Column != currentCol)
                        this.listViewFonts.Columns[currentCol].Text = this.listViewFonts.Columns[currentCol].Text.TrimEnd((char)0x25bc, (char)0x25b2, ' ');
                }
            }

            for (int i = 0; i < rowCount; i++)
            {
                this.listViewFonts.Items[i].UseItemStyleForSubItems = false;
                this.listViewFonts.Items[i].SubItems[e.Column].BackColor = Color.WhiteSmoke;
                currentCol = e.Column;
            }
        }

        InstalledFontCollection ifc = new InstalledFontCollection();
        int currentCol = -1;
        bool sort;

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

        public class ListViewItemComparer : IComparer
        {
            public bool sort_b;
            public SortOrder order = SortOrder.Ascending;

            private int col;

            public ListViewItemComparer()
            {
                col = 0;
            }

            public ListViewItemComparer(int column, bool sort)
            {
                col = column;
                sort_b = sort;
            }

            public int Compare(object x, object y)
            {
                if (sort_b)
                {
                    return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
                }
                else
                {
                    return String.Compare(((ListViewItem)y).SubItems[col].Text, ((ListViewItem)x).SubItems[col].Text);
                }
            }
        }

    }
}
