using System.Web;

namespace IWPS
{
    public static class SessionHelper
    {
        public static string StaffNo(HttpSessionState s) => s["StaffNo"]?.ToString() ?? "";
        public static string UserName(HttpSessionState s) => s["UserName"]?.ToString() ?? "";
        public static string Dept(HttpSessionState s) => s["Dept"]?.ToString() ?? "";
        public static string Role(HttpSessionState s) => s["Role"]?.ToString() ?? "";
        public static bool IsLoggedIn(HttpSessionState s) => !string.IsNullOrEmpty(s["StaffNo"]?.ToString());

        public static void SetUser(HttpSessionState s, string staffNo, string userName, string dept, string role)
        {
            s["StaffNo"] = staffNo;
            s["UserName"] = userName;
            s["Dept"] = dept;
            s["Role"] = role;
        }

        public static void Clear(HttpSessionState s) => s.Clear();
    }
}
