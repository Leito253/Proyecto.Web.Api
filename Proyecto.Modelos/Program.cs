/* using System;
using System.Reflection.Metadata;
using QRCoder;
using SkiaSharp;

 
dotnet add package QRCoder
dotnet add package SkiaSharp
dotnet add package SkiaSharp.NativeAssets.Linux.NoDependencies 


string url = "";

QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();

QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);

BitmapByteQRCode qRCode = new BitmapByteQRCode(qRCodeData);

byte[] qrCodeBytes = qRCode.GetGraphic(20);

using var bitmap = SKBitmap.Decode(qrCodeBytes);
using var image = SKImage.FromBitmap(bitmap);
using var data = image.Encode(SKEncodedImageFormat.Png, 100);
File.WriteAllBytes("qrCode.png", data.ToArray()); */