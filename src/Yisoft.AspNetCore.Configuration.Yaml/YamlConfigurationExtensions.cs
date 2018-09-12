//      )                             *
//   ( /(        *   )       (      (  `
//   )\()) (   ` )  /( (     )\     )\))(
//  ((_)\  )\   ( )(_)))\ ((((_)(  ((_)()\
// __ ((_)((_) (_(_())((_) )\ _ )\ (_()((_)
// \ \ / / (_) |_   _|| __|(_)_\(_)|  \/  |
//  \ V /  | | _ | |  | _|  / _ \  | |\/| |
//   |_|   |_|(_)|_|  |___|/_/ \_\ |_|  |_|
//
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.
//
// Copyright © Yi.TEAM. All rights reserved.
// -------------------------------------------------------------------------------

using System;
using Microsoft.Extensions.FileProviders;
using Yisoft.AspNetCore.Configuration.Yaml;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Configuration
{
    public static class YamlConfigurationExtensions
    {
        public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, string path)
        {
            return AddYamlFile(builder, null, path, false, false);
        }

        public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, string path, bool optional)
        {
            return AddYamlFile(builder, null, path, optional, false);
        }

        public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
        {
            return AddYamlFile(builder, null, path, optional, reloadOnChange);
        }

        public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder,
            IFileProvider provider,
            string path,
            bool optional,
            bool reloadOnChange)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (string.IsNullOrEmpty(path)) throw new ArgumentException(Resources.Error_InvalidFilePath, nameof(path));

            return builder.AddYamlFile(s =>
            {
                s.FileProvider = provider;
                s.Path = path;
                s.Optional = optional;
                s.ReloadOnChange = reloadOnChange;
                s.ResolveFileProvider();
            });
        }

        internal static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, Action<YamlConfigurationSource> configureSource)
        {
            var source = new YamlConfigurationSource();

            configureSource(source);

            return builder.Add(source);
        }
    }
}
