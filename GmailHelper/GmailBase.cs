using System;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;

namespace GmailHelper
{
    public class GoogleBase
    {
        const string PrivateKey =
            "-----BEGIN PRIVATE KEY-----\nMIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQCk9t8dQui/JjwN\nl/UvoG5AsNeVxw1RDSQFA/U1Dd89rhk56aJPPf5SShN5LAUzbBtWZdGzb/G3U8zQ\nGn2/BpszoKc0TkXPZT2lAiPT4YMm37NTCbnkBdAXsybxTt+Pf4stKwNWhTDGfEja\n+oRl5Y27H3BWxD5PFne6wK7Au963grRf3zs3gtIZasyWKaZeSnpfPervJLwo4UM/\n6V0pKBCkr6hdbGcdVQySqkLyrm2TSbUAJJ20QpWpGlq8fkoUmAFfgc1gfCClT5S4\n3+tOw5zVS9r7GRebGh5Ficx5RMZJvbS4BrYQoNvs1O31gSk102fvgJ45Vecpkkk0\nE0svkEO7AgMBAAECggEAPNDqpl4Bcl8FKrnH4ZwSqXTIteYhaa0fh13TK5EGqTWj\nBS+17+LZ/LpkfXqWHEQRvANjkPSHN2AElP//NcqYsyraGbV1lSUs2cV1MerksBMu\niEGAr6jY87PPeozqbHvH8on1/BK0TaiKL3jGEM7VNpQ7lTFaC0wsLcRVaKaaUCqf\nCYxzOcShj+l6FenubYgTH9unTcVuwP0+xycJXDPmg6SW+sEs8NBlP8shFDELuRU0\noK7AK7x1R/8+aiQ81v+uiUCFkKQeUegzbj0Bvp2i1l9hVejd+oiCC3rlX7q4Gupu\nwpso1voNxjnkXmt63CDMYSZvuJva3SGed+jj5EGqAQKBgQDRi2Yzxo0XNagLu7aG\neLG9MYC5S4Qku3nC8aADpSIGhIxXz2fj2QsNojKcVLa1DIlIxdRw2/FNTU73LMTm\n7wWpEEjXbwzZQho8xHhHttyZyPfgxt3pJ86Kz8LWKABobXklm94AzDX9mEzTPdqn\neX0WTiZh4m9+VntfDnmOZPdguwKBgQDJiVhKhQTTQ66klzsqadMB/LR7jKtiArI2\nkSAd5wPx0vgcH8eMibqdnenERU1t7pxAo4NExnfGt3uvPdsSJBfljKJIZXBDwMML\nnpNw/iq+pJhNAWbpHoPHjFCMJJ3DbEm2P+uDimxzKdh3MT86huLVCY0riMyVQIQA\nHdSGHdn5AQKBgAaPms+cM+a6I1zar8heFxKZbjJfDvAnfpSABEaY8QvLMqm9ML+N\nmC8BGRyT+nSIw/OnXhN6j3+gIAzVNgUVDtRQUjeMnpZe33tvm2SDcTzPchNzppca\nS/lEsBEMh5a9dsGas8TKoBYkoqMqmKINx8DfCYyfDmaiRpKfXWb7+lLpAoGANkbJ\nAnjEjekida1M/+U0MMrQCJ4f4et793oqPiNjSpNYwqpEYbMOETjgJj3L1pl2d2oS\nBkm1JE1yAAYWo9IYpXe0lO7Mx1J4tP5mEv5AoMtPQR3pi0rujqVZZATQgDr/txeL\n5Ac5oeJZK/CVaLu/B0TlQh47is6vPiDKhbwwYgECgYADS+yRWwFGM6vkBnotYjh8\nKg60eGXCvALu4JXgPB4ha9dK+WfUXAB+DSBFf8YEwHAInd1VIVC/9KL6txYp58OZ\n0iVKnP3Wnc16Lx50MDmhx0ktwzKDB2Zvad5E8RSeXG/3/erpnpVlfR9034qtiLJ3\nKGPLilPjN4LIOGH50iJq+w==\n-----END PRIVATE KEY-----\n";

