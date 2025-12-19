using Blazored.LocalStorage;
using HotelManagermentSystem.Components;
using HotelManagermentSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// --- PHẦN 1: ĐĂNG KÝ SERVICES (Dòng này phải ở trên cùng) ---

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Đăng ký thư viện LocalStorage
builder.Services.AddBlazoredLocalStorage();

// Đăng ký AuthService của bạn
builder.Services.AddScoped<IAuthService, AuthService>();

// Cấu hình HttpClient với URL API của bạn
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7156/")
});

// --- PHẦN 2: BUILD APP ---
var app = builder.Build();

// --- PHẦN 3: CẤU HÌNH PIPELINE (Middleware) ---

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found");
app.UseHttpsRedirection();

// Antiforgery phải nằm trước MapRazorComponents
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();