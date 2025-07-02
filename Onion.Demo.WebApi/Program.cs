using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Onion.Demo.Application;
using Onion.Demo.Application.Services;
using Onion.Demo.Domain.Interfaces;
using Onion.Demo.Domain.Models;
using Onion.Demo.Infra.Data;
using Onion.Demo.Infra.Data.Context;
using Onion.Demo.Infra.Data.Handler;
using Onion.Demo.Infra.Data.Services;
using Onion.Demo.WebApi.Configurations;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.AddEFCoreConfiguration();


//// 配置 Identity 系统
//builder.Services.AddIdentity<User, IdentityRole>()
//    .AddEntityFrameworkStores<AppDbContext>()
//    .AddDefaultTokenProviders();

//builder.Services.AddScoped<JwtService>();
//builder.Services.AddScoped<AuthenticationService>();

#region Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule<AutofacModule>();
    containerBuilder.RegisterModule<ApplicationAutofacModule>();

});
#endregion


// 添加 JWT 认证
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWTSettings:Issuer"], // 配置文件中的 Issuer
            ValidAudience = builder.Configuration["JWTSettings:Audience"], // 配置文件中的 Audience
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:Key"])) // 配置文件中的密钥
        };
    });

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("DynamicPermission",
        p => p.Requirements.Add(new PermissionRequirement()));
});
builder.Services.AddAuthentication();
builder.Services.AddControllers();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}




app.UseHttpsRedirection();
app.UseAuthentication(); // 必须调用 UseAuthentication
app.UseAuthorization();  // 必须调用 UseAuthorization
app.MapControllers();

app.Run();
