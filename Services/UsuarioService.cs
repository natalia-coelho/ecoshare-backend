using AutoMapper;
using ecoshare_backend.Data;
using ecoshare_backend.Data.DTOs;
using ecoshare_backend.Models;
using Microsoft.AspNetCore.Identity;

namespace ecoshare_backend.Services
{
    public class UsuarioService
    {
        //private UserDbContext _userDbContext;
        private SignInManager<Usuario> _signInManager;
        private IMapper _mapper;
        private UserManager<Usuario> _userManager;
        private TokenService _tokenService;
        private PerfilService _perfilService;

        public UsuarioService(SignInManager<Usuario> signInManager, IMapper mapper, UserManager<Usuario> userManager, TokenService tokenService, PerfilService perfilService)
        {
            _signInManager = signInManager;
            _mapper = mapper;
            _userManager = userManager;
            _tokenService = tokenService;
            _perfilService = perfilService;
        }

        public async Task RegisterUser(UserRegistrationDTO userDTO)
        {
            try
            {
                Usuario user = _mapper.Map<Usuario>(userDTO);
                //create user at the database
                IdentityResult result = await _userManager.CreateAsync(user, userDTO.Password);

                if (result.Succeeded)
                {
                    switch (userDTO.Role) 
                    {
                        case 0:
                            userDTO.RoleObject = UserRole.ADMIN;
                            break;
                        case 1:
                            userDTO.RoleObject = UserRole.CLIENTE;
                            break;

                        case 2:
                            userDTO.RoleObject = UserRole.FORNECEDOR;
                            break;

                        case 3:
                            userDTO.RoleObject = UserRole.DEVELOPER;
                            break;
                    }

                    await _userManager.AddToRoleAsync(user, userDTO.RoleObject.Value.ToString());
                    await _perfilService.CriarPerfil(user); // TODO: Testar esse método
                }
                else
                    throw new Exception(result.Errors.FirstOrDefault().ToString());
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        public async Task<string> LoginAsync(UserLoginDTO userDto)
        {
            // isPersistent -> cookie vai persistir depois que o usuário logar: False
            var result = await _signInManager.PasswordSignInAsync(userDto.Username, userDto.Password, false, false);

            if (!result.Succeeded)
                throw new Exception("Login inválido");

            var user = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == userDto.Username);

            if (user == null)
                throw new Exception("falha na autenticação"!);

            var token = _tokenService.GenerateToken(user);

            return token;
        }

        public async Task<Usuario?> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(Usuario user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(Usuario user, string token, string newPassword)
        {
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }
    }
}
