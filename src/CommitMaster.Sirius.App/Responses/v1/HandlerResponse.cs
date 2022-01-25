using System.Collections.Generic;
using System.Net;
using CommitMaster.Sirius.Domain.Contracts.v1.Validation;

namespace CommitMaster.Sirius.App.Responses.v1
{
    public class HandlerResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public IList<Error> Errors { get; set; }
    }
}
