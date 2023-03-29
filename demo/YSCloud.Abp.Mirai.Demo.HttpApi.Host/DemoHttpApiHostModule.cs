using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Swashbuckle;
using YSCloud.Abp.Mirai.Common.Handlers;
using YSCloud.Abp.Mirai.Common.Services;
using YSCloud.Abp.Mirai.Http;
using YSCloud.Abp.Mirai.Http.Services;
using YSCloud.Abp.Mirai.Webhook;

namespace YSCloud.Abp.Mirai.Demo.HttpApi.Host;

[DependsOn(
    typeof(AbpMiraiHttpModule),
    typeof(AbpMiraiWebhookModule),
    typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
    typeof(AbpAutofacModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule)
    )]
public class DemoHttpApiHostModule : AbpModule
{

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        context.Services.AddTransient<IMiraiMessageHandler, DefaultMiraiMessageHandler>();
        context.Services.AddTransient<IMiraiMessageManager, MiraiHttpMessageManager>();

        Configure<AbpMiraiHttpOptions>(op =>
        {
            op.Host = configuration["AbpMiraiHttpOptions:Host"];
            op.Port = configuration["AbpMiraiHttpOptions:Port"];
            op.VerifyKey = configuration["AbpMiraiHttpOptions:VerifyKey"];
            op.PollingRate = Convert.ToInt32(configuration["AbpMiraiHttpOptions:PollingRate"]);
        });

        Configure<AbpDbContextOptions>(options =>
        {
            options.UseSqlServer();
        });

        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = true;
        });

        //if (hostingEnvironment.IsDevelopment())
        //{
        //    Configure<AbpVirtualFileSystemOptions>(options =>
        //    {
        //        options.FileSets.ReplaceEmbeddedByPhysical<DemoDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Abp.Mirai.Demo.Domain.Shared", Path.DirectorySeparatorChar)));
        //        options.FileSets.ReplaceEmbeddedByPhysical<DemoDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Abp.Mirai.Demo.Domain", Path.DirectorySeparatorChar)));
        //        options.FileSets.ReplaceEmbeddedByPhysical<DemoApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Abp.Mirai.Demo.Application.Contracts", Path.DirectorySeparatorChar)));
        //        options.FileSets.ReplaceEmbeddedByPhysical<DemoApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Abp.Mirai.Demo.Application", Path.DirectorySeparatorChar)));
        //    });
        //}

        context.Services.AddAbpSwaggerGenWithOAuth(
            configuration["AuthServer:Authority"],
            new Dictionary<string, string>
            {
                {"Demo", "Demo API"}
            },
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("ar", "ar", "العربية"));
            options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
            options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish"));
            options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
            options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi", "in"));
            options.Languages.Add(new LanguageInfo("is", "is", "Icelandic", "is"));
            options.Languages.Add(new LanguageInfo("it", "it", "Italiano", "it"));
            options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
            options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
            options.Languages.Add(new LanguageInfo("ro-RO", "ro-RO", "Română"));
            options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
            options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak"));
            options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
            options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            options.Languages.Add(new LanguageInfo("de-DE", "de-DE", "Deutsch"));
            options.Languages.Add(new LanguageInfo("es", "es", "Español"));
            options.Languages.Add(new LanguageInfo("el", "el", "Ελληνικά"));
        });

        context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["AuthServer:Authority"];
                options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                options.Audience = "Demo";
            });

        Configure<AbpDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = "Demo:";
        });

        var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("Demo");
        if (!hostingEnvironment.IsDevelopment())
        {
            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
            dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "Demo-Protection-Keys");
        }

        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(
                        configuration["App:CorsOrigins"]
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray()
                    )
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();

        app.UseMultiTenancy();

        app.UseAbpRequestLocalization();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support APP API");

            var configuration = context.GetConfiguration();
            options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            options.OAuthScopes("Demo");
        });
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
