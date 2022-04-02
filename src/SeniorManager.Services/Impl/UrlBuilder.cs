using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;

namespace SeniorManager.Services.Impl
{
    public class UrlBuilder : IUrlBuilder
    {
        private string _url;
        private readonly Dictionary<string, string> _queryString;

        public UrlBuilder()
        {
            _queryString = new Dictionary<string, string>();
        }

        public UrlBuilder ToUrl(string url)
        {
            _url = url;

            return this;
        }

        public UrlBuilder AddParameter(string key, string value)
        {
            _queryString.Add(key, value ?? "");

            return this;
        }

        public string Build()
        {
            return new Uri(QueryHelpers.AddQueryString(_url, _queryString)).AbsoluteUri;
        }
    }
}