﻿using BusinessLayer.Services.Interface;
using BusinessLayer.Services.Services;
using DataAccessLayer.GenericRepository.Interface;
using DataAccessLayer.GenericRepository.Repository;
using DataAccessLayer.Repository.Interface;
using DataAccessLayer.Repository.Repository;

namespace BusinessLayer.Mapping
{
    public static class Mapping
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IFeedBackService,FeedbackService>();
            services.AddScoped<IFeedBackRepository,FeedBackRepository>();
            services.AddScoped<IAuthRepository,AuthRepository>();
            services.AddScoped<IAuthService,AuthService>();
            services.AddLogging();
        }
    }
}
