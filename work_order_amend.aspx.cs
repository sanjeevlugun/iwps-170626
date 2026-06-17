using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace IWPS
{
    public partial class work_order_amend : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) LoadWOs();
        }

        void LoadWOs()
        {
            try
            {
                DataTable dt = DBHelper.ExecuteQuery("SELECT WO_NO,CONTRACT_VALUE,WO_END_DATE FROM MATPASS.TBL_IWPS_WO_HEADER WHERE STATUS='APPROVED' ORDER BY WO_NO");
                ddlWO.Items.Clear();
                ddlWO.Items.Add(new System.Web.UI.WebControls.ListItem("-- Select WO --", ""));
                foreach (DataRow r in dt.Rows)
                    ddlWO.Items.Add(new System.Web.UI.WebControls.ListItem(r["WO_NO"].ToString(), r["WO_NO"].ToString()));
            }
            catch { }
        }

        protected void ddlWO_Changed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlWO.SelectedValue)) return;
            try
            {
                DataTable dt = DBHelper.ExecuteQuery("SELECT CONTRACT_VALUE,WO_END_DATE FROM MATPASS.TBL_IWPS_WO_HEADER WHERE WO_NO=:w",
                    DBHelper.P("w", ddlWO.SelectedValue));
                if (dt.Rows.Count > 0)
                {
                    txtCurCV.Text = string.Format("{0:N2}", dt.Rows[0]["CONTRACT_VALUE"]);
                    txtCurEnd.Text = dt.Rows[0]["WO_END_DATE"] == DBNull.Value ? "" : Convert.ToDateTime(dt.Rows[0]["WO_END_DATE"]).ToString("dd-MMM-yyyy");
                }
            }
            catch { }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlWO.SelectedValue)) return;
            try
            {
                DBHelper.ExecuteNonQuery(@"INSERT INTO MATPASS.TBL_IWPS_WO_AMENDMENT
                    (WO_NO,AMEND_REASON,NEW_END_DATE,NEW_CONTRACT_VALUE,STATUS,CREATED_BY,CREATED_DATE)
                    VALUES(:w,:r,:ned,:ncv,'INITIATED',:u,SYSTIMESTAMP)",
                    DBHelper.P("w", ddlWO.SelectedValue),
                    DBHelper.P("r", txtReason.Text),
                    DBHelper.PDate("ned", string.IsNullOrEmpty(txtNewEnd.Text) ? (DateTime?)null : DateTime.Parse(txtNewEnd.Text)),
                    DBHelper.P("ncv", string.IsNullOrEmpty(txtNewCV.Text) ? (object)DBNull.Value : decimal.Parse(txtNewCV.Text)),
                    DBHelper.P("u", SessionHelper.StaffNo(Session)));
                ClientScript.RegisterStartupScript(GetType(), "ok", "toastr.success('Amendment request submitted.');", true);
                txtReason.Text = txtNewEnd.Text = txtNewCV.Text = txtCurCV.Text = txtCurEnd.Text = "";
                ddlWO.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "err", "toastr.error('" + ex.Message.Replace("'","") + "');", true);
            }
        }
    }
}
