using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Diploma.Reports
{
    public partial class ReportTemplate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    String reportFolder = "ОтчетТест";
                    ReportViewer1.Height = Unit.Pixel(Convert.ToInt32(Request["Height"])-58);
                    ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                    ReportViewer1.ServerReport.ReportServerUrl = new Uri("http://192.168.82.36/ReportServer");
                    ReportViewer1.ServerReport.ReportPath = String.Format("/{0}/{1}",reportFolder,Request["Report3"].ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}