using System;
using System.Data;

namespace IWPS
{
    public partial class loi_print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string loiNo = Request.QueryString["id"];
                if (string.IsNullOrEmpty(loiNo))
                {
                    Response.Write("<script>alert('Invalid LOI reference.'); window.close();</script>");
                    return;
                }
                LoadLoi(loiNo);
            }
        }

        private void LoadLoi(string loiNo)
        {
            try
            {
                string sql = @"SELECT h.LOI_NO, h.ENQ_NO, h.LOI_DATE, h.VALIDITY_DATE, h.WORK_DESCRIPTION,
                                      h.CONTRACT_VALUE, h.PAYMENT_TERMS, h.SECURITY_DEPOSIT_REQD, h.SECURITY_AMOUNT,
                                      h.CREATED_BY, h.DEPT_CODE,
                                      NVL(e.PARTY_NAME,'N/A') AS PARTY_NAME
                               FROM MATPASS.TBL_IWPS_LOI_HEADER h
                               LEFT JOIN MATPASS.TBL_IWPS_ENQ_HEADER e ON e.ENQ_NO = h.ENQ_NO
                               WHERE h.LOI_NO = :p1";

                DataTable dt = DBHelper.ExecuteQuery(sql, DBHelper.P("p1", loiNo));

                if (dt.Rows.Count == 0)
                {
                    Response.Write("<script>alert('LOI not found.'); window.close();</script>");
                    return;
                }

                DataRow row = dt.Rows[0];
                lblLoiNo.Text = row["LOI_NO"].ToString();
                lblEnqNo.Text = row["ENQ_NO"].ToString();
                lblLoiDate.Text = Convert.ToDateTime(row["LOI_DATE"]).ToString("dd MMMM yyyy");
                lblValidityDate.Text = Convert.ToDateTime(row["VALIDITY_DATE"]).ToString("dd MMMM yyyy");
                lblWorkDescription.Text = row["WORK_DESCRIPTION"].ToString();
                lblContractValue.Text = Convert.ToDecimal(row["CONTRACT_VALUE"]).ToString("N2");
                lblPaymentTerms.Text = row["PAYMENT_TERMS"].ToString();
                lblSecurityDepositReqd.Text = row["SECURITY_DEPOSIT_REQD"].ToString() == "Y" ? "Yes" : "No";
                lblPartyName.Text = row["PARTY_NAME"].ToString();
                lblCreatedBy.Text = row["CREATED_BY"].ToString();
                lblOrgName.Text = "INTEGRATED WORKS PROCUREMENT SYSTEM";
                lblDeptName.Text = "Department: " + row["DEPT_CODE"].ToString();
                lblPrintDate.Text = DateTime.Now.ToString("dd-MMM-yyyy HH:mm");

                if (row["SECURITY_DEPOSIT_REQD"].ToString() == "Y")
                {
                    pnlSecurityAmount.Visible = true;
                    lblSecurityAmount.Text = Convert.ToDecimal(row["SECURITY_AMOUNT"]).ToString("N2");
                }
                else
                {
                    pnlSecurityAmount.Visible = false;
                }

                // Load notes
                string sqlNotes = @"SELECT n.NOTE_TITLE, n.NOTE_TEXT
                                    FROM MATPASS.TBL_IWPS_LOI_NOTES ln
                                    JOIN MATPASS.TBL_IWPS_LOI_NOTE_MASTER n ON n.NOTE_CODE = ln.NOTE_CODE
                                    WHERE ln.LOI_NO = :p1
                                    ORDER BY n.NOTE_TITLE";
                DataTable dtNotes = DBHelper.ExecuteQuery(sqlNotes, DBHelper.P("p1", loiNo));
                rptNotes.DataSource = dtNotes;
                rptNotes.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Error loading LOI: {ex.Message.Replace("'", "\\'")}');</script>");
            }
        }
    }
}
