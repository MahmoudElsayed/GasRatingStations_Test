using GasStationRatingSystem.BLL;
using GasStationRatingSystem.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace GasStationRatingSystem.IOC
{
    public static class DependencyContainer
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            #region DAL

            services.AddScoped(typeof(IUnitOfWork<GasStationRatingSystemContext>), typeof(UnitOfWork<GasStationRatingSystemContext>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
               services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion
            var BllClasses = typeof(CityBll).Assembly.GetTypes().Where(p=>p.IsClass&&p.Name.ToLower().Contains("bll"));

            #region BLL
            BllClasses.ToList().ForEach(p=>{
                services.AddScoped(p);

            });
            //services.AddScoped<UserBll>();
            //services.AddScoped<SendBll>();
            //services.AddScoped<GasStationBll>();
            //services.AddScoped<VisitBll>();
            //services.AddScoped<QuestionsBll>();
            //services.AddScoped<CityBll>();
            //services.AddScoped<ManualDistributionBll>();
            //services.AddScoped<DashboardBll>();
            #endregion

        }
    }
}
