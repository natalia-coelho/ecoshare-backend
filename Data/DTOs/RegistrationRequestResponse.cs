﻿using ecoshare_backend.Data.DTOs;

namespace ecoshare_backend.Models.DTOs
{
    public class RegistrationRequestResponse : AuthResult
    {
        public RegistrationRequestResponse() { }
        public bool Result { get; set; }
    }
}
