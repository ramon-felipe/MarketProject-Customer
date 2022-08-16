using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProject.CustomerService.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static Result<byte[]> Serialize(this object obj)
        {
            if (obj == null)
                return Result.Failure<byte[]>(ResultError.Create<ArgumentNullException>("Parâmetro nullo."));

            var json = JsonConvert.SerializeObject(obj);
            return Result.Success(Encoding.ASCII.GetBytes(json));
        }

        public static T DeSerialize<T>(this byte[] arrBytes)
        {
            var json = Encoding.Default.GetString(arrBytes);

            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string DeSerializeText(this byte[] arrBytes)
        {
            return Encoding.Default.GetString(arrBytes);
        }
    }
}
