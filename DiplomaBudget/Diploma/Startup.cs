﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Diploma.Startup))]
namespace Diploma
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}