using Blazored.LocalStorage;
using HotelManagermentSystem.Components;
using HotelManagermentSystem.Services;
using HotelManagermentSystem.Services.EmployeeServices;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAntiforgery();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddBlazoredLocalStorage();

// ===== AUTH CORE =====
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<ApiAuthenticationStateProvider>());

// ===== HTTP =====
builder.Services.AddTransient<JwtAuthorizationHandler>();

builder.Services.AddHttpClient<IEmployeeManagementService, EmployeeManagementService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7156/");
})
.AddHttpMessageHandler<JwtAuthorizationHandler>();

builder.Services.AddHttpClient<IAuthService, AuthService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7156/");
});

var app = builder.Build();

// ===== PIPELINE =====
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStatusCodePagesWithReExecute("/not-found");
app.UseRouting();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
