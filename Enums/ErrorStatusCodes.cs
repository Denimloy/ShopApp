using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Enums
{
    public enum ErrorStatusCodes
    {
        [Description("Bad Request")]
        BadRequest = 400,
        [Description("Unauthorized")]
        Unauthorized = 401,
        [Description("Payment Required")]
        PaymentRequired = 402,
        [Description("Forbidden")]
        Forbidden = 403,
        [Description("Not Found")]
        NotFound = 404,
        [Description("Method Not Allowed")]
        MethodNotAllowed = 405,
        [Description("Not Acceptable")]
        NotAcceptable = 406,
        [Description("Proxy Authentication Required")]
        ProxyAuthenticationRequired = 407,
        [Description("Request Timeout")]
        RequestTimeout = 408,
        [Description("Conflict")]
        Conflict = 409,
        [Description("Gone")]
        Gone = 410,
        [Description("Length Required")]
        LengthRequired = 411,
        [Description("Precondition Failed")]
        PreconditionFailed = 412,
        [Description("Request Entity Too Large")]
        RequestEntityTooLarge = 413,
        [Description("URI Too Long")]
        RequestUriTooLong = 414,
        [Description("Unsupported Media Type ")]
        UnsupportedMediaType = 415,
        [Description("Requested Range Not Satisfiable")]
        RequestedRangeNotSatisfiable = 416,
        [Description("Expectation Failed")]
        ExpectationFailed = 417,
        [Description("Misdirected Request")]
        MisdirectedRequest = 421,
        [Description("Unprocessable Entity")]
        UnprocessableEntity = 422,
        [Description("Locked")]
        Locked = 423,
        [Description("Failed Dependency")]
        FailedDependency = 424,
        [Description("Upgrade Required")]
        UpgradeRequired = 426,
        [Description("Precondition Required")]
        PreconditionRequired = 428,
        [Description("Too Many Requests")]
        TooManyRequests = 429,
        [Description("Request Header Fields Too Large")]
        RequestHeaderFieldsTooLarge = 431,
        [Description("Unavailable For Legal Reasons")]
        UnavailableForLegalReasons = 451,
        [Description("Internal Server Error")]
        InternalServerError = 500,
        [Description("Not Implemented")]
        NotImplemented = 501,
        [Description("Bad Gateway")]
        BadGateway = 502,
        [Description("Service Unavailable")]
        ServiceUnavailable = 503,
        [Description("Gateway Timeout")]
        GatewayTimeout = 504,
        [Description("HTTP Version Not Supported")]
        HttpVersionNotSupported = 505,
        [Description("Variant Also Negotiates")]
        VariantAlsoNegotiates = 506,
        [Description("Insufficient Storage")]
        InsufficientStorage = 507,
        [Description("Loop Detected")]
        LoopDetected = 508,
        [Description("Not Extended")]
        NotExtended = 510,
        [Description("Network Authentication Required")]
        NetworkAuthenticationRequired = 511
    }
}
