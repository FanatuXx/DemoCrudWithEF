using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Results
{
    public class Error
    {
        public static Error None => new Error(string.Empty, string.Empty);
        public static Error Create(string code, string description)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(code);
            ArgumentException.ThrowIfNullOrWhiteSpace(description);

            return new Error(code, description);
        }

        public string Code { get; }
        public string Description { get; }

        private Error(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public override string ToString()
        {
            return $"{Code} : {Description}";
        }
    }
}
