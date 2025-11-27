using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Peo.Web.Spa;
using Peo.Web.Spa.Services;
using Peo.Web.Spa.Services.Identity.Home;
using Peo.Web.Spa.Services.Identity.Login;
using Peo.Web.Spa.Services.Identity.Login.Interface;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// MudBlazor + Snackbar config
builder.Services.AddMudServices(cfg =>
{
    cfg.SnackbarConfiguration.PositionClass = MudBlazor.Defaults.Classes.Position.TopRight;
    cfg.SnackbarConfiguration.PreventDuplicates = true;
    cfg.SnackbarConfiguration.NewestOnTop = true;
    cfg.SnackbarConfiguration.ShowCloseIcon = true;
    cfg.SnackbarConfiguration.VisibleStateDuration = 4000;
    cfg.SnackbarConfiguration.HideTransitionDuration = 150;
    cfg.SnackbarConfiguration.ShowTransitionDuration = 150;
});

// ThemeService para lembrar dark/light
builder.Services.AddScoped<ThemeService>();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();

builder.Services.AddScoped<AuthHeaderHandler>();

var configuredApiBase = builder.Configuration["ApiBaseUrl"];
string apiBase;

// URL de onde a SPA está rodando (ex: http://localhost:5100/)
var spaBaseUri = new Uri(builder.HostEnvironment.BaseAddress);

if (!string.IsNullOrWhiteSpace(configuredApiBase))
{
    apiBase = configuredApiBase;

    // Se a SPA estiver rodando em http://localhost:5100
    // e o appsettings ainda estiver apontando pro 7276,
    // sobrescreve para usar o BFF na 5000
    if (spaBaseUri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase)
        && spaBaseUri.Port == 5100
        && configuredApiBase.Contains("localhost:7276", StringComparison.OrdinalIgnoreCase))
    {
        apiBase = "http://localhost:5000";
    }
}
else
{
    // fallback: se um dia tirarem do appsettings, mantém compatibilidade
    if (spaBaseUri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase)
        && spaBaseUri.Port == 5100)
    {
        apiBase = "http://localhost:5000";
    }
    else
    {
        apiBase = "https://localhost:7276/v1/";
    }
}

builder.Services.AddHttpClient("Api")
                .AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddScoped(sp =>
{
    var http = sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api");
    return new WebApiClient(apiBase, http); // passa baseUrl e httpClient
});

builder.Services.AddScoped<IAuthService, WebApiClientAuthAdapter>();
builder.Services.AddScoped<ITokenStore, TokenStoreLocalStorage>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
