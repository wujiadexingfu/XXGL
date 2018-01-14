using DbEntity.MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXGL.Base.IDAL;
using XXGL.Base.Models;
using XXGL.Base.Models.Authenticated;
using XXGL.Base.Models.UserViewModel;

namespace XXGL.Base.IService
{
  public   interface IUsersService
    {
      UserModel  GetUser(string ID);

       Account GetAccount(string ID);

       List<UserGridItem> GetUserList(UserParameter Parameters ,out int TotalCount);

       RequestResult ChangePassword(string UniqueID, string NewPassword);

       EditUserInputFormViewModel GetUserByUniqueID(string uniqueID);

       RequestResult EditUser(EditUserInputFormViewModel editUserInputFormViewModel);

    }
}
