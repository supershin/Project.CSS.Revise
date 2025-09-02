using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.CSS.Revise.Web.Commond;
using Project.CSS.Revise.Web.Models;
using Project.CSS.Revise.Web.Service;
using QRCoder;
using SkiaSharp;
using System.IO.Compression;
using System.Linq;                // (optional) for LINQ in font helper
using System.Net.Http;            // ✅ for HttpClient
using System.Text.Json;

namespace Project.CSS.Revise.Web.Controllers
{
    public class QrDemoController : BaseController
    {
        public QrDemoController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

        private string GetRegisterCounterQRCodeUrl(int counterNo, int QueueTypeID, string ProjectID, string ProjectName)
        {
            string viewName = (QueueTypeID == 48) ? "RegisterBankCounter" : "RegisterInspectCounter";
            string paramEncrypt = string.Format("{0}:{1}:{2}", ProjectID, QueueTypeID, counterNo).EnCryptText();
            string url = string.Format("{0}Register/{1}?param={2}","https://aswinno.assetwise.co.th/qcinspection/", viewName, paramEncrypt);
            return url;
        }

        // GET /QrDemo/zip?projectId=...&projectName=...&queueType=bank&qty=20
        [HttpGet("/QrDemo/zip")]
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> ZipCounters(
            [FromQuery] string projectId,
            [FromQuery] string projectName,
            [FromQuery] string queueType,
            [FromServices] IWebHostEnvironment env,   // ✅ required before optional
            [FromQuery] int qty = 1                   // optional last
        )
        {
            if (string.IsNullOrWhiteSpace(projectId)) return BadRequest("projectId required");
            if (string.IsNullOrWhiteSpace(projectName)) return BadRequest("projectName required");
            if (qty < 1) return BadRequest("qty must be >= 1");

            int queueTypeId = queueType?.Equals("bank", StringComparison.OrdinalIgnoreCase) == true ? 48 : 49;
            string typeLabel = queueTypeId == 48 ? "Bank" : "Inspect";
            string typeEmoji = queueTypeId == 48 ? "🏦" : "📋";

            string iconUrl = BaseUrl + "assets/images/logo/ASW_Logo_Rac_dark-bg.png";

            var textTypeface = GetTextTypeface(env);  // ← Prompt-first, Thai-safe
            var emojiTypeface = TryGetEmojiTypeface();

            string rootFolder = $"{Sanitize(projectId)}";
            string typeFolder = queueTypeId == 48 ? "Bank" : "Inspect";
            string imagesFolder = projectName;

            using var centerIcon = await TryLoadImageFromUrl(iconUrl);  // ✅ dispose when done

            using var msZip = new MemoryStream();
            using (var archive = new ZipArchive(msZip, ZipArchiveMode.Create, leaveOpen: true))
            {
                for (int i = 1; i <= qty; i++)
                {
                    string registerUrl = GetRegisterCounterQRCodeUrl(i, queueTypeId, projectId, projectName);

                    using var gen = new QRCodeGenerator();
                    using var data = gen.CreateQrCode(registerUrl, QRCodeGenerator.ECCLevel.Q);
                    var png = new PngByteQRCode(data);
                    byte[] qrBytes = png.GetGraphic(10);

                    byte[] pngBytes = ComposePrettyPng(
                        qrBytes: qrBytes,
                        centerIcon: centerIcon,
                        bgCircleHex: "#123F6D",
                        projectName: projectName,
                        projectId: projectId,
                        counterNo: i,
                        queueTypeId: queueTypeId,
                        typeLabel: typeLabel,
                        typeEmoji: typeEmoji,
                        textTypeface: textTypeface,
                        emojiTypeface: emojiTypeface
                    );

                    string fileName = $"{Sanitize(projectName)}_{i}.png";
                    string entryPath = $"{imagesFolder}/{fileName}";
                    var entry = archive.CreateEntry(entryPath, CompressionLevel.Optimal);
                    using var entryStream = entry.Open();
                    await entryStream.WriteAsync(pngBytes, 0, pngBytes.Length);
                }
            }
            msZip.Position = 0;

            string zipName = $"{rootFolder}_{typeFolder}_QRcode.zip";
            return File(msZip.ToArray(), "application/zip", zipName);
        }

        // ===== helpers =====

        private static string Sanitize(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            foreach (var c in Path.GetInvalidFileNameChars())
                s = s.Replace(c, '_');
            return s.Trim();
        }

