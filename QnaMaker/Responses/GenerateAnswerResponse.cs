using System.Collections.Generic;
using Newtonsoft.Json;

namespace QnaMakerApi.Responses
{
    public class AnswerItem
    {
        [JsonProperty("answer")]
        public string Answer { get; set; }

        [JsonProperty("score")]
        public float Score { get; set; }
    }

    /// <summary>
    ///     A successful call returns the result of the question.
    /// </summary>
    public class GenerateAnswerResponse
    {
        /// <summary>
        ///     List of answers for the user query sorted in decreasing order of ranking score.
        /// </summary>
        [JsonProperty("answers")]
        public IList<AnswerItem> Answers { get; set; }
    }
}