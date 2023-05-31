using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using App.Contracts.BLL;
using App.Domain.Identity;
using App.Public.Mappers.Identity;
using Base.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.DTO;
using AppUser = App.Public.DTO.v1.Identity.AppUser;
using JwtResponse = App.Public.DTO.v1.Identity.JwtResponse;


namespace WebApp.ApiControllers.Identity;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/identity/[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly SignInManager<App.Domain.Identity.AppUser> _signInManager;
    private readonly UserManager<App.Domain.Identity.AppUser> _userManager;
    private readonly ILogger<AccountController> _logger;
    private readonly IConfiguration _configuration;
    private readonly Random _rnd = new Random();
    private readonly IAppBLL _bll;

    public AccountController(SignInManager<App.Domain.Identity.AppUser> signInManager, UserManager<App.Domain.Identity.AppUser> userManager,
        ILogger<AccountController> logger, IConfiguration configuration, IAppBLL  bll)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
        _configuration = configuration;
        _bll = bll;
    }
    
    
    /// <summary>
    /// Gets user by id from backend
    /// </summary>
    /// <param name="id">Supply user id</param>
    /// <returns>User by id</returns>
    [Produces( "application/json" )]
    [Consumes( "application/json" )]
    [ProducesResponseType( typeof(AppUser), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<App.Public.DTO.v1.Identity.AppUser>> GetUserById(Guid id)
    {

        var res = await _bll.AppUsers.FirstOrDefaultAsync(id);
        
        return AppUserMapper.ToPublic(res!);
    }
    
    /// <summary>
    /// Login into the rest backend - generates JWT to be included in
    /// Authorize: Brearer xyz
    /// </summary>
    /// <param name="loginData">Supply email and password</param>
    /// <returns>JWT amd refresh token</returns>
    [HttpPost]
    public async Task<ActionResult<App.Public.DTO.v1.Identity.JwtResponse>> LogIn([FromBody] App.Public.DTO.v1.Identity.Login loginData)
    {
        // verify username
        var appUser = await _userManager.FindByEmailAsync(loginData.Email); 
        Console.WriteLine(appUser);
        if (appUser == null)
        {
            _logger.LogWarning("WebApi login failed, email {} not found", loginData.Email);
            await Task.Delay(_rnd.Next(100, 1000));
            return NotFound("User/Password problem");
        }

        // verify username and password
        var result = await _signInManager.CheckPasswordSignInAsync(appUser, loginData.Password, false);
        if (!result.Succeeded)
        {
            _logger.LogWarning("WebApi login failed, password problem for user {}", loginData.Email);
            await Task.Delay(_rnd.Next(100, 1000));
            return NotFound("User/Password problem");
        }

        // get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get ClaimsPrincipal for user {}", loginData.Email);
            await Task.Delay(_rnd.Next(100, 1000));
            return NotFound("User/Password problem");
        }
        
        
        // appUser.RefreshTokens = await _bll
        //     .Entry(appUser)
        //     .Collection(a => a.RefreshTokens!)
        //     .Query()
        //     .Where(t => t.AppUserId == appUser.Id)
        //     .ToListAsync();
        
        // generate jwt
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"],
            _configuration["JWT:Issuer"],
            _configuration["JWT:Issuer"],
            DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireInDays"))
        );

        var refreshTokenList = (await _bll.RefreshTokens.GetRefreshTokensByUser(appUser.Id)).ToList();

        Console.WriteLine(refreshTokenList.ToString());
        if (refreshTokenList.Count() != 0)
        {
            foreach (var userRefreshToken in refreshTokenList)
            {
                if (userRefreshToken.TokenExpirationDateTime < DateTime.UtcNow &&
                    userRefreshToken.PreviousTokenExpirationDateTime < DateTime.UtcNow)
                {
                    Console.WriteLine(userRefreshToken.Id); 
                    _bll.RefreshTokens.Remove(userRefreshToken);
                }
            }
        }

        var refreshToken = new App.BLL.DTO.Identity.RefreshToken();
            // refreshToken.AppUserId = appUser.Id;
            // _bll.RefreshTokens.Add(refreshToken);
            
            // var res = new JwtResponse()
            // {
            //     Token = jwt,
            //     RefreshToken = refreshToken.Token,
            //     Email = appUser.Email   
            // };

            // make new refresh token, obsolete old ones
            if (refreshTokenList != null && refreshTokenList.Count != 0)
            {
                refreshToken = refreshTokenList.First();
                refreshToken.Id = new Guid();
                refreshToken.PreviousToken = refreshToken.Token;
                refreshToken.PreviousTokenExpirationDateTime = DateTime.UtcNow.AddMinutes(1);
                refreshToken.Token = Guid.NewGuid().ToString();
                refreshToken.TokenExpirationDateTime = DateTime.UtcNow.AddDays(7);
                _bll.RefreshTokens.Add(refreshToken);
                await _bll.SaveChangesAsync();
            }
            else
            {
                refreshToken = new App.BLL.DTO.Identity.RefreshToken()
                {
                    AppUserId = appUser.Id,
                    Token = Guid.NewGuid().ToString(),
                    TokenExpirationDateTime = DateTime.UtcNow.AddDays(7)
                };
                // appUser.RefreshTokens = new List<RefreshToken>();
                _bll.RefreshTokens.Add(refreshToken);
                await _bll.SaveChangesAsync();
            }

            var res = new JwtResponse()
            {
                Token = jwt,
                RefreshToken = refreshToken.Token,
                Email = appUser.Email,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                AppUserId = appUser.Id
            };

            return Ok(res);
        }

    /// <summary>
    /// Register into the rest backend - generates JWT to be included in
    /// Authorize: Brearer xyz
    /// </summary>
    /// <param name="registrationData">Supply firstname, lastname, email and password</param>
    /// <returns>JWT and refresh token</returns>
    [HttpPost]
    public async Task<ActionResult<App.Public.DTO.v1.Identity.JwtResponse>> Register(App.Public.DTO.v1.Identity.Register registrationData)
    {
        // verify user
        var appUser = await _userManager.FindByEmailAsync(registrationData.Email);
        if (appUser != null)
        {
            _logger.LogWarning("User with email {} is already registered", registrationData.Email);
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Title = "App error",
                Status = HttpStatusCode.BadRequest,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            };
            errorResponse.Errors["email"] = new List<string>()
            {
                "Email already registered"
            };
            return BadRequest(errorResponse);
        }

        var refreshToken = new RefreshToken();
        appUser = new App.Domain.Identity.AppUser()
        {
            Email = registrationData.Email,
            UserName = registrationData.Email,
            FirstName = registrationData.FirstName,
            LastName = registrationData.LastName,
            RefreshTokens = new List<App.Domain.Identity.RefreshToken>()
            {
                refreshToken
            }
        }; 
        
        // create user (system will do it)
        var result = await _userManager.CreateAsync(appUser, registrationData.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        // get full user from system with fixed data
        appUser = await _userManager.FindByEmailAsync(appUser.Email);
        if (appUser == null)
        {
            _logger.LogWarning("User with email {} is not found after registration", registrationData.Email);
            return BadRequest($"User with email {registrationData.Email} is not found after registration");
        }


        // get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get ClaimsPrincipal for user {}", registrationData.Email);
            return NotFound("User/Password problem");
        }

        // generate jwt
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"],
            _configuration["JWT:Issuer"],
            _configuration["JWT:Issuer"],
            DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireInDays"))
        );

        var res = new JwtResponse()
        {
            Token = jwt,
            RefreshToken = refreshToken.Token,
            Email = appUser.Email,
            FirstName = appUser.FirstName,
            LastName = appUser.LastName,
            AppUserId = appUser.Id
        };

        return Ok(res);
    }

    /// <summary>
    /// Generates refreshed jwt and refresh token
    /// Authorize: Bearer xyz
    /// </summary>
    /// <param name="refreshTokenModel">Supply jwt and and refresh token</param>
    /// <returns>New token and refresh token and also user firstname and lastname</returns>
    [HttpPost]
    public async Task<ActionResult> RefreshToken([FromBody] App.Public.DTO.v1.Identity.RefreshTokenModel refreshTokenModel)
    {
        JwtSecurityToken jwtToken;
        // get user info from jwt
        try
        {
            jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshTokenModel.Jwt);
            if (jwtToken == null)
            {
                var errorResponse = new RestApiErrorResponse()
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "App error",
                    Status = HttpStatusCode.BadRequest,
                    TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                };

                errorResponse.Errors["jwt"] = new List<string>()
                {
                    "No token"
                };
                return BadRequest(errorResponse);
            }
        }
        catch (Exception e)
        {
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "App error",
                Status = HttpStatusCode.BadRequest,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            };

            errorResponse.Errors["token"] = new List<string>()
            {
                $"Cant parse the token, {e.Message}"
            };
            return BadRequest(errorResponse);
        }

        
        
        var userEmail = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        if (userEmail == null)
        {
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "App error",
                Status = HttpStatusCode.BadRequest,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            };

            errorResponse.Errors["email"] = new List<string>()
            {
                "No email in jwt"
            };
            return BadRequest(errorResponse);
        }
        
        // get user and tokens
        var appUser = await _userManager.FindByEmailAsync(userEmail);
        if (appUser == null)
        {
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "App error",
                Status = HttpStatusCode.NotFound,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            };

            errorResponse.Errors["email"] = new List<string>()
            {
                $"User with email {userEmail} not found"
            };
            return NotFound(errorResponse);        }
        
        // load and compare refresh tokens
        

        var refreshTokens = (await _bll.RefreshTokens.GetRefreshTokensByUser(appUser.Id)).ToList();
        
        // await _bll.Entry(appUser).Collection(u => u.RefreshTokens!)
        //     .Query()
        //     .Where(x => 
        //         (x.Token == refreshTokenModel.RefreshToken && x.TokenExpirationDateTime > DateTime.UtcNow) ||
        //         (x.PreviousToken == refreshTokenModel.RefreshToken && x.PreviousTokenExpirationDateTime > DateTime.UtcNow))
        //     .ToListAsync();

        var validRefreshTokens = new List<App.BLL.DTO.Identity.RefreshToken>();
        
        if (refreshTokens.Count != 0)
        {
            foreach (var token in refreshTokens)
            {
                if (
                    (token.Token == refreshTokenModel.RefreshToken && token.TokenExpirationDateTime > DateTime.UtcNow) ||
                    (token.PreviousToken == refreshTokenModel.RefreshToken && token.PreviousTokenExpirationDateTime > DateTime.UtcNow)
                )
                {
                    validRefreshTokens.Add(token);
                }
            }
        }
        else
        {
            return Problem("RefreshTokens collection is null");
        }
        
        if (validRefreshTokens.Count == 0)
        {
            return Problem("RefreshToken collection is empty, no valid refresh tokens found");
        }
        
        if (validRefreshTokens.Count != 1)
        {
            return Problem("More than one valid refresh token found.");
        }
        
        
        // generate new jwt
        
        // get claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get ClaimsPrincipal for user {}", userEmail);
            var errorResponse = new RestApiErrorResponse()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "App error",
                Status = HttpStatusCode.NotFound,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            };

            errorResponse.Errors["claims"] = new List<string>()
            {
                "User/Password problem"
            };
            return NotFound(errorResponse);
        }

        // generate jwt
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"],
            _configuration["JWT:Issuer"],
            _configuration["JWT:Issuer"],
            DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireInDays"))
        );
        
        // make new refresh token, obsolete old ones
        var refreshToken = validRefreshTokens.First();
        if (refreshToken.Token == refreshTokenModel.RefreshToken)
        {
            refreshToken.PreviousToken = refreshToken.Token;
            refreshToken.PreviousTokenExpirationDateTime = DateTime.UtcNow.AddMinutes(1);

            refreshToken.Token = Guid.NewGuid().ToString();
            refreshToken.TokenExpirationDateTime = DateTime.UtcNow.AddDays(7);

            _bll.RefreshTokens.Add(refreshToken);
            await _bll.SaveChangesAsync();
        }

        var res = new JwtResponse()
        {
            Token = jwt,
            RefreshToken = refreshToken.Token,
            Email = appUser.Email,
            FirstName = appUser.FirstName,
            LastName = appUser.LastName,
            AppUserId = appUser.Id
        };

        return Ok(res);

    }
}