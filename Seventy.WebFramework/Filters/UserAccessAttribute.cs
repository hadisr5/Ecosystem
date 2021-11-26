using Seventy.Common.Enums;
using System;

namespace Seventy.WebFramework.Filters
{
    public class UserAccessAttribute : Attribute
    {
        private readonly eAccessControl userAccessControl;
        private readonly eAccessType userAccessType;
        private readonly int index;
        private readonly bool anonymouseAccess;
        public UserAccessAttribute(eAccessControl userAccessControl,eAccessType userAccessType, int index, bool anonymouseAccess = false)
        {
            this.userAccessControl = userAccessControl;
            this.userAccessType = userAccessType;
            this.index = index;
            this.anonymouseAccess = anonymouseAccess;
        }
        public virtual eAccessControl UserAccessControl
        {
            get { return userAccessControl; }
        }
        public virtual eAccessType UserAccessType
        {
            get { return userAccessType; }
        }
        public virtual int Index
        {
            get { return index; }
        }
        public virtual bool AnonymousAccess
        {
            get { return anonymouseAccess; }
        }
    }
}