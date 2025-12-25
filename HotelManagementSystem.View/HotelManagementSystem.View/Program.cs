using HotelManagementSystem.View.Components;
using HotelManagermentSystem.View.Services;
using HotelManagermentSystem.View.Services.EmployeeServices;
using Microsoft.AspNetCore.Components.Authorization;

var apiBaseUrl = "https://localhost:7156";
var builder = WebApplication.CreateBuilder(args);

// 1. Thêm dịch vụ Razor Components và HỖ TRỢ SERVER MODE
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents() // Sửa lỗi InvalidOperationException của bạn
    .AddInteractiveWebAssemblyComponents();

// Thay vì để trống builder.Services.AddAuthentication()
builder.Services.AddAuthentication(options =>
{
    // Thiết lập tên Scheme mặc định
    options.DefaultAuthenticateScheme = "BlazorAuth";
    options.DefaultChallengeScheme = "BlazorAuth";
})
.AddCookie("BlazorAuth", options =>
{
    // Khi truy cập trang bị chặn, hệ thống sẽ tự động chuyển hướng về đây
    options.LoginPath = "/login";
});

// 2. Cấu hình HttpClient để gọi API .NET 8 của bạn
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiBaseUrl) // Thay bằng URL API thực tế của bạn
});

// 3. Đăng ký các Service xử lý nghiệp vụ
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddTransient<JwtAuthorizationHandler>();
builder.Services.AddScoped<ITokenService, TokenService>(); // Giả định bạn đã có class này

builder.Services.AddHttpClient<IEmployeeManagementService, EmployeeManagementService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
})
.AddHttpMessageHandler<JwtAuthorizationHandler>();

// 4. Cấu hình Hệ thống Xác thực (Authentication)
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();

var app = builder.Build();

// Cấu hình Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();



// 5. Đăng ký các Render Mode
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode() // Kích hoạt Server Mode cho trang Login
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(HotelManagementSystem.View.Client._Imports).Assembly);

app.Run();