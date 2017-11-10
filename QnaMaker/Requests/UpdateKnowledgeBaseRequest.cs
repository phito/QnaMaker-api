using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace QnaMakerApi.Requests
{
    public class UpdateActions
    {
        [JsonProperty("qnaPairs")]
        public List<QnaPair> QnaPairs { get; set; } = new List<QnaPair>();

        [JsonProperty("urls")]
        public List<string> Urls { get; set; } = new List<string>();
    }

    /// <summary>
    ///     Add or delete QnA Pairs and / or URLs to an existing knowledge base.
    /// </summary>
    public class UpdateKnowledgeBaseRequest
    {
        /// <summary>
        ///     Knowledge base identity.
        /// </summary>
        [JsonIgnore]
        public Guid KnowledgeBaseId { get; set; }

        /// <summary>
        ///     Data to be added to the knowledge base.
        /// </summary>
        [JsonProperty("add")]
        public UpdateActions Add { get; set; } = new UpdateActions();

        /// <summary>
        ///     Data to be deleted from the knowledge base
        /// </summary>
        [JsonProperty("delete")]
        public UpdateActions Delete { get; set; } = new UpdateActions();
    }
}