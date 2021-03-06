﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubmitSys.DAL
{
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.IO;

    using Newtonsoft.Json;

    using SubmitSys.Properties;

    internal class DBUploadDataTable : DBTableBase
    {
        public DBUploadDataTable(string config, string key)
            : base(config, key)
        {
        }

        #region Methods

        internal DataTable SelectData(string starttime, string endtime)
        {
            var dt = BuildTable();
            this.OpenConnection();
            var sql = SqlQueries["Select"];
            sql = sql.Replace("{start}", starttime).Replace("{end}", endtime);
            using (var cmd = new SqlCommand(sql, Connection))
            {
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var row = dt.NewRow();
                        row[Resources.SelectColumnName] = true;
                        foreach (var f in Fields)
                        {
                            row[f.Value] = reader[f.Key].ToString().Trim();
                        }

                        dt.Rows.Add(row);
                    }
                }
            }

            return dt;
        }

        private DataTable BuildTable()
        {
            var dt = new DataTable();
            dt.Columns.Add(Resources.SelectColumnName, typeof(bool));
            foreach (var f in Fields)
            {
                dt.Columns.Add(f.Value);
            }

            return dt;
        }

        #endregion
    }
}
