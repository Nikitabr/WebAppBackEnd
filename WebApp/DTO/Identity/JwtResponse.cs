﻿namespace WebApp.DTO.Identity;

public class JwtResponse
{
    public string Token { get; set; } = default!;

    public string RefreshToken { get; set; } = default!;
    
    //extra fields?
    public string Email { get; set; } = default!;
}