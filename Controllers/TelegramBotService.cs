using Folivora.Scaffold;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Проект;
using static System.Net.Mime.MediaTypeNames;

public class TelegramBotService : BackgroundService
{
    private CartrigDbContext _context;
    public TelegramBotService(CartrigDbContext context, ITelegramBotClient botClient)
    {
        _context = context;
        _botClient = botClient;
    }
    private readonly ITelegramBotClient _botClient;

    //public TelegramBotService(ITelegramBotClient botClient)
    //{
    //    _botClient = botClient;
    //}
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //_botClient.StartReceiving(
        //    //HandleUpdateAsync,
        //    //HandleErrorAsync,
        //    cancellationToken: stoppingToken
        //);

        await Task.Delay(-1, stoppingToken); // Держим бота работающим в фоне
    }
    //    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    //    {
    //        if (update?.Message?.Text is null) return; // Проверяем, что сообщение содержит текст

    //        long chatId = update.Message.Chat.Id;
    //        string text = update.Message.Text;

    //        Console.WriteLine($"Получено сообщение от {chatId}: {text}");

    //        // Пробуем интерпретировать сообщение как ID теста
    //        if (int.TryParse(text, out int id))
    //        {
    //            Zaiavka? zaiavka = _context.Zaiavkas.FirstOrDefault(u => u.id_zv == id);
    //            if (zaiavka != null)
    //            {
    //                string response = $"id: {zaiavka.id_zv}\nnumber: {zaiavka.mun_off}\nparent: {zaiavka.parent_off} \nstatus: {zaiavka.status_zv}\nmodel cartridg: {zaiavka.cartridg_model}";
    //                await botClient.SendTextMessageAsync(chatId, response, cancellationToken: cancellationToken);
    //            }
    //            else
    //            {
    //                await botClient.SendTextMessageAsync(chatId, "Заявка с таким ID не найден.", cancellationToken: cancellationToken);
    //            }
    //        }
    //        else
    //        {
    //            await botClient.SendTextMessageAsync(chatId, $"Вы сказали: {text}", cancellationToken: cancellationToken);
    //        }
    //    }

    //    private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    //    {
    //        Console.WriteLine($"Ошибка: {exception.Message}");
    //        return Task.CompletedTask;
    //    }
}
