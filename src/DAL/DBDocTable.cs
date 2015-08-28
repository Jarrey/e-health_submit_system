// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DBDocTable.cs" company="Jarrey, jar_bob@163.com">
//   Copyright © Jarrey, jar_bob@163.com
// </copyright>
// <summary>
//   Defines the DBDocTable type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SubmitSys.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;

    using Newtonsoft.Json;

    internal class DBDocTable : DBTableBase
    {
        public DBDocTable(string config)
            : base(config)
        {
        }

        #region Properties

        protected override string Key
        {
            get
            {
                return "DocumentTable";
            }
        }

        protected override IDictionary<string, string> Fields
        {
            get
            {
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("FieldMaps/DBDocTable.map"));
            }
        }

        #endregion

        internal void UpdateTable(DataTable dt)
        {
            this.OpenConnection();
            foreach (DataRow row in dt.Rows)
            {
                if (this.IsRecordExisted(row[0].ToString()))
                {
                    using (var cmd = new SqlCommand(this.BuildQuery(row, "Update"), Connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (var cmd = new SqlCommand(this.BuildQuery(row, "Insert"), Connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private bool IsRecordExisted(string bh)
        {
            this.OpenConnection();
            using (var cmd = new SqlCommand(
                    string.Format("SELECT Count(*) FROM {0} WHERE {1}='{2}'", this.TableName, this.Fields.Keys.First(), bh),
                    Connection))
            {
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        return reader.GetInt32(0) > 0;
                    }
                }
            }

            return false;
        }

        private string BuildQuery(DataRow data, string queryKey)
        {
            if (!SqlQueries.ContainsKey(queryKey))
            {
                throw new Exception("Cannot find query key " + queryKey);
            }

            var q = SqlQueries[queryKey];
            q = this.Fields.Aggregate(q, (c, f) => c.Replace("{" + f.Key + "}", data[f.Value].ToString()));
            return q;
        }
    }
}
