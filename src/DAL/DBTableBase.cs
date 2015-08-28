// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DBTableBase.cs" company="Jarrey, jar_bob@163.com">
//   Copyright © Jarrey, jar_bob@163.com
// </copyright>
// <summary>
//   Defines the DBTableBase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SubmitSys.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using SubmitSys.Properties;

    internal class DBTableBase : IDisposable
    {
        protected SqlConnection Connection;

        protected DBTableBase(string config)
        {
            this.Connection = new SqlConnection(Settings.Default.DBConnection);
            var dbTables = JsonConvert.DeserializeObject<dynamic>(config);
            foreach (dynamic t in dbTables)
            {
                if (t.Key == this.Key)
                {
                    TableName = t.DBTableName;
                    SqlQueries = (t.Commands as JObject).ToObject<IDictionary<string, string>>();
                }
            }
        }

        protected void OpenConnection()
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
        }

        public void Dispose()
        {
            Connection.Close();
        }

        protected virtual string Key
        {
            get
            {
                return string.Empty;
            }
        }

        protected virtual IDictionary<string, string> Fields
        {
            get
            {
                return null;
            }
        } 

        protected string TableName { get; private set; }

        protected IDictionary<string, string> SqlQueries { get; private set; }
    }
}
