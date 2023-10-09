using log_reg.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = "ShadeNetCookie";
    options.ExpireTimeSpan = TimeSpan.FromDays(20);
    options.SlidingExpiration = true;
});
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "shade_net";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});



string connection_string;
string SelectDb()
{
    string local_connection = "Server=localhost;Database=CloudFive;Trusted_Connection=True;TrustServerCertificate=True;";
    string release_connection = "Server=10.0.0.5;Database=CloudFive;User Id=dotnetcloud;Password=Salamence4003;TrustServerCertificate=true";

    try
    {
        while (true)
        {
            using (SqlConnection connection = new SqlConnection(local_connection))
            {
                connection.Open();
            }
            connection_string = local_connection;
            break;
        }
    }
    catch (SqlException e)
    {
        connection_string = release_connection;
    }
    return connection_string;
}


builder.Services.AddDbContext<UsersContext>(options =>
{
    options.UseSqlServer(SelectDb());
});

var connectionString = builder.Configuration.GetConnectionString(SelectDb());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseCookiePolicy();

app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
