using API.Contex;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core
{
    public static class Exception_C
    {
        public static void Add(string functiojnName,string mesage)
        {
            try
            {
                ExceptionText exceptionText = new ExceptionText()
                {
                    FunctionName = functiojnName,
                    Message = mesage,
                    CreatedOn = TimeZone.GetCurrentDateTime()
                };

                using (var context = new DB003())
                {
                   context.ExceptionTexts.Add(exceptionText);
                   int  output = context.SaveChanges();
                }

                exceptionText = null;
            }
            catch(Exception ex)
            {

            }
        }
    }
}
