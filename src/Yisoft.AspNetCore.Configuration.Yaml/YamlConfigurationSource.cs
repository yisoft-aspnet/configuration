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

using Microsoft.Extensions.Configuration;

namespace Yisoft.AspNetCore.Configuration.Yaml
{
    internal class YamlConfigurationSource : FileConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);

            return new YamlConfigurationProvider(this);
        }
    }
}
