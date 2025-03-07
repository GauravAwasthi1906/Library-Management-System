using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using BusinessLayer.Utilities;
using DataAccessLayer.Entities;
using DataAccessLayer.GenericRepository.Interface;
using DataAccessLayer.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLayer.Services.Services
{
    public class AuthService : IAuthService
    {   
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;
        private readonly IGenericRepository<Employee> _generic;
        private readonly IAuthRepository _auth;
        private readonly PasswordHasher<object> _passwordHasher;
        public AuthService(IConfiguration configuration, ILogger<AuthService> logger, IGenericRepository<Employee> employee, IAuthRepository auth)
        {
            _configuration=configuration;
            _generic = employee;
            _logger = logger;
            _auth = auth;
            _passwordHasher = new PasswordHasher<object>();
        }
        public async Task<ServiceResponse> SignIn(Employee employee)
        {
            try
            {
                var data = await _auth.GetUserByMail(employee.Email);
                if (data == null)
                {
                    _logger.LogError("Incorrect Email");
                    return new ServiceResponse(false, "Incorrect Email");
                }

                var result = _passwordHasher.VerifyHashedPassword(null, data.Password, employee.Password);
                if (result != PasswordVerificationResult.Success)
                {
                    _logger.LogWarning("Incorrect Password");
                    return new ServiceResponse(false, "Incorrect Password");
                }

                _logger.LogInformation("Login process completed successfully");

                // Read Secret Key from appsettings.json
                var secretKey = _configuration["JwtSettings:SecretKey"];
                var key = Encoding.UTF8.GetBytes(secretKey);

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, data.Id.ToString()),
                new Claim(ClaimTypes.Email, data.Email),
                new Claim("FullName", data.Full_Name),
                new Claim("Date", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"))
            };

                var tokenHandler = new JwtSecurityTokenHandler();
                var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

                var token = new JwtSecurityToken(
                    issuer: _configuration["JwtSettings:Issuer"],
                    audience: _configuration["JwtSettings:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: signingCredentials
                );

                var jwtToken = tokenHandler.WriteToken(token);

                return new ServiceResponse(true, jwtToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during login: {ex.Message}");
                return new ServiceResponse(false, ex.Message);
            }
        }


        public async Task<ServiceResponse> SignUp(Employee employee)
        {
            try {
                var matching = await _auth.GetUserByMail(employee.Email);
                if (matching != null) {
                    return new ServiceResponse(false, "Employee Already Exists");
                }
                var password = _passwordHasher.HashPassword(null, employee.Password);
                var emp = new Employee
                { 
                    Full_Name = employee.Full_Name,
                    Email = employee.Email,
                    Password = password,
                    Designation=employee.Designation
                };
                await _generic.AddNewData(emp);
                return new ServiceResponse(true, "Employee Added Successfully");
            }
            catch(Exception ex) {
                _logger.LogError($"{ex.Message}");
                return new ServiceResponse(false, ex.Message);
            }
        }
    }
}
