using System;
using System.Data;
using System.Web.UI;

namespace IWPS
{
    public partial class loi_input : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEnquiries();
                LoadNotes();
                txtLoiDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        private void LoadEnquiries()
        {
            try
            {
                string dept = SessionHelper.Dept(Session);
                string sql = @"SELECT ENQ_NO, ENQ_NO || ' - ' || NVL(PARTY_NAME, 'N/A') AS DISPLAY_TEXT
                               FROM MATPASS.TBL_IWPS_ENQ_HEADER
                               WHERE STATUS = 'PRICE-OPEN' AND DEPT_CODE = :p1
                               ORDER BY ENQ_NO";
                DataTable dt = DBHelper.ExecuteQuery(sql, DBHelper.P("p1", dept));
                ddlEnquiry.DataSource = dt;
                ddlEnquiry.DataTextField = "DISPLAY_TEXT";
                ddlEnquiry.DataValueField = "ENQ_NO";
                ddlEnquiry.DataBind();
                ddlEnquiry.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Enquiry --", ""));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err", $"toastr.error('Error loading enquiries: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        private void LoadNotes()
        {
            try
            {
                string sql = "SELECT NOTE_CODE, NOTE_TITLE FROM MATPASS.TBL_IWPS_LOI_NOTE_MASTER ORDER BY NOTE_TITLE";
                DataTable dt = DBHelper.ExecuteQuery(sql);
                cblNotes.DataSource = dt;
                cblNotes.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err", $"toastr.error('Error loading notes: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        protected void ddlEnquiry_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlPartyInfo.Visible = false;
            if (string.IsNullOrEmpty(ddlEnquiry.SelectedValue)) return;

            try
            {
                string sql = @"SELECT PARTY_NAME, AWARDED_VALUE FROM MATPASS.TBL_IWPS_ENQ_HEADER WHERE ENQ_NO = :p1";
                DataTable dt = DBHelper.ExecuteQuery(sql, DBHelper.P("p1", ddlEnquiry.SelectedValue));
                if (dt.Rows.Count > 0)
                {
                    lblPartyName.Text = dt.Rows[0]["PARTY_NAME"].ToString();
                    lblEnqValue.Text = dt.Rows[0]["AWARDED_VALUE"].ToString();
                    pnlPartyInfo.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err", $"toastr.error('Error loading party info: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                string staffNo = SessionHelper.StaffNo(Session);
                string dept = SessionHelper.Dept(Session);

                long seq = DBHelper.GetNextSeq("LOI_SEQ");
                string loiNo = $"LOI-{dept}-{DateTime.Today:yyyyMMdd}-{seq:D3}";

                string secDeposit = ddlSecurityDepositReqd.SelectedValue;
                string secAmount = secDeposit == "Y" ? txtSecurityAmount.Text.Trim() : "0";

                string sqlHeader = @"INSERT INTO MATPASS.TBL_IWPS_LOI_HEADER
                    (LOI_NO, ENQ_NO, LOI_DATE, VALIDITY_DATE, WORK_DESCRIPTION, CONTRACT_VALUE,
                     PAYMENT_TERMS, SECURITY_DEPOSIT_REQD, SECURITY_AMOUNT, STATUS,
                     CREATED_BY, CREATED_DATE, DEPT_CODE)
                    VALUES (:p1,:p2,TO_DATE(:p3,'YYYY-MM-DD'),TO_DATE(:p4,'YYYY-MM-DD'),:p5,:p6,:p7,:p8,:p9,'DRAFT',:p10,SYSTIMESTAMP,:p11)";

                DBHelper.ExecuteNonQuery(sqlHeader,
                    DBHelper.P("p1", loiNo),
                    DBHelper.P("p2", ddlEnquiry.SelectedValue),
                    DBHelper.P("p3", txtLoiDate.Text.Trim()),
                    DBHelper.P("p4", txtValidityDate.Text.Trim()),
                    DBHelper.P("p5", txtWorkDescription.Text.Trim()),
                    DBHelper.P("p6", txtContractValue.Text.Trim()),
                    DBHelper.P("p7", txtPaymentTerms.Text.Trim()),
                    DBHelper.P("p8", secDeposit),
                    DBHelper.P("p9", secAmount),
                    DBHelper.P("p10", staffNo),
                    DBHelper.P("p11", dept));

                // Insert selected notes
                foreach (System.Web.UI.WebControls.ListItem item in cblNotes.Items)
                {
                    if (item.Selected)
                    {
                        string sqlNote = @"INSERT INTO MATPASS.TBL_IWPS_LOI_NOTES (LOI_NO, NOTE_CODE, CREATED_BY, CREATED_DATE)
                                          VALUES (:p1, :p2, :p3, SYSTIMESTAMP)";
                        DBHelper.ExecuteNonQuery(sqlNote,
                            DBHelper.P("p1", loiNo),
                            DBHelper.P("p2", item.Value),
                            DBHelper.P("p3", staffNo));
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                    $"toastr.success('LOI {loiNo} created successfully.'); setTimeout(function(){{ window.location='print_loi_list.aspx'; }}, 2000);", true);
                ClearForm();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err", $"toastr.error('Error creating LOI: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("print_loi_list.aspx");
        }

        private void ClearForm()
        {
            ddlEnquiry.SelectedIndex = 0;
            pnlPartyInfo.Visible = false;
            txtLoiDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtValidityDate.Text = "";
            txtWorkDescription.Text = "";
            txtContractValue.Text = "";
            txtPaymentTerms.Text = "";
            ddlSecurityDepositReqd.SelectedIndex = 0;
            txtSecurityAmount.Text = "";
            foreach (System.Web.UI.WebControls.ListItem item in cblNotes.Items)
                item.Selected = false;
        }
    }
}
