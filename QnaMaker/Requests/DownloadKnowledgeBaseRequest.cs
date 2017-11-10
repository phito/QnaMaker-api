using System;

namespace QnaMakerApi.Requests
{
    /// <summary>
    ///     Downloads all the data associated with the specified knowledge base.
    /// </summary>
    public class DownloadKnowledgeBaseRequest
    {
        /// <summary>
        ///     Knowledge base identity.
        /// </summary>
        public Guid KnowledgeBaseId { get; set; }
    }
}