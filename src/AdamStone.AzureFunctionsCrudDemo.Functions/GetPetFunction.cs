using AdamStone.AzureFunctionsCrudDemo.Domain.DataPersistence;
using AdamStone.AzureFunctionsCrudDemo.Domain.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;

namespace AdamStone.AzureFunctionsCrudDemo.Functions
{
    /// <summary>
    /// Represents a function that handles HTTP GET requests for a <see cref="Pet" />.
    /// </summary>
    public class GetPetFunction
    {
        private readonly PetDbContext DbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPetFunction" /> class.
        /// </summary>
        /// <param name="dbContext">
        /// A type that is used to access persisted data.
        /// </param>
        public GetPetFunction(PetDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Handles an HTTP GET request for a <see cref="Pet"/>.
        /// </summary>
        /// <param name="req">
        /// The request data.
        /// </param>
        /// <param name="executionContext">
        /// Contextual information about the function execution.
        /// </param>
        /// <returns>
        /// An HTTP response.
        /// </returns>
        [Function("GetPetFunction")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req, FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("GetPetFunction");
            logger.LogInformation($"An HTTP GET request for a {nameof(Pet)} was received.");
            var query = HttpUtility.ParseQueryString(req.Url.Query);
            var id = query?["id"];

            if (String.IsNullOrEmpty(id))
            {
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                var entity = DbContext.Pets.Find(id);

                if (entity is null)
                {
                    return req.CreateResponse(HttpStatusCode.NotFound);
                }

                var serializer = new DataContractJsonSerializer(typeof(Pet));
                using var stream = new MemoryStream();
                serializer.WriteObject(stream, entity);
                var buffer = stream.ToArray();
                var response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                response.Body.Write(buffer, 0, buffer.Length);
                logger.LogInformation($"An HTTP GET request for a {nameof(Pet)} was processed successfully.");
                return response;
            }
            catch (Exception exception)
            {
                logger.LogInformation($"An HTTP GET request for a {nameof(Pet)} failed. {exception.Message}");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
