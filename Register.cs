
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
using System.Data.OleDb;



namespace Atmsystem
{
    public partial class Regs_frm
    {
        public Regs_frm(object txtaddr = null)
        {

            constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "\\ATMsystem.accdb";
            conn = new System.Data.OleDb.OleDbConnection(constr);

            InitializeComponent();
            this.txtaddr = (TextBox)txtaddr;
        }
        public new FormClosedEventHandler FormClosed { get; private set; }

        public object Txtfname => txtfname;

        public object Txtcontact => txtcontact;

        public object Cbday => Cbday1;

        public object TxtPinCode => txtPincode;
        public OleDbDataAdapter Adapt { get; private set; }
        public object Cbday1 { get; }

        public object Cbday2 => Cbday1;


        readonly string constr;

        public Regs_frm(OleDbDataAdapter adapt1, object cbday)
        {
            this.Adapt = adapt1;

            this.Cbday1 = cbday;
        }
        readonly System.Data.OleDb.OleDbConnection conn;

        public Regs_frm(object cbyear, object txtaddr = null)
        {
            this.cbyear = (ComboBox)cbyear;
            this.txtaddr = (TextBox)txtaddr;
        }

        public System.Data.OleDb.OleDbConnection GetConn()
        {
            return conn;
        }

        public object GetCbyear()
        {
            return cbyear;
        }

        public void BtnRegister_Click(object sender, EventArgs e, OleDbConnection conn, object cbyear)
        {
            BtnRegister_Click(sender, e, conn, cbyear, txtcontact, GetCbmonth());
        }

        public ComboBox GetCbmonth()
        {
            return cbmonth;
        }

        public void BtnRegister_Click(object sender, EventArgs e, OleDbConnection conn, object cbyear, TextBox txtcontact, ComboBox cbmonth)
        {
            BtnRegister_Click(sender, e, conn, cbyear, txtcontact, cbmonth, cbGender);
        }

        public void BtnRegister_Click(object sender, EventArgs e, OleDbConnection conn, object cbyear, TextBox txtcontact, ComboBox cbmonth, ComboBox cbGender)
        {
            BtnRegister_Click(sender, e, conn, cbyear, txtcontact, cbmonth, cbGender, txtPincode, GetTxtAcctNo());
        }

        public TextBox GetTxtAcctNo()
        {
            return txtAcctNo;
        }

        public void BtnRegister_Click(System.Object sender, System.EventArgs e, System.Data.OleDb.OleDbConnection conn, object cbyear, TextBox txtcontact, ComboBox cbmonth, ComboBox cbGender, TextBox txtPincode, TextBox txtAcctNo)
        {
            if (sender is null)
            {
                throw new ArgumentNullException(nameof(sender));
            }

            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }

#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast
            if (Cbday1 != "" || cbyear != "")
            {
                if (Cbday1 != "" || cbyear != "")
                {

                    MessageBox.Show("Pls Complete all Fields");
                }
                else
                {
                    System.Data.OleDb.OleDbDataAdapter adapt1 = new System.Data.OleDb.OleDbDataAdapter("select * from tblinfo where Firstname=\'" + txtfname + "\'", conn);
                    DataSet dset1 = new DataSet();
                    adapt1.Fill(dset1);
                    if (dset1.Tables[0].Rows.Count != 0)
                    {
                        MessageBox.Show("Account name already exist");
                    }
                    else
                    {
                        string dbcommand = "INSERT into tblinfo (account_no, Firstname, Lastname, Address, Contact_no, Gender, Birthday, pin_code , type, balance)" + " VALUES (\'" + (txtAcctNo + "\',\'") + txtfname + "\',\'" + txtlname + "\',\'" + txtaddr + "\',\'" + txtcontact + "\',\'" + cbGender + "\',\'" + cbmonth
                             + Cbday1 + cbyear + "\',\'" + txtPincode + "\',\'" + "Active" + "\',\'" + "1000" + "\')";
                        System.Data.OleDb.OleDbDataAdapter adapt = new System.Data.OleDb.OleDbDataAdapter(dbcommand, conn);
                        DataSet dset = new DataSet();
                        _ = adapt.Fill(dset);



                        MessageBox.Show("You Have Succesfully Registered!");
                        this.Hide();
                        atmsystem.Login_frm.Default.Show();
                    }
                }
#pragma warning restore CS0252 // Possible unintended reference comparison; left hand side needs cast
            }
            else
            {
                MessageBox.Show("Enter All Fields");

            }
        }

        private new void Hide()
        {
            throw new NotImplementedException();
        }

        public object Lbldate => lbldate;
        public void Regs_frm_Load()
        {
            _ = DateTime.Now.ToString();

            conn.ConnectionString = constr;
            conn.Open();
        }

        public override bool Equals(object obj)
        {
            return obj is Regs_frm frm &&
                   EqualityComparer<object>.Default.Equals(txtfname, frm.txtfname);
        }

        public override int GetHashCode()
        {
            return 511713834 + EqualityComparer<object>.Default.GetHashCode(Cbday1);
        }
    }
}
