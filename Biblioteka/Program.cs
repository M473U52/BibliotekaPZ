using Biblioteka.Context;
using Biblioteka.Repositories.DbImplementations;
using Biblioteka.Repositories.Interfaces;
using Biblioteka.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Biblioteka.Areas.Identity.Data;
using Biblioteka.Services;
using NuGet.Protocol;
using Serilog;
using Biblioteka.Models;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

//using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IBorrowingRepository, BorrowingRepository>();
builder.Services.AddScoped<IAuthorRepository, DbAuthorRepository>();
builder.Services.AddScoped<IBookRepository, DbBookRepository>();
builder.Services.AddScoped<IBookTypeRepository, DbBookTypeRepository>();
builder.Services.AddScoped<IGenreRepository, DbGenreRepository>();
builder.Services.AddScoped<IPublisherRepository, DbPublisherRepository>();
builder.Services.AddScoped<IRoomRepository, DbRoomRepository>();
builder.Services.AddScoped<IRoomReservationRepository, DbRoomReservationRepository>();
builder.Services.AddScoped<ITagRepository, DbTagRepository>();
builder.Services.AddScoped<IEmployeeDataRepository, EmployeeDataRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<IReaderRepository, ReaderRepository>();
builder.Services.AddScoped<IReader_BorrowingsRepository, Reader_BorrowingsRepository>();
builder.Services.AddScoped<IBookOpinionRepository, BookOpinionsRepository>();
builder.Services.AddScoped<IEventRepository,  EventRepository>();
builder.Services.AddScoped<IRoomTypeRepository, DbRoomTypeRepository>();
builder.Services.AddScoped<IFAQRepository, DbFAQRepository>();
builder.Services.AddDbContext<BibContext>(options =>
{
    //options.UseOracle(builder.Configuration.GetConnectionString("Database"), b=>b.UseOracleSQLCompatibility("11"));
	options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDataBase"));
});

// Configuring ASP Identity
builder.Services.AddDefaultIdentity<BibUser>(options =>
{
	options.SignIn.RequireConfirmedAccount = false;
	options.Password.RequiredLength = 8;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<BibContext>()
.AddDefaultTokenProviders()
.AddErrorDescriber<BibErrorDescriber>();

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromMinutes(5);
});

// Add services to the container.
//builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs.txt").Filter.ByIncludingOnly(e =>
                e.Properties.ContainsKey("SaveToFile")));


builder.Services.AddControllersWithViews().AddMvcOptions(options =>
{
    // Customize the error message for numeric binding failures
    options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(_ => "nieprawidlowa wartosc");
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
app.MapDefaultControllerRoute();

app.UseRouting();

app.UseAuthorization();

/*app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");*/
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

	var roles = new[] { "Admin", "Employee", "Reader", "Author","Guest" };

	foreach (var role in roles)
	{
		if (!await roleManager.RoleExistsAsync(role))
			await roleManager.CreateAsync(new IdentityRole(role));
	}
}

using (var scope = app.Services.CreateScope())
{
	var userManager = scope.ServiceProvider.GetRequiredService<UserManager<BibUser>>();

	string email = "admin@admin.com";
	string password = "admin123";
	if (await userManager.FindByEmailAsync(email) == null)
	{
		var user = new BibUser();

		user.name = "Admin";
		user.surname = "Admin";
		user.UserName = email;
		user.Email = email;
		user.EmailConfirmed = true;

		await userManager.CreateAsync(user, password);

		await userManager.AddToRoleAsync(user, "Admin");
	}

    email = "employee@employee.com";
	password = "employee123";
	if (await userManager.FindByEmailAsync(email) == null)
	{
		var user = new BibUser();

		user.name = "Employee";
		user.surname = "Employee";
		user.UserName = email;
		user.Email = email;
		user.EmailConfirmed = true;

		await userManager.CreateAsync(user, password);

		await userManager.AddToRoleAsync(user, "Employee");
	}


    // czytelnik
    email = "jan.kowalski@gmail.com";
    password = "zaq12wsx";
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new BibUser();

        user.name = "jan";
        user.surname = "kowalski";
        user.UserName = email;
        user.Email = email;
        user.EmailConfirmed = true;

        await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, "Reader");
    }


    // employee
    email = "janusz.kowalski@gmail.com";
    password = "zaq12wsx";
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new BibUser();

        user.name = "Janusz";
        user.surname = "Kowalski";
        user.UserName = email;
        user.Email = email;
        user.EmailConfirmed = true;

        await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, "Employee");
    }

    // autor
    email = "autor123@autor.com";
    password = "zaq12wsx";
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new BibUser();

        user.name = "autor";
        user.surname = "autorski";
        user.UserName = email;
        user.Email = email;
        user.EmailConfirmed = true;

        await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, "Author");
    }


    // gosc
    email = "gosc@gosc.com";
    password = "zaq12wsx";
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new BibUser();

        user.name = "gosc";
        user.surname = "gosciowski";
        user.UserName = email;
        user.Email = email;
        user.EmailConfirmed = true;

        await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, "Guest");
    }
}

app.Run();
