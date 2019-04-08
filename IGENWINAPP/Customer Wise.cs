using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using IGENWINAPP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGENWINAPP
{
    public partial class CustomerWise : Form
    {
        public CustomerWise()
        {
            InitializeComponent();
        }
        
        private void CustomerWise_Load(object sender, EventArgs e)
        {
            ParameterVlues parameterVlues = new ParameterVlues();

            ReportDocument cryRpt = new ReportDocument();

            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();
            Tables CrTables;

            string path = "E:\\IGENWinForm\\IGENWINAPP\\IGENWINAPP\\Reports\\Customer Wise.rpt";
            cryRpt.Load(path);


            parameterVlues.PraVal = "A039-0";

            cryRpt.SetParameterValue("Customer", parameterVlues.PraVal);

            crConnectionInfo.UserID = "sa";
            crConnectionInfo.Password = "#compaq123";
            crConnectionInfo.ServerName = "PE1-PC01\\SQLEXPRESS2008";

            crConnectionInfo.DatabaseName = "DMSIGENDB01";


            CrTables = cryRpt.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }


            crystalReportViewer1.ReportSource = cryRpt;
            crystalReportViewer1.Refresh();
        }
    }
}
