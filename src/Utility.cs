namespace SubmitSys
{
    using System.Data;
    using System.IO;

    using CsvHelper;
    using CsvHelper.Configuration;

    using SubmitSys.Properties;

    internal static class Utility
    {
        internal static DataTable ReadCsvToDataTable(string fileName, string mapperFile)
        {
            var mapper = new FieldMapper(mapperFile);
            var datatable = new DataTable();
            var csvConfig = new CsvConfiguration { Delimiter = ",", TrimHeaders = true, TrimFields = true };
            using (var textReader = new StreamReader(fileName))
            {
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
            }

            return datatable;
        }
    }
}
