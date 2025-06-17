using Microsoft.AspNetCore.Identity;
using Onion.Demo.Domain.Interfaces;
using Onion.Demo.Domain.Models;
using Onion.Demo.Infra.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Demo.Application.Services
{
    public class AuthenticationService
    {
        private readonly IUnitOfWork _uow;
        private readonly JwtService _jwtService;

        public AuthenticationService(IUnitOfWork uow, JwtService jwtService)
        {
            _uow = uow;
            _jwtService = jwtService;
        }

        // 用户登录
        public async Task<string> AuthenticateAsync(string userName, string password)
        {
            var user = await _uow.User.FindByUserNameAsync(userName);
            if (user == null || !user.ValidatePassword(password))
            {
                throw new UnauthorizedAccessException("用户名或密码错误!");
            }

            // 登录成功后生成 JWT Token
            var token = _jwtService.GenerateJwtToken(user);
            return token;
        }

        // 注册用户
        public async Task RegisterAsync(string userName, string password)
        {
            var existingUser = await _uow.User.FindByUserNameAsync(userName);
            if (existingUser != null)
            {
                throw new InvalidOperationException("当前用户名已存在!");
            }

            var user = User.Create(userName, password);
            await _uow.User.AddAsync(user);
            await _uow.SaveAsync();
        }

    }
}
