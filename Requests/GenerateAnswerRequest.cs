using System;
using Newtonsoft.Json;

namespace QnaMakerApi.Requests
{
    /// <summary>
    ///     Returns the list of answers for the given question sorted in descending order of ranking score.
    /// </summary>
    public class GenerateAnswerRequest
    {
        /// <summary>
        ///     Knowledge base identity.
        /// </summary>
        [JsonIgnore]
        public Guid KnowledgeBaseId { get; set; }

        /// <summary>
        ///     User question to be queried against your knowledge base.
        /// </summary>
        [JsonProperty("question")]
        public string Question { get; set; }

        /// <summary>
        ///     Number of ranked results you want in the output. Default is 1.
        /// </summary>
        [JsonProperty("top")]
        public int Top { get; set; } = 1;
    }
}