using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CslaAspNetCoreIdentityTiers.Business;

namespace CslaAspNetCoreIdentityTiers.WindowsForms
{
    public partial class FormMain
        : FormBaseSystemFont
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private async void TestExistsNormalizedUserNameAsync(object sender, EventArgs e)
        {
            try
            {
                var exists = await AppUser.ExistsNormalizedUserNameAsync("PERSON@EXAMPLE.COM");

                MessageBox.Show("Exists returned: " + exists);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
