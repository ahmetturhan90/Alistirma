using Alistirma;
using Alistirma.Caching;
using Alistirma.Queue;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region ExceptionMiddleware
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();


#endregion
#region Redis
IConfiguration configuration = builder.Configuration;
var redisConnection = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
builder.Services.AddSingleton<IConnectionMultiplexer>(redisConnection);
builder.Services.AddSingleton<ICacheManager, RedisCacheService>();



#endregion

#region Jwt
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = ctx => {
            //Gerekirse burada gelen token içerisindeki çeþitli bilgilere göre doðrulam yapýlabilir.
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = ctx => {
            Console.WriteLine("Exception:{0}", ctx.Exception.Message);
            return Task.CompletedTask;
        }
    };
});

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#region jwt
app.UseAuthentication();
#endregion
app.UseHttpsRedirection();

app.UseAuthorization();
#region ExceptionMiddleware2
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
#endregion

app.MapControllers();

app.Run();





