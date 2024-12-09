using InveonBootcamp.AssignmentW1.BestPracticesAPI.Data;
using InveonBootcamp.AssignmentW1.BestPracticesAPI.Services;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Redis baðlantýsýný yapýlandýr
builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
    ConnectionMultiplexer.Connect("localhost:6379"));

// Baðýmlýlýklarý ekle
builder.Services.AddScoped<IDataRepository, InMemoryDatabase>();
builder.Services.AddScoped<CachingService>();

// Swagger ve diðer varsayýlan hizmetler
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger ve hata yönetimi
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();