using AspNetCoreBackEnd.Core.Middleware;
using AspNetCoreBackEnd.Data;
using AspNetCoreBackEnd.Service.Mapping;
using AspNetCoreBackEnd.Web.Extenisons;
using AspNetCoreBackEnd.Web.Modules;
using AspNetCoreBackEnd.Web.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

#region Localizer
builder.Services.AddSingleton<LanguageService>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization(options =>
    options.DataAnnotationLocalizerProvider = (type, factory) =>
    {
        var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
        return factory.Create(nameof(SharedResource), assemblyName.Name);
    });

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportCultures = new List<CultureInfo>
    {
        new CultureInfo("en-US"),
        new CultureInfo("tr-TR"),
    };
    options.DefaultRequestCulture = new RequestCulture(culture: "tr-TR", uiCulture: "tr-TR");
    options.SupportedCultures = supportCultures;
    options.SupportedUICultures = supportCultures;
    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
});
#endregion


// JSON dıngısel referanslarını ınlemek ve JSON ııktısını dızenlemek iıin.
builder.Services.AddControllersWithViews().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    opt.JsonSerializerOptions.WriteIndented = false;
});
builder.Services.AddAutoMapper(typeof(MapProfile));

// Veritabanı baılantısı ve DbContext ayarları.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var migrationsAssembly = typeof(AppDbContext).Assembly.GetName().Name;
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), b => b.MigrationsAssembly(migrationsAssembly).EnableStringComparisonTranslations());
});

//builder.Services.AddDbContext<AppDbContext>(opt =>
//{
//    opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
//});


// Identity yapılandırması ve parola politikaları.
builder.Services.AddIdentityWithExt();

// Autofac ile baıımlılık enjeksiyonu.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));

// Uygulama ıerez yapılandırması.
builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.Name = "AppCookie";
    opt.LoginPath = new PathString("/Member/Login");
    opt.LogoutPath = new PathString("/Member/Logout");
    opt.AccessDeniedPath = new PathString("/Panel/");
    opt.Cookie.HttpOnly = true;
    opt.ExpireTimeSpan = TimeSpan.FromDays(60);
    opt.SlidingExpiration = true;
});

// Yetkilendirme politikaları.
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AuthPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
    });
});

// CORS politikaları.
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("AllowedHosts", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Antiforgery token yapılandırması.
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-XSRF-Token";
});

// HttpContext eriıimi.
builder.Services.AddHttpContextAccessor();

// Uygulamayı oluıturma ve middleware yapılandırmaları.
var app = builder.Build();
app.UseCors("AllowedHosts");

app.UseHsts();

// ızel durum yınetimi.
app.UseMiddleware<ErrorHandlingMiddleware>();

// HTTPS yınlendirmesi.
app.UseHttpsRedirection();
app.UseStaticFiles();

// Routing.
app.UseRouting();

// Kimlik doırulama ve yetkilendirme.
app.UseAuthentication();

app.UseStaticFiles();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseAuthorization();

// Controller rotaları.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Uygulamayı baılat.
app.Run();
