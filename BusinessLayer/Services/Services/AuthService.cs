using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using DataAccessLayer.Entities;
using DataAccessLayer.GenericRepository.Interface;
using DataAccessLayer.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLayer.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IGenericRepository<Employee> _generic;
        private readonly IAuthRepository _auth;
        private readonly PasswordHasher<object> _passwordHasher;
        public AuthService(ILogger<AuthService> logger, IGenericRepository<Employee> employee, IAuthRepository auth)
        {
            _generic = employee;
            _logger = logger;
            _auth = auth;
            _passwordHasher = new PasswordHasher<object>();
        }
        public async Task<ServiceResponse> SignIn(Employee employee)
        {
            try {
                var data = await _auth.GetUserByMail(employee.Email);
                if (data== null)
                {
                    _logger.LogError("Incorrect Email");
                    return new ServiceResponse(false,"Incorrect Email");
                }
                
                var result = _passwordHasher.VerifyHashedPassword(null, data.Password, employee.Password );
                if (result != PasswordVerificationResult.Success)
                {
                    _logger.LogWarning("Incorrect Password");
                    return new ServiceResponse(false, "Incorrect Password");
                }
                _logger.LogInformation("Logging process Done");
                var token = GenerateJwtToken(data);
                return new ServiceResponse(true, token);
            }
            catch(Exception ex) {
                _logger.LogError($"{ex.Message}");
                return new ServiceResponse(false,ex.Message);
            }
        }
        private string GenerateJwtToken(Employee employee)
        {
            var claims = new[]
            {
        new Claim("Id", employee.Id.ToString()), 
        new Claim("FullName", employee.Full_Name),
        new Claim(JwtRegisteredClaimNames.Sub, employee.Email),
        new Claim("Designation", employee.Designation),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKeyHere"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "YourIssuer", 
                audience: "YourAudience", 
                claims: claims,
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<ServiceResponse> SignUp(Employee employee)
        {
            try {
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
