using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ConnectFourServer
{
    public static class helper
    {
        public static string ReadStringIgnoreNull(this BinaryReader reader )
        {
            string res = reader.ReadString();
            while (res.Length<1)
                res = reader.ReadString();
            return res;
        }
    }
}
