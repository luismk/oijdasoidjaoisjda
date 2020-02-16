using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PangyaAPI.Repository.DapperExt
{
    public static class DapperExt
    {
        public static object UpdateFields<T>(this IDbConnection connection, object param, IDbTransaction transaction = null, int? commandTimeOut = null, CommandType? commandType = null)
        {
            var names = new List<string>();
            object id = null;

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(param))
            {
                if (!"Id".Equals(property.Name, StringComparison.InvariantCultureIgnoreCase))
                    names.Add(property.Name);
                else
                    id = property.GetValue(param);
            }

            if (id != null && names.Count > 0)
            {
                var sql = string.Format("UPDATE {1} SET {0} WHERE Id=@Id", string.Join(",", names.Select(t => { t = t + "=@" + t; return t; })), typeof(T).Name);
                if (Debugger.IsAttached)
                    Trace.WriteLine(string.Format("UpdateFields: {0}", sql));
                return connection.Execute(sql, param, transaction, commandTimeOut, commandType) > 0 ? id : null;
            }
            return null;
        }

        public static object UpdateFields<T>(this IDbConnection connection, object fields, CommandDefinition commandDefinition)
        {
            return UpdateFields<T>(connection, fields, commandDefinition.Transaction, commandDefinition.CommandTimeout, commandDefinition.CommandType);
        }
    }
}
