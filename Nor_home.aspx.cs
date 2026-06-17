using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace IWPS
{
    public partial class Nor_home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadDashboard();
        }

        private void LoadDashboard()
        {
            string staffNo = SessionHelper.StaffNo(Session);
            try
            {
                object cnt;
                var p = new OracleParameter[] { new OracleParameter("staffno", staffNo) };

                cnt = DBHelper.ExecuteScalar("SELECT COUNT(*) FROM MATPASS.TBL_IWPS_INDENT_HEADER WHERE STATUS='DRAFT' AND INITIATOR=:staffno", p);
                lblMyIndents.Text = cnt != null ? cnt.ToString() : "0";

                cnt = DBHelper.ExecuteScalar("SELECT COUNT(*) FROM MATPASS.TBL_IWPS_ENQUIRY_HEADER WHERE STATUS='DRAFT' AND INITIATOR=:staffno", p);
                lblMyEnquiries.Text = cnt != null ? cnt.ToString() : "0";

                cnt = DBHelper.ExecuteScalar("SELECT COUNT(*) FROM MATPASS.TBL_IWPS_LOI_HEADER WHERE STATUS='DRAFT' AND INITIATOR=:staffno", p);
                lblMyLOIs.Text = cnt != null ? cnt.ToString() : "0";

                cnt = DBHelper.ExecuteScalar("SELECT COUNT(*) FROM MATPASS.TBL_IWPS_WO_HEADER WHERE WO_STATUS='DRAFT' AND INITIATOR=:staffno", p);
                lblMyWOs.Text = cnt != null ? cnt.ToString() : "0";

                DataTable dtIndents = DBHelper.ExecuteQuery("SELECT * FROM (SELECT INDENT_NO, INDENT_DATE, TITLE, INDENT_DEPT, STATUS FROM MATPASS.TBL_IWPS_INDENT_HEADER WHERE INITIATOR=:staffno ORDER BY INITIATION_DATE DESC) WHERE ROWNUM<=5", p);
                gvMyIndents.DataSource = dtIndents;
                gvMyIndents.DataBind();

                DataTable dtWOs = DBHelper.ExecuteQuery("SELECT * FROM (SELECT WO_NO, WO_DATE, NATURE_OF_WORK, DEPT, WO_STATUS FROM MATPASS.TBL_IWPS_WO_HEADER WHERE INITIATOR=:staffno ORDER BY WO_DATE DESC) WHERE ROWNUM<=5", p);
                gvMyWOs.DataSource = dtWOs;
                gvMyWOs.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("NOR Dashboard error: " + ex.Message);
                gvMyIndents.DataSource = new DataTable();
                gvMyIndents.DataBind();
                gvMyWOs.DataSource = new DataTable();
                gvMyWOs.DataBind();
            }
        }
    }
}
