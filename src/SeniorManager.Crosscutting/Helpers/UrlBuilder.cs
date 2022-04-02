using Microsoft.AspNetCore.WebUtilities;
using SeniorManager.Crosscutting.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeniorManager.Crosscutting.Helpers
{
    public class UrlBuilder : IUrlBuilder
    {
        private string url;
        private readonly Dictionary<string, string> queryString = new Dictionary<string, string>();

        public IUrlBuilder ToUrl(string url)
        {
            this.url = url;

            return this;
        }

        public IUrlBuilder AddParameter(string key, string value)
        {
            queryString.Add(key, value ?? "");

            return this;
        }

        public string Build()
        {
            return new Uri(QueryHelpers.AddQueryString(url, queryString)).AbsoluteUri;
        }
    }
}
