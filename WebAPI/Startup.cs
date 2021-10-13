using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Cursos;
using Dominio;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Persistencia;
using WebAPI.Middleware;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authentication;
using Aplicacion.Contratos;
using Seguridad;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using AutoMapper;
using Persistencia.DapperConexion;
using Persistencia.DapperConexion.Instructor;
using Persistencia.DapperConexion.Paginacion;

namespace WebAPI
{
    public class Startup
    {


        // TODOS NUESTROS SERVICIOS  <--------------------------


        public Startup(IConfiguration configuration)    //Constructor de la clase
        {
            
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(o => o.AddPolicy("corsApp", builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();

            }));



            services.AddDbContext<CursosOnlineContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                //Dsd aquí me conecto al valor de conexion de mi appsettings.json
            });

            //Dspd d implementar las interfaces IInstructor, IFactoryConecion agregamos el AddOptions 
            services.AddOptions();


            services.Configure<ConexionConfiguracion>(Configuration.GetSection("ConnectionStrings"));




            //Creamos un servicio para Imediator
            //Recordar q esta linea es tambien para usar Middleware---- PASAMOS ABAJO para la ejecucion dl Middleware
            services.AddMediatR(typeof(Consulta.Manejador).Assembly);

            //Recordar la liberia de Fluent y RegisterValidat... es para validar mi clase Nuevo.


            services.AddControllers(opt =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

                opt.Filters.Add(new AuthorizeFilter(policy));


            })
            .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());
            //Metodo, Funcionamiento de los ""CONTROLLERS"

            //Esta var representa la instancia d la clase Usuario q proviene dsd "Dominio"
            var builder = services.AddIdentityCore<Usuario>();


            //Le decimos q add un IdentityCore de tipo Usuario e importamos dsde dominio a "Usuario"
            var IdentityBuilder = services.AddIdentityCore<Usuario>();

            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);


            identityBuilder.AddRoles<IdentityRole>();

      
            identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<Usuario, IdentityRole>>();
            //------>>>>Con estas 2 lineas ya instanciamos el servicio de "RolManager"


        
            identityBuilder.AddEntityFrameworkStores<CursosOnlineContext>();

            identityBuilder.AddSignInManager<SignInManager<Usuario>>();
            services.TryAddSingleton<ISystemClock, SystemClock>();


            services.AddScoped<IJwtGenerador, JwtGenerador>();

     
            services.AddScoped<IUsuarioSesion, UsuarioSesion>();

    
            services.AddAutoMapper(typeof(Consulta.Manejador));


            //**Agregamos las inyecciones d dependencia para sus interfaces e implementaciones.
            services.AddTransient<IFactoryConnection, FactoryConnection>();

       
            services.AddScoped<IInstructor, InstructorRepositorio>();

            services.AddScoped<IPaginacion, PaginacionRepositorio>();



            //LAS CONFIGURACION DE SWAGGER <<<<<<-------------------------------
            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "Servicios para mantenimiento de cursos",
                    Version = "v2"

                });
            
                c.CustomSchemaIds(c => c.FullName);
                //Osea trabaje con el nombre completo d las clases q permiten mapear la data dl cliente
            });






            //Creamos el obj "key"
            //Pasamos la palabra claves dentro dl proyecto Aplicacion "Mi palabra secreta" envuelto dentro d 1 obj key

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra secreta"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {

                opt.TokenValidationParameters = new TokenValidationParameters
                {

                    //Configuramos los param.

                    //Indica q el request debe ser validado por la logica config y pasando por el entityCore
                    ValidateIssuerSigningKey = true,

                    //Pasamos la palabra claves dentro dl proyecto Aplicacion "Mi palabra secreta" envuelto dentro d 1 obj key

                    IssuerSigningKey = key,



                    ValidateAudience = false,

                 

                    ValidateIssuer = false

                };

            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Terminamos d configurar el core, la integracion con el cliente
            app.UseCors("corsApp");



        
            app.UseMiddleware<ManejadorErrorMiddleware>();


            //METODO Configure, determina como va a trabajar mi proyecto mi funcionalidad dependiendo el ambiente

            if (env.IsDevelopment()) //Si el ambiente está en desarrollo THEN usemos...
            {
               


                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseAuthentication();



            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //---->>>>>>>>>>>> CONFIGURAMOS EL SWAGGER 

            app.UseSwagger();

            //Le indicamos q genere la interfaz grafica por cada endPOINT
            app.UseSwaggerUI(c =>
            {

                c.SwaggerEndpoint("/swagger/v2/swagger.json", "Cursos Online v2");

            });

        }
    }
}


