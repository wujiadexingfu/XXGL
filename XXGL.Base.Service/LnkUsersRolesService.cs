using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXGL.Base.IDAL;
using XXGL.Base.IService;

namespace XXGL.Base.Service
{
    public class LnkUsersRolesService : ILnkUsersRolesService
    {
        public ILnkUsersRolesRepository _lnkUsersRolesRepository { get; set; }
        public LnkUsersRolesService(ILnkUsersRolesRepository lnkUsersRolesRepository)
        {
            _lnkUsersRolesRepository = lnkUsersRolesRepository;
        }
    }
}
