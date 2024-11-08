using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Ticketron.Data;
using Ticketron.Interfaces;
using Ticketron.Repository;
using Ticketron.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"))
        .EnableTokenAcquisitionToCallDownstreamApi()
            .AddMicrosoftGraph(builder.Configuration.GetSection("MicrosoftGraph"))
            .AddInMemoryTokenCaches();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IUnregUserRepository, UnregUserRepository>();
builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IGroupMemberRepository, GroupMemberRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder =>
        {
            builder.WithOrigins("https://white-cliff-06c7dbb03.5.azurestaticapps.net", "http://localhost:5173/", "https://localhost:7197")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var connection = String.Empty;
var storageConnectionString = String.Empty;

//if (builder.Environment.IsDevelopment())
//{
//    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
//}
//else
//{
//    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.json");
//}

builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.json");

connection = builder.Configuration.GetConnectionString("AZURE_SQL_Connection");
storageConnectionString = builder.Configuration.GetConnectionString("AZURE_STORAGE_Connection");

builder.Services.AddSingleton(new BlobServiceClient(storageConnectionString));

builder.Services.AddScoped<IBlobService, BlobService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContextService, UserContextService>();

builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlServer(connection));

var app = builder.Build();
app.UseSwagger();
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}
if (!app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();