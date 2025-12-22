using Blazored.LocalStorage;
using HotelManagermentSystem.Components;
using HotelManagermentSystem.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// --- 1. ĐĂNG KÝ SERVICES ---
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Thêm dịch vụ Antiforgery (Bắt buộc nếu dùng Form hoặc các trang có Metadata này)
builder.Services.AddAntiforgery();


builder.Services.AddCascadingAuthenticationState();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7156/") });

builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<ApiAuthenticationStateProvider>());

var app = builder.Build();

// --- 2. CẤU HÌNH PIPELINE (THỨ TỰ QUAN TRỌNG TẠI ĐÂY) ---

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// QUAN TRỌNG: UseStatusCodePages phải nằm TRƯỚC Routing để bắt được các lỗi 404
app.UseStatusCodePagesWithReExecute("/not-found");

app.UseRouting();


app.UseAntiforgery(); // Đặt ngay sau Authorization và trước Map endpoints

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();