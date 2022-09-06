using System;
using System.Windows.Forms;

namespace Game_of_Life
{
    public partial class FormHelp : Form
    {
        public FormHelp()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://ru.wikipedia.org/wiki/%D0%98%D0%B3%D1%80%D0%B0_%C2%AB%D0%96%D0%B8%D0%B7%D0%BD%D1%8C%C2%BB");
        }

        private void linkLabel1_MouseEnter(object sender, EventArgs e)
        {
            linkLabel1.LinkVisited = true;
        }

        private void linkLabel1_MouseLeave(object sender, EventArgs e)
        {
            linkLabel1.LinkVisited = false;
        }

    }
}
