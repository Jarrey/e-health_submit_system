// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataFile.cs" company="Jarrey, jar_bob@163.com">
//   Copyright © Jarrey, jar_bob@163.com
// </copyright>
// <summary>
//   Defines the DataFile type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SubmitSys
{
    using System.Collections.Generic;
    using System.Data;
    using System.IO;

    using Newtonsoft.Json;

    /// <summary>
    /// The data file.
    /// </summary>
    internal class DataFile
    {
        private static dynamic DataFileMapper
        {
            get
            {
                var mapperJson = File.ReadAllText("FieldMaps/DataFile.map");
                return JsonConvert.DeserializeObject<List<dynamic>>(mapperJson);
            }
        }

        /// <summary>
        /// The file info.
        /// </summary>
        private readonly FileInfo fileInfo;

        public DataFile(string file)
        {
            this.fileInfo = new FileInfo(file);
            this.Table = Utility.ReadCsvToDataTable(fileInfo, Mapper);
        }

        public string Key
        {
            get
            {
                foreach (dynamic map in DataFileMapper)
                {
                    if (fileInfo.Name.Contains(map.Key.ToString()))
                    {
                        return map.Key.ToString();
                    }
                }

                return null;
            }
        }

        public string DisplayName
        {
            get
            {
                foreach (dynamic map in DataFileMapper)
                {
                    if (fileInfo.Name.Contains(map.Key.ToString()))
                    {
                        return map.DisplayName.ToString();
                    }
                }

                return null;
            }
        }

        public DataTable Table { get; private set; }

        public FieldMapper Mapper
        {
            get
            {
                foreach (dynamic map in DataFileMapper)
                {
                    if (fileInfo.Name.Contains(map.Key.ToString()))
                    {
                        return new FieldMapper(map.Mapper.ToString()); 
                    }
                }

                return null;
            }
        }
    }
}
