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
    public partial class Ex1_3_2 : Form
    {
        String cns = "data source=(local); initial catalog=Dafesty; integrated security = true";
        SqlConnection cn;
        SqlCommand cm;
        DataSet ds;
        SqlDataAdapter da;
        SqlCommandBuilder cmb;
        int counter;

        public Ex1_3_2()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            counter--;

            showCustomerInfo();

            checkDirectionalBtns();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            counter++;

            showCustomerInfo();

            checkDirectionalBtns();
        }

        private void checkDirectionalBtns()
        {
            if (counter >= ds.Tables["Customers"].Rows.Count - 1)
            {
                btnNext.Enabled = false;
                btnEnd.Enabled = false;
            }
            else
            {
                btnNext.Enabled = true;
                btnEnd.Enabled = true;
            }

            if (counter <= 0)
            {
                btnBack.Enabled = false;
                btnStart.Enabled = false;
            }
            else
            {
                btnBack.Enabled = true;
                btnStart.Enabled = true;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            counter = 0;

            showCustomerInfo();

            checkDirectionalBtns();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            counter = ds.Tables["Customers"].Rows.Count - 1;

            showCustomerInfo();

            checkDirectionalBtns();
        }

        private void showCustomerInfo()
        {
            txtCustomerID.Text = ds.Tables["Customers"].Rows[counter]["CustomerID"].ToString();
            txtCustomerName.Text = ds.Tables["Customers"].Rows[counter]["CustomerName"].ToString();
            txtMemberCategory.Text = ds.Tables["Customers"].Rows[counter]["MemberCategory"].ToString();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            counter = 0;

            cn = new SqlConnection(cns);

            cn.Open();

            fillDataSet();

            cn.Close();

            showCustomerInfo();

            checkDirectionalBtns();
        }

        private void fillDataSet()
        {
            cm = new SqlCommand("SELECT CustomerID, CustomerName, MemberCategory FROM Customers", cn);

            da = new SqlDataAdapter(cm);

            ds = new DataSet();

            da.Fill(ds, "Customers");

            ds.Tables["Customers"].DefaultView.Sort = "CustomerID";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            cmb = new SqlCommandBuilder(da);

            cn.Open();

            ds.Tables["Customers"].Rows[counter]["CustomerID"] = txtCustomerID.Text;
            ds.Tables["Customers"].Rows[counter]["CustomerName"] = txtCustomerName.Text;
            ds.Tables["Customers"].Rows[counter]["MemberCategory"] = txtMemberCategory.Text;

            da.Update(ds, "Customers");

            cn.Close();

            MessageBox.Show("The customer information has been successfully updated!", "Update Successful");
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            cmb = new SqlCommandBuilder(da);

            cn.Open();

            DataRow dr = ds.Tables["Customers"].NewRow();
            dr["CustomerID"] = txtCustomerID.Text;
            dr["CustomerName"] = txtCustomerName.Text;
            dr["MemberCategory"] = txtMemberCategory.Text;
            ds.Tables["Customers"].Rows.Add(dr);

            da.Update(ds, "Customers");

            cn.Close();

            MessageBox.Show("The customer information has been successfully added!", "Insert Successful");

            counter = 0;
            fillDataSet();

            showCustomerInfo();
            checkDirectionalBtns();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            cmb = new SqlCommandBuilder(da);

            cn.Open();

            ds.Tables["Customers"].Rows[counter].Delete();

            da.Update(ds, "Customers");

            cn.Close();

            MessageBox.Show("The customer information has been successfully deleted!", "Delete Successful");

            counter = 0;
            fillDataSet();

            showCustomerInfo();
            checkDirectionalBtns();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            bool isFound = false;
            for (int i = 0; i < ds.Tables["Customers"].Rows.Count - 1; i++)
            {
                if (ds.Tables["Customers"].Rows[i]["CustomerID"].ToString() == txtFindCustomerID.Text)
                {
                    isFound = true;
                    counter = i;
                    break;
                }
            }

            if (isFound)
            {
                showCustomerInfo();
                checkDirectionalBtns();
            }
            else
            {
                MessageBox.Show("The customer ID can't be found");
            }
        }
    }
}
