using System.Web;

namespace GreenRoad.Web.Helpers
{
    public class TokenHelper
    {
        private static HttpContextBase Current
        {
            get { return new HttpContextWrapper(HttpContext.Current); }
        }

        public object AccessToken
        {
            get { return Current.Session?["Access Token"]; }
            set
            {
                if (Current.Session != null)
                {
                    Current.Session["Access Token"] = value;
                }
            }
        }
    }
}