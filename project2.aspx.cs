using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_1
{
    public partial class project2 : System.Web.UI.Page
    {
            string connectionString = "server=.;Database=project1;Integrated Security=True";

            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    txtname.Focus();
                    BindGrid();
                }
            }

            protected void btnsignin_Click(object sender, EventArgs e)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO signup (UserName, Password, Email, DOB) VALUES (@UserName, @Password, @Email, @DOB)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@UserName", txtname.Text);
                    cmd.Parameters.AddWithValue("@Password", txtpwd.Text);
                    cmd.Parameters.AddWithValue("@Email", txtemail.Text);
                DateTime dob;
                if (DateTime.TryParse(txtcld.Text, out dob))
                {
                    cmd.Parameters.AddWithValue("@DOB", dob);
                }
                con.Open();
                    cmd.ExecuteNonQuery();
                }

                ClearFields();
                BindGrid();
            }

            private void ClearFields()
            {
                txtname.Text = "";
                txtpwd.Text = "";
                txtemail.Text = "";
                txtcld.Text = "";
            }

            private void BindGrid()
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM signup ORDER BY sno";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }

            protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
            {
                int sno = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM signup WHERE sno = @sno";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@sno", sno);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

            ResequenceSno();
            BindGrid();
            }

            private void ResequenceSno()
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(@"
                    WITH CTE AS (
                        SELECT sno, ROW_NUMBER() OVER (ORDER BY sno) AS rn
                        FROM signup
                    )
                    UPDATE signup
                   SET RowNumber = CTE.rn
                   FROM signup
                   INNER JOIN CTE ON signup.sno = CTE.sno", con);
                    
                    cmd.ExecuteNonQuery();
                }
            }

            protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
            {
                GridView1.EditIndex = e.NewEditIndex;
                BindGrid();
            }

            protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
            {
                GridView1.EditIndex = -1;
                BindGrid();
            }

            protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
            {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int sno = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

            string username = ((TextBox)row.FindControl("txtUsername")).Text;
            string password = ((TextBox)row.FindControl("txtPassword")).Text;
            string email = ((TextBox)row.FindControl("txtEmail")).Text;
            string dob = ((TextBox)row.FindControl("txtDOB")).Text;


            using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE signup SET UserName=@UserName, Password=@Password, Email=@Email, DOB=@DOB WHERE sno=@sno";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@UserName", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@DOB", Convert.ToDateTime(dob));
                    cmd.Parameters.AddWithValue("@sno", sno);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                GridView1.EditIndex = -1;
                BindGrid();
            }

            protected void btnreset_Click(object sender, EventArgs e)
            {
                ClearFields();
            }

        
    }
}