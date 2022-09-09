using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Linq;
using Nethereum.JsonRpc.Client;
using Newtonsoft.Json;

namespace SuiDotNet.Client
{
    public static class SuiEx
    {
        public static (string package, string module, string type, string? generic) GetType(string suiType)
        {
            var regexStr = @"^(?<package>\S+?):{2}(?<module>\S+?):{2}(?<type>\S[^<]*)(<{1}(?<generics>[\S\s]*)>)?";
            var regex = new Regex(regexStr);
            var match = regex.Match(suiType);

            return (
                package: match.Groups["package"].Value,
                module: match.Groups["module"].Value,
                type: match.Groups["type"].Value,
                generic: match.Groups["generic"]?.Value
            );
        }

        public static object ObjectFromDictionary(IDictionary<string, object> dict, Type objectType)
        {
            var jsonSerializerSettings = DefaultJsonSerializerSettingsFactory.BuildDefaultJsonSerializerSettings();
            var json = JsonConvert.SerializeObject(dict, jsonSerializerSettings);
            var obj = JsonConvert.DeserializeObject(json, objectType, jsonSerializerSettings);
            return obj;
        }

        public static bool IsOfSuiType(Type suiType, string type, Dictionary<string, string>? packageIdOverrides)
        {
            var moveStruct = suiType.GetCustomAttribute<MoveType>();
            if (moveStruct == null)
                throw new Exception("Supplied type not annotated with MoveStruct");

            if (!string.IsNullOrEmpty(moveStruct.PackageId) && packageIdOverrides?.ContainsKey(moveStruct.PackageId) == true)
                moveStruct.PackageId = packageIdOverrides[moveStruct.PackageId];

            var regexStr = @"^(?<package>\S+?):{2}(?<module>\S+?):{2}(?<type>\S[^<]*)(<{1}(?<generics>[\S\s]*)>)?";
            var regex = new Regex(regexStr);
            var match = regex.Match(type);

            // Match Package
            if (!string.IsNullOrEmpty(moveStruct.PackageId) && !string.Equals(moveStruct.PackageId, match.Groups["package"].Value, StringComparison.InvariantCultureIgnoreCase))
                return false;

            if (!string.IsNullOrEmpty(moveStruct.Module) && !string.Equals(moveStruct.Module, match.Groups["module"].Value, StringComparison.InvariantCultureIgnoreCase))
                return false;

            if (!string.IsNullOrEmpty(moveStruct.Struct) && !string.Equals(moveStruct.Struct, match.Groups["type"].Value, StringComparison.InvariantCultureIgnoreCase))
                return false;

            if (suiType.GenericTypeArguments.Any())
            {
                foreach (var subType in suiType.GenericTypeArguments)
                {
                    if (!IsOfSuiType(subType, match.Groups["generics"].Value, packageIdOverrides))
                        return false;
                }
            }

            return true;
        }
    }
}

