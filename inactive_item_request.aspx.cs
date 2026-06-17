using System;
using System.Data;
namespace IWPS {
    public partial class inactive_item_request : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                DataTable dt = DBHelper.ExecuteQuery("SELECT ITEM_CODE, ITEM_NAME FROM MATPASS.TBL_IWPS_ITEM_MASTER WHERE STATUS='ACTIVE' ORDER BY ITEM_NAME");
                ddlItem.DataSource = dt; ddlItem.DataTextField = "ITEM_NAME"; ddlItem.DataValueField = "ITEM_CODE"; ddlItem.DataBind();
                ddlItem.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Item --", ""));
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e) {
            if (ddlItem.SelectedValue == "" || string.IsNullOrWhiteSpace(txtReason.Text)) {
                ClientScript.RegisterStartupScript(GetType(), "w", "toastr.warning('Select item and enter reason.');", true); return;
            }
            try {
                DBHelper.ExecuteNonQuery("INSERT INTO MATPASS.TBL_IWPS_ITEM_MASTER_LOGS (ITEM_CODE,ACTION,ACTION_BY,ACTION_DATE,REMARKS) VALUES (:ic,'INACTIVE_REQ',:ab,SYSTIMESTAMP,:rem)",
                    DBHelper.P("ic", ddlItem.SelectedValue), DBHelper.P("ab", SessionHelper.StaffNo(Session)), DBHelper.P("rem", txtReason.Text.Trim()));
                ClientScript.RegisterStartupScript(GetType(), "ok", "toastr.success('Item inactivation request submitted.');", true);
                ddlItem.SelectedIndex = 0; txtReason.Text = "";
            } catch (Exception ex) {
                ClientScript.RegisterStartupScript(GetType(), "err", $"toastr.error('{ex.Message.Replace("'","\\'")}');", true);
            }
        }
    }
}
