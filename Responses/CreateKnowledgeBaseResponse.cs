using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace QnaMakerApi.Responses
{
    public class DataExtractionResult
    {
        [JsonProperty("sourceType")]
        public string SourceType { get; set; }

        [JsonProperty("extractionStatusCode")]
        public string ExtractionStatusCode { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }
    }

    /// <summary>
    ///     A successful call returns the knowledge base identity.
    /// </summary>
    public class CreateKnowledgeBaseResponse
    {
        /// <summary>
        ///     Holds the unique guid representing the knowledge base.
        /// </summary>
        [JsonProperty("kbId")]
        public Guid Guid { get; set; }

        /// <summary>
        ///     Data extraction results.
        /// </summary>
        [JsonProperty("dataExtractionResults")]
        public IList<DataExtractionResult> DataExtractionResults { get; set; }
    }
}