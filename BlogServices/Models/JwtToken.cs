﻿namespace BlogServices.Models
{
    public class JwtToken
    {
        public string Token { get; set; }
        public long Expires { get; set; }
    }
}
