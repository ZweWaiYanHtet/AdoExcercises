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
    public partial class Ex1_3 : Form
    {
        String cns = "data source=(local); initial catalog=Dafesty; integrated security = true";
        SqlConnection cn;
        SqlCommand cm;
        DataSet ds;
        SqlDataAdapter da;
        SqlCommandBuilder cmb;

        public Ex1_3()
        {
            InitializeComponent();
        }

        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            cn = new SqlConnection(cns);
            cn.Open();

            cm = new SqlCommand("SELECT VideoCode, MovieTitle, RentalPrice FROM Movies WHERE VideoCode = " + txtVideoCode.Text, cn);

            da = new SqlDataAdapter(cm);

            ds = new DataSet();

            da.Fill(ds, "Movies");

            txtMovieTitle.Text = ds.Tables[0].Rows[0]["MovieTitle"].ToString();
            txtRentalPrice.Text = ds.Tables[0].Rows[0]["RentalPrice"].ToString();

            cn.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            cmb = new SqlCommandBuilder(da);
            cn.Open();

            ds.Tables[0].Rows[0]["MovieTitle"] = txtMovieTitle.Text;
            ds.Tables[0].Rows[0]["RentalPrice"] = txtRentalPrice.Text;

            da.Update(ds, "Movies");
            MessageBox.Show("Update Successful!");

            cn.Close();
        }
    }
}
