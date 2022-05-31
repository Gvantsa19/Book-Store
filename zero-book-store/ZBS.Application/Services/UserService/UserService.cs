using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZBS.Application.Configuration.Models;
using ZBS.Application.Email;
using ZBS.Application.Services.UserServ.Dtoes;
using ZBS.Infrastructure.DBContexts;
using ZBS.Infrastructure.GenericRepositoryP;
using ZBS.Infrastructure.Models;
using ZBS.Infrastructure.Repositories.User;
using ZBS.Infrastructure.Repositories.User.Dto;
using ZBS.Shared.Exceptions;
using ZBS.Shared.Helpers;

namespace ZBS.Application.Services.UserServ
{
    public class UserService : IUserService
    {
        private readonly DataBaseContext context;
        private readonly ILogger<UserService> _logger;
        private readonly IPasswordHelper passwordHelper;
        private readonly IGenericRepository<User> genericRepository;
        private readonly IConfiguration configuration;
        private readonly EmailSend _mailSend;
        private readonly IUserRepository _userReository;

        public UserService(DataBaseContext context, ILogger<UserService> logger, IPasswordHelper passwordHelper, 
            IGenericRepository<User> genericRepository,
             JwtConfig jwtConfig, IConfiguration configuration,EmailSend mailSend,
             IUserRepository userRepository)
        {
            this.context = context;
            _logger = logger;
            this.passwordHelper = passwordHelper;
            this.genericRepository = genericRepository;
            this.configuration = configuration;
            _mailSend = mailSend;
            _userReository = userRepository;
        }

        public async Task<AuthenticateResDto> LoginUserAsync(CustomerLoginDto login)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == login.Email);

            if (user == null)
            {
                //throw new EmailTakenException(string.Format("User Not Found"));
                _logger.LogInformation($"User does not exists");
            }



            if (!passwordHelper.VerifyHash(login.Password, Convert.FromBase64String(user.Password), Convert.FromBase64String(user.Salt)))
            {
                throw new Exception("Password is incorrect.");
            }

            genericRepository.Save();

            return new AuthenticateResDto
            {
                Result = true,
                Token = CreateToken(user)
            };
        }

        public async Task<GetDetailsResDto> Register(RegisterCustomerDto registerCustomerDto)
        {
            var existingUser = await context.Users.FirstOrDefaultAsync(u => 
            u.FirstName == registerCustomerDto.FirstName || u.Email == registerCustomerDto.Email);

            if (existingUser != null)
            {
                throw new UsernameTakenException(string.Format("Username or Email is already taken.", registerCustomerDto.FirstName));
            }

            var (password, Salt) = passwordHelper.CreateHash(registerCustomerDto.Password);



            var user = new User 
            {
                FirstName = registerCustomerDto.FirstName,
                LastName = registerCustomerDto.LastName,
                Email = registerCustomerDto.Email,
                Password = registerCustomerDto.Password,
                DateOfBirth = registerCustomerDto.DateOfBirth,
                MobileNumber = registerCustomerDto.MobileNumber,
                Education = registerCustomerDto.Education,
                Address = registerCustomerDto.Address,
                Role = Infrastructure.Models.Enum.Role.Customer,
                DateCreated = DateTime.Now
            };

            user.Password = Convert.ToBase64String(password);
            user.Salt = Convert.ToBase64String(Salt);


            await genericRepository.CreateAsync(user);
            genericRepository.Save();

            return new GetDetailsResDto 
            { 
                Id = user.Id,
                FirstName = user.FirstName,
                LastName= user.LastName,
                Email = user.Email,
                Password = user.Password,
                Education = user.Education,
                Address = user.Address,
                MobileNumber = user.MobileNumber,
                DateOfBirth = user.DateOfBirth,
                Salt = user.Salt,
                Role = user.Role
            };


        }

        public async Task ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            if (changePasswordDto == null)
            {
                throw new Exception(string.Format("No data!"));
            }

            if(changePasswordDto.Password != changePasswordDto.ConfirmPassword)
            {
                throw new Exception(string.Format("Incorrect password"));
            }

            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == changePasswordDto.Id);

            if(user== null || user.DateDeleted!=null)
            {
                throw new UserNotFoundException(string.Format("User not found"));
            }

            var (password, Salt) = passwordHelper.CreateHash(changePasswordDto.Password);


            user.Password = Convert.ToBase64String(password);
            user.Salt = Convert.ToBase64String(Salt);
            user.DateUpdated=DateTime.Now;

            await genericRepository.UpdateByIdAsync(user);
            genericRepository.Save();
        }


        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                configuration.GetSection("AppSettings:Secret").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task ForgotPasswordAsync(forgotPassswordDto _forgotPassswordDto)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == _forgotPassswordDto.email);

            if (user == null || user.DateDeleted != null)
            {
                throw new UserNotFoundException(String.Format("User not found"));
            }

            var tempPassword = passwordHelper.GenerateRandomString(12);

            _mailSend.send(_forgotPassswordDto.email, "New Temporary Password", $"Temporary password is {tempPassword} .");

            var (password, Salt) = passwordHelper.CreateHash(tempPassword);

            user.Password = Convert.ToBase64String(password);
            user.Salt = Convert.ToBase64String(Salt);

            genericRepository.Save();
        }

        public async Task<UserEntity> GetUserByIdAsync(int id)
        {
            var user = await _userReository.GetUserByIdAsync(id);

            if( user == null || user.DateDeleted != null)
            {
                throw new UserNotFoundException(string.Format("User not found"));
            }

            return user;
        }

        public async Task<UserEntity> DeleteUserByIdAsync(int id)
        {
            var user = await _userReository.GetUserByIdAsync(id);

            if (user == null || user.DateDeleted != null)
            {
                throw new UserNotFoundException(string.Format("User not found"));
            }

            return await _userReository.DeleteUserById(id);
        }

        public async Task<GetUserProfileDto> UpdateUserProfile(UpdateUserProfileDto dto)
        {
            return await _userReository.UpdateUserProfile(dto);
        }
    }
}
