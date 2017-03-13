using System.Collections.Generic;
using Newtonsoft.Json;

namespace QnaMakerApi.Requests
{
    public class QnaPair
    {
        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("answer")]
        public string Answer { get; set; }
    }

    /// <summary>
    ///     Creates a new knowledge base.
    /// </summary>
    public class CreateKnowledgeBaseRequest
    {
        /// <summary>
        ///     Friendly name for the knowledge base.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     List of question and answer pairs to be added to the knowledge base.
        ///     Max 1000 Q-A pairs per request.
        /// </summary>
        [JsonProperty("qnaPairs")]
        public List<QnaPair> QnaPairs { get; set; } = new List<QnaPair>();

        /// <summary>
        ///     List of URLs to be processed and indexed in the knowledge base.
        ///     In case of existing URL, it will be fetched again and KB will be updated with new data.
        ///     Max 5 urls per request.
        /// </summary>
        [JsonProperty("urls")]
        public List<string> Urls { get; set; } = new List<string>();
    }
}