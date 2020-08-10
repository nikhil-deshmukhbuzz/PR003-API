using API.Contex;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Core
{
    public class User_Mgmnt
    {
        DB003 context = new DB003();

        public bool AddUser(User user,string type)
        {
            bool result = false;

            try
            {
                var user_exists = context.Users
                    .Where(w => w.MobileNo == user.MobileNo)
                    .FirstOrDefault();

                if (user_exists == null)
                {
                    string profile;
                    if (type == "PG")
                    {
                        profile = "PGOwner";

                    }
                    else if (type == "TNT")
                    {
                        profile = "Tenant";
                    }
                    else
                    {
                        profile = "Unknown";
                    }

                    var profile_output = context.ProfileMasters
                        .Where(w => w.ProfileName == profile)
                        .FirstOrDefault();

                    if (profile_output != null)
                    {
                        user.IsActive = true;
                        user.ProfileMasterID = profile_output.ProfileID;
                        user.CreatedOn = DateTime.Now;
                        user.ModifiedOn = DateTime.Now;


                        long user_output;
                        using (var user_context = new DB003())
                        {
                            user_context.Users.Add(user);
                            user_output = user_context.SaveChanges();
                        }
                        result = true;
                    }
                }
                else
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}
