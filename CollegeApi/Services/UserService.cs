using AutoMapper;
using AutoMapper.Execution;
using CollegeApi.Data;
using CollegeApi.Data.Repository;
using CollegeApi.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace CollegeApi.Services
{
    public class UserService:IUserService
    {
        private readonly IMapper _mapper;

        private readonly ICollegeRepository<User> _userRepository;
        public UserService(IMapper mapper ,ICollegeRepository<User> userRepository) 
        {
            _userRepository=userRepository;
            _mapper=mapper;
        }

        public ( string PasswordHash,string Salt) CreatePasswordHashWithSalt(string password)
        {
            //create salt
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            //creaate password hash
            var hash=Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password:password,
                salt:salt,
                prf:KeyDerivationPrf.HMACSHA256,
                iterationCount:10000,
                numBytesRequested:256/8
                ));
            return (hash,Convert.ToBase64String(salt));
        }

        public async Task<bool> CreateUserAsync(UserDTO dto)
        {
            //Old way
            //if(dto == null)
            //    throw new ArgumentNullException(nameof(dto));

            //New way
            ArgumentNullException.ThrowIfNull(dto, $"the argument {nameof(dto)} is null");

            var existingUser = await _userRepository.GetAsync(u => u.Username.Equals(dto.Username));

            if (existingUser != null)
            {
                throw new Exception("The username already taken");
            }

            User user = _mapper.Map<User>(dto);
            user.IsDeleted = false;
            user.CreatedDate = DateTime.Now;
            user.ModifiedDate = DateTime.Now;

            if (!string.IsNullOrEmpty(dto.Password))
            {
                var passwordHash = CreatePasswordHashWithSalt(dto.Password);
                user.Password = passwordHash.PasswordHash;
                user.PasswordSalt = passwordHash.Salt;
            }

            await _userRepository.CreateAsync(user);

            return true;
        }
    }
}
