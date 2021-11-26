using Microsoft.AspNetCore.Http;
using System;

namespace Seventy.Web.StartupCustomizations
{
    public static class SessionExtension
    {

        public static bool CreateSession(this ISession session, string key, string value)
        {
            var old = session.GetString(key.ToString());
            if (!String.IsNullOrEmpty(old))
                session.Remove(key.ToString());
            session.SetString(key.ToString(), value);
            return true;
        }

        public static string GetSession(this ISession session, string key)
        {
            var old = session.GetString(key.ToString());
            return old;

        }

        public static bool RemoveSession(this ISession session, string key)
        {
            session.Remove(key);
            return true;

        }
    }
}
