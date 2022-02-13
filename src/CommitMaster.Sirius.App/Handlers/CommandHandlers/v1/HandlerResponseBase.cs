using System.Collections.Generic;
using System.Linq;
using System.Net;
using CommitMaster.Sirius.App.Responses.v1;
using CommitMaster.Sirius.Domain.Contracts.v1.Validation;

namespace CommitMaster.Sirius.App.Handlers.CommandHandlers.v1
{
    public abstract class HandlerResponseBase
    {
        private List<Error> MapValidationResults(List<System.ComponentModel.DataAnnotations.ValidationResult> errors)
        {
            return errors.Select(e => new Error
                { Code = e.MemberNames.First(), Message = e.ErrorMessage }).ToList();
        }


        public HandlerResponse<T> FailValidation<T>(List<System.ComponentModel.DataAnnotations.ValidationResult> errors)
        {
            return new HandlerResponse<T> {
                Success = false,
                StatusCode = HttpStatusCode.BadRequest,
                Errors = MapValidationResults(errors)
            };
        }
        
        public HandlerResponse<T> ErroInterno<T>()
        {
            return new HandlerResponse<T> {
                Success = false,
                StatusCode = HttpStatusCode.BadRequest,
                Errors = new List<Error>{ new Error{Code = "Erro interno", Message = "Erro interno"} }
            };
        }
        
        public HandlerResponse<T> SucessoCriado<T>(T data)
        {
            return new HandlerResponse<T> {
                Success = true,
                StatusCode = HttpStatusCode.Created,
                Errors = null,
                Data = data
            };
        }
        
        public HandlerResponse<T> Sucesso<T>(T data)
        {
            return new HandlerResponse<T> {
                Success = true,
                StatusCode = HttpStatusCode.OK,
                Errors = null,
                Data = data
            };
        }
        
        public HandlerResponse<T> ErroCommand<T>(string code, string message)
        {
            return new HandlerResponse<T> {
                Success = false,
                StatusCode = HttpStatusCode.BadRequest,
                Errors =  new List<Error>{ new (){Code = code, Message = message} }
            };
        }
        
       
    }
}
