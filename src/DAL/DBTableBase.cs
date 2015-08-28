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
    using System.IO;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using SubmitSys.Properties;

    internal class DBTableBase : IDisposable
    {
        protected SqlConnection Connection;

        private string mapperFile;

        protected DBTableBase(string config, string key)
        {
            this.Key = key;
            this.Connection = new SqlConnection(Settings.Default.DBConnection);
            var dbTables = JsonConvert.DeserializeObject<dynamic>(config);
            foreach (dynamic t in dbTables)
            {
                if (t.Key == key)
                {
                    mapperFile = t.Mapper;
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

        protected string Key { get; set; }

        protected IDictionary<string, string> Fields
        {
            get
            {
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(mapperFile)); ;
            }
        } 

        protected string TableName { get; private set; }

        protected IDictionary<string, string> SqlQueries { get; private set; }
    }
}
