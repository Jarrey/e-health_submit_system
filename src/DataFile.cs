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

        private string name;

        public DataFile(Stream stream, string name)
        {
            this.name = name;
            this.Table = Utility.ReadCsvToDataTable(stream, Mapper);
        }

        public DataFile(string file)
        {
            this.name = Path.GetFileName(file);
            this.Table = Utility.ReadCsvToDataTable(new FileInfo(file), Mapper);
        }

        public string Key
        {
            get
            {
                foreach (dynamic map in DataFileMapper)
                {
                    if (name.Contains(map.Key.ToString()))
                    {
                        return map.Key.ToString();
                    }
                }

                return name;
            }
        }

        public bool CanNew
        {
            get
            {
                foreach (dynamic map in DataFileMapper)
                {
                    if (name.Contains(map.Key.ToString()))
                    {
                        return (bool)map.CanNew;
                    }
                }

                return false;
            }
        }

        public string DisplayName
        {
            get
            {
                foreach (dynamic map in DataFileMapper)
                {
                    if (name.Contains(map.Key.ToString()))
                    {
                        return map.DisplayName.ToString();
                    }
                }

                return name;
            }
        }

        public DataTable Table { get; private set; }

        public FieldMapper Mapper
        {
            get
            {
                foreach (dynamic map in DataFileMapper)
                {
                    if (name.Contains(map.Key.ToString()))
                    {
                        return new FieldMapper(map.Mapper.ToString()); 
                    }
                }

                return FieldMapper.Default;
            }
        }
    }
}
