namespace App.Public.DTO.v1.Identity;

public class JwtResponse
{
    public string Token { get; set; } = default!;

    public string RefreshToken { get; set; } = default!;
    
    //extra fields?
    
    public string FirstName { get; set; } = default!;
    
    public string LastName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public Guid AppUserId { get; set; } = default!;
}