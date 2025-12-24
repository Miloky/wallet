using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Wallet.Api;
using Wallet.Api.Controllers;

var builder = WebApplication.CreateBuilder(args);

// TODO: Migrate to dotnet 10
// TODO: Add logging templates, display correlation id when logging in console.
// TODO: Collect unit tests covarage 
// TODO: T2/Module tests


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddSimpleConsole();
    loggingBuilder.AddSeq(builder.Configuration.GetSection("Seq"));
});

builder.Services.Configure<WalletDatabaseSettings>(
    builder.Configuration.GetSection("WalletDatabase"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var walletDatabaseSettings = sp.GetRequiredService<IOptions<WalletDatabaseSettings>>();
    return new MongoClient(walletDatabaseSettings.Value.ConnectionString);
});

builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var mongoClient = sp.GetRequiredService<IMongoClient>();
    var walletDatabaseSettings = sp.GetRequiredService<WalletDatabaseSettings>();
    return mongoClient.GetDatabase(walletDatabaseSettings.DatabaseName);
});

builder.Services.AddSingleton<TransactionService>();
builder.Services.AddSingleton<AccountService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<CorrelationIdMiddleware>();

app.MapControllers();

app.Run();