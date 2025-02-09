using BookingBotAPI.Data;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using System.Threading.Tasks;
using System.Threading;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BookingBotDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add other services like controllers, if applicable
builder.Services.AddControllers();  // This is necessary for a Web API

// Register TelegramBotService with token from configuration (or directly)
string telegramBotToken = builder.Configuration["TelegramBotToken"]; // Use configuration or hardcode token
builder.Services.AddSingleton<TelegramBotService>(new TelegramBotService(telegramBotToken));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();  // Maps your controller routes if you have any

// Ensure the Telegram bot service starts
var telegramBotService = app.Services.GetRequiredService<TelegramBotService>();

app.Run();

// TelegramBotService class
public class TelegramBotService
{
    private readonly TelegramBotClient _botClient;

    public TelegramBotService(string token)
    {
        _botClient = new TelegramBotClient(token);

        // Обработчик обновлений
        _botClient.StartReceiving(
            new UpdateType[] { UpdateType.Message }, // Можно фильтровать типы обновлений
            HandleUpdatesAsync, // Метод для обработки обновлений
            HandleErrorAsync // Метод для обработки ошибок
        );
    }

    // Асинхронная обработка обновлений
    private async Task HandleUpdatesAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message != null && update.Message.Type == MessageType.Text)
        {
            var chatId = update.Message.Chat.Id;
            var text = update.Message.Text;

            // Логика для обработки сообщений
            if (text.ToLower().Contains("записаться"))
            {
                await botClient.SendTextMessageAsync(chatId, "Выберите услугу для записи.");
            }
            else
            {
                await botClient.SendTextMessageAsync(chatId, "Привет! Напишите 'записаться' для начала.");
            }
        }
    }

    // Обработка ошибок
    private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Ошибка: {exception.Message}");
        return Task.CompletedTask;
    }

    // Метод для отправки сообщений
    public async Task SendMessageAsync(long chatId, string message)
    {
        await _botClient.SendTextMessageAsync(chatId, message);
    }
}