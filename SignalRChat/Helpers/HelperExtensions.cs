using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SignalRChat.Helpers
{
    public static class HelperExtensions
    {
        private const string WWWROOT = "/wwwroot";
        private static IEnumerable<string> CSS_FILES = null;
        private static IEnumerable<string> JS_FILES = null;
        private static bool staticFilesLoaded;

        public static IHtmlContent PreloadResources(this IHtmlHelper htmlHelper)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException(nameof(htmlHelper));
            }

            LoadResources();

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var js in JS_FILES)
            {
                stringBuilder.AppendLine($"<link href=\"{js}\" rel=\"preload\" as=\"script\">");
            }
            foreach (var css in CSS_FILES)
            {
                stringBuilder.AppendLine($"<link href=\"{css}\" rel=\"preload\" as=\"style\">");
            }

            return new HtmlString(stringBuilder.ToString());
        }

        public static IHtmlContent RenderVueJsFiles(this IHtmlHelper htmlHelper)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException();
            }

            LoadResources();
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var js in JS_FILES)
            {
                stringBuilder.AppendLine($"<script src=\"{js}\"></script>");
            }

            return new HtmlString(stringBuilder.ToString());
        }

        public static IHtmlContent RenderVueCssFiles(this IHtmlHelper htmlHelper)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException();
            }

            LoadResources();
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var css in CSS_FILES)
            {
                stringBuilder.AppendLine($"<link href=\"{css}\" rel=\"stylesheet\">");
            }

            return new HtmlString(stringBuilder.ToString());
        }

        private static void LoadResources()
        {
            if (!staticFilesLoaded)
            {
                JS_FILES = Directory.GetFiles(WWWROOT, "*.js", SearchOption.AllDirectories)
                                    .Select(x => x.Substring(WWWROOT.Length)
                                    .Replace("\\", "/", StringComparison.InvariantCulture));

                CSS_FILES = Directory.GetFiles(WWWROOT, "*.css", SearchOption.AllDirectories)
                                     .Select(x => x.Substring(WWWROOT.Length)
                                     .Replace("\\", "/", StringComparison.InvariantCulture));
            }
        }
    }
}
