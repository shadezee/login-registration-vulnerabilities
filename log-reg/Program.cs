using log_reg.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "shade_net";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});


string connection_string;
string SelectDb()
{
    string local_connection = "enter local connection string";
    string release_connection = "enter hosted database connection string";

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

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
