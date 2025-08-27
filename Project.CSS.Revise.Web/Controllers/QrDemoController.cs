using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Text.Json;

// ImageSharp (no System.Drawing)
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.Fonts;
using SixLabors.ImageSharp.Drawing;

namespace Project.CSS.Revise.Web.Controllers
{
    public class QrDemoController : Controller
    {
        public IActionResult Simple()
        {
            var payload = JsonSerializer.Serialize(new
            {
                type = "QC",
                projectId = "102C028",
                projectName = "Ocean View Residence",
                counterNo = 5,
                queueType = "bank"
            });

            using var gen = new QRCodeGenerator();
            using var data = gen.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            var png = new PngByteQRCode(data);

            // ✅ ใช้โอเวอร์โหลดง่ายสุด (ขาว-ดำ) เลี่ยงปัญหาสี
            byte[] bytes = png.GetGraphic(10);

            return File(bytes, "image/png", "QC_102C028_5_bank.png");
        }

        // ========== Pretty: add center icon + captions ==========

        public IActionResult Pretty([FromServices] IWebHostEnvironment env)
        {
            // mock data
            string projectId = "102C028";
            string projectName = "Ocean View Residence";
            int counterNo = 5;
            string queueType = "bank"; // or "inspect"

            var payload = JsonSerializer.Serialize(new
            {
                type = "QC",
                projectId,
                projectName,
                counterNo,
                queueType
            });

            // 1) QR bytes (ขาว-ดำแบบง่าย)
            using var gen = new QRCodeGenerator();
            using var data = gen.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            var png = new PngByteQRCode(data);
            byte[] qrBytes = png.GetGraphic(10);

            using var qrImg = Image.Load<Rgba32>(qrBytes);

            // 2) canvas
            int padding = 24;
            int captionHeight = 86;
            int outW = qrImg.Width + padding * 2;
            int outH = qrImg.Height + captionHeight + padding * 2;

            using var canvas = new Image<Rgba32>(outW, outH, SixLabors.ImageSharp.Color.White);

            // 3) draw QR
            canvas.Mutate(ctx =>
            {
                ctx.DrawImage(qrImg, new Point(padding, padding), 1f);
            });

            // 4) center icon (circle + glyph B/I) — ไม่ต้องวัด ใช้จัดกึ่งกลางแทน
            var center = new PointF(padding + qrImg.Width / 2f, padding + qrImg.Height / 2f);
            float r = MathF.Min(qrImg.Width, qrImg.Height) * 0.12f;
            var mainColor = queueType == "bank"
                ? SixLabors.ImageSharp.Color.ParseHex("105C5C")
                : SixLabors.ImageSharp.Color.ParseHex("C58700");

            canvas.Mutate(ctx =>
            {
                ctx.Fill(mainColor, new EllipsePolygon(center, r));
                ctx.Draw(SixLabors.ImageSharp.Color.White, r * 0.12f, new EllipsePolygon(center, r));

                string glyph = queueType == "bank" ? "B" : "I";
                var glyphFont = ResolveFont("Prompt", r * 0.95f); // น้ำหนักปกติ

                var glyphOpts = new RichTextOptions(glyphFont)
                {
                    Origin = center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                ctx.DrawText(glyphOpts, glyph, SixLabors.ImageSharp.Color.White);
            });

            // 5) captions under QR — ก็ไม่ต้องวัด ใช้จัดกึ่งกลางได้เหมือนกัน
            string title = $"{projectName}  #{counterNo}";
            string typeText = queueType == "bank" ? "Bank Pre-Approve" : "Inspect";

            var titleFont = ResolveFont("Prompt", 18);
            var subFont = ResolveFont("Prompt", 14);

            float baseY = padding + qrImg.Height + 10;

            canvas.Mutate(ctx =>
            {
                var titleFont = ResolveFont("Prompt", 18);
                var titleOpts = new RichTextOptions(titleFont)
                {
                    Origin = new PointF(outW / 2f, baseY),
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                ctx.DrawText(titleOpts, title, SixLabors.ImageSharp.Color.ParseHex("1E1E1E"));


                // ระยะห่างคร่าว ๆ 26px ใต้บรรทัดแรก
                var subFont = ResolveFont("Prompt", 14);
                var subOpts = new RichTextOptions(subFont)
                {
                    Origin = new PointF(outW / 2f, baseY + 26), // ระยะใต้ title โดยประมาณ
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                ctx.DrawText(subOpts, typeText, mainColor);
            });

            // 6) return PNG
            using var ms = new MemoryStream();
            canvas.SaveAsPng(ms);
            var fileName = $"QC_{projectId}_{counterNo}_{queueType}_pretty.png";
            return File(ms.ToArray(), "image/png", fileName);
        }

        // ฟอนต์ helper: ไม่ใช้ TryFind / Measure — ใช้ CreateFont พร้อม fallback
        private static Font ResolveFont(string preferredName, float size)
        {
            foreach (var name in new[] { preferredName, "Segoe UI", "Arial", "Helvetica", "Roboto" })
            {
                try { return SystemFonts.CreateFont(name, size, FontStyle.Regular); }
                catch { /* try next */ }
            }
            var fam = SystemFonts.Collection.Families.First();
            return fam.CreateFont(size, FontStyle.Regular);
        }
    }
}
