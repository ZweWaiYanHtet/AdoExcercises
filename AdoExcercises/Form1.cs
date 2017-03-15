using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AdoExcercises
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int videoCode = Convert.ToInt32(txtVideoCode.Text);
            double rentalPrice = Convert.ToDouble(txtRentalPrice.Text);

            String cns = "data source=(local); initial catalog=Dafesty; integrated security = true";
            SqlConnection cn = new SqlConnection(cns);
            SqlCommand cm = new SqlCommand("UPDATE Movies SET RentalPrice =" + rentalPrice + " WHERE VideoCode = " + videoCode, cn);
            cn.Open();
            if (cm.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Update Successful");
            }
            cn.Close();
        }
    }
}
