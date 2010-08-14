﻿using System;
using System.Collections.Generic;
using System.Text;

using NAppUpdate.Framework.Utils;

namespace NAppUpdate.Framework.Sources
{
    public class SimpleWebSource : IUpdateSource
    {
        public SimpleWebSource() { }
        public SimpleWebSource(string feedUrl)
        {
            this.FeedUrl = feedUrl;
        }

        public string FeedUrl { get; set; }

        #region IUpdateSource Members

        public string GetUpdatesFeed()
        {
            FileDownloader fd = new FileDownloader(FeedUrl);
            byte[] data = fd.Download();

            if (data == null || data.Length == 0)
                return string.Empty;

            int charsCount = Encoding.UTF8.GetCharCount(data);

            char[] chars = new char[charsCount];
            int bytesUsed, charsUsed;
            bool completed;
            Encoding.UTF8.GetDecoder().Convert(data, 0, data.Length, chars, 0, charsCount, true,
                out bytesUsed, out charsUsed, out completed);

            return new string(chars);
        }

        public byte[] GetFile(string url)
        {
            return new FileDownloader(url).Download();
        }

        #endregion
    }
}
