using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Common.Messages.Alerts
{
    public class AlertsModel
    {
        public partial class Alerts
        {
            [JsonProperty("Language")]
            public string Language { get; set; }

            [JsonProperty("AlertTypes")]
            public AlertTypes AlertTypes { get; set; }
        }

        public partial class AlertTypes
        {
            [JsonProperty("Success")]
            public JObject[] Success { get; set; }

            [JsonProperty("Warning")]
            public JObject[] Warning { get; set; }

            [JsonProperty("Error")]
            public JObject[] Error { get; set; }

            [JsonProperty("Info")]
            public JObject[] Info { get; set; }
        }
    }
}
