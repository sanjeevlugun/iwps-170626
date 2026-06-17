using System;
using System.Data;
using System.Web.UI.WebControls;
namespace IWPS {
    public partial class enquiry_party_clcid_update : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                DataTable dt = DBHelper.ExecuteQuery("SELECT ENQ_NO FROM MATPASS.TBL_IWPS_ENQUIRY_HEADER ORDER BY ENQ_NO");
                ddlEnquiry.DataSource = dt; ddlEnquiry.DataTextField = "ENQ_NO"; ddlEnquiry.DataValueField = "ENQ_NO"; ddlEnquiry.DataBind();
                ddlEnquiry.Items.Insert(0, new ListItem("-- Select Enquiry --", ""));
            }
        }
        protected void ddlEnquiry_SelectedIndexChanged(object sender, EventArgs e) {
            if (ddlEnquiry.SelectedValue == "") { pnlGrid.Visible = false; return; }
            LoadParties();
        }
        void LoadParties() {
            try {
                DataTable dt = DBHelper.ExecuteQuery("SELECT ENQ_PARTY_ID, PARTY_NAME, NVL(CLC_ID,'-') CLC_ID FROM MATPASS.TBL_IWPS_ENQUIRY_PARTY WHERE ENQ_NO=:e ORDER BY ENQ_PARTY_ID", DBHelper.P("e", ddlEnquiry.SelectedValue));
                gvParties.DataSource = dt; gvParties.DataBind(); pnlGrid.Visible = true;
            } catch (Exception ex) {
                ClientScript.RegisterStartupScript(GetType(), "err", $"toastr.error('{ex.Message.Replace("'","\\'")}');", true);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e) {
            try {
                foreach (GridViewRow row in gvParties.Rows) {
                    string id = gvParties.DataKeys[row.RowIndex].Value.ToString();
                    string clc = ((TextBox)row.FindControl("txtClc")).Text.Trim();
                    DBHelper.ExecuteNonQuery("UPDATE MATPASS.TBL_IWPS_ENQUIRY_PARTY SET CLC_ID=:clc WHERE ENQ_PARTY_ID=:id", DBHelper.P("clc", clc), DBHelper.P("id", id));
                }
                ClientScript.RegisterStartupScript(GetType(), "ok", "toastr.success('CLC IDs updated.');", true);
                LoadParties();
            } catch (Exception ex) {
                ClientScript.RegisterStartupScript(GetType(), "err", $"toastr.error('{ex.Message.Replace("'","\\'")}');", true);
            }
        }
    }
}
