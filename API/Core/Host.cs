using API.Contex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core
{
    public class Host
    {
        public string GetHostedURL(string serviceName)
        {
            DB003 context = null;
            string Url = "";
            try
            {
                context = new DB003();
                var output = context.Hosted_Service_Urls
                     .Where(w => w.ServiceName == serviceName && w.IsActive)
                     .FirstOrDefault();
                if (output != null)
                {
                    Url = output.URL.Trim();
                }

            }
            catch
            {

            }
            finally
            {
                context = null;
            }
            return Url;
        }
    }
}
