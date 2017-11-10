namespace QnaMakerApi.Responses
{
    public class DownloadKnowledgeBaseResponse
    {
        /// <summary>
        ///     SAS url (valid for 30 mins) to tsv file in blob storage.
        /// </summary>
        public string BlobUrl { get; set; }
    }
}