﻿using EnsureThat;
using MyUrlShortener.DataAccess.Repositories.IRepositories;
using MyUrlShortener.Models;
using System.Security.Cryptography;
using System.Text;

namespace MyUrlShortener.Services.UrlShortener
{
    public sealed class UrlShortenerService : IUrlShortenerService
    {
        private static readonly char[] chars =
           "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        private readonly IShortenedUrlRepository _shortenedUrlsRepository;

        public UrlShortenerService(IShortenedUrlRepository shortenedUrlsRepository)
        {
            _shortenedUrlsRepository = shortenedUrlsRepository;
        }

        public string ShortenUrl(string originalUrl)
        {            
            try
            {
                EnsureArg.IsNotNullOrWhiteSpace(originalUrl, nameof(originalUrl));

                originalUrl = CheckForHttp(originalUrl);
              
                var existingShortenedUrl = _shortenedUrlsRepository.GetFirstOrDefault(x => x.OriginalUrl == originalUrl);
                if (existingShortenedUrl != null)
                {
                    return existingShortenedUrl.Code;
                }
                
                var code = GenerateShortCode();
                ShortenedUrl shortenedUrl = new(originalUrl, code);
                _shortenedUrlsRepository.Add(shortenedUrl);

                return shortenedUrl.Code;
            }
            catch (Exception ex)
            {
                throw new UrlShortenerServiceException("Could not shorten url: " + ex.Message);
            }
        }
       
        public string GetOriginalUrl(string code)
        {
            var shortenedUrlObj = _shortenedUrlsRepository.GetFirstOrDefault(s => s.Code == code);

            if(shortenedUrlObj == null)
            {
                throw new UrlShortenerServiceException("Could not find original url");
            }

            return shortenedUrlObj.OriginalUrl;
        }

        private string GenerateShortCode()
        {
            var size = 10;
            byte[] data = new byte[4 * size];
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }
            //we could also check if this code already exists in the ddbb

            return result.ToString();
        }

        private string CheckForHttp(string url)
        {
            if (!url.Contains("http"))
            {
                url = "https://" + url;
            }
            return url;
        }
    }
}