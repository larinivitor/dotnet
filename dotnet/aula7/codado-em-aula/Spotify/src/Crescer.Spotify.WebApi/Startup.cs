using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Rewrite;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Infra.Repository;
using Crescer.Spotify.Dominio.Servicos;
using Crescer.Spotify.Infra;
using Crescer.Spotify.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Crescer.Spotify.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey
                            (Encoding.UTF8.GetBytes(Configuration["SecuritySettings:SigningKey"]))
                    };
                });

            services.Configure<SecuritySettings>(options => Configuration.GetSection("SecuritySettings").Bind(options));

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Spotify API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });
            });

            var connectionString = Configuration.GetConnectionString("BaseSpotify");

            services.AddDbContext<SpotifyContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<MusicaService, MusicaService>();
            services.AddScoped<AlbumService, AlbumService>();
            services.AddScoped<UsuarioService, UsuarioService>();
            services.AddScoped<AvaliacaoService, AvaliacaoService>();

            services.AddScoped<IMusicaRepository, MusicaRepository>();
            services.AddScoped<IAlbumRepository, AlbumRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader());

            // Diz que a aplicação utiliza os atributos de [Authorize]
            app.UseAuthentication();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Spotify API");
            });

            app.UseMvc();

            app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));
        }
    }
}
