﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace BootstrapBlazor.Localization.Json
{
    /// <summary>
    /// LocalizationOptions 配置类
    /// </summary>
    public class JsonLocalizationOptions : LocalizationOptions
    {
        /// <summary>
        /// 获得/设置 自定义 IStringLocalizer 接口 默认为空
        /// </summary>
        public IStringLocalizer? StringLocalizer { get; set; }

        /// <summary>
        /// 获得/设置 自定义 Json 格式资源流集合
        /// </summary>
        public Func<string, IConfiguration>? LocalizerConfigurationFactory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Assembly>? AdditionalAssemblies { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        public JsonLocalizationOptions()
        {
            ResourcesPath = "Locales";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourcesPath">resx 资源文件路径</param>
        /// <typeparam name="TType"></typeparam>
        /// <returns></returns>
        public IStringLocalizer CreateStringLocalizer<TType>(string? resourcesPath = null)
        {
            var options = Options.Create(new LocalizationOptions() { ResourcesPath = resourcesPath ?? ResourcesPath });
            var loggerFactory = ServiceProviderHelper.ServiceProvider.GetRequiredService<ILoggerFactory>();
            return new ResourceManagerStringLocalizerFactory(options, loggerFactory).Create(typeof(TType));
        }
    }
}
