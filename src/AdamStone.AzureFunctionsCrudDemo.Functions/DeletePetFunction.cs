using AdamStone.AzureFunctionsCrudDemo.Domain.DataPersistence;
using AdamStone.AzureFunctionsCrudDemo.Domain.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Web;

namespace AdamStone.AzureFunctionsCrudDemo.Functions
{
    /// <summary>
    /// Represents a function that handles HTTP DELETE requests for a <see cref="Pet" />.
    /// </summary>
    public class DeletePetFunction
    {
        private readonly PetDbContext DbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeletePetFunction" /> class.
        /// </summary>
        /// <param name="dbContext">
        /// A type that is used to access persisted data.
        /// </param>
        public DeletePetFunction(PetDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        /// Handles an HTTP DELETE request for a <see cref="Pet"/>.
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
        [Function("DeletePetFunction")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequestData req, FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("DeletePetFunction");
            logger.LogInformation($"An HTTP DELETE request for a {nameof(Pet)} was received.");
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

                DbContext.Remove(entity);
                DbContext.SaveChanges();

                var response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                logger.LogInformation($"An HTTP DELETE request for a {nameof(Pet)} was processed successfully.");
                return response;
            }
            catch (Exception exception)
            {
                logger.LogInformation($"An HTTP DELETE request for a {nameof(Pet)} failed. {exception.Message}");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
