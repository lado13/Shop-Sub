using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shop.Data;
using Shop.Interfaces;
using Shop.Model;
using Shop.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




// Registering services for dependency injection.
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtToken, JwtToken>();
builder.Services.AddScoped<IUserOrderService, UserOrderService>();


// Configure JWT authentication
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });






// Configuring the database context to use SQL Server with the connection string from the configuration.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});




// Configuring Identity with custom password requirements and adding support for Entity Framework stores.
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 1;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



// Ensure roles are created during application startup.
using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
    AppDbContext.EnsureRolesCreated(serviceScope.ServiceProvider).Wait();
}







// Configure middleware.
app.UseHttpsRedirection();

// Adds route matching to the middleware pipeline.
app.UseRouting();

// Configuring CORS to allow requests from the frontend application.
app.UseCors(Option =>
    Option.WithOrigins("http://localhost:4200")
      .AllowAnyMethod()
      .AllowAnyHeader()
      .AllowCredentials() // If your frontend application sends credentials (cookies, headers, etc.)
);


app.UseCors("AllowAllOrigins");
app.UseStaticFiles();

// Adds authentication to the middleware pipeline.
app.UseAuthorization();

// Adds authorization to the middleware pipeline.
app.UseAuthentication();

app.MapControllers();

app.Run();
