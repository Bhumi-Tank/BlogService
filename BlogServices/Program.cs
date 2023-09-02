using Microsoft.EntityFrameworkCore;
using BlogServices.Data;
using BlogServices.Repositories;
using BlogServices.Services;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                    policyName =>
                    {
                        policyName.WithOrigins("http://localhost:4200").AllowAnyHeader();
                    });
});
builder.Services.AddDbContext<BlogServicesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogServicesContext") ?? throw new InvalidOperationException("Connection string 'BlogServicesContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IArticlesRepository, ArticlesRepository>();
builder.Services.AddScoped<IArticlesService, ArticlesService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
