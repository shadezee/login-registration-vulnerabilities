using log_reg.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
