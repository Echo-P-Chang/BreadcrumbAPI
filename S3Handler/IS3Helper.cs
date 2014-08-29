using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3Handler
{
    public interface IS3Helper
    {
        bool CreateBucket(string userid);
        bool CheckBucketIsExist(string userid);
        bool Upload(string userID, string fileName, Stream file);
    }
}
