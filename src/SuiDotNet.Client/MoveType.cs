using System;

namespace SuiDotNet.Client
{
    public class MoveType : Attribute
    {
        public string? PackageId { get; set; }
        public string? Module { get; set; }
        public string? Struct { get; set; }
    }
}