        private static async Task<SKImage?> TryLoadImageFromUrl(string url)
        {
            try
            {
                using var http = new HttpClient();
                var bytes = await http.GetByteArrayAsync(url);
                using var data = SKData.CreateCopy(bytes);
                return SKImage.FromEncodedData(data);
            }
            catch { return null; }
        }

        // Prefer Prompt; fallback to any Thai-capable font; finally to Default
        private static SKTypeface GetTextTypeface(IWebHostEnvironment env)
        {
            return TryLoadPromptTypeface(env)
                ?? TryLoadThaiTypeface(env)
                ?? SKTypeface.Default;
        }

        // Try to load Prompt from bundled files or system fonts; verify it supports Thai
        private static SKTypeface? TryLoadPromptTypeface(IWebHostEnvironment env)
        {
            static bool SupportsThai(SKTypeface tf)
            {
                using var f = new SKFont(tf);
                return f.ContainsGlyphs("ประเทศไทยกขคงจฉชซญำ่้๊ิีุู์ๆฯ");
            }

            // 1) bundled Prompt first
            var roots = new[] { env.WebRootPath, AppContext.BaseDirectory }
                .Where(p => !string.IsNullOrWhiteSpace(p)).Distinct();
            var promptFiles = new[] { "Prompt-Regular.ttf", "Prompt-Medium.ttf", "Prompt-Light.ttf" };
            foreach (var root in roots)
            {
                var fontsDir = Path.Combine(root!, "fonts");
                foreach (var file in promptFiles)
                {
                    var full = Path.Combine(fontsDir, file);
                    if (System.IO.File.Exists(full))
                    {
                        var tf = SKTypeface.FromFile(full);
                        if (tf != null && SupportsThai(tf)) return tf;
                    }
                }
            }

            // 2) system family “Prompt” if installed
            var sys = SKTypeface.FromFamilyName("Prompt");
            if (sys != null && SupportsThai(sys)) return sys;

            return null;
        }

        // Generic Thai-capable fallback (Noto/Sarabun/etc.)
        private static SKTypeface? TryLoadThaiTypeface(IWebHostEnvironment env)
        {
            static bool SupportsThai(SKTypeface tf)
            {
                using var f = new SKFont(tf);
                return f.ContainsGlyphs("ประเทศไทยกขคงจฉชซญำ่้๊ิีุู์ๆฯ");
            }

            var roots = new[] { env.WebRootPath, AppContext.BaseDirectory }
                .Where(p => !string.IsNullOrWhiteSpace(p)).Distinct();
            var files = new[] { "NotoSansThai-Regular.ttf", "Sarabun-Regular.ttf" };
            foreach (var root in roots)
            {
                var fontsDir = Path.Combine(root!, "fonts");
                foreach (var file in files)
                {
                    var full = Path.Combine(fontsDir, file);
                    if (System.IO.File.Exists(full))
                    {
                        var tf = SKTypeface.FromFile(full);
                        if (tf != null && SupportsThai(tf)) return tf;
                    }
                }
            }

            foreach (var fam in new[] { "Noto Sans Thai", "Sarabun", "Tahoma", "Arial Unicode MS", "Segoe UI" })
            {
                var tf = SKTypeface.FromFamilyName(fam);
                if (tf != null && SupportsThai(tf)) return tf;
            }

            // last try: font manager match
            SKTypeface? matched = null;
            try
            {
                matched = SKFontManager.Default.MatchCharacter(
                    familyName: null,
                    style: SKFontStyle.Normal,
                    bcp47: new[] { "th" },
                    character: 'ก'
                );
            }
            catch
            {
                matched = SKFontManager.Default.MatchCharacter(
                    familyName: null,
                    weight: SKFontStyleWeight.Normal,
                    width: SKFontStyleWidth.Normal,
                    slant: SKFontStyleSlant.Upright,
                    bcp47: new[] { "th" },
                    character: 'ก'
                );
            }
            return matched;
        }


        private static SKTypeface TryGetEmojiTypeface()
        {
            foreach (var name in new[] { "Segoe UI Emoji", "Apple Color Emoji", "Noto Color Emoji" })
            {
                var tf = SKTypeface.FromFamilyName(name);
                if (tf != null) return tf;
            }
            return SKTypeface.Default;
        }

