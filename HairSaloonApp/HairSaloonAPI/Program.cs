using HairSaloonAPI.Data;
using HairSaloonAPI.Interfaces.Services;
using HairSaloonAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddTransient<IAppointmentService, AppointmentService>();
builder.Services.AddTransient<IRegisterUserService, RegisterUserService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ILoginUserService, LoginUserService>();
builder.Services.AddTransient<IPersonService, PersonService>();

string frontendUrl = builder.Configuration.GetValue<string>("FrontendUrl");

builder.Services.AddCors(option => option.AddPolicy("MyCorsPolicy", builder =>
{
    builder.WithOrigins(frontendUrl)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyCorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
