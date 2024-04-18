using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GridPagingSorting
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }

        private void BindGrid()
        {
            string query = "SELECT * FROM Person";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        cmd.Connection = con;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridView1.DataSource = dt;
                            GridView1.DataBind();

                            //for sorting
                            ViewState["dirState"] = dt;
                            ViewState["sortdr"] = "Asc";
                        }
                    }
                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = (DataTable)ViewState["dirState"];
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToString(ViewState["sortdr"]) == "Asc")
                {
                    dt.DefaultView.Sort = e.SortExpression + " Desc";
                    ViewState["sortdr"] = "Desc";
                }
                else
                {
                    dt.DefaultView.Sort = e.SortExpression + " Asc";
                    ViewState["sortdr"] = "Asc";
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();


            }
        }

        protected void ButtonBubble_Click(object sender, EventArgs e)
        {
            BubbleSort();
        }

        private void BubbleSort()
        {
            DataTable dt;
            string query = "SELECT top(15) * FROM Person";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        cmd.Connection = con;
                        using (dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridViewBubble.DataSource = dt;
                            GridViewBubble.DataBind();
                        }
                    }
                }
            }
            //sort with bubblesort
            //create a list from datatable using linq
            List<Person> persons = dt.AsEnumerable()
                .Select(row => new Person
                {
                    // assuming column 0's type is Nullable<long>
                    Id = row.Field<int>(0),
                    FirstName = String.IsNullOrEmpty(row.Field<string>(1))
                        ? "not found"
                        : row.Field<string>(1),
                    LastName=row.Field<string>(2),
                    Phone=row.Field<string>(3),
                    PostalCode = row.Field<string>(4),
                    Income = row.Field<int>(5),
                }).ToList();

            
            for (int i = 0; i < persons.Count - 1; i++)
                for (int j = 0; j < persons.Count - i - 1; j++)
                    if (String.Compare(persons[j].LastName,persons[j + 1].LastName)>0)
                    {
                        var tempVar = persons[j];
                        persons[j] = persons[j + 1];
                        persons[j + 1] = tempVar;
                    }
            GridViewBubble.DataSource = persons;
            GridViewBubble.DataBind();


        }
    }

    public class Person
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public int Income { get; set; }
    }
}