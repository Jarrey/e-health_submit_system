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
                    using (var cmd = new SqlCommand(this.BuildUpdateQuery(row), Connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (var cmd = new SqlCommand(this.BuildInsertQuery(row), Connection))
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

        private string BuildUpdateQuery(DataRow data)
        {
            var keyColumnName = this.Fields.First().Key;
            var keyColumnValue = this.Fields.First().Value;
            var values = string.Join(", ", this.Fields.Select(c => string.Format("[{0}]=N'{1}'", c.Key, data[c.Value])));
            return string.Format("UPDATE {0} SET {1} WHERE {2}='{3}'", this.TableName, values, keyColumnName, data[keyColumnValue]);
        }

        private string BuildInsertQuery(DataRow data)
        {
            var columns = string.Join(", ", this.Fields.Keys);
            var values = string.Join(", ", this.Fields.Select(c => string.Format("N'{0}'", data[c.Value])));
            return string.Format("INSERT INTO {0} ({1}) VALUES ({2})", this.TableName, columns, values);
        }
    }
}
