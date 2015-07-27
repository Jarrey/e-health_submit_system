namespace SubmitSys
{
    using System.Collections.Generic;
    using System.Data;

    using Newtonsoft.Json;

    public class Actions
    {
        public Actions()
        {
            Steps = new Dictionary<string, Step>();
        }

        [JsonProperty("login_url")]
        public string LoginUrl { get; set; }

        [JsonProperty("steps")]
        public Dictionary<string, Step> Steps { get; set; }
    }

    public class Step
    {
        [JsonProperty("frame_url")]
        public string FrameUrlKey { get; set; }

        [JsonProperty("script")]
        public string Script { get; set; }

        [JsonProperty("pre_status")]
        public StepStatus PreStatus { get; set; }

        [JsonProperty("next_status")]
        public StepStatus NextStatus { get; set; }

        [JsonProperty("has_data")]
        public bool HasData { get; set; }

        [JsonProperty("increase")]
        public bool DataIncrease { get; set; }

        [JsonIgnore]
        public DataSet Data { get; set; }
    }
}
