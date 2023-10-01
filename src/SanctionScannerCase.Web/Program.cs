using Microsoft.EntityFrameworkCore;
using SanctionScannerCase.Persistence;
using SanctionScannerCase.Persistence.Contexts;
using SanctionScannerCase.Application;
using Serilog;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using SanctionScannerCase.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
//servis registrationlar� �a��rd�m
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

//a�a��daki scope'yi kullanarak projeyi aya�a kald�rd���n�zda otomatik olarak migrationlar�n �al��t�r�lmas�n� sa�lad�m.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SanctionScannerDbContext>();
    dbContext.Database.Migrate();
}


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();