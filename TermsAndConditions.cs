using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TRABYAHE
{
    public partial class TermsAndConditions : Form
    {
        public TermsAndConditions()
        {
            InitializeComponent();
            btnContinue.Enabled = false;
        }

        private void chkBoxAgree_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxAgree.Checked)
            {
                btnContinue.Enabled = true;
                Placeholder.IsEnabledBtnSignIn = true;
            }
            else
            {
                btnContinue.Enabled = false;
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignUp signUp = new SignUp();
            signUp.ShowDialog();

            
        }
    }
}
