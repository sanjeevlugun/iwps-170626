using System;
using System.Data;
using System.Web.UI.WebControls;
namespace IWPS {
    public partial class indent_wise_initiate_enquiry : System.Web.UI.Page {
        DataTable EnqParties {
            get { if (ViewState["EnqParties"] == null) { var dt = new DataTable(); dt.Columns.Add("PARTY_NAME"); dt.Columns.Add("PARTY_EMAIL"); dt.Columns.Add("PARTY_MOBILE"); dt.Columns.Add("EMD_REQD"); ViewState["EnqParties"] = dt; } return (DataTable)ViewState["EnqParties"]; }
            set { ViewState["EnqParties"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                txtEnqDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                DataTable dt = DBHelper.ExecuteQuery("SELECT INDENT_NO FROM MATPASS.TBL_IWPS_INDENT_HEADER WHERE STATUS='APPROVED' ORDER BY INDENT_NO");
                ddlIndent.DataSource = dt; ddlIndent.DataTextField = "INDENT_NO"; ddlIndent.DataValueField = "INDENT_NO"; ddlIndent.DataBind();
                ddlIndent.Items.Insert(0, new ListItem("-- Select Indent --", ""));
            }
        }
        protected void ddlIndent_SelectedIndexChanged(object sender, EventArgs e) {
            if (ddlIndent.SelectedValue == "") { pnlItems.Visible = false; return; }
            try {
                DataTable dt = DBHelper.ExecuteQuery(@"SELECT ii.ITEM_CODE, im.ITEM_NAME ITEM_DESCRIPTION, im.UOM, ii.QTY FROM MATPASS.TBL_IWPS_INDENT_ITEM ii JOIN MATPASS.TBL_IWPS_ITEM_MASTER im ON im.ITEM_CODE=ii.ITEM_CODE WHERE ii.INDENT_NO=:ind", DBHelper.P("ind", ddlIndent.SelectedValue));
                gvItems.DataSource = dt; gvItems.DataBind(); pnlItems.Visible = true;
            } catch (Exception ex) {
                ClientScript.RegisterStartupScript(GetType(), "err", $"toastr.error('{ex.Message.Replace("'","\\'")}');", true);
            }
        }
        protected void btnAddParty_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(txtPartyName.Text)) { ClientScript.RegisterStartupScript(GetType(), "w", "toastr.warning('Enter party name.');", true); return; }
            var parties = EnqParties;
            parties.Rows.Add(txtPartyName.Text.Trim(), txtPartyEmail.Text.Trim(), txtPartyMobile.Text.Trim(), ddlEmd.SelectedValue);
            EnqParties = parties; gvParties.DataSource = EnqParties; gvParties.DataBind();
            txtPartyName.Text = ""; txtPartyEmail.Text = ""; txtPartyMobile.Text = "";
        }
        protected void RemoveParty_Command(object sender, CommandEventArgs e) {
            var parties = EnqParties; parties.Rows.RemoveAt(Convert.ToInt32(e.CommandArgument)); EnqParties = parties;
            gvParties.DataSource = EnqParties; gvParties.DataBind();
        }
        protected void btnSubmit_Click(object sender, EventArgs e) {
            if (ddlIndent.SelectedValue == "" || ddlEnqType.SelectedValue == "") { ClientScript.RegisterStartupScript(GetType(), "w", "toastr.warning('Select indent and enquiry type.');", true); return; }
            if (EnqParties.Rows.Count == 0) { ClientScript.RegisterStartupScript(GetType(), "w", "toastr.warning('Add at least one party.');", true); return; }
            try {
                string dept = SessionHelper.Dept(Session);
                string date = DateTime.Now.ToString("yyyyMMdd");
                int seq = DBHelper.GetNextSeq("ENQ_SEQ");
                string enqNo = $"ENQ-{dept}-{date}-{seq:D3}";
                DBHelper.ExecuteNonQuery(@"INSERT INTO MATPASS.TBL_IWPS_ENQUIRY_HEADER (ENQ_NO,ENQ_TYPE,ENQ_DATE,INDENT_NO,DESCRIPTION,EST_VALUE,DEPT,STATUS,CREATED_BY,CREATED_DATE) VALUES (:eno,:ety,:ed,:ind,:desc,:ev,:dept,'INITIATED',:cb,SYSTIMESTAMP)",
                    DBHelper.P("eno", enqNo), DBHelper.P("ety", ddlEnqType.SelectedValue), DBHelper.PDate("ed", Convert.ToDateTime(txtEnqDate.Text)),
                    DBHelper.P("ind", ddlIndent.SelectedValue), DBHelper.P("desc", txtDesc.Text.Trim()), DBHelper.P("ev", txtEstValue.Text.Trim()),
                    DBHelper.P("dept", dept), DBHelper.P("cb", SessionHelper.StaffNo(Session)));
                foreach (GridViewRow row in gvItems.Rows) {
                    string ic = row.Cells[0].Text;
                    string idesc = row.Cells[1].Text;
                    string uom = row.Cells[2].Text;
                    string qty = ((TextBox)row.FindControl("txtQty")).Text.Trim();
                    DBHelper.ExecuteNonQuery("INSERT INTO MATPASS.TBL_IWPS_ENQUIRY_ITEMS (ENQ_NO,ITEM_CODE,ITEM_DESCRIPTION,UOM,QTY) VALUES (:eno,:ic,:id,:uom,:qty)",
                        DBHelper.P("eno", enqNo), DBHelper.P("ic", ic), DBHelper.P("id", idesc), DBHelper.P("uom", uom), DBHelper.P("qty", qty));
                }
                foreach (DataRow r in EnqParties.Rows)
                    DBHelper.ExecuteNonQuery("INSERT INTO MATPASS.TBL_IWPS_ENQUIRY_PARTY (ENQ_NO,PARTY_NAME,PARTY_EMAIL,PARTY_MOBILE,EMD_REQD) VALUES (:eno,:pn,:pe,:pm,:em)",
                        DBHelper.P("eno", enqNo), DBHelper.P("pn", r["PARTY_NAME"]), DBHelper.P("pe", r["PARTY_EMAIL"]), DBHelper.P("pm", r["PARTY_MOBILE"]), DBHelper.P("em", r["EMD_REQD"]));
                DBHelper.ExecuteNonQuery("INSERT INTO MATPASS.TBL_IWPS_ENQUIRY_PROCESS_LOGS (ENQ_NO,ACTION,ACTION_BY,ACTION_DATE,REMARKS) VALUES (:eno,'INITIATED',:ab,SYSTIMESTAMP,'Enquiry initiated from indent')",
                    DBHelper.P("eno", enqNo), DBHelper.P("ab", SessionHelper.StaffNo(Session)));
                ViewState["EnqParties"] = null; gvParties.DataSource = new DataTable(); gvParties.DataBind();
                ClientScript.RegisterStartupScript(GetType(), "ok", $"toastr.success('Enquiry {enqNo} created.');", true);
            } catch (Exception ex) {
                ClientScript.RegisterStartupScript(GetType(), "err", $"toastr.error('{ex.Message.Replace("'","\\'")}');", true);
            }
        }
    }
}
