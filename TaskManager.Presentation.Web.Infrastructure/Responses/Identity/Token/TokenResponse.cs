﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TaskManager.Presentation.Web.Infrastructure.Responses.Identity.Token
{
    public class TokenResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public bool IsVerified { get; set; }
        public string JWToken { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }
    }
}
