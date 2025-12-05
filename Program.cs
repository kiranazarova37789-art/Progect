using Folivora.Scaffold;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

// ?? Добавляем поддержку контроллеров и базы данных
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddDbContext<CartrigDbContext>(ServiceLifetime.Transient);

// ?? Загружаем настройки бота
var botConfig = builder.Configuration.GetSection("BotConfig");
string botToken = botConfig["Token"];


if (string.IsNullOrEmpty(botToken))
{
    throw new Exception("Ошибка: Telegram Bot Token не найден в appsettings.json");
}

// ?? Регистрируем Telegram Bot Client
builder.Services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(botToken));
builder.Services.AddHostedService<TelegramBotService>(); // Запускаем бота в фоне

var app = builder.Build();

// ?? Настройки HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseForwardedHeaders();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// ?? Маршруты
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
