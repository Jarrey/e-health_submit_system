// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DBUpdateFlag.cs" company="Jarrey, jar_bob@163.com">
//   Copyright © Jarrey, jar_bob@163.com
// </copyright>
// <summary>
//   Defines the DBUpdateFlag type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SubmitSys.DAL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    using SubmitSys.Properties;

    internal class DBUpdateFlag : IDisposable
    {
        private const string spName = "p_updateyjflag";

        private SqlConnection Connection;

        public DBUpdateFlag()
        {
            Connection = new SqlConnection(Settings.Default.DBConnection);
        }

        internal void UpdateFlag(string id, string tableType)
        {
            using (var cmd = new SqlCommand(spName, Connection) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.Add("@tname", SqlDbType.VarChar).Value = tableType;
                cmd.Parameters.Add("@dabh", SqlDbType.VarChar).Value = id;
                cmd.Parameters.Add("@flag", SqlDbType.Int).Value = 1;
                OpenConnection();
                cmd.ExecuteNonQuery();
            }
        }

        private void OpenConnection()
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
    }
}
