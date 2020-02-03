using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace TaxLegal.Cbc.Report.Api.Infrastructure.Utilities
{
    public static class FileHelpers
    {
        private static readonly byte[] AllowedChars = { };

        private static readonly Dictionary<string, List<byte[]>> FileSignature = new Dictionary<string, List<byte[]>>
        {
            {".gif", new List<byte[]> {new byte[] {0x47, 0x49, 0x46, 0x38}}},
            {".png", new List<byte[]> {new byte[] {0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A}}},
            {
                ".jpeg", new List<byte[]>
                {
                    new byte[] {0xFF, 0xD8, 0xFF, 0xE0},
                    new byte[] {0xFF, 0xD8, 0xFF, 0xE2},
                    new byte[] {0xFF, 0xD8, 0xFF, 0xE3}
                }
            },
            {
                ".jpg", new List<byte[]>
                {
                    new byte[] {0xFF, 0xD8, 0xFF, 0xE0},
                    new byte[] {0xFF, 0xD8, 0xFF, 0xE1},
                    new byte[] {0xFF, 0xD8, 0xFF, 0xE8}
                }
            },
            {
                ".zip", new List<byte[]>
                {
                    new byte[] {0x50, 0x4B, 0x03, 0x04},
                    new byte[] {0x50, 0x4B, 0x4C, 0x49, 0x54, 0x45},
                    new byte[] {0x50, 0x4B, 0x53, 0x70, 0x58},
                    new byte[] {0x50, 0x4B, 0x05, 0x06},
                    new byte[] {0x50, 0x4B, 0x07, 0x08},
                    new byte[] {0x57, 0x69, 0x6E, 0x5A, 0x69, 0x70}
                }
            },
        };

        public static async Task<byte[]> ProcessFormFile<T>(IFormFile formFile,
                                                            ModelStateDictionary modelState, IEnumerable<string> permittedExtensions,
                                                            long sizeLimit)
        {
            if (formFile == null) throw new ArgumentNullException(nameof(formFile));
            var fieldDisplayName = string.Empty;
            var property = typeof(T).GetProperty(formFile.Name.Substring(formFile.Name.IndexOf(".", StringComparison.Ordinal) + 1));

            if (property != null)
                if (property.GetCustomAttribute(typeof(DisplayAttribute)) is
                    DisplayAttribute displayAttribute)
                    fieldDisplayName = $"{displayAttribute.Name} ";

            var trustedFileNameForDisplay = WebUtility.HtmlEncode(
                                                                  formFile.FileName);

            if (formFile.Length == 0)
            {
                modelState.AddModelError(formFile.Name,
                                         $"{fieldDisplayName}({trustedFileNameForDisplay}) is empty.");

                return new byte[0];
            }

            if (formFile.Length > sizeLimit)
            {
                var megabyteSizeLimit = sizeLimit / 1048576;
                modelState.AddModelError(formFile.Name,
                                         $"{fieldDisplayName}({trustedFileNameForDisplay}) exceeds " +
                                         $"{megabyteSizeLimit:N1} MB.");

                return new byte[0];
            }

            try
            {
                await using var memoryStream = new MemoryStream();
                await formFile.CopyToAsync(memoryStream);

                if (memoryStream.Length == 0)
                    modelState.AddModelError(formFile.Name,
                                             $"{fieldDisplayName}({trustedFileNameForDisplay}) is empty.");

                if (!IsValidFileExtensionAndSignature(
                                                      formFile.FileName, memoryStream, permittedExtensions))
                    modelState.AddModelError(formFile.Name,
                                             $"{fieldDisplayName}({trustedFileNameForDisplay}) file " +
                                             "type isn't permitted or the file's signature " +
                                             "doesn't match the file's extension.");
                else
                    return memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                modelState.AddModelError(formFile.Name,
                                         $"{fieldDisplayName}({trustedFileNameForDisplay}) upload failed. " + $"Please contact the Help Desk for support. Error: {ex.HResult}");
            }

            return new byte[0];
        }

        public static async Task<byte[]> ProcessStreamedFile(
            MultipartSection section, ContentDispositionHeaderValue contentDisposition,
            ModelStateDictionary modelState, IEnumerable<string> permittedExtensions, long sizeLimit)
        {
            try
            {
                await using var memoryStream = new MemoryStream();
                await section.Body.CopyToAsync(memoryStream);

                if (memoryStream.Length == 0)
                {
                    modelState.AddModelError("File", "The file is empty.");
                }
                else if (memoryStream.Length > sizeLimit)
                {
                    var megabyteSizeLimit = sizeLimit / 1048576;
                    modelState.AddModelError("File",
                                             $"The file exceeds {megabyteSizeLimit:N1} MB.");
                }
                else if (!IsValidFileExtensionAndSignature(
                                                           contentDisposition.FileName.Value, memoryStream,
                                                           permittedExtensions))
                {
                    modelState.AddModelError("File",
                                             "The file type isn't permitted or the file's " +
                                             "signature doesn't match the file's extension.");
                }
                else
                {
                    return memoryStream.ToArray();
                }
            }
            catch (Exception ex)
            {
                modelState.AddModelError("File",
                                         "The upload failed. Please contact the Help Desk " +
                                         $" for support. Error: {ex.HResult}");
            }

            return new byte[0];
        }

        private static bool IsValidFileExtensionAndSignature(string fileName, Stream data, IEnumerable<string> permittedExtensions)
        {
            if (string.IsNullOrEmpty(fileName) || data == null || data.Length == 0) return false;

            var ext = Path.GetExtension(fileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext)) return false;

            data.Position = 0;

            using var reader = new BinaryReader(data);
            if (ext.Equals(".txt") || ext.Equals(".csv") || ext.Equals(".prn"))
            {
                if (AllowedChars.Length == 0)
                {
                    for (var i = 0; i < data.Length; i++)
                        if (reader.ReadByte() > sbyte.MaxValue) return false;
                }
                else
                {
                    for (var i = 0; i < data.Length; i++)
                    {
                        var b = reader.ReadByte();
                        if (b > sbyte.MaxValue ||
                            !AllowedChars.Contains(b))
                            return false;
                    }
                }

                return true;
            }

            var signatures = FileSignature[ext];
            var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

            return signatures.Any(signature =>
                                      headerBytes.Take(signature.Length).SequenceEqual(signature));
        }
    }
}