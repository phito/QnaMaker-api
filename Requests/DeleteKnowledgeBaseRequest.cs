using System;

namespace QnaMakerApi.Requests
{
    /// <summary>
    ///     Deletes the specified knowledge base and all data associated with it.
    /// </summary>
    public class DeleteKnowledgeBaseRequest
    {
        /// <summary>
        ///     Knowledge base identity.
        /// </summary>
        public Guid KnowledgeBaseId { get; set; }
    }
}