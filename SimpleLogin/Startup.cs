using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleLogin.Data;

namespace SimpleLogin {
public class Startup {
    public Startup(IConfiguration configuration) {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services) {
        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddSingleton<WeatherForecastService>();
        
        // Added to template, an authentication state provider
        services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
        
        
        // setting up policies
        services.AddAuthorization(options => {
            options.AddPolicy("MustBeNinja", p => p.RequireAuthenticatedUser().RequireClaim("Role","Ninja"));
            options.AddPolicy("MustBePirate", p => p.RequireAuthenticatedUser().RequireClaim("Role","Pirate"));
            options.AddPolicy("MustBeNinjaOrPirate", p => p.RequireAuthenticatedUser().RequireClaim("Role","Pirate","Ninja"));
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        if (env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
        } else {
            app.UseExceptionHandler("/Error");

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        // Added to template, tell Blazor to use authentication and authorization.
        app.UseAuthentication();
        app.UseAuthorization();
        
        
        
        app.UseEndpoints(endpoints => {
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
        });
    }
}
}