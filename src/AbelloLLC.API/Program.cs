using AbelloLLC.Business.DTOs.Common;
using AbelloLLC.Business.Exceptions;
using AbelloLLC.Business.HelperServices.Implementations;
using AbelloLLC.Business.HelperServices.Interfaces;
using AbelloLLC.Business.Mappers;
using AbelloLLC.Business.Services.Implementations;
using AbelloLLC.Business.Services.Inerfaces;
using AbelloLLC.Business.Validators;
using AbelloLLC.Core.Entities.Identity;
using AbelloLLC.DataAccess.Background;
using AbelloLLC.DataAccess.Configurations;
using AbelloLLC.DataAccess.Contexts;
using AbelloLLC.DataAccess.Repositories.Implementations;
using AbelloLLC.DataAccess.Repositories.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    

}).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,

        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecurityKey"])),

    };

});
builder.Services.AddScoped<ITokenService,TokenService>();
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<AppDbContextInitializer>();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(DriverMapper).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IVehicleTypeRepository, VehicleTypeRepository>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<IVehicleTypeService, VehicleTypeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddHostedService<DriverReserv>();
builder.Services.AddFluentValidation(o=>o.RegisterValidatorsFromAssembly(typeof(DriverValidator).Assembly));
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")); 
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("", builder =>
    {
        builder.WithOrigins("")
        .AllowAnyHeader()
        .AllowAnyMethod();


    });
});
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(6);
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(error =>
{
    error.Run(async context =>
    {
        var feature = context.Features.Get<IExceptionHandlerFeature>();
        var statusCode = HttpStatusCode.InternalServerError;
        string message = feature.Error.Message;
        if (feature.Error is IBaseException)
        {
            var exception = (IBaseException)feature.Error;
            statusCode = exception.StatusCode;
            message = exception.errorMessage;
        }
        var response = new ResponseDTO(message,statusCode);
        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsJsonAsync(response);
        await context.Response.CompleteAsync();
    });
});
using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
    await initializer.InitializerAsync();
    await initializer.UserSeedAsync();

}; app.UseHttpsRedirection();

app.UseCors("");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
