using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PseudoRandomGen
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GeneratorForm gf = new GeneratorForm();
            gf.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GenerateCustomForm gcf = new GenerateCustomForm();
            gcf.Show();
        }
    }
}
