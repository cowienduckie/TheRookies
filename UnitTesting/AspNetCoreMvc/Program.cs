using AspNetCoreMvc.DataAccess;
using AspNetCoreMvc.DataAccess.Interfaces;
using AspNetCoreMvc.Services;
using AspNetCoreMvc.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IPersonService, PersonService>();
builder.Services.AddTransient<IPersonDataAccess, StaticPersonDataAccess>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.MaxValue;
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Rookies}/{action=Index}/{index?}");

app.Run();
