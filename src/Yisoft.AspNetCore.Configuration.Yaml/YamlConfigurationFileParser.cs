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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using SharpYaml.Serialization;

namespace Yisoft.AspNetCore.Configuration.Yaml
{
    internal class YamlConfigurationFileParser
    {
        private readonly Stack<string> _context = new Stack<string>();
        private readonly IDictionary<string, string> _data = new SortedDictionary<string, string>(StringComparer.Ordinal);
        private string _currentPath;

        public IDictionary<string, string> Parse(Stream input)
        {
            _data.Clear();
            _context.Clear();

            var yaml = new YamlStream();
            yaml.Load(new StreamReader(input));

            if (yaml.Documents.Count <= 0) return _data;

            var rootNode = yaml.Documents[0].RootNode;

            _VisitYamlNode("", rootNode);

            return _data;
        }

        private void _VisitYamlNode(string context, YamlNode node)
        {
            var yamlScalarNode = node as YamlScalarNode;

            if (yamlScalarNode == null)
            {
                var yamlMappingNode = node as YamlMappingNode;

                if (yamlMappingNode == null)
                {
                    var yamlSequenceNode = node as YamlSequenceNode;

                    if (yamlSequenceNode == null) return;

                    _VisitYamlSequenceNode(context, yamlSequenceNode);
                }
                else _VisitYamlMappingNode(context, yamlMappingNode);
            }
            else _VisitYamlScalarNode(context, yamlScalarNode);
        }

        private void _VisitYamlScalarNode(string context, YamlScalarNode node)
        {
            _EnterContext(context);

            if (_data.ContainsKey(_currentPath)) throw new FormatException(string.Format(Resources.Error_KeyIsDuplicated, _currentPath));

            _data[_currentPath] = node.Value;

            _ExitContext();
        }

        private void _VisitYamlMappingNode(string context, YamlMappingNode node)
        {
            _EnterContext(context);

            foreach (var yamlNode in node.Children)
            {
                context = ((YamlScalarNode) yamlNode.Key).Value;

                _VisitYamlNode(context, yamlNode.Value);
            }

            _ExitContext();
        }

        private void _VisitYamlSequenceNode(string context, YamlSequenceNode node)
        {
            _EnterContext(context);

            for (var i = 0; i < node.Children.Count; i++)
            {
                _VisitYamlNode(i.ToString(), node.Children[i]);
            }

            _ExitContext();
        }

        private void _EnterContext(string context)
        {
            if (!string.IsNullOrEmpty(context)) _context.Push(context);

            _currentPath = ConfigurationPath.Combine(_context.Reverse());
        }

        private void _ExitContext()
        {
            if (_context.Any()) _context.Pop();

            _currentPath = ConfigurationPath.Combine(_context.Reverse());
        }
    }
}
