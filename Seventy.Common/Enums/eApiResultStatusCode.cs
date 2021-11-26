using System.ComponentModel.DataAnnotations;

namespace Seventy.Common.Enums
{
    public enum eApiResultStatusCode
    {
        [Display(Name = "Proccess has been done successfully")]
        Success = 0,

        [Display(Name = "Exception happened")]
        ServerError = 1,

        [Display(Name = "Parameters are not valid")]
        BadRequest = 2,

        [Display(Name = "Not found")]
        NotFound = 3,

        [Display(Name = "List empty")]
        ListEmpty = 4,

        [Display(Name = "Exception happened in process")]
        LogicError = 5,

        [Display(Name = "Authentication exception")]
        UnAuthorized = 6
    }
}
