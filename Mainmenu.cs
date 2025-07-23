
using System.Diagnostics;
using System;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using Microsoft.VisualBasic;
using System.Data;
using System.Collections.Generic;
using System.Linq;



namespace atmsystem
{
    public partial class Mainmenu
    {
        public Mainmenu()
        {
            InitializeComponent();

        }

        #region Default Instance

        private static Mainmenu defaultInstance;


        public static Mainmenu Default
        {
            get
            {
                if (defaultInstance == null)
                {
                    defaultInstance = new Mainmenu();
                    defaultInstance.FormClosed += new FormClosedEventHandler(DefaultInstance_FormClosed);
                }

                return defaultInstance;
            }
        }

        public DataSet Ds => ds;

        static void DefaultInstance_FormClosed(object sender, FormClosedEventArgs e)
        {
            defaultInstance = null;
        }

        #endregion
        readonly System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();
        readonly System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter();
        private static readonly DataSet dataSet = new DataSet();
        readonly DataSet ds = dataSet;
        readonly System.Data.OleDb.OleDbConnection con = new System.Data.OleDb.OleDbConnection();
        readonly string sql;

        public Mainmenu(string sql)
        {
            this.sql = sql;
        }

        public void Label4_Click(System.Object sender, System.EventArgs e)
        {

        }

        public void Button4_Click(System.Object sender, System.EventArgs e)
        {
            Login_frm.Default.Show();
            this.Hide();
        }

        public void Mainmenu_Load(System.Object sender, System.EventArgs e)
        {
            lbldate.Text = DateTime.Now.ToString();


        }

        public string GetSql()
        {
            return sql;
        }

        public System.Data.OleDb.OleDbCommand GetCmd()
        {
            return cmd;
        }

        public void Button1_Click(object sender, EventArgs e, string sql, System.Data.OleDb.OleDbCommand cmd)
        {
            if (sender is null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            if (sql is null)
            {
                throw new ArgumentNullException(nameof(sql));
            }

            DataTable Log_in = new DataTable();
            try
            {




                con.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\ATMsystem.accdb";
                cmd.Connection = con;
                cmd.CommandText = "SELECT * FROM tblinfo where  account_no = " + lblaccno.Text + "";
                da.SelectCommand = cmd;
                da.Fill(Log_in);
                if (Log_in.Rows.Count > 0)
                {
                    string balance = default;

                    balance = (string)(Log_in.Rows[0]["balance"]);

                    Receipt.Default.Show();
                    Receipt.Default.lblname.Text = lblname.Text;
                    //Receipt.lblaccno.Text = lblaccno.Text
                    Receipt.Default.lblbal.Text = balance;
                    Receipt.Default.Label4.Hide();
                    Receipt.Default.Label3.Hide();
                    Receipt.Default.lbldep.Hide();
                    Receipt.Default.lblwith.Hide();
                    Receipt.Default.Label6.Hide();
                    Receipt.Default.lblnewbal.Hide();

                    this.Hide();


                }
                else
                {
                    MessageBox.Show("Pincode is incorrect");

                }






            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        public void Btnwith_Click(System.Object sender, System.EventArgs e)
        {
            Withdraw.Default.Show();
            this.Hide();

        }

        public void Btndep_Click(System.Object sender, System.EventArgs e)
        {
            Deposit.Default.Show();
            Hide();
        }

        public void Lblname_Click(System.Object sender, System.EventArgs e)
        {

        }

        public void GroupBox1_Enter(System.Object sender, System.EventArgs e)
        {

        }

        public void Button2_Click(System.Object sender, System.EventArgs e)
        {

        }
    }
}
