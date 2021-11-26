using System;
using System.ComponentModel;

namespace Seventy.DomainClass.Core
{
    public interface ICoreBase
    {
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int? RegUserID { get; set; }
        public DateTime RegDate { get; set; }
    }
    public abstract class CoreBase<TKey> : ICoreBase where TKey : struct
    {
        public TKey? ID { get; set; }

        [DisplayName("توضیح")]
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
        public int? RegUserID { get; set; }
        public DateTime RegDate { get; set; } = DateTime.Now;
    }
    public abstract class CoreBase : CoreBase<int>
    {
    }
}
