using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Onion.Demo.Application;
using Onion.Demo.Infra.Consul;
using Onion.Demo.Infra.Data;
using Onion.Demo.Infra.Data.Handler;
using Onion.Demo.WebApi.Configurations;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.AddEFCoreConfiguration();
builder.AddConsulConfiguration();


//// ���� Identity ϵͳ
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


// ��� JWT ��֤
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWTSettings:Issuer"], // �����ļ��е� Issuer
            ValidAudience = builder.Configuration["JWTSettings:Audience"], // �����ļ��е� Audience
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:Key"])) // �����ļ��е���Կ
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
app.UseAuthentication(); // ������� UseAuthentication
app.UseAuthorization();  // ������� UseAuthorization
app.MapControllers();

app.Run();
