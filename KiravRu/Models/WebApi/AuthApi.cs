﻿using System.Collections.Generic;

namespace KiravRu.Models.WebApi
{
    public class AuthApi
    {
        public string UserName { get; set; }
        public List<string> Errors { get; set; }
        public int ResultCode { get; set; }

        public AuthApi()
        {
            Errors = new List<string>();
            ResultCode = 0;
        }
    }
}
