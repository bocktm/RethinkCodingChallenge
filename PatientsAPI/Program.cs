using PatientsAPI.DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PatientsContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("PatientsDBContext")); });


var corsPolicyName = "CorsAllowOrigin";

builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: corsPolicyName, policy =>
        {
            policy.WithOrigins("*");
            policy.WithMethods("*");
            policy.WithHeaders("*");
        });
    }
);

var app = builder.Build();

app.UseCors(corsPolicyName);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
