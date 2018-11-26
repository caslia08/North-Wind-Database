using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NorthWindDB
{
    public partial class Customer_form : Form
    {
        OleDbConnection conn;
        OleDbCommand CustomerCommand;
        OleDbDataAdapter CustomerAdapter;
        DataTable CustomerTable;
        CurrencyManager CustomerManager;
        public Customer_form()
        {
            InitializeComponent();
        }

        private void Customers_Load(object sender, EventArgs e)
        {
            
            var connectionstring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Alia\Documents\NorthWind.accdb;
            Persist Security Info = False; ";

            conn = new OleDbConnection(connectionstring);
            conn.Open();
            
            CustomerCommand = new OleDbCommand("Select * from Customers", conn);
            CustomerAdapter = new OleDbDataAdapter();
            CustomerAdapter.SelectCommand = CustomerCommand;
            CustomerTable = new DataTable();
            CustomerAdapter.Fill(CustomerTable);
            

            //Data Binding

            txtCustID.DataBindings.Add("Text",CustomerTable,"CustomerID");
            txtCompName.DataBindings.Add("Text",CustomerTable,"CompanyName");
            txtContName.DataBindings.Add("Text",CustomerTable,"ContactName");
            txtContTitle.DataBindings.Add("Text",CustomerTable,"ContactTitle");

            //CURRENCY manager 

            CustomerManager = (CurrencyManager)BindingContext[CustomerTable];


            conn.Close();
            conn.Dispose();
            CustomerAdapter.Dispose();
            CustomerCommand.Dispose();
            CustomerTable.Dispose();


            

        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            CustomerManager.Position = 0;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            CustomerManager.Position++;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            CustomerManager.Position--;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            CustomerManager.Position = CustomerManager.Count - 1;
        }
    }
}
