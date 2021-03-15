﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EIA.Domain.Model
{
    public class Series
    {
        [JsonPropertyName("series_id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("f")]
        public string  Frequency { get; set; }

        [JsonPropertyName("units")]
        public string Units { get; set; }

        [JsonPropertyName("updated")]
        public string Updated { get; set; }
    }
}