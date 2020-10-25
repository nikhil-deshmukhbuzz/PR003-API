using API.Contex;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core
{
    public static class Monitor_C
    {
        public static void Add(string sericeName, string message)
        {
            try
            {
                using (var context = new DB003())
                {
                    var monitor = new Monitor()
                    {
                        ServiceName = sericeName,
                        ExecutionTime = TimeZone.GetCurrentDateTime(),
                        Message = message
                    };
                    context.Monitors.Add(monitor);
                    var email_output = context.SaveChanges();

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
