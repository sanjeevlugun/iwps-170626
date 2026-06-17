using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IWPS
{
    public partial class loi_note_master : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadNotes();
            }
        }

        private void LoadNotes()
        {
            try
            {
                string sql = "SELECT NOTE_CODE, NOTE_TITLE, NOTE_TEXT, CREATED_BY, CREATED_DATE FROM MATPASS.TBL_IWPS_LOI_NOTE_MASTER ORDER BY CREATED_DATE DESC";
                DataTable dt = DBHelper.ExecuteQuery(sql);
                gvNotes.DataSource = dt;
                gvNotes.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err", $"toastr.error('Error loading notes: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                string staffNo = SessionHelper.StaffNo(Session);
                string noteCode = hfNoteCode.Value;

                if (string.IsNullOrEmpty(noteCode))
                {
                    // Insert
                    long seq = DBHelper.GetNextSeq("LOI_NOTE_SEQ");
                    noteCode = $"NOTE-{seq:D3}";

                    string sql = @"INSERT INTO MATPASS.TBL_IWPS_LOI_NOTE_MASTER 
                        (NOTE_CODE, NOTE_TITLE, NOTE_TEXT, CREATED_BY, CREATED_DATE)
                        VALUES (:p1, :p2, :p3, :p4, SYSTIMESTAMP)";

                    DBHelper.ExecuteNonQuery(sql,
                        DBHelper.P("p1", noteCode),
                        DBHelper.P("p2", txtNoteTitle.Text.Trim()),
                        DBHelper.P("p3", txtNoteText.Text.Trim()),
                        DBHelper.P("p4", staffNo));

                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "toastr.success('Note saved successfully.');", true);
                }
                else
                {
                    // Update
                    string sql = @"UPDATE MATPASS.TBL_IWPS_LOI_NOTE_MASTER 
                        SET NOTE_TITLE=:p1, NOTE_TEXT=:p2, UPDATED_BY=:p3, UPDATED_DATE=SYSTIMESTAMP
                        WHERE NOTE_CODE=:p4";

                    DBHelper.ExecuteNonQuery(sql,
                        DBHelper.P("p1", txtNoteTitle.Text.Trim()),
                        DBHelper.P("p2", txtNoteText.Text.Trim()),
                        DBHelper.P("p3", staffNo),
                        DBHelper.P("p4", noteCode));

                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", "toastr.success('Note updated successfully.');", true);
                }

                ClearForm();
                LoadNotes();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err", $"toastr.error('Error saving note: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        protected void gvNotes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditNote")
            {
                string noteCode = e.CommandArgument.ToString();
                try
                {
                    string sql = "SELECT NOTE_CODE, NOTE_TITLE, NOTE_TEXT FROM MATPASS.TBL_IWPS_LOI_NOTE_MASTER WHERE NOTE_CODE=:p1";
                    DataTable dt = DBHelper.ExecuteQuery(sql, DBHelper.P("p1", noteCode));
                    if (dt.Rows.Count > 0)
                    {
                        hfNoteCode.Value = dt.Rows[0]["NOTE_CODE"].ToString();
                        txtNoteCode.Text = dt.Rows[0]["NOTE_CODE"].ToString();
                        txtNoteTitle.Text = dt.Rows[0]["NOTE_TITLE"].ToString();
                        txtNoteText.Text = dt.Rows[0]["NOTE_TEXT"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "err", $"toastr.error('Error loading note: {ex.Message.Replace("'", "\\'")}');", true);
                }
            }
        }

        private void ClearForm()
        {
            hfNoteCode.Value = "";
            txtNoteCode.Text = "";
            txtNoteTitle.Text = "";
            txtNoteText.Text = "";
        }
    }
}
