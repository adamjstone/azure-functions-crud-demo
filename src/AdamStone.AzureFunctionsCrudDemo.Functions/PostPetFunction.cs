using AdamStone.AzureFunctionsCrudDemo.Domain.DataPersistence;
using AdamStone.AzureFunctionsCrudDemo.Domain.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;

namespace AdamStone.AzureFunctionsCrudDemo.Functions
{
    /// <summary>
    /// Represents a function that handles HTTP POST requests for a <see cref="Pet" />.
    /// </summary>
    public class PostPetFunction
    {
        private readonly PetDbContext DbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostPetFunction" /> class.
        /// </summary>
        /// <param name="dbContext">
        /// A type that is used to access persisted data.
        /// </param>
        public PostPetFunction(PetDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Handles an HTTP POST request for a <see cref="Pet"/>.
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
        [Function("PostPetFunction")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req, FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("PostPetFunction");
            logger.LogInformation($"An HTTP GET request for a {nameof(Pet)} was received.");
            var bufferLength = Convert.ToInt32(req.Body.Length);

            if (bufferLength == 0)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                var buffer = new Byte[bufferLength];
                req.Body.Read(buffer, 0, bufferLength);
                using var stream = new MemoryStream(buffer);
                var serializer = new DataContractJsonSerializer(typeof(Pet));
                var entity = serializer.ReadObject(stream) as Pet;
                DbContext.Add(entity);
                DbContext.SaveChanges();
                var response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                logger.LogInformation($"An HTTP POST request for a {nameof(Pet)} was processed successfully.");
                return response;
            }
            catch (Exception exception)
            {
                logger.LogInformation($"An HTTP POST request for a {nameof(Pet)} failed. {exception.Message}");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}