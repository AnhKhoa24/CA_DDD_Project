using System.Net;
using FluentResults;

namespace Application.Common.Errors;

public class DuplicateEmailError : IError
{
    public List<IError> Reasons => new List<IError>();

    public string Message => "This email already exits.";

    public Dictionary<string, object> Metadata => new Dictionary<string, object>();
    public string Detail => "Hihiahaha";
}