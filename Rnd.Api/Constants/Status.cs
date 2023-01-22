using System.Net;

namespace Rnd.Api.Constants;

public readonly struct Status
{
    public const int Ok = (int) HttpStatusCode.OK;
    public const int BadRequest = (int) HttpStatusCode.BadRequest;
}