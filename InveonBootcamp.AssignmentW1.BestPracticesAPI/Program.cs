using InveonBootcamp.AssignmentW1.BestPracticesAPI.Data;
using InveonBootcamp.AssignmentW1.BestPracticesAPI.Services;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Redis ba�lant�s�n� yap�land�r
builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
    ConnectionMultiplexer.Connect("localhost:6379"));

// Ba��ml�l�klar� ekle
builder.Services.AddScoped<IDataRepository, InMemoryDatabase>();
builder.Services.AddScoped<CachingService>();

// Swagger ve di�er varsay�lan hizmetler
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger ve hata y�netimi
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();