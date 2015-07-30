namespace SubmitSys
{
    using System.Data;
    using System.IO;

    using CsvHelper;
    using CsvHelper.Configuration;

    using SubmitSys.Properties;

    internal static class Utility
    {
        internal static DataTable ReadCsvToDataTable(Stream stream, FieldMapper mapper)
        {
            using (var textReader = new StreamReader(stream))
            {
                return ReadCore(textReader, mapper);
            }
        }

        internal static DataTable ReadCsvToDataTable(FileInfo file, FieldMapper mapper)
        {
            using (var fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var textReader = new StreamReader(fs))
                {
                    return ReadCore(textReader, mapper);
                }
            }
        }

        private static DataTable ReadCore(StreamReader textReader, FieldMapper mapper)
        {
            var datatable = new DataTable();
            var csvConfig = new CsvConfiguration { Delimiter = ",", TrimHeaders = true, TrimFields = true };
            var csv = new CsvReader(textReader, csvConfig);
            var isHeaderRead = false;
            var hasRecord = true;
            while (hasRecord)
            {
                hasRecord = csv.Read();
                if (!isHeaderRead)
                {
                    datatable.Columns.Add(Resources.SelectColumnName, typeof(bool));
                    foreach (var column in csv.FieldHeaders)
                    {
                        datatable.Columns.Add(mapper.Map(column));
                    }

                    isHeaderRead = true;
                }

                if (hasRecord)
                {
                    var row = datatable.NewRow();
                    row[Resources.SelectColumnName] = false;
                    foreach (var columnName in csv.FieldHeaders)
                    {
                        var value = csv.GetField(columnName);
                        row[mapper.Map(columnName)] = value;
                    }

                    datatable.Rows.Add(row);
                }
            }

            return datatable;
        }
    }
}
