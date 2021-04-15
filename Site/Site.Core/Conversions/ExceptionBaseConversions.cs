using Site.Core.DTO.Common;
using Site.Core.Exceptions;

namespace Site.Core.Conversions
{
    public static class ExceptionBaseConversions
    {
        public static ErrorDto AsDto(this ExceptionBase exceptionBase) => new ErrorDto
            {Code = exceptionBase.Code, Reason = exceptionBase.Reason};
    }
}