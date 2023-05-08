using Dapper;
using System.Data;

namespace Findox.Api.Data.Extensions
{
    public class GenericArrayHandler<T> : SqlMapper.TypeHandler<T[]>
    {
        public override void SetValue(IDbDataParameter parameter, T[] value)
        {
            parameter.Value = value;
        }

        public override T[] Parse(object value) => (T[])value;
    }
}
