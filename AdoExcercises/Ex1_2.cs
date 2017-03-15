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
    public partial class Ex1_2 : Form
    {
        public Ex1_2()
        {
            InitializeComponent();
        }

        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            String cns = "data source=(local); initial catalog=Dafesty; integrated security = true";
            SqlConnection cn = new SqlConnection(cns);
            SqlCommand cm = new SqlCommand("SELECT * FROM Movies WHERE Rating = '" + txtRating.Text + "'", cn);
            cn.Open();
            SqlDataReader rd = cm.ExecuteReader();

            while (rd.Read())
            {
                lstMovies.Items.Add(rd["MovieTitle"].ToString());
            }

            rd.Close();
            cn.Close();
        }

        private void Ex1_2_Load(object sender, EventArgs e)
        {

        }
    }
}
