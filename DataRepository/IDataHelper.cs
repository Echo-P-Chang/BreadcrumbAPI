using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository
{
    public interface IDataHelper
    {
        bool InsertUser(Dictionary<string, object> us);
        bool CheckUserIsExist(string userID);
        bool StorePin(Dictionary<string, object> pn);
        Dictionary<string, object> GetUser(string userID);
        Dictionary<string, object> GetPinWithPinID(string owner, double pinDate);
        List<Dictionary<string, object>> GetPinWithUserID(string userID, double since, int takeCnt);
        List<Dictionary<string, object>> GetPinWithUserIDs(List<string> userIDs, double since, int takeCnt);
        
    }
}
