using HotelManagementSystem.Api.Data;
using HotelManagementSystem.Api.Extensions;
using HotelManagementSystem.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);


builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
           {
       {
           new OpenApiSecurityScheme
           {
               Reference = new OpenApiReference
               {
                   Type=ReferenceType.SecurityScheme,
                   Id="Bearer"
               },
               Scheme="oauth2",
               Name="Bearer",
               In= ParameterLocation.Header,

           },
           new string[]{}
       }
           });
});
//builder.Services.AddSwaggerGen();
//DB
builder.Services.AddDbContext<AppDbContext>(options =>
      options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Service
builder.Services.AddApplication();

builder.Services.AddControllers();

// Authen
builder.Services.AddAuthenExtension(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
        policy.WithOrigins("https://localhost:7254") // Điền Port của project Blazor vào đây
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowBlazor");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    // Cấu hình giao diện người dùng (ánh xạ tới /swagger)
    app.UseSwaggerUI();
}

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    try
//    {
//        var db = services.GetRequiredService<AppDbContext>();

//        // ⚠️ Chỉ nên dùng EnsureDeleted/EnsureCreated trong môi trường phát triển/test
//        db.Database.EnsureDeleted(); // Xóa database cũ
//        db.Database.EnsureCreated(); // Tạo database mới

//        // Gọi hàm Seed để đổ dữ liệu mẫu
//        DbSeeder.Seed(db);

//        var logger = services.GetRequiredService<ILogger<Program>>();
//        logger.LogInformation("Database successfully seeded.");
//    }
//    catch (Exception ex)
//    {
//        var logger = services.GetRequiredService<ILogger<Program>>();
//        logger.LogError(ex, "An error occurred while seeding the database.");
//    }
//}
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<PermissionMiddleware>();




app.UseHttpsRedirection();
//app.UseAppRateLimiter();
app.MapControllers();

app.Run();
