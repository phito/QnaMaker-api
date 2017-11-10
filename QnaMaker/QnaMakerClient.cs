using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QnaMakerApi.Exceptions;
using QnaMakerApi.Requests;
using QnaMakerApi.Responses;

namespace QnaMakerApi
{
    public class QnaMakerClient : IDisposable
    {
        private const string BaseAddress = "https://westus.api.cognitive.microsoft.com/qnamaker/v2.0/knowledgebases/";
        private const int MaxQnaPairs = 1000;
        private const int MaxQnaUrls = 5;

        private readonly HttpClient _client;

        public QnaMakerClient(string subscriptionKey)
        {
            SubscriptionKey = subscriptionKey;

            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseAddress)
            };
        }

        /// <summary>
        ///     Subscription key which provides access to this API.
        /// </summary>
        public string SubscriptionKey { get; set; }

        public void Dispose()
        {
            _client.Dispose();
        }

        #region Api Methods

        #region Create Knowledge Base

        /// <summary>
        ///     Creates a new knowledge base.
        /// </summary>
        /// <param name="name">Friendly name for the knowledge base.</param>
        /// <param name="qnapairs">
        ///     List of question and answer pairs to be added to the knowledge base.
        ///     Max 1000 Q-A pairs per request.
        /// </param>
        /// <param name="urls">
        ///     List of URLs to be processed and indexed in the knowledge base.
        ///     In case of existing URL, it will be fetched again and KB will be updated with new data.
        ///     Max 5 urls per request.
        /// </param>
        /// <exception cref="QnaMakerError" />
        /// <returns>
        ///     <see cref="CreateKnowledgeBaseResponse" />
        /// </returns>
        public async Task<CreateKnowledgeBaseResponse> CreateKnowledgeBase(string name, List<QnaPair> qnapairs = null,
            List<string> urls = null)
        {
            if (qnapairs == null)
            {
                qnapairs = new List<QnaPair>();
            }
            if (urls == null)
            {
                urls = new List<string>();
            }
            return await CreateKnowledgeBase(new CreateKnowledgeBaseRequest
            {
                Name = name,
                QnaPairs = qnapairs,
                Urls = urls
            });
        }

        /// <summary>
        ///     Creates a new knowledge base.
        /// </summary>
        /// <exception cref="QnaMakerError" />
        /// <returns>
        ///     <see cref="CreateKnowledgeBaseResponse" />
        /// </returns>
        public async Task<CreateKnowledgeBaseResponse> CreateKnowledgeBase(CreateKnowledgeBaseRequest req)
        {
            if (string.IsNullOrEmpty(req.Name))
            {
                throw new ArgumentNullException(nameof(req.Name));
            }
            if (req.QnaPairs != null && req.QnaPairs.Count > MaxQnaPairs)
            {
                throw new ArgumentOutOfRangeException(nameof(req.QnaPairs), $"Max {MaxQnaPairs} Q-A pairs per request");
            }
            if (req.Urls != null && req.Urls.Count > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(req.Urls), $"Max {MaxQnaUrls} urls per request");
            }

            return await Send<CreateKnowledgeBaseResponse>(HttpMethod.Post, "create", req);
        }

        #endregion

        #region Delete Knowledge Base

        /// <summary>
        ///     Deletes the specified knowledge base and all data associated with it.
        /// </summary>
        /// <param name="id">Knowledge base identity.</param>
        /// <returns>
        ///     <see cref="DeleteKnowledgeBaseResponse" />
        /// </returns>
        public async Task<DeleteKnowledgeBaseResponse> DeleteKnowledgeBase(Guid id)
        {
            return await DeleteKnowledgeBase(new DeleteKnowledgeBaseRequest
            {
                KnowledgeBaseId = id
            });
        }

        /// <summary>
        ///     Deletes the specified knowledge base and all data associated with it.
        /// </summary>
        /// <returns>
        ///     <see cref="DeleteKnowledgeBaseResponse" />
        /// </returns>
        public async Task<DeleteKnowledgeBaseResponse> DeleteKnowledgeBase(DeleteKnowledgeBaseRequest req)
        {
            return await Send<DeleteKnowledgeBaseResponse>(HttpMethod.Delete, $"{req.KnowledgeBaseId}");
        }

        #endregion

        #region Download Knowledge Base

        /// <summary>
        ///     Downloads all the data associated with the specified knowledge base.
        /// </summary>
        /// <param name="id">Knowledge base identity.</param>
        /// <exception cref="QnaMakerError" />
        /// <returns>
        ///     <see cref="DownloadKnowledgeBaseResponse" />
        /// </returns>
        public async Task<DownloadKnowledgeBaseResponse> DownloadKnowledgeBase(Guid id)
        {
            return await DownloadKnowledgeBase(new DownloadKnowledgeBaseRequest
            {
                KnowledgeBaseId = id
            });
        }

        /// <summary>
        ///     Downloads all the data associated with the specified knowledge base.
        /// </summary>
        /// <exception cref="QnaMakerError" />
        /// <returns>
        ///     <see cref="DownloadKnowledgeBaseResponse" />
        /// </returns>
        public async Task<DownloadKnowledgeBaseResponse> DownloadKnowledgeBase(DownloadKnowledgeBaseRequest req)
        {
            var response = await Send(HttpMethod.Get, $"{req.KnowledgeBaseId}");
            return new DownloadKnowledgeBaseResponse
            {
                BlobUrl = response.Replace("\"", "")
            };
        }

        #endregion

        #region Generate Answer

        /// <summary>
        ///     Returns the list of answers for the given question sorted in descending order of ranking score.
        /// </summary>
        /// <param name="id">Knowledge base identity.</param>
        /// <param name="question">User question to be queried against your knowledge base.</param>
        /// <param name="top">Number of ranked results you want in the output.</param>
        /// <exception cref="QnaMakerError" />
        /// <returns>
        ///     <see cref="GenerateAnswerResponse" />
        /// </returns>
        public async Task<GenerateAnswerResponse> GenerateAnswer(Guid id, string question, int top = 1)
        {
            if (string.IsNullOrEmpty(question))
            {
                throw new ArgumentNullException(nameof(question));
            }
            return await GenerateAnswer(new GenerateAnswerRequest
            {
                KnowledgeBaseId = id,
                Question = question,
                Top = top
            });
        }

        /// <summary>
        ///     Returns the list of answers for the given question sorted in descending order of ranking score.
        /// </summary>
        /// <exception cref="QnaMakerError" />
        /// <returns>
        ///     <see cref="GenerateAnswerResponse" />
        /// </returns>
        public async Task<GenerateAnswerResponse> GenerateAnswer(GenerateAnswerRequest req)
        {
            return await Send<GenerateAnswerResponse>(HttpMethod.Post, $"{req.KnowledgeBaseId}/generateAnswer", req);
        }

        #endregion

        #region Publish Knowledge Base

        /// <summary>
        ///     Publish all unpublished in the knowledgebase to the prod endpoint.
        /// </summary>
        /// <param name="id">Knowledge base identity.</param>
        /// <exception cref="QnaMakerError" />
        /// <returns>
        ///     <see cref="PublishKnowledgeBaseResponse" />
        /// </returns>
        public async Task<PublishKnowledgeBaseResponse> PublishKnowledgeBase(Guid id)
        {
            return await PublishKnowledgeBase(new PublishKnowledgeBaseRequest
            {
                KnowledgeBaseId = id
            });
        }

        /// <summary>
        ///     Publish all unpublished in the knowledgebase to the prod endpoint.
        /// </summary>
        /// <exception cref="QnaMakerError" />
        /// <returns>
        ///     <see cref="PublishKnowledgeBaseResponse" />
        /// </returns>
        public async Task<PublishKnowledgeBaseResponse> PublishKnowledgeBase(PublishKnowledgeBaseRequest req)
        {
            return await Send<PublishKnowledgeBaseResponse>(HttpMethod.Put, $"{req.KnowledgeBaseId}");
        }

        #endregion

        #region Update Knowledge Base

        /// <summary>
        ///     Add or delete QnA Pairs and / or URLs to an existing knowledge base.
        /// </summary>
        /// <param name="id">Knowledge base identity.</param>
        /// <param name="add">Data to be added to the knowledge base.</param>
        /// <param name="delete">Data to be deleted from the knowledge base.</param>
        /// <exception cref="QnaMakerError" />
        /// <returns>
        ///     <see cref="UpdateKnowledgeBaseResponse" />
        /// </returns>
        public async Task<UpdateKnowledgeBaseResponse> UpdateKownledgeBase(Guid id, UpdateActions add = null,
            UpdateActions delete = null)
        {
            if (add == null)
            {
                add = new UpdateActions();
            }
            if (delete == null)
            {
                delete = new UpdateActions();
            }
            return await UpdateKownledgeBase(new UpdateKnowledgeBaseRequest
            {
                KnowledgeBaseId = id,
                Add = add,
                Delete = delete
            });
        }

        /// <summary>
        ///     Add or delete QnA Pairs and / or URLs to an existing knowledge base.
        /// </summary>
        /// <exception cref="QnaMakerError" />
        /// <returns>
        ///     <see cref="UpdateKnowledgeBaseResponse" />
        /// </returns>
        public async Task<UpdateKnowledgeBaseResponse> UpdateKownledgeBase(UpdateKnowledgeBaseRequest req)
        {
            return await Send<UpdateKnowledgeBaseResponse>(new HttpMethod("PATCH"), $"{req.KnowledgeBaseId}", req);
        }

        #endregion

        #endregion

        #region Web Methods

        private async Task<TR> Send<TR>(HttpMethod method, string url, object data = null)
        {
            using (var request = new HttpRequestMessage(method, url))
            {
                if (data != null && method == HttpMethod.Post)
                {
                    var body = JsonConvert.SerializeObject(data);
                    request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                }
                request.Headers.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);
                using (var response = await _client.SendAsync(request))
                {
                    return await GetResponseContent<TR>(response);
                }
            }
        }

        private async Task<string> Send(HttpMethod method, string url, object data = null)
        {
            using (var request = new HttpRequestMessage(method, url))
            {
                if (data != null && method == HttpMethod.Post)
                {
                    var body = JsonConvert.SerializeObject(data);
                    request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                }
                request.Headers.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);
                using (var response = await _client.SendAsync(request))
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }

        private async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            content = WebUtility.HtmlDecode(content);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(content);
            }

            if (!string.IsNullOrEmpty(content))
            {
                var error = JsonConvert.DeserializeObject<QnaMakerErrorJson>(content);
                throw new QnaMakerException(response.StatusCode, error.Error);
            }
            throw new QnaMakerException(response.StatusCode);
        }

        #endregion
    }
}