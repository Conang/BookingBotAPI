using BookingBotAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddDbContext<BookingBotDbContext>(options =>
                    options.UseNpgsql(context.Configuration.GetConnectionString("DefaultConnection")));

                string telegramBotToken = context.Configuration["TelegramBotToken"];
                services.AddSingleton(new TelegramBotClient(telegramBotToken));

                services.AddHostedService<TelegramBotService>();
            })
            .Build();

        await builder.RunAsync();
    }
}

public class TelegramBotService : BackgroundService
{
    private readonly TelegramBotClient _botClient;
    private readonly IServiceProvider _services;
    private readonly ILogger<TelegramBotService> _logger;

    public TelegramBotService(TelegramBotClient botClient, IServiceProvider services, ILogger<TelegramBotService> logger)
    {
        _botClient = botClient;
        _services = services;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("��� ������� � ������� ����������...");

        _botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            new ReceiverOptions { AllowedUpdates = Array.Empty<UpdateType>() },
            stoppingToken
        );

        await Task.Delay(-1, stoppingToken);
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not { Text: { } text }) return;

        var chatId = update.Message.Chat.Id;
        _logger.LogInformation($"��������� �� {chatId}: {text}");

        using var scope = _services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BookingBotDbContext>();

        var master = await dbContext.Masters.FirstOrDefaultAsync(m => m.TelegramId == chatId.ToString(), cancellationToken);

        if (text.ToLower().Contains("�����������"))
        {
            if (master != null)
            {
                await botClient.SendTextMessageAsync(chatId, "�� ��� ���������������� ��� ������.", cancellationToken: cancellationToken);
                return;
            }

            await botClient.SendTextMessageAsync(chatId, "������� ���� ��� ��� �����������:", cancellationToken: cancellationToken);
            dbContext.Masters.Add(new Master { Name = "", TelegramId = chatId.ToString() });
            await dbContext.SaveChangesAsync(cancellationToken);
            return;
        }

        if (master != null && string.IsNullOrEmpty(master.Name))
        {
            master.Name = text;
            dbContext.Masters.Update(master);
            await dbContext.SaveChangesAsync(cancellationToken);
            await botClient.SendTextMessageAsync(chatId, "����������� ���������. ����� ����������!", cancellationToken: cancellationToken);
            return;
        }

        string response = master == null ? "�� ��� �� ����������������. �������� '�����������' ��� ������." : "������!";
        await botClient.SendTextMessageAsync(chatId, response, cancellationToken: cancellationToken);
    }

    private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError($"������ ����: {exception.Message}");
        return Task.CompletedTask;
    }
}
