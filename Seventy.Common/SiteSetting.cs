using System;
using System.Collections.Generic;
using System.Text;

namespace Seventy.Common
{
    public class SiteSetting
    {
        public string DefaultController { get; set; }
        public string DefaultAction { get; set; }
        public string ElmahPath { get; set; }
        public JwtSetting JwtSetting { get; set; }
        public IdentitySetting IdentitySetting { get; set; }
    }
    public class IdentitySetting
    {
        public bool PasswordRequireDigit { get; set; }
        public int PasswordRequiredLength { get; set; }
        public bool PasswordRequireNonAlphanumic { get; set; }
        public bool PasswordRequireUppercase { get; set; }
        public bool PasswordRequireLowercase { get; set; }
        public bool RequireUniqueEmail { get; set; }
    }
    public class JwtSetting
    {
        public string SecretKey { get; set; }
        public string Encryptkey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int NotBeforeTimeMinutes { get; set; }
        public int ExpirationMinutes { get; set; }

    }
}
