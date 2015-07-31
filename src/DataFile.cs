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
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;

    using Newtonsoft.Json;

    /// <summary>
    /// The data file.
    /// </summary>
    internal class DataFile
    {
        private readonly string name;

        private static dynamic DataFileMapper
        {
            get
            {
                var mapperJson = File.ReadAllText("FieldMaps/DataFile.map");
                return JsonConvert.DeserializeObject<List<dynamic>>(mapperJson);
            }
        }

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

        public StepStatus NewStep
        {
            get
            {
                var step = StepStatus.OpenDocTabForNew;
                foreach (dynamic map in DataFileMapper)
                {
                    if (name.Contains(map.Key.ToString()))
                    {
                        Enum.TryParse(map.NewStep.ToString(), true, out step);
                        break;
                    }
                }

                return step;
            }
        }

        public StepStatus ModifyStep
        {
            get
            {
                var step = StepStatus.OpenDocTabForModify;
                foreach (dynamic map in DataFileMapper)
                {
                    if (name.Contains(map.Key.ToString()))
                    {
                        Enum.TryParse(map.ModifyStep.ToString(), true, out step);
                        break;
                    }
                }

                return step;
            }
        }

        public string Parameter
        {
            get
            {
                foreach (dynamic map in DataFileMapper)
                {
                    if (name.Contains(map.Key.ToString()))
                    {
                        if (map.Parameter != null)
                        {
                            return map.Parameter.ToString();
                        }
                    }
                }

                return string.Empty;
            }
        }
    }
}
