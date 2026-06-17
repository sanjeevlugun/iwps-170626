using System;
using System.Data;
using System.Web.UI.WebControls;
namespace IWPS {
    public partial class category_wise_initiate_enquiry : System.Web.UI.Page {
        DataTable EnqItems {
            get { if (ViewState["EnqItems"] == null) { var dt = new DataTable(); dt.Columns.Add("ITEM_CODE"); dt.Columns.Add("ITEM_DESCRIPTION"); dt.Columns.Add("UOM"); dt.Columns.Add("QTY"); ViewState["EnqItems"] = dt; } return (DataTable)ViewState["EnqItems"]; }
            set { ViewState["EnqItems"] = value; }
        }
        DataTable EnqParties {
            get { if (ViewState["EnqParties"] == null) { var dt = new DataTable(); dt.Columns.Add("PARTY_NAME"); dt.Columns.Add("PARTY_EMAIL"); dt.Columns.Add("PARTY_MOBILE"); dt.Columns.Add("EMD_REQD"); ViewState["EnqParties"] = dt; } return (DataTable)ViewState["EnqParties"]; }
            set { ViewState["EnqParties"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                txtEnqDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                LoadCategories();
            }
        }
        void LoadCategories() {
            DataTable dt = DBHelper.ExecuteQuery("SELECT CATEGORY_CODE, CATEGORY_NAME FROM MATPASS.TBL_IWPS_CATEGORY_MASTER WHERE STATUS='ACTIVE' ORDER BY CATEGORY_NAME");
            ddlCategory.DataSource = dt; ddlCategory.DataTextField = "CATEGORY_NAME"; ddlCategory.DataValueField = "CATEGORY_CODE"; ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("-- Select Category --", ""));
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e) {
            ddlItem.Items.Clear(); ddlItem.Items.Add(new ListItem("-- Select Item --", ""));
            if (ddlCategory.SelectedValue == "") return;
            DataTable dt = DBHelper.ExecuteQuery("SELECT ITEM_CODE, ITEM_NAME FROM MATPASS.TBL_IWPS_ITEM_MASTER WHERE CATEGORY_CODE=:c AND STATUS='ACTIVE' ORDER BY ITEM_NAME", DBHelper.P("c", ddlCategory.SelectedValue));
            ddlItem.DataSource = dt; ddlItem.DataTextField = "ITEM_NAME"; ddlItem.DataValueField = "ITEM_CODE"; ddlItem.DataBind();
            ddlItem.Items.Insert(0, new ListItem("-- Select Item --", ""));
        }
        protected void btnAddItem_Click(object sender, EventArgs e) {
            if (ddlItem.SelectedValue == "" || string.IsNullOrWhiteSpace(txtQty.Text)) { ClientScript.RegisterStartupScript(GetType(), "w", "toastr.warning('Select item and enter qty.');", true); return; }
            DataTable dt = DBHelper.ExecuteQuery("SELECT ITEM_CODE, ITEM_NAME ITEM_DESCRIPTION, UOM FROM MATPASS.TBL_IWPS_ITEM_MASTER WHERE ITEM_CODE=:c", DBHelper.P("c", ddlItem.SelectedValue));
            if (dt.Rows.Count > 0) {
                var items = EnqItems;
                items.Rows.Add(dt.Rows[0]["ITEM_CODE"], dt.Rows[0]["ITEM_DESCRIPTION"], dt.Rows[0]["UOM"], txtQty.Text.Trim());
                EnqItems = items;
                gvItems.DataSource = EnqItems; gvItems.DataBind();
                txtQty.Text = "";
            }
        }
        protected void RemoveItem_Command(object sender, CommandEventArgs e) {
            var items = EnqItems; items.Rows.RemoveAt(Convert.ToInt32(e.CommandArgument)); EnqItems = items;
            gvItems.DataSource = EnqItems; gvItems.DataBind();
        }
        protected void btnAddParty_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(txtPartyName.Text)) { ClientScript.RegisterStartupScript(GetType(), "w", "toastr.warning('Enter party name.');", true); return; }
            var parties = EnqParties;
            parties.Rows.Add(txtPartyName.Text.Trim(), txtPartyEmail.Text.Trim(), txtPartyMobile.Text.Trim(), ddlEmd.SelectedValue);
            EnqParties = parties;
            gvParties.DataSource = EnqParties; gvParties.DataBind();
            txtPartyName.Text = ""; txtPartyEmail.Text = ""; txtPartyMobile.Text = "";
        }
        protected void RemoveParty_Command(object sender, CommandEventArgs e) {
            var parties = EnqParties; parties.Rows.RemoveAt(Convert.ToInt32(e.CommandArgument)); EnqParties = parties;
            gvParties.DataSource = EnqParties; gvParties.DataBind();
        }
        protected void btnSubmit_Click(object sender, EventArgs e) {
            if (ddlEnqType.SelectedValue == "" || ddlCategory.SelectedValue == "" || string.IsNullOrWhiteSpace(txtDesc.Text)) {
                ClientScript.RegisterStartupScript(GetType(), "w", "toastr.warning('Fill all required fields.');", true); return;
            }
            if (EnqItems.Rows.Count == 0) { ClientScript.RegisterStartupScript(GetType(), "w", "toastr.warning('Add at least one item.');", true); return; }
            if (EnqParties.Rows.Count == 0) { ClientScript.RegisterStartupScript(GetType(), "w", "toastr.warning('Add at least one party.');", true); return; }
            try {
                string dept = SessionHelper.Dept(Session);
                string date = DateTime.Now.ToString("yyyyMMdd");
                int seq = DBHelper.GetNextSeq("ENQ_SEQ");
                string enqNo = $"ENQ-{dept}-{date}-{seq:D3}";
                DBHelper.ExecuteNonQuery(@"INSERT INTO MATPASS.TBL_IWPS_ENQUIRY_HEADER (ENQ_NO,ENQ_TYPE,ENQ_DATE,CATEGORY_CODE,DESCRIPTION,EST_VALUE,DEPT,STATUS,CREATED_BY,CREATED_DATE) VALUES (:eno,:ety,:ed,:cat,:desc,:ev,:dept,'INITIATED',:cb,SYSTIMESTAMP)",
                    DBHelper.P("eno", enqNo), DBHelper.P("ety", ddlEnqType.SelectedValue), DBHelper.PDate("ed", Convert.ToDateTime(txtEnqDate.Text)),
                    DBHelper.P("cat", ddlCategory.SelectedValue), DBHelper.P("desc", txtDesc.Text.Trim()), DBHelper.P("ev", txtEstValue.Text.Trim()),
                    DBHelper.P("dept", dept), DBHelper.P("cb", SessionHelper.StaffNo(Session)));
                foreach (DataRow r in EnqItems.Rows)
                    DBHelper.ExecuteNonQuery("INSERT INTO MATPASS.TBL_IWPS_ENQUIRY_ITEMS (ENQ_NO,ITEM_CODE,ITEM_DESCRIPTION,UOM,QTY) VALUES (:eno,:ic,:id,:uom,:qty)",
                        DBHelper.P("eno", enqNo), DBHelper.P("ic", r["ITEM_CODE"]), DBHelper.P("id", r["ITEM_DESCRIPTION"]), DBHelper.P("uom", r["UOM"]), DBHelper.P("qty", r["QTY"]));
                foreach (DataRow r in EnqParties.Rows)
                    DBHelper.ExecuteNonQuery("INSERT INTO MATPASS.TBL_IWPS_ENQUIRY_PARTY (ENQ_NO,PARTY_NAME,PARTY_EMAIL,PARTY_MOBILE,EMD_REQD) VALUES (:eno,:pn,:pe,:pm,:em)",
                        DBHelper.P("eno", enqNo), DBHelper.P("pn", r["PARTY_NAME"]), DBHelper.P("pe", r["PARTY_EMAIL"]), DBHelper.P("pm", r["PARTY_MOBILE"]), DBHelper.P("em", r["EMD_REQD"]));
                DBHelper.ExecuteNonQuery("INSERT INTO MATPASS.TBL_IWPS_ENQUIRY_PROCESS_LOGS (ENQ_NO,ACTION,ACTION_BY,ACTION_DATE,REMARKS) VALUES (:eno,'INITIATED',:ab,SYSTIMESTAMP,'Enquiry initiated')",
                    DBHelper.P("eno", enqNo), DBHelper.P("ab", SessionHelper.StaffNo(Session)));
                ViewState["EnqItems"] = null; ViewState["EnqParties"] = null;
                gvItems.DataSource = new DataTable(); gvItems.DataBind();
                gvParties.DataSource = new DataTable(); gvParties.DataBind();
                ClientScript.RegisterStartupScript(GetType(), "ok", $"toastr.success('Enquiry {enqNo} created.');", true);
            } catch (Exception ex) {
                ClientScript.RegisterStartupScript(GetType(), "err", $"toastr.error('{ex.Message.Replace("'","\\'")}');", true);
            }
        }
    }
}
