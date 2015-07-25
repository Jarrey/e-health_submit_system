namespace SubmitSys
{
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;

    using Newtonsoft.Json;

    using SubmitSys.Properties;

    internal class FieldMapper
    {
        private IDictionary<string, string> mapper;
        public FieldMapper(string mapFile)
        {
            try
            {
                var mapperJson = File.ReadAllText(mapFile);
                mapper = JsonConvert.DeserializeObject<Dictionary<string, string>>(mapperJson);
            }
            catch
            {
                MessageBox.Show(string.Format(Resources.ReadMapFileError, mapFile), Resources.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal string Map(string fieldName)
        {
            if (mapper != null && mapper.ContainsKey(fieldName))
            {
                return mapper[fieldName];
            }
            else
            {
                return fieldName;
            }
        }
    }
}
