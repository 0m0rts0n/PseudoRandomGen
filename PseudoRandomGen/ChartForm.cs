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
    public partial class ChartForm : Form
    {
        public string TBInfo
        {
            get { return InfoTB.Text; }
            set { InfoTB.Text = value; }
        }
        public ListBox LBInfo
        {
            get { return InfoLB; }
            set { InfoLB.Items.AddRange(value.Items); }
        }
        public ChartForm()
        {
            InitializeComponent();
        }
    }
}
