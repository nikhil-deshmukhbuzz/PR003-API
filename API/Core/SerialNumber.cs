using API.Contex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core
{
    public class SerialNumber
    {
        DB003 context = new DB003();

        public string GenerateRegistrationNumber()
        {
            string Code = "";

            var NoOfRegistration = context.Registrations.Count() + 1;

            Code = "RPG-" + String.Format("{0:000000}", NoOfRegistration);

            return Code.ToUpper();
        }

        public string GeneratePGNumber()
        {
            string Code = "";

            var NoOfPG = context.PGs.Count() + 1;

            Code = "PG-" + String.Format("{0:000000}", NoOfPG);

            return Code.ToUpper();
        }

        public string GenerateInvoiceNumber()
        {
            string Code = "";

            var NoOfPG = context.Rents.Count() + 1;

            Code = "INV-" + String.Format("{0:000000}", NoOfPG);

            return Code.ToUpper();
        }
    }
}
