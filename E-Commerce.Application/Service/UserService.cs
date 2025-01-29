using E_Commerce.Application.DTOs;
using E_Commerce.Application.Interfaces;
using E_Commerce.Application.Mapper;
using E_Commerce.Data.Entities;
using E_Commerce.Data.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Service;

public class UserService : IUserService
{
    private readonly UserMapper _mapper;
    private readonly ILogger<UserService> _logger;
    private readonly IUserRepository<User> _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly AuthenticationService _authenticationService;
    private readonly TokenMapper _tokenMapper;
    public UserService(UserMapper userMapper, ILogger<UserService> logger, IUserRepository<User> userRepository, IUnitOfWork unitOfWork,
        AuthenticationService authenticationService, TokenMapper tokenMapper)
    {
        _mapper = userMapper;
        _logger = logger;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _authenticationService = authenticationService;
        _tokenMapper = tokenMapper;
    }

    public async Task<Result> AddUserService(UserDTO u)
    {
        Result result = new Result();

        result.Success = true;
        try
        {
            //Comprobar si el nuevo usuario ha indicado un correo ya existente.

            var EmailUser = await _userRepository.CheckEmailAsync(u.Email);

            if (EmailUser)
            {
                result.Success = false;
                result.Error = $"Error al intentar registrarse {u.FirstName}, el correo que ha utilizado está en uso. Por favor, prueba con otro.";
                _logger.LogError($"Error al intentar darse de alta {u.FirstName}, el correo electrónico {u.Email} ya está en uso");

                return result;
            }

            var userApp = _mapper.MapToUser(u);

            if (userApp == null)
            {
                result.Success = false;
                result.Error = $"Error al intentar crear el nuevo usuario: {u.FirstName} {u.LastName} con email: {u.Email}";
                _logger.LogError(result.Error.ToString());

                return result;
            }

            await _userRepository.AddUserAsync(userApp);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitAsync();

            result.Text = $"Se ha creado correctamente el nuevo usuario: {u.FirstName} {u.LastName} con email: {u.Email}";

        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Error = $"Error al intentar crear el nuevo usuario: {u.FirstName} {u.LastName} con email: {u.Email}";
            _logger.LogError(ex.ToString(), result.Error.ToString());

            await _unitOfWork.RollbackAsync();

            return result;
        }

        return result;
    }

    public async Task<Result> LoginUser(int id, AuthApiViewModelDTO u)
    {
        Result result = new Result();
        Token token = new Token();

        result.Success = true;

        try
        {
            var userApp = await _userRepository.GetIdUserAsync(id);

            if (userApp.Id != id)
            {
                result.Success = false;
                result.Error = "Error, el id no cohincide al usuario correcto";
                _logger.LogError(result.Error.ToString());

                return result;
            }

            userApp = await _authenticationService.AuthenticationAsync(u.email, u.password);

            if (userApp == null)
            {
                result.Success = false;
                result.Error = "Error, el id no cohincide al usuario correcto";
                _logger.LogError(result.Error.ToString());

                return result;
            }

            token = _tokenMapper.MapToToken(token, u);

            result.GenericObject = token;

        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Error = $"Error al intentar logear el email: {u.email}";
            _logger.LogError(result.Error.ToString(), ex.ToString());

            return result;
        }

        return result;
    }
}