        private static byte[] ComposePrettyPng(
            byte[] qrBytes,
            SKImage? centerIcon,
            string bgCircleHex,
            string projectName,
            string projectId,
            int counterNo,
            int queueTypeId,
            string typeLabel,
            string typeEmoji,
            SKTypeface textTypeface,
            SKTypeface emojiTypeface)
        {
            using var qrData = SKData.CreateCopy(qrBytes);
            using var qrImg = SKImage.FromEncodedData(qrData);
            int qrW = qrImg.Width, qrH = qrImg.Height;

            int padding = 24, captionHeight = 86;
            int outW = qrW + padding * 2, outH = qrH + captionHeight + padding * 2;

            var info = new SKImageInfo(outW, outH, SKColorType.Rgba8888, SKAlphaType.Premul);
            using var surface = SKSurface.Create(info);
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.White);

            // draw QR
            canvas.DrawImage(qrImg, padding, padding);

            // center circle + icon
            float cx = padding + qrW / 2f, cy = padding + qrH / 2f, r = MathF.Min(qrW, qrH) * 0.12f;
            var brandBg = SKColor.Parse(bgCircleHex);
            using (var fill = new SKPaint { IsAntialias = true, Color = brandBg })
                canvas.DrawCircle(cx, cy, r, fill);

            if (centerIcon != null)
            {
                float target = r * 2f * 0.80f, iw = centerIcon.Width, ih = centerIcon.Height;
                float scale = Math.Min(target / iw, target / ih);
                var dest = SKRect.Create(cx - (iw * scale) / 2f, cy - (ih * scale) / 2f, iw * scale, ih * scale);

                using var clip = new SKPath(); clip.AddCircle(cx, cy, r);
                canvas.Save(); canvas.ClipPath(clip, SKClipOperation.Intersect, antialias: true);
                canvas.DrawImage(centerIcon, dest); canvas.Restore();
            }
            else
            {
                using var tpaint = new SKPaint
                {
                    IsAntialias = true,
                    Color = SKColors.White,
                    Typeface = textTypeface,
                    TextSize = r * 0.95f,
                    TextAlign = SKTextAlign.Center
                };
                var fm = tpaint.FontMetrics;
                float baseline = cy - (fm.Ascent + fm.Descent) / 2f;
                canvas.DrawText(queueTypeId == 48 ? "B" : "I", cx, baseline, tpaint);
            }

            // white ring on top
            using (var ring = new SKPaint { IsAntialias = true, Color = SKColors.White, Style = SKPaintStyle.Stroke, StrokeWidth = r * 0.12f })
                canvas.DrawCircle(cx, cy, r, ring);

            // captions (Thai-safe)
            string title = $"{projectName}  #{counterNo}";
            float baseTop = padding + qrH + 10;

            using var titlePaint = new SKPaint
            {
                IsAntialias = true,
                Color = new SKColor(0x1E, 0x1E, 0x1E),
                Typeface = textTypeface,
                TextSize = 18,
                TextAlign = SKTextAlign.Center
            };
            var fmTitle = titlePaint.FontMetrics;
            float titleBaseline = baseTop - fmTitle.Ascent;
            canvas.DrawText(title, outW / 2f, titleBaseline, titlePaint);

            float lineH = fmTitle.Descent - fmTitle.Ascent;

            using var emojiPaint = new SKPaint
            {
                IsAntialias = true,
                Color = SKColors.Black,
                Typeface = emojiTypeface,
                TextSize = 14,
                TextAlign = SKTextAlign.Left
            };
            using var labelPaint = new SKPaint
            {
                IsAntialias = true,
                Color = brandBg,
                Typeface = textTypeface,
                TextSize = 14,
                TextAlign = SKTextAlign.Left
            };

            float emojiW = emojiPaint.MeasureText(typeEmoji);
            float labelW = labelPaint.MeasureText(" " + typeLabel);
            float totalW = emojiW + labelW;
            float startX = (outW / 2f) - (totalW / 2f);
            var fmLabel = labelPaint.FontMetrics;
            float secondBaseline = (titleBaseline + lineH + 6) - fmLabel.Ascent;

            canvas.DrawText(typeEmoji, startX, secondBaseline, emojiPaint);
            canvas.DrawText(" " + typeLabel, startX + emojiW, secondBaseline, labelPaint);

            using var finalImg = surface.Snapshot();
            using var encoded = finalImg.Encode(SKEncodedImageFormat.Png, 100);
            return encoded.ToArray();
        }
    }
}
