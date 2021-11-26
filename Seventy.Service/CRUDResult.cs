using Microsoft.EntityFrameworkCore.ChangeTracking;
using Seventy.DomainClass.Core;

namespace Seventy.Service
{
    public class CRUDResult
    {
        public bool Successful { get; set; }
        public string Message { get; set; }
        public int? ResultID { get; set; }
        public ICoreBase ResultEntity { get; set; }
    }
}
