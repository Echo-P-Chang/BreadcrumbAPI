using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookHandler
{
    public interface IFacebookHelper
    {
        string GetUserID(string accessToken);
        dynamic GetUserHeadshot(string accessToken);
        List<string> GetUsersFriends(string accessToken);
        dynamic GetUserProfile(string accessToken);

    }
}
