using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;


namespace ZoomanagerAPI
{
    public class StartUp
    {
        public IConfiguration Configuration { get; }

        public StartUp  (IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Phương thức này được gọi trong quá trình runtime để thêm các dịch vụ vào container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Thêm các dịch vụ cần thiết cho ứng dụng của bạn ở đây.
            // Ví dụ:
            // services.AddMvc();
            services.AddCors(c =>
            {
                c.AddPolicy("allowOrigin",options=>options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options=>options.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options=>options.SerializerSettings.ContractResolver=new DefaultContractResolver());

            services.AddControllers();
        }

        // Phương thức này được gọi trong quá trình runtime để cấu hình request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options=>options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Đối với môi trường Production, bạn có thể thêm các xử lý lỗi khác ở đây.
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Cấu hình các middleware và các cài đặt khác ở đây.
            // Ví dụ:
            // app.UseHttpsRedirection();
            // app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
