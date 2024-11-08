
using Microsoft.Graph;
using Ticketron.Interfaces;

public class UserSyncService : BackgroundService
{
    private readonly GraphServiceClient _graphClient;
    private readonly IUserRepository _userRepository;

    public UserSyncService(GraphServiceClient graphClient, IUserRepository userRepository)
    {
        _graphClient = graphClient;
        _userRepository = userRepository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await SyncEntraUsersToDatabase();
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken); // Adjust as needed
        }
    }

    private async Task SyncEntraUsersToDatabase()
    {
        try
        {
            var users = await _graphClient.Users.Request().Select("id,displayName,mail").GetAsync();

            foreach (var graphUser in users)
            {
                var azureObjectId = Guid.Parse(graphUser.Id);
                var email = graphUser.Mail;
                var name = graphUser.DisplayName;

                if (string.IsNullOrEmpty(email)) continue;

                var existingUser = _userRepository.GetUsers().FirstOrDefault(u => u.AzureObjectId == azureObjectId || u.Email == email);

                if (existingUser == null)
                {
                    var newUser = new Ticketron.Models.User
                    {
                        Name = name,
                        Email = email,
                        AzureObjectId = azureObjectId
                    };

                    _userRepository.CreateUser(newUser);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
           
        }
    }
}
