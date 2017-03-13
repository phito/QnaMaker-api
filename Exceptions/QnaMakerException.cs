using System;
using System.Net;
using Newtonsoft.Json;

namespace QnaMakerApi.Exceptions
{
    public class QnaMakerError
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    internal class QnaMakerErrorJson
    {
        [JsonProperty("error")]
        public QnaMakerError Error { get; set; }
    }

    public class QnaMakerException : Exception
    {
        public QnaMakerException()
        {
        }

        public QnaMakerException(HttpStatusCode code)
        {
            HttpStatusCode = code;
        }

        public QnaMakerException(HttpStatusCode code, QnaMakerError error)
        {
            HttpStatusCode = code;
            Error = error;
        }

        [JsonIgnore]
        public HttpStatusCode HttpStatusCode { get; set; }

        public QnaMakerError Error { get; set; }
    }
}