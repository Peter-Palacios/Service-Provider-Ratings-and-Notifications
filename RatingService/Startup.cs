using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service_Provider_Ratings_and_Notifications.Services;
using System;

public class Startup
{
	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}
	public IConfiguration Configuration { get; }
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddSingleton<IRatingService, RatingServiceImp>();
		services.AddControllers();
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();
		services.AddLogging();
	}
	
	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();

			app.UseSwagger();

			app.UseSwaggerUI();

		}

		//app.UseHttpsRedirection();
		app.UseRouting();
		app.UseAuthorization();
		app.UseEndpoints(endpoints =>

		{
			endpoints.MapControllers();
		});
	}
}
