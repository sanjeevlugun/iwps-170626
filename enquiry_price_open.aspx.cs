using System; using System.Data; using System.Web.UI.WebControls;
namespace IWPS {
    public partial class enquiry_price_open : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) { if (!IsPostBack) { LoadEnqDDL(); if (!string.IsNullOrEmpty(Request.QueryString["enq"])) { ddlEnq.SelectedValue = Request.QueryString["enq"]; LoadParties(); } } }
        void LoadEnqDDL() {
            try {
                ddlEnq.Items.Clear(); ddlEnq.Items.Add(new ListItem("-- Select Enquiry --",""));
                var dt = DBHelper.ExecuteQuery("SELECT ENQUIRY_NO,TITLE FROM MATPASS.TBL_IWPS_ENQUIRY_HEADER WHERE STATUS IN ('APPROVED','PART-I') ORDER BY ENQUIRY_NO");
                foreach (DataRow r in dt.Rows) ddlEnq.Items.Add(new ListItem($"{r["ENQUIRY_NO"]} - {r["TITLE"]}", r["ENQUIRY_NO"].ToString()));
            } catch { }
        }
        void LoadParties() {
            string no = ddlEnq.SelectedValue; if (string.IsNullOrEmpty(no)) return;
            try {
                gvParties.DataSource = DBHelper.ExecuteQuery("SELECT ep.PARTY_CODE,NVL(pm.PARTY_NAME,ep.PARTY_CODE) PARTY_NAME,ep.QUALIFIED_STATUS,ep.QUOTED_AMOUNT,ep.COUNTER_OFFER,ep.RANK FROM MATPASS.TBL_IWPS_ENQUIRY_PARTY ep LEFT JOIN MATPASS.TBL_IWPS_PARTY_MASTER pm ON ep.PARTY_CODE=pm.PARTY_CODE WHERE ep.ENQUIRY_NO=:n ORDER BY ep.RANK", DBHelper.P(":n",no));
                gvParties.DataBind(); divPrice.Visible = true; ViewState["SelEnq"] = no;
            } catch { }
        }
        protected void ddlEnq_Changed(object sender, EventArgs e) { LoadParties(); }
        protected void btnOpenPrice_Click(object sender, EventArgs e) {
            string no = ddlEnq.SelectedValue; if (string.IsNullOrEmpty(no)) return;
            try {
                DBHelper.ExecuteNonQuery("UPDATE MATPASS.TBL_IWPS_ENQUIRY_HEADER SET STATUS='PART-II' WHERE ENQUIRY_NO=:n AND STATUS='PART-I'", DBHelper.P(":n",no));
                DBHelper.ExecuteNonQuery("UPDATE MATPASS.TBL_IWPS_ENQUIRY_HEADER SET STATUS='PART-II' WHERE ENQUIRY_NO=:n AND STATUS='APPROVED'", DBHelper.P(":n",no));
                lblMsg.Text="Price opened."; ScriptManager.RegisterStartupScript(this,GetType(),"t","toastr.success('Price Opened!');",true);
            } catch (Exception ex) { lblMsg.Text="Error: "+ex.Message; }
            LoadParties();
        }
        protected void btnSaveQuotes_Click(object sender, EventArgs e) {
            string no = ViewState["SelEnq"]?.ToString(); if (string.IsNullOrEmpty(no)) return;
            try {
                decimal minAmt = decimal.MaxValue; int rank = 1;
                foreach (GridViewRow r in gvParties.Rows) {
                    string pc = gvParties.DataKeys[r.RowIndex].Value.ToString();
                    decimal amt = 0; decimal.TryParse(((TextBox)r.FindControl("txtAmt")).Text, out amt);
                    string co = ((DropDownList)r.FindControl("ddlCO")).SelectedValue;
                    DBHelper.ExecuteNonQuery("UPDATE MATPASS.TBL_IWPS_ENQUIRY_PARTY SET QUOTED_AMOUNT=:a,COUNTER_OFFER=:c WHERE ENQUIRY_NO=:n AND PARTY_CODE=:p", DBHelper.P(":a",amt), DBHelper.P(":c",co), DBHelper.P(":n",no), DBHelper.P(":p",pc));
                    if (amt < minAmt) minAmt = amt;
                }
                // rank
                var dt = DBHelper.ExecuteQuery("SELECT PARTY_CODE,QUOTED_AMOUNT FROM MATPASS.TBL_IWPS_ENQUIRY_PARTY WHERE ENQUIRY_NO=:n ORDER BY QUOTED_AMOUNT ASC", DBHelper.P(":n",no));
                int rk = 1;
                foreach (DataRow dr in dt.Rows) { DBHelper.ExecuteNonQuery("UPDATE MATPASS.TBL_IWPS_ENQUIRY_PARTY SET RANK=:r WHERE ENQUIRY_NO=:n AND PARTY_CODE=:p", DBHelper.P(":r",rk++), DBHelper.P(":n",no), DBHelper.P(":p",dr["PARTY_CODE"])); }
                lblMsg.Text="Quotes saved and ranked."; ScriptManager.RegisterStartupScript(this,GetType(),"t","toastr.success('Quotes saved & ranked!');",true);
            } catch (Exception ex) { lblMsg.Text="Error: "+ex.Message; }
            LoadParties();
        }
        protected void gvParties_RowCommand(object sender, GridViewCommandEventArgs e) {
            if (e.CommandName != "AWARD") return;
            string no = ViewState["SelEnq"]?.ToString(); string pc = e.CommandArgument.ToString();
            try {
                DBHelper.ExecuteNonQuery("UPDATE MATPASS.TBL_IWPS_ENQUIRY_PARTY SET AWARD_STATUS='Y' WHERE ENQUIRY_NO=:n AND PARTY_CODE=:p", DBHelper.P(":n",no), DBHelper.P(":p",pc));
                DBHelper.ExecuteNonQuery("UPDATE MATPASS.TBL_IWPS_ENQUIRY_HEADER SET STATUS='PRICE-OPEN' WHERE ENQUIRY_NO=:n", DBHelper.P(":n",no));
                lblMsg.Text=$"Party {pc} awarded."; ScriptManager.RegisterStartupScript(this,GetType(),"t","toastr.success('Party Awarded!');",true);
            } catch (Exception ex) { lblMsg.Text="Error: "+ex.Message; }
            LoadParties();
        }
    }
}
