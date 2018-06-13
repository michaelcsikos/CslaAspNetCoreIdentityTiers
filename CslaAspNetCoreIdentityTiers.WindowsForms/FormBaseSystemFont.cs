using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CslaAspNetCoreIdentityTiers.WindowsForms
{
    public partial class FormBaseSystemFont
        : Form
    {
        public FormBaseSystemFont()
        {
            Font = new Font(SystemFonts.MessageBoxFont.FontFamily, 9.0F);

            InitializeComponent();

            AutoScaleMode = AutoScaleMode.Font;
        }
    }
}
