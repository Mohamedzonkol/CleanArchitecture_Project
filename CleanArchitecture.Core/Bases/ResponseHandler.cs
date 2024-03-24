using CleanArchitecture.Core.SheardResourses;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Core.Bases;

public class ResponseHandler(IStringLocalizer<SheardResourses.SheardResourses> stringLocalizer)
{


    public Response<T> Deleted<T>(string Message = null)
    {
        return new Response<T>()
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Succeeded = true,
            Message = Message == null ? stringLocalizer[SheardResoursesKeys.Deleted] : Message
        };
    }
    public Response<T> Success<T>(T entity, string Message = null, object Meta = null)
    {
        return new Response<T>()
        {
            Data = entity,
            StatusCode = System.Net.HttpStatusCode.OK,
            Succeeded = true,
            Message = Message == null ? stringLocalizer[SheardResoursesKeys.Created] : Message,
            Meta = Meta
        };
    }
    public Response<T> Unauthorized<T>()
    {
        return new Response<T>()
        {
            StatusCode = System.Net.HttpStatusCode.Unauthorized,
            Succeeded = true,
            Message = stringLocalizer[SheardResoursesKeys.UnAuthorized]
        };
    }
    public Response<T> BadRequest<T>(string Message = null)
    {
        return new Response<T>()
        {
            StatusCode = System.Net.HttpStatusCode.BadRequest,
            Succeeded = false,
            Message = Message == null ? stringLocalizer[SheardResoursesKeys.BadRequest] : Message
        };
    }
    public Response<T> UnprocessableEntity<T>(string Message = null)
    {
        return new Response<T>()
        {
            StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
            Succeeded = false,
            Message = Message == null ? "Unprocessable Entity" : Message
        };
    }

    public Response<T> NotFound<T>(string message = null)
    {
        return new Response<T>()
        {
            StatusCode = System.Net.HttpStatusCode.NotFound,
            Succeeded = false,
            Message = message == null ? stringLocalizer[SheardResoursesKeys.NotFound] : message
        };
    }

    public Response<T> Created<T>(T entity, object Meta = null)
    {
        return new Response<T>()
        {
            Data = entity,
            StatusCode = System.Net.HttpStatusCode.Created,
            Succeeded = true,
            Message = stringLocalizer[SheardResoursesKeys.Created],
            Meta = Meta
        };
    }
}