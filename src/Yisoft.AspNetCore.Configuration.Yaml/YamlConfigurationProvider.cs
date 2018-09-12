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

using System.IO;
using Microsoft.Extensions.Configuration;

namespace Yisoft.AspNetCore.Configuration.Yaml
{
    internal class YamlConfigurationProvider : FileConfigurationProvider
    {
        public YamlConfigurationProvider(FileConfigurationSource source) : base(source) { }

        public override void Load(Stream stream)
        {
            var parser = new YamlConfigurationFileParser();

            Data = parser.Parse(stream);
        }
    }
}
