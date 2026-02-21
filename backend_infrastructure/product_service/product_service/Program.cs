using product_service.host;
var app = WebApplication.CreateBuilder(args)
    .ConfigureServices()
    .Build();
app.UseHexagonal();
app.Run();
