using System.Net;

namespace Application.Common.Errors;

public interface IErrorCustom
{
    HttpStatusCode StatusCode  { get; set; }
}