using Domain.Entities.identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Application;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Converters;
using Infrastructure;
using Application.Identity;
using persistence;
using Application.Helpers;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicy",
        builder => builder.WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});


builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddIdentity<User, Role>(options => options.User.RequireUniqueEmail = true)
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;
});

// Token duration
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
    options.TokenLifespan = TimeSpan.FromHours(1));

// Application Cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "Authentication";
    options.ExpireTimeSpan = TimeSpan.FromDays(10);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWTSecretKey"))
                        )
                    };
                });

//builder.Services.ConfigureApplicationCookie(options =>
  ///  options.Events.OnRedirectToAccessDenied =
     //   options.Events.OnRedirectToLogin = c =>
       // {
         //   c.Response.StatusCode = StatusCodes.Status401Unauthorized;
           // return Task.FromResult<object>(null);
        //});



// Add services to the container.
builder.Services.ComfigureApplicationServices();
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureInfrastructureServices(builder.Configuration);




// GDPR
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.ConsentCookie.Name = "Consent";
    options.CheckConsentNeeded = _ => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

// Antiforgery
builder.Services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");




builder.Services.AddMvc()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();
// Security Headers
app.UseSecurityHeaders();

if (builder.Environment.IsDevelopment())
{
    // Exception Handling
    app.UseDeveloperExceptionPage();
}
else
{
    // Strict Transport Security
    app.UseHsts();
}


// Static Files
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = context =>
        context.Context.Response.Headers.Add("Cache-Control", "public,max-age=31536000")
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});

// GDPR
app.UseCookiePolicy();

// Routing
app.UseRouting();

app.UseHttpsRedirection();

// Authentication
app.UseAuthentication();
app.UseAuthorization();


// Response Caching
app.UseResponseCaching();


app.MapControllers();

app.Run();