using etraducao.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using etraducao.Models.Interfaces;
using etraducao.Models.Services;
using etraducao.Data.Repositorio;

namespace etraducao
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
            services.AddDbContext<EtraducaoContexto>();
            services.AddDbContext<IdentityContexto>
            (
                options => options.UseSqlServer("Data Source=localhost;Initial Catalog=etraducao2.azurewebsites.net;Integrated Security=True;Connection Timeout=30;")
            );

            services.AddScoped<ILeitorDeImagemService, LeitorDeImagens>();
            services.AddScoped<ILeitorDePdfService, LeitorDePdf>();
            services.AddScoped<IServicoDeEmail, EnvioDeEmail>();

            services.AddScoped<ICobrancaRepositorio, CobrancaRepositorio>();
            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            services.AddScoped<IDocumentoRepositorio, DocumentoRepositorio>();
            services.AddScoped<ISolicitacaoRepositorio, SolicitacaoRepositorio>();
            services.AddScoped<IControleDeValorRepositorio, ControleDeValorRepositorio>();

            services.AddDefaultIdentity<IdentityUser>(options => 
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = true;
            })
            .AddEntityFrameworkStores<IdentityContexto>();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Solicitacao}/{action=Realizar}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