        static string StatusPrivateKey = "-----BEGIN PRIVATE KEY-----\nMIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQCL8NGEfALE+WVz\noLQQxdS7f8HeHSiJ0zf6FzJPX3DXbCWh5SM+AFSeAFfPHYICCaZVnABzZABq927K\nJ1ORhyjuO+xKQt7m6gb5TkYY7GYuMK2CO2M21F6iq8yDPVTKgllSUraveJIhyepU\ndSFnY1XK/HFozqIhIHSkztOezKOPeF9USmWXTY4qTam0QsVLA9GlyxM740loNG8D\nDb8SsH+a+IQK2gQhp1TQhkj+hDHyE1D7Imjas421+JIQF/sBA9Gq5EBkbcmaumt+\nyVMZ9b3o/Tdcg6XLcOShDRP38h3y9HR8hEK0HNXpGUWHi1o/qBT9vJ0uCWQtWBBS\noWPgAMPjAgMBAAECgf8CuCi0pI196VBtxB+XdH7DxlGk6VYqB1GzWplBTBwVFiz/\nin1pWzsycfiiiIoeilx0V8Ymlus3cVGnnAIrzeo9DtXFaoc/r6w3i7FnAONyYgOn\n0H+fpx6R7jusyCiHB40PTmqOjyzOQmVUYwb+n4lnDzCeh1C7rpG8ffB/ybCKxR3m\n5Uw5sk6wzOBXI+TT2QkNzlSyYZ+B7Bz2FCyyaWwlUDnenE15xewniqmP/R0SyV4a\nrrrSqDxV27c6bSHU5jee9Y3gbyTmunkPCqXjKdIbafX5B4kJNf5UlxCxK+QU11xi\nTqdfPbChM/wRwFRc3BMDUutwpgD22JqJhYkPSmECgYEAxTSXaoL/cX90F/HQSVH9\n2QYMs115HTMhq3ZZQRMS3Qggr2q06hCW6vaIFCXBW/eatO9RgRk7v7SaUeFjYXEM\nPBupZi71nCuFSB/XgwF6CN18QIRVf1wRVK3HP1daaWDNjiPbLsdDaohYjPNDy8s9\naO7VwCLdqLyIz0VRp2PWY4MCgYEAtamU8881CQFQBCb02q0sNtvjO1cBFh40lIWv\n1z/IWnp+AUUDB5g14cCMs4STkpZZmZuPHcu0NMnac1mOj+141gNriYI38xvXSumB\nSwuUWGDkIQ/HmEh0I0Oy5pgNleK6HQc81WujB3l9gDKhvyp8NVcyD2V82owHBENr\ntXCXUCECgYEAtPvJIrVKne0srHT+hOu8npoQueRFLOkqM2QQEcrhevzkkljJ5C6J\ncuYCiQsFY9PfpcIk+Oghj5S+M/s368uGQPvaAa+DNWozjbe7X73RM6WhTMymcpYa\nSa6at6UBksiGnbaGh23Uj0bGjcUMKVJC77SRcx4Mc7ljjWW+xHK55OUCgYBfu7uU\ndMjlMvzhA0qcBxC65EwulF4tMsPQ1VbiX2mNTOokbyL2mJ8klh1P6Xj/kh4r1g+o\nyxXvj8SAb1yyupmoZur/71De2/gsMTwCuVp27LCsP62WsfAKOX4JCW53Y6Fi9NJ5\nQh0LLloV+hjFLnWAgrAN7xTJ82srMwCLAIetIQKBgQC/Ejk+JUow2VaWEAXn2Bp6\nz5scOn65e8po5tqK0Mjl5eWwHseRpQHFMa8s4drlOx4jmDJpgCTnZHihy2f86Uc3\naoSO1bdlY8f0OXcPPDIcBKOz8ztGMLCDPH56SQqSHZD1RtdaRO4+wNqffkzQIsDq\nG8moO83q2H7KwGh+SmGTSQ==\n-----END PRIVATE KEY-----\n";
        static string StatusEmail = "status@euphoric-coast-346418.iam.gserviceaccount.com";
        public const string StatusOfficialEmail = "status@shopimo.co";

        const string BJSEmail = "admin-274@gmail-access-for-shipping.iam.gserviceaccount.com";
        public static GmailService GetMailService(string email = "bjs@shopimo.co")
        {
            var privateKey = PrivateKey;
            var acc = BJSEmail;
            if (email == StatusOfficialEmail)
            {
                privateKey = StatusPrivateKey;
                acc = StatusEmail;
            }
            var credential = new ServiceAccountCredential(
                new ServiceAccountCredential.Initializer(acc)
                {
                    User = email,
                    Scopes = new[]
                    {
                        "https://mail.google.com/",

                    }
                }.FromPrivateKey(privateKey));
            if (credential.RequestAccessTokenAsync(CancellationToken.None).Result)
            {
                var initializer = new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Gmail Access"
                };
                return new GmailService(initializer);
            }
            return null;

        }

        //// Helpers
        //public static string GetServiceJson() =>
        //    SiteMetaDAO.getMeta("GoogleServiceAccount")?.MetaText;
        //private static BaseClientService.Initializer GetServiceInitializer(string[] scope)
        //{
        //    var credential = GetGoogleCredential(scope);
        //    if (credential == null) return null;
        //    return new BaseClientService.Initializer
        //    {
        //        HttpClientInitializer = credential,
        //        ApplicationName = "Octacer Portal"
        //    };
        //}

        //private static GoogleCredential GetGoogleCredential(string[] scope)
        //{
        //    if (scope?.Length == 0)
        //        return null;
        //    var json = null;// GetServiceJson();

        //    if (string.IsNullOrWhiteSpace(json))
        //        return null;
        //    var credential = GoogleCredential
        //        .FromJson(json)
        //        .CreateScoped(scope);
        //    return credential;
        //}
    }
}
