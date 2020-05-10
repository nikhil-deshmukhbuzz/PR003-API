using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using API.Contex;
using API.Core;
using API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        //DB002A context = new DB002A();
        ////ApplicationDbContext applicationDbContext = new ApplicationDbContext();

        ////private readonly UserManager<ApplicationUser> userManager;
        ////private readonly RoleManager<ApplicationRole> roleManager;

        ////public UserManagementController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        ////{
        ////    this.userManager = userManager;
        ////    this.roleManager = roleManager;
        ////}



        //[Route("Add")]
        //[HttpPost]
        //public async Task<bool> Add([FromBody] UserMaster _oUserMaster)
        //{
        //    try
        //    {
        //        dynamic output = false;

        //        string username = GenerateUsername(_oUserMaster.Name);
        //        _oUserMaster.UserName = username;
        //        _oUserMaster.Password = CommonLogic.EncryptString(username + "@123");

        //        if (_oUserMaster.ProfileMaster.ProfileName == "CoOrdinator")
        //        {
        //            using (var contextUser = new DB002A())
        //            {
        //                var role = context.ProfileMasters
        //                .Where(w => w.ProfileName == "CoOrdinator")
        //                .FirstOrDefault();

        //                _oUserMaster.ProfileID = role.ProfileID;
                        
        //                using (var contextU = new DB002A())
        //                {
        //                    _oUserMaster.ProfileMaster = null;
        //                    contextU.UserMasters.Add(_oUserMaster);
        //                    output = contextU.SaveChanges();
        //                }
        //            }
        //        }
        //        else if (_oUserMaster.ProfileMaster.ProfileName == "Administrator")
        //        {
        //            using (var contextUser = new DB002A())
        //            {
        //                var role = context.ProfileMasters
        //                .Where(w => w.ProfileName == "Administrator")
        //                .FirstOrDefault();

        //                _oUserMaster.ProfileID = role.ProfileID;
                        
        //                using (var contextU = new DB002A())
        //                {
        //                    _oUserMaster.ProfileMaster = null;
        //                    contextU.UserMasters.Add(_oUserMaster);
        //                    output = contextU.SaveChanges();
        //                }
        //            }
        //        }
        //        else if (_oUserMaster.ProfileMaster.ProfileName == "Secretary")
        //        {
        //            using (var contextUser = new DB002A())
        //            {
        //                var role = context.ProfileMasters
        //                .Where(w => w.ProfileName == "Secretary")
        //                .FirstOrDefault();

        //                _oUserMaster.ProfileID = role.ProfileID;
        //                _oUserMaster.CoOrdinatorID = null;


        //                using (var contextU = new DB002A())
        //                {
        //                    _oUserMaster.ProfileMaster = null;
        //                    contextU.UserMasters.Add(_oUserMaster);
        //                    output = contextU.SaveChanges();
        //                }

        //            }
        //        }
        //        return Convert.ToBoolean(output);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        context = null;
        //    }
        //}


        //    //[Route("Update")]
        //    //[HttpPost]
        //    //public async Task<bool> Update([FromBody] UserMaster _oUserMaster)
        //    //{
        //    //    try
        //    //    {
        //    //        int output = 0;

        //    //        UserViewModel userViewModel = new UserViewModel
        //    //        {
        //    //            Name = _oUserMaster.Name,
        //    //            UserName = _oUserMaster.UserName,
        //    //            Password = _oUserMaster.Password
        //    //        };

        //    //        ApplicationUser applicationUser = applicationDbContext.Users
        //    //            .Where(w => w.UserName == _oUserMaster.UserName)
        //    //            .FirstOrDefault();
        //    //        if (applicationUser != null)
        //    //        {
        //    //            UserController userController = new UserController(userManager, roleManager);
        //    //            var appOutput = await userController.UpdateUser(applicationUser.Id, userViewModel);

        //    //            if (Convert.ToBoolean(appOutput.Value))
        //    //            {
        //    //                using (var context = new DB002())
        //    //                {
        //    //                    var input = context.UserMasters
        //    //                                   .Where(s => s.UserID == _oUserMaster.UserID)
        //    //                                   .FirstOrDefault();

        //    //                    if (_oUserMaster.Password != "")
        //    //                        input.Password = _oUserMaster.Password;

        //    //                    input.Name = _oUserMaster.Name;
        //    //                    input.MobileNo = _oUserMaster.MobileNo;
        //    //                    input.IsActive = _oUserMaster.IsActive;

        //    //                    output = context.SaveChanges();
        //    //                }
        //    //            }
        //    //        }
        //    //        return Convert.ToBoolean(output);
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        throw;
        //    //    }
        //    //    finally
        //    //    {
        //    //        context = null;
        //    //    }
        //    //}

        //[Route("ChangePassword")]
        //[HttpPost]
        //public async Task<bool> ChangePassword([FromBody] UserMaster _oUserMaster)
        //{
        //    try
        //    {
        //        var user = context.UserMasters
        //         .Where(u => u.UserID == _oUserMaster.UserID && CommonLogic.DecryptString(u.Password.Trim()) == _oUserMaster.OldPassword.Trim())
        //         .FirstOrDefault();

        //        if (user != null)
        //        {
        //            using (var context = new DB002A())
        //            {
        //                var input = context.UserMasters
        //                               .Where(s => s.UserID == _oUserMaster.UserID)
        //                               .FirstOrDefault();
                        
        //                    input.Password = CommonLogic.EncryptString(_oUserMaster.Password.Trim());

        //               var output = context.SaveChanges();
        //               return true;
        //            }
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        context = null;
        //    }
        //}

    
        //[Route("ValidateUser")]
        //[HttpPost]
        //public IActionResult ValidateUser([FromBody] UserMaster userMaster)
        //{
        //    try
        //    {
        //        UserManagement userManagement = new UserManagement();
             

        //        var user = context.UserMasters
        //           .Where(u => u.UserName.Trim() == userMaster.UserName.Trim() && CommonLogic.DecryptString(u.Password.Trim()) == userMaster.Password.Trim())
        //           .Include(p => p.ProfileMaster)
        //           .FirstOrDefault();
                
        //        if (user != null)
        //        {

        //            userManagement.UserMaster = user;
        //            userManagement.Status = "Success";
        //            var menu = context.MenuProfileLinks
        //                .Where(w => w.ProfileID == user.ProfileID)
        //                 .Include(i => i.MenuMaster)
        //                 .OrderBy(o => o.MenuMaster.SequenceNo)
        //                 .ToList();

        //            List<MenuMaster> listOfMenuMaster = new List<MenuMaster>();
        //            foreach (var item in menu)
        //            {
        //                if (item.MenuMaster.IsActive)
        //                    listOfMenuMaster.Add(item.MenuMaster);
        //            }

        //            userManagement.ListOfMenuMaster = listOfMenuMaster;
        //        }
        //        else
        //        {
        //            userManagement.Status = "Invalid";
        //        }


        //        return Ok(userManagement);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //[Route("UpdateStatus")]
        //[HttpPost]
        //public bool UpdateStatus([FromBody] UserMaster _oUserMaster)
        //{
        //    try
        //    {
        //        int output;
        //        using (var context = new DB002A())
        //        {
        //            var input = context.UserMasters
        //                           .Where(s => s.UserID == _oUserMaster.UserID)
        //                           .FirstOrDefault();

        //            input.IsActive = _oUserMaster.IsActive;

        //            output = context.SaveChanges();
        //        }
        //        return Convert.ToBoolean(output);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        context = null;
        //    }
        //}


        //[Route("GetList")]
        //[HttpGet]
        //public IActionResult GetList(string profileName, long organisationId, long coordinatorId)
        //{

        //    try
        //    {
        //        var output = new List<UserMaster>();
        //        if (profileName == "Superadmin")
        //        {
        //            output = context.UserMasters
        //               .Include(i => i.ProfileMaster)
        //               .Include(i => i.Block)
        //               .Where(
        //                      w => w.ProfileMaster.ProfileName == "CoOrdinator")
        //               .OrderBy(o => o.ProfileID)
        //               .ToList();
        //        }
        //        else if (profileName == "CoOrdinator")
        //        {
        //            output = context.UserMasters
        //               .Include(i => i.ProfileMaster)
        //               .Include(i => i.Organisation)
        //               .Where(
        //                      w => w.ProfileMaster.ProfileName == "Administrator" && w.CoOrdinatorID == coordinatorId)
        //               .OrderBy(o => o.ProfileID)
        //               .ToList();
        //        }
        //        else if (profileName == "Administrator")
        //        {
        //            output = context.UserMasters
        //           .Include(i => i.ProfileMaster)
        //           .Include(i => i.Block)
        //           .Where(

        //                  w => w.OrganisationID == organisationId &&
        //                  w.ProfileMaster.ProfileName == "Secretary" ||
        //                  w.ProfileMaster.ProfileName == "Member"
        //                  )
        //           .OrderBy(o => o.ProfileID)
        //           .ToList();
        //        }
        //        else if (profileName == "Secretary")
        //        {
        //            output = context.UserMasters
        //            .Include(i => i.ProfileMaster)
        //            .Include(i => i.Block)
        //            .Where(
        //                  w => w.OrganisationID == organisationId ||
        //                  w.ProfileMaster.ProfileName == "Secretary" ||
        //                  w.ProfileMaster.ProfileName == "Member"
        //                  )
        //            .OrderBy(o => o.ProfileID)
        //            .ToList();
        //        }

        //        return Ok(output);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        context = null;
        //    }
        //}

        //[Route("Get")]
        //[HttpGet]
        //public IActionResult Get(long userId)
        //{

        //    try
        //    {
        //        var output = context.UserMasters
        //                    .Include(i => i.ProfileMaster)
        //                    .Where(w => w.UserID == userId)
        //                    .FirstOrDefault();
        //        return Ok(output);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        context = null;
        //    }
        //}

        //[Route("GetProfileMasterList")]
        //[HttpGet]
        //public IActionResult GetProfileMasterList()
        //{

        //    try
        //    {
        //        var output = context.ProfileMasters
        //            .Where(
        //                   w => w.ProfileName != "Superadmin" &&
        //                   w.ProfileName != "Administrator" &&
        //                   w.ProfileName != "Inventory Supervisor" &&
        //                   w.ProfileName != "Account Manager"
        //                   )
        //            .ToList();
        //        return Ok(output);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        context = null;
        //    }
        //}

        //public string GenerateUsername(string Name)
        //{
        //    bool isExists = false;
        //    string username = Regex.Replace(Name, @"\s+", "").ToLower();
        //    string temp_username = Regex.Replace(Name, @"\s+", "").ToLower();


        //    int i = 1;
        //    do
        //    {
        //        var user = context.UserMasters
        //                    .Include(ii => ii.ProfileMaster)
        //                    .Where(w => w.UserName == username)
        //                    .FirstOrDefault();


        //        if (user != null)
        //        {
        //            username = temp_username + i;
        //            isExists = true;
        //            i++;
        //        }
        //        else
        //        {
        //            isExists = false;
        //        }
        //    } while (isExists);

        //    return username;
        //}
    }
}