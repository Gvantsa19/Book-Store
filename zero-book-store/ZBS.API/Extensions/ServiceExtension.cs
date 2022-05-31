using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stripe;
using System.Text;
using ZBS.Application.Configuration.Models;
using ZBS.Application.Email;
using ZBS.Application.Services.BookServ;
using ZBS.Application.Services.CartService;
using ZBS.Application.Services.LoggedInUserServ;
using ZBS.Application.Services.OrderService;
using ZBS.Application.Services.PaymentServ;
using ZBS.Application.Services.RatingService;
using ZBS.Application.Services.RoleControlServ;
using ZBS.Application.Services.SalesService;
using ZBS.Application.Services.UserServ;
using ZBS.Infrastructure.DBContexts;
using ZBS.Infrastructure.GenericRepositoryP;
using ZBS.Infrastructure.Models;
using ZBS.Infrastructure.Repositories.BookCategory;
using ZBS.Infrastructure.Repositories.Books;
using ZBS.Infrastructure.Repositories.Cart;
using ZBS.Infrastructure.Repositories.EBook;
using ZBS.Infrastructure.Repositories.Orders;
using ZBS.Infrastructure.Repositories.Sales;
using ZBS.Infrastructure.Repositories.Ratings;
using ZBS.Infrastructure.Repositories.User;
using ZBS.Shared.EmailHelpers;
using ZBS.Shared.Helpers;
using ZBS.Infrastructure.Repositories.Authors;
using ZBS.Application.Services.AuthorService;
using ZBS.Application.CurrencyExchange;
using ZBS.Infrastructure.Repositories.PaymentHistory;

namespace ZBS.API.Extensions
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection Services, IConfiguration configuration)
        {
            // Add services to the container.
            Services.AddScoped<IUserService, UserService>();
            Services.AddScoped<IPasswordHelper, PasswordHelper>();
            Services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            Services.AddScoped<JwtConfig>();
            Services.AddScoped<AppSettings>();
            Services.AddControllers();
            Services.AddEndpointsApiExplorer();

            Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Zero Book Store",
                    Description = "Book's online store",
                    Version = "v1"
                });
                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = JwtBearerDefaults.AuthenticationScheme,
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });



            Services.AddScoped<IBookCategoryRepository, BookCategoryRepository>();
            Services.AddScoped<IRoleControlService, RoleControlService>();
            Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            Services.AddScoped<ILoggedInUserService, LoggedInUserService>();

            Services.AddScoped(sp =>
            {
                return new DbcontextDapper(sp.GetService<IConfiguration>().GetConnectionString("DefaultConnection"));
            });


            Services.AddDbContext<DataBaseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });


            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(configuration.GetSection("AppSettings:Secret").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            

            Services.AddAutoMapper(typeof(Program));
            Services.AddControllersWithViews();



           
            Services.AddScoped<IBookRepository, BookRepository>();
            Services.AddScoped<IBookCategoryRepository, BookCategoryRepository>();
            Services.AddScoped<IUserRepository, UserRepository>();
            Services.AddScoped<IBookService, BookService>();
            Services.AddScoped<IOrderRepository, OrderRepository>();
            Services.AddScoped<IOrderService, Application.Services.OrderService.OrderService>();
            Services.AddScoped<EmailSend>(); 
            var section = configuration.GetSection("EmailSettings");

            Services.AddSingleton(configuration.GetSection("EmailSettings").Get<EmailSettings>());

            Services.AddScoped<EBookRepository>();
            Services.AddScoped<ISalesRepository, SalesRepository>();
            Services.AddScoped<ISalesService, SalesService>();

            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];

            Services.AddScoped<IPaymentService,PaymentService>();
            Services.AddScoped<IRatingService, RatingService>();
            Services.AddScoped<IRatingRepository, RatingRepository>();

            Services.AddScoped<IBasketService, BasketService>();
            Services.AddScoped<IBasketRepository, BasketRepository>();

            Services.AddScoped<IAuthorRepository, AuthorRepository>();
            Services.AddScoped<IAuthorService, AuthorService>();

            Services.AddScoped<CurrencyExchangeRate>();
            Services.AddScoped<IPaymentHistoryRepository, PaymentHistoryRepository>();
        }
    }
}
