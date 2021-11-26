using Seventy.Common.Enums;
using System;

namespace Seventy.WebFramework.Filters
{
    public class MenuAttribute : Attribute
    {
        private readonly eMenu eMenu;
        private readonly eModule eModule;
        private readonly int order;
        public MenuAttribute(eMenu eMenu,eModule eModule, int order)
        {
            this.eMenu = eMenu;
            this.eModule = eModule;
            this.order = order;
        }
        public virtual eMenu Menu
        {
            get { return eMenu; }
        }
        public virtual eModule Module
        {
            get { return eModule; }
        }
        public virtual int Order
        {
            get { return order; }
        }
    }
}