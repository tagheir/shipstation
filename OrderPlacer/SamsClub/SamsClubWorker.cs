
using Generics.Common;
using Generics.Db;
using Generics.HelperModels;
using HttpRequester;
using OrderPlacer.SamsClub.Models;

using System;
using System.Collections.Generic;

using System.Linq;
using System.Net;

using System.Threading;
using System.Threading.Tasks;

namespace OrderPlacer.SamsClub
{
    public static class SamsClubWorker
    {
        public static int CurrentId = 1;
        private static string Authorization = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IjQ5NTYzNUQ3N0NGOUM4NDc3RDJDNEUyODA5RThGMTYwMzc0RENFQ0UiLCJ4NXQiOiJTVlkxMTN6NXlFZDlMRTRvQ2VqeFlEZE56czQifQ.eyJpc3MiOiJodHRwczovL3RpdGFuLnNhbXNjbHViLmNvbS9jOGNjMDcwZS1kZmVkLTQ1YzktYmRhNS0yOWQwMDEyMWFjYmIvdjIuMC8iLCJleHAiOjE2Nzc3NDMzNzksIm5iZiI6MTY3Nzc0MTU3OSwiYXVkIjoiMzc2OTdmMjUtYjcyYy00NWRmLWJlNTEtNTU4Zjg4MjRmYzY0IiwibWkiOiJkNzZlM2YwYTdkMTM2NTZmMjBjNDQyYzgwYTI2MTI4OTcyNDgwNDQ0ODZhNGJjNGQ0NWVkMTA2M2I4N2FkOTg0Iiwic2wiOiJZIiwiZmx0IjoiMTY3Nzc0MTU3OCIsImp0aSI6IjA5MTlhM2EwLWQ5NDItNDcwOC05YWM4LWMwYTdmNjg2MzQ1MiIsImNoaSI6IndlYiIsInVmbiI6IlRpYXJhIiwibXQiOiJCIiwibXMiOiJBIiwibWV4IjoxNzAzMzc1OTk5LCJlbWMiOmZhbHNlLCJwaWQiOiIxOTk2M2MwZjc1MjgyMGYzMmMyNjFiMzJhZDYzMjNhZjMyZjY4MmI3MDExNzRjZjYyZDBkNGJhN2Q5YWE5OTM0Mjk3Yjk1YjFiM2IxNTBiZDU2ZmFhY2Q2YTFmNmFhMjAiLCJtdXVpZCI6IjQ5MTg2MWM3ZjRiZmJkMWZhMDdiNWQ3Mzk2OGI2YWNmODcyNGMwYWFjZTliZDQ1NTY3NDVkM2E3YzhjNWQ2MmYwMWY4MDJiZTQ2OWMwNzI2ZjcwNWFjNzI0NzcwYmZkMSIsInVucSI6IkIyQyIsImNzY3AiOiJzYy5ucy5hIHNjLnMuciIsInJmIjoiTiIsImlzc3VlclVzZXJJZCI6IjBmODY3M2U5ZmY4MjBmMjYzZmVmZDE4NGM4ZDhjMzQ3ZjYzZDg3ZWUyZjUyNTMyOTdkZmJmYjViMTAxZGZhZjQiLCJwYyI6IjgxNjYiLCJidnQiOiIxN2VlMzQ0YWI1ZGFiZGVhNTFjMTlkNGMyODljYzAzNDY0NjE3NDY1M2QzMjMwMzIzMzMwMzMzMDMyMjY2NTZkNjE2OTZjNjE2NDY0NzI2NTczNzMzZDZmNzI2NDY1NzI3MzI1MzQzMDczNjg2ZjcwNjk2ZDZmMmU2MzZmMjY3NTczNjU3MjY5NjQzZDM0MzkzNDMwMzIzMDYyNjYyZDMyMzQzOTYxMmQzNDYzNjUzNDJkNjI2NDMxNjMyZDY2MzQzNjY0NjMzMTMwNjQzMjYyMzM2MSIsImNodCI6IlMiLCJ0cGNuIjoic2Ftcy1zcGEiLCJibSI6IlkiLCJzaWMiOiI1MDk5Iiwic3ViIjoiYmQ4MmZkZGItMGYwMS00MGFlLWI3M2UtZDZkZjE5ZGU3ZDE5IiwidGZwIjoiQjJDXzFBX1NpZ25JbldlYldpdGhLbXNpIiwibm9uY2UiOiJkZWZhdWx0Tm9uY2UiLCJzY3AiOiJzYy5zLnIgc2Mucy5hIHNjLm5zLmEiLCJhenAiOiJjZGMxYmJhMi1mMWEyLTQ3MmQtOGU0ZS1mMDZiMjFmM2Q4MWEiLCJ2ZXIiOiIxLjAiLCJpYXQiOjE2Nzc3NDE1Nzl9.V7Pg86DmGnMd0oifhqMfqBC-yAfzQ0OD8a6QETHHW9oP8-tj0JbrFJbLN6u0RhGewbUe5FGcoGbdndevMjheA6bXCQWXJsZueU_gkfEF0yEOh15Do4N02Q5Bgnwe1CFMHjUF6ks711XKh_qvyiDNMp9Bvnw0YI4gFbFGKv3DL7zUd_HxapucF93kGHo84mg-ztG51q0tHezFHFBti9WRkY_YZYhR-rBjxNwdWG2Avz9zs7S1IiDSKnSZbO3cxjQD57FEEzaVKV6IagPU4jh0eBZ7qIzwPTooi5gDTHu-z6QHL9o1kEmyt8GZdwnVw6oujSDjlKDZzg2Y28DFWv0WoDLoSIsWtopo7W-YS9pX-ZVINGytpq15ZSf8VACrh70saY1HPFMKUISc9H-6ayCcQm8YLDu30-2c50zCm1DSpl-TnFrxQ_zuK_3QfyjQETcRR6Gi9FIBG0ENXkdOgg8mBBJIHn3mPAuifMaVNKKxUmPGcRHnGIPHN9fAbrb4ZJt_oB-FAZOjGxlC-jANNcM7HKgY4s6I81gJcts9O-EIDWuGWjCGKuN-b7y3U7PbUVBNhihqb-x0QE8x0T_gbGR4yFG535dqmdvNSXBxMVCiEZgkflSBgMD1xGpyZU6dNeCH4g8ej6u80bZ9VD5xWMZ0-3S3F8MC9j2TlOSMIc9zGF0";

        private static string CartId = "ca-06bf26cb-2906-4a1e-907f-65667d11f96d";
        private static string ContentType = "application/json";
        private static string CsrfToken = "";
        private static string Accept = "application/json, text/plain, */*";
        private static string Authority = "www.samsclub.com";
        private static string AcceptLanguage = "en-US,en;q=0.9";
        private static string RemoveCartRefrer = "https://www.samsclub.com/cart?xid=hdr_cart_view-cart-and-checkout";
        private static string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.0.0 Safari/537.36";
        public static string Cookie = " vtc=afMMl8aPCeaRcbs1ofhsmQ; _gcl_au=1.1.1856935416.1674556813; _pxvid=7b2e31f3-9bd3-11ed-ad8f-4b554359546f; __pxvid=7bbb074a-9bd3-11ed-a0d8-0242ac120002; s_ecid=MCMID%7C42687557492704258100021573272982009037; QuantumMetricUserID=f48944b48d94dc4cc549b098d4f39fdd; __gads=ID=e2d66ba97ebcc8a4:T=1674556815:S=ALNI_MYmvYex04xNXoHrmPmPNVdzE26UDw; _mibhv=anon-1674556818895-4701752193_4591; _fbp=fb.1.1674556819530.1114143490; _pin_unauth=dWlkPU1qRTBOalUwWXpVdE1qRTVaaTAwWmpFeUxUaGhNVGN0WTJWaU9UWTRNVFU0TkdReg; salsify_session_id=776c48ec-637a-472e-b820-68b7614d98cc; BVBRANDID=d4c00314-d45c-4602-b816-5d22d3f9ff30; _br_uid_2=uid%3D3571142747270%3Av%3D13.0%3Ats%3D1674556818724%3Ahc%3D2; _tt_enable_cookie=1; _ttp=_vL8jVHKOXggqpZQ6ERqSMe6f1F; _uetvid=7de4b5209bd311edbc7b593114cd2bf3; cto_bundle=3tHUCV9yelJjdmtFVGkyMkxRUmZiTTNORW1WQzRUSUQlMkZ5UEJGZ1A0TmJFRThEMXlIZVg4WHN0aFkwZGJHTnEzbFZuSlFsUllFWExaNjBRVUYlMkYzT0gxNm9MNGpRM2tlJTJGMEdNNGh3MjJZdTlSaWZzS3hkWVFEVkMxOFVSOHlrczJFRXJVNXN4Z2h6bFlhRkg4cThaUEYwNll6JTJCQSUzRCUzRA; sod=20230126_d1778ceb-b344-41af-9c79-44d2bd8cf32b; SSLB=1; astracxo=pc-f18f47c1-13bc-4e90-b4d9-3e6a7c721803; memType=business; astract=ca-06bf26cb-2906-4a1e-907f-65667d11f96d; pilotusercookie=memId%3Ad76e3f0a7d13656f20c442c80a2612897248044486a4bc4d45ed1063b87ad984%7Cplus%3AN; bvUserToken=062caf4259e9da0e6ddbd625407bcfb9646174653d323032333032303126656d61696c616464726573733d6f726465727325343073686f70696d6f2e636f267573657269643d34393430323062662d323439612d346365342d626431632d663436646331306432623361; firstNameCookie=Tiara; myPreferredClubName=Duluth%2C+GA; myPreferredClub=8166; samsathrvi=RVI~prod14220240-prod24544046-prod25430027-P03003095-prod23132701; SAT_SFL_SUBSTITUTIONS=1; SSID1=CQDInR0OAAgAAACFtc9jcwRBEIW1z2MLAAAAAACXeeFl60QAZABjBqAcAQMTqiQA60QAZAEAwxsBAEAcAQA; SSSC1=362.G7192166713041945715.11|72864.2402835; SSRT1=60QAZAADAA; sxp-rl-SAT_GEO_LOC-rn=77; sxp-rl-SAT_WSS01-rn=13; SAT_WPWCNP=0; sxp-rl-SAT_OD_SURVEY-rn=28; sxp-rl-SAT_CHAT-rn=64; sxp-rl-SAT_CART_SUBSTITUTIONS-rn=89; sxp-rl-SAT_DISABLE_SYN_PREQUALIFY-rn=5; az-reg=wus; sxp-rl-SAT_REORDER_V3-rn=15; bstc=RhOIe8yu6-dDJ7lp3ajJ4A; bm_mi=9C57258E90EAA9EEFDE0693173A78171~YAAQXZ4QAlD3q5iGAQAAnUMNoRKsQHJzeaync6m++gQfPPVhISYTz+1QRdvRIdU8p5qDVih8aZAOt/GN8DC5HVDkQNwrccXlWhXPCPk0AlUNx1KIA9JD9YPEMkstSgQnVaTeDJ02ERRIpL23oqDEUTnOF5MSZTUZlVmv+TMkqtR2I5m6v6dA9Wsxy2H1XEk47ptic87XMDcxckpPu3KpZPHTYQDk6xGRozSGDE903Awc7EW9x/JDjG6MG7nwvPZOGe9cEyBkLLMVqDLyt2WWDc/99cNPE6T4syb49hee9hSx4le+SPh5iFf94es9GJq3OqO1QozuunKY1fOFQ7zpJtqVtXLfjKiKxojIfZ7jJ39CxHZKYKAN9MFmZIAb4w==~1; ak_bmsc=E647A3C495454851CD7B8E9C9EE2E79F~000000000000000000000000000000~YAAQXZ4QAlT3q5iGAQAA30MNoRJxrYesGpRk44+9n8/sO9eA/qe8JkwAiJgggQIsfswSD8fC9S+lcL4IFqaECBmCnxVvs7YbU4Ur0WLtf817QOVocIyffjMS9T108lDXElqiTMz66HbEXHyDBpnR88tDguXYDvnU2rxilSTYdSMxC7r43lFabtr77zVUvJHWNQ7m6E97EoVX0lzous0hAve+EGgtug0ZX73cqOFBCkhXXUc55F801GzQHHtv9RwiaFSh6XFIsb5g9x3v/PyCkTccENAiH4ncg5ebeAgS+YK7TFewU7FqDk8Lx7HK3VigcOKGtfdyvAouRqr8LZ8acLPYPuiF8fLriJnJwDSWidBUdEi0GkuIH8x+amh4Zy+UIE2DE9Wr4/hN05UhQ6gapQwbc+GkyOoSiLBJZZ7JKmp1FBLt3Gmb5ioDbvX7dAOZ9e6C9vdfH++EE24Cq9M/vhVYm6nUkceK45fB1i28+D5O78k/V5KXwryjeKoT8zxMF8SxuIu7P8maUqeWwAnG618A9PYkGaGxcc3/drgqFv6eiImqow2FRaKzQuwdbWfW9vI=; authToken=eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IjQ5NTYzNUQ3N0NGOUM4NDc3RDJDNEUyODA5RThGMTYwMzc0RENFQ0UiLCJ4NXQiOiJTVlkxMTN6NXlFZDlMRTRvQ2VqeFlEZE56czQifQ.eyJpc3MiOiJodHRwczovL3RpdGFuLnNhbXNjbHViLmNvbS9jOGNjMDcwZS1kZmVkLTQ1YzktYmRhNS0yOWQwMDEyMWFjYmIvdjIuMC8iLCJleHAiOjE2Nzc3NDEwNDYsIm5iZiI6MTY3NzczOTI0NiwiYXVkIjoiMzc2OTdmMjUtYjcyYy00NWRmLWJlNTEtNTU4Zjg4MjRmYzY0IiwidW5xIjoiQjJDIiwic2wiOiJZIiwiZmx0IjoiMTY3NTIzMzIxNiIsImp0aSI6IjlkNjU2NGM4LTYwOGYtNDg3Yy04ZjE5LTk5MjgwNTdiMjYzZiIsImNoaSI6IndlYiIsIm1pIjoiZDc2ZTNmMGE3ZDEzNjU2ZjIwYzQ0MmM4MGEyNjEyODk3MjQ4MDQ0NDg2YTRiYzRkNDVlZDEwNjNiODdhZDk4NCIsInN1YiI6ImJkODJmZGRiLTBmMDEtNDBhZS1iNzNlLWQ2ZGYxOWRlN2QxOSIsImlzc3VlclVzZXJJZCI6IjBmODY3M2U5ZmY4MjBmMjYzZmVmZDE4NGM4ZDhjMzQ3ZjYzZDg3ZWUyZjUyNTMyOTdkZmJmYjViMTAxZGZhZjQiLCJlaWQiOiIiLCJwcmkiOiIiLCJ1Zm4iOiJUaWFyYSIsIm10IjoiQiIsIm1zIjoiQSIsIm1leCI6MTcwMzM3NTk5OSwiZW1jIjpmYWxzZSwicGlkIjoiMTk5NjNjMGY3NTI4MjBmMzJjMjYxYjMyYWQ2MzIzYWYzMmY2ODJiNzAxMTc0Y2Y2MmQwZDRiYTdkOWFhOTkzNDI5N2I5NWIxYjNiMTUwYmQ1NmZhYWNkNmExZjZhYTIwIiwibXV1aWQiOiI0OTE4NjFjN2Y0YmZiZDFmYTA3YjVkNzM5NjhiNmFjZjg3MjRjMGFhY2U5YmQ0NTU2NzQ1ZDNhN2M4YzVkNjJmMDFmODAyYmU0NjljMDcyNmY3MDVhYzcyNDc3MGJmZDEiLCJjc2NwIjoic2MubnMuYSBzYy5zLnIiLCJyZiI6IlkiLCJwYyI6IjgxNjYiLCJidnQiOiIxN2VlMzQ0YWI1ZGFiZGVhNTFjMTlkNGMyODljYzAzNDY0NjE3NDY1M2QzMjMwMzIzMzMwMzMzMDMyMjY2NTZkNjE2OTZjNjE2NDY0NzI2NTczNzMzZDZmNzI2NDY1NzI3MzI1MzQzMDczNjg2ZjcwNjk2ZDZmMmU2MzZmMjY3NTczNjU3MjY5NjQzZDM0MzkzNDMwMzIzMDYyNjYyZDMyMzQzOTYxMmQzNDYzNjUzNDJkNjI2NDMxNjMyZDY2MzQzNjY0NjMzMTMwNjQzMjYyMzM2MSIsImNodCI6IlMiLCJ0cGNuIjoic2Ftcy1zcGEiLCJibSI6IlkiLCJzaWMiOiI1MDk5IiwidGZwIjoiQjJDXzFBX1NpZ25JbldlYldpdGhLbXNpIiwibm9uY2UiOiJkZWZhdWx0Tm9uY2UiLCJzY3AiOiJzYy5zLnIgc2Mucy5hIHNjLm5zLmEiLCJhenAiOiJjZGMxYmJhMi1mMWEyLTQ3MmQtOGU0ZS1mMDZiMjFmM2Q4MWEiLCJ2ZXIiOiIxLjAiLCJpYXQiOjE2Nzc3MzkyNDZ9.QZn77o-fiOUMshEgugyljL65DrdGAKxOWy6nI8TKUTXHMSZ2oRMCRfe3g7cZ1TWbkujwUhhFgRaXeJG16BSwVcNdfqteKhlVXMEWxb4715tG2L87o7Bk_UWtIYsKxymGAeTtgUqj1GCMdq_MWvWv2r6DALuyZntetoXtOdN-tNK25D5FH9D_apEgITXWHUvC9vMcPjCT4ujZpgxJ0b158BMiL1IO-3De2w6PAkcEcNg7YTzw7Prh4EnciwTqGjREwaDCU9hvTr-OeHbh2FXp0advs9rFuvcWKDfKe0yvDf1ZdgkhnDNg95IKC0OpckpMuShMw0GF0j5g0HUVLr_WJBm-LXchtWck4Qudt88yElRih-iNbjcrg2uFotWkoX-v2uWQ7vvTjcmnbsP5TyBxr4hAzbIPejdSjzPqYVbfNWoTX3d0CwU9G5-sXR02KwIDVhIRoCrdLWuDOv0Tdsv6FJ7Ny_j7m6Qqf_GNoTJwYyKiSZ3UJMDGAf5sGImFNWVv-G8JTnbe_5zc6xbgw-uVgbjasj3Hc-NmK1YyugHJZbR0SYaPXgnXck-7tPHKVnaRlClSN0q-wPqJ08F4Ncq0Ng5L0q8_eOKtPKTfPlT2tCP7N02YGqQVrjyhmaivgblgWj442cEMBpRFa-Tw0PQqlwrbNBgOB3a2eX36-Z13zqQ; sxp-rl-SAT_CART_SUBSTITUTIONS-c=r|1|95; SAT_GEO_LOC=0; SAT_CART_SUBSTITUTIONS=1; sxp-rl-SAT_CHAT-c=r|1|100; SAT_CHAT=1; sxp-rl-SAT_WSS01-c=r|1|100; sxp-rl-SAT_OD_SURVEY-c=r|1|20; sxp-rl-SAT_DISABLE_SYN_PREQUALIFY-c=r|1|0; SAT_WSS01=1; SAT_OD_SURVEY=0; sxp-rl-SAT_REORDER_V3-c=r|1|50; sxp-rl-SAT_GEO_LOC-c=r|1|50; SAT_DISABLE_SYN_PREQUALIFY=0; SAT_REORDER_V3=1; TS4fc56a0b027=0800b316f6ab20005d1e8f6e3c58671833e8a4e6d33adde03e92abe52a29452b04ffa6e12106e786084b8a0ada11300037282fd42922489afd9c9c11e8792fa3ed6a531591f11e2b02f2a16deb8222f9cb985546f02ea5a6b57631657f4d2706; rcs=eF5jYSlN9jAwT05KSjZM0zUys0jRNUkzM9M1SjWx0E1OMzNMtDAxNEg0SOHKLSvJTBEwNDc20zXUNQQApKsOkA; AMCVS_B98A1CFE53309C340A490D45%40AdobeOrg=1; AMCV_B98A1CFE53309C340A490D45%40AdobeOrg=1585540135%7CMCIDTS%7C19419%7CMCMID%7C42687557492704258100021573272982009037%7CMCAAMLH-1678344050%7C3%7CMCAAMB-1678344050%7CRKhpRz8krg2tLO6pguXWp5olkAcUniQYPHaMWWgdJ3xzPWQmdj0y%7CMCOPTOUT-1677746450s%7CNONE%7CMCAID%7CNONE%7CvVersion%7C4.4.0; s_cc=true; pxcts=2abca2a5-b8c5-11ed-8e94-57464c6f4876; _pxff_idp_c=1,s; _px3=5da765bfd3d913b793a92e43dd79b1ed21664c959525cd607db4a39da615c2ac:YwR5VWGiu/KrfBaqsnblsCMsxzWJ8aqu7APosRqV+qbSjo6lUPiThuXNXInUxOsfCB8eEPmgfvr7Oq7WxxucWg==:1000:yCc+fv0AWKDd30oHNoDn9NXpCGBFJc/gcElNFa+FgiOApNXk2DN8MAzmAA4oMR2KfYDUwLO05dTLxC9J1XdmOV439QQDpuUlxfVako0jDb/cjNpAXXz4gb36WQZgC49k+9nTnn/b3n2hQsaALVOG81QEYMF9AZA2nLceU/zQ8H1/5ikFEH5yFOCCbaJby4/QlHlOW5yBScDN30wnHCnGjw==; __gpi=UID=00000ba88fb3451e:T=1674556815:RT=1677739253:S=ALNI_MYb-7nZwvxg41vddL71uYayJYErBg; smtrsession=loggedMemType%7Ctrue%5EauthEvent%7Ctrue; QuantumMetricSessionID=a16521fd3d75b7f372e3b91c3e4f5fda; s_sq=%5B%5BB%5D%5D; _pxde=f8455e98d9f7e329342a56e856d6614bf794b8787832920a053c7f7be651b65e:eyJ0aW1lc3RhbXAiOjE2Nzc3MzkyNzQ5MDV9; acstkn=74:7#326588478#7=484497046_1677739276481; TS01b2183e=0176a9cf86cc622acd682f93d009641b1974540e099ed4c2154bc671afad4cd9b868214e50da2757cfa46ffc52271e1fca46398509; TS4c030c5e027=08cb8c7367ab2000539c982ec2cd260cbb210a16c0ba4e7a3d62e1c6c4c38f4cd0e89fb0569b6994085adc6e2c113000e07ffe280716def4ccb0e57e435ef360842cfb4e085f2e2ac55cf370fc858bbc26d56c7d6a45c2bdfb49e9712b86409f; akavpau_P1_Sitewide=1677739883~id=2679197e82a2db0f588430cadee47867; smtrrmkr=638133360866630000%5E0185e35d-464b-4074-9cd0-8e11fc9aa6e1%5E0186a10d-657a-46e8-9882-c9fa21667925%5E0%5E103.255.4.77; TS01bc32b7=0176a9cf860909cddb51148b42cce7ddbc9b792ec22f66ef9cbbc2394bae4a2fdfa2af08927919ef6b8234f58f1b5d5d85a1dc55eb; xptwj=js:53a43f8e3b4f8071e272:3A+HF58JU8JwrkThCZrejg/QhWu5+NglT4qkcg66VFRO0RX5FfEdRd0BMuSsOXseu4IMaJy0VPO5/Tf1pB2ZjaFhY5+cH7Syze1SQaHZRZcgKap44MTb6Xt3p5GUCLZ0sdYB6zdp4M7Omj6WvWrAxe/4DBvYhQ==; seqnum=8; TS0171ed6c=0176a9cf860909cddb51148b42cce7ddbc9b792ec22f66ef9cbbc2394bae4a2fdfa2af08927919ef6b8234f58f1b5d5d85a1dc55eb; akavpau_P2_Cart_Checkout=1677739885~id=68bc868c711f09ad13309d6863dbf9b9; bm_sv=77B5D974744C8C3B9DAB8EC80EC5EA8E~YAAQXZ4QApH9q5iGAQAAntoNoRIelTXQTHzX1uY54ZH7yQN/AKOTEiG858LghwseajJV1r6+r0262QEp+5YOcvl9CtI1E0DIX9seYLKMxzh+H9PjITAB6pM39OsmTDc0c/0cAB7bTTTRbUHo4QGIiPzqLC0xE6xLM67bc80xutvuKunURBbi2ET1yhfuu+EpXRjYee+c5Z/Xpwjms+mzwDI3lJoiChFYwlusr7NGJ1iAVTDWti2nadRgR5oTvp62+xM=~1";
        public static string DefaultPhoneNumber = "8008791197";
        public static string DefaultAddressId = "058b0b95-fb2e-4267-854d-86dbe5f56d8e";

        // URLS
        private static readonly string EligibleOrder = BasePath + "api/node/vivaldi/account/v3/eligible-order";
        public static readonly string MemberShipInfoUrl = "https://www.samsclub.com/api/node/vivaldi/account/v3/membership?ts=9394066391659163376003";
        private static string GetAddressesUrl => "https://www.samsclub.com/api/node/vivaldi/account/v3/address";
        private static string RemoveAddress(string addressId) => $"https://www.samsclub.com/api/node/vivaldi/account/v3/address/{addressId}";
        private static string RemoveItemsFromCart(string CartId) => BasePath + $"api/node/vivaldi/cxo/v2/carts/{CartId}/removelineitems?rsg=FULL";
        private static string AddItemsToCart(string CartId) => BasePath + $"api/node/vivaldi/cxo/v2/carts/{CartId}/lineitems?rsg=FULL";
        private static readonly string BasePath = "https://www.samsclub.com/";
        private static int storeId = 8166;
        private static string ProductSearcher(string sku) => $"https://www.samsclub.com/api/node/vivaldi/browse/v2/products/search?searchTerm={sku}&clubId={storeId}";
        private static string GetCart(string CartId) => $"https://www.samsclub.com/api/node/vivaldi/cxo/v2/carts/{CartId}?rsg=FULL&appendC7=true";
        private static string ValidateAddressUrl => BasePath + "api/node/vivaldi/account/v3/address?avsValidate=true";
        private static string WalletInfo => "https://www.samsclub.com/api/node/vivaldi/account/v3/sams-wallet?response_group=full";
        private static string GetLatestCart() => "https://www.samsclub.com/api/node/vivaldi/cxo/v2/carts";
        private static string TaxExemptUrl(string ContractId) => $"https://www.samsclub.com/api/node/vivaldi/cxo/v2/contracts/{ContractId}/lineitems";

        public static async Task<HttpResponser> InvokeRequest(HttpRequester.HttpRequester request)
        {
            request.IsAuthorized = false;
            var resp = await HttpHandler.Request(request);
            if (resp.StatusCode.Equals("Unauthorized"))
            {
                var token = LayerDao.SiteMetaDAO.GetKey(Generics.SiteMetaConstants.ACCESS_TOKEN_SAMSCLUB);
                request.CookieContainer.Add(new System.Net.Cookie()
                {
                    Name = "authToken",
                    Value = token.VALUE,
                    Domain = ".samsclub.com"
                });
                Cookie = request.CookieContainer.GetCookieHeader(new System.Uri("https://www.samsclub.com"));
                return await HttpHandler.Request(request);
            }
            return resp;
        }
        public static HttpRequester.HttpRequester GetRequest(HttpMethod httpMethod, string Url)
        {

            var request = new HttpRequester.HttpRequester()
            {
                Method = httpMethod,
                Url = Url,
                IsAuthorized = false,
                Params = new System.Collections.Generic.List<HeaderParams>()
                {
                   HeaderParams.GetHeaderParam(HeaderType.authorization,Authorization),
                   HeaderParams.GetHeaderParam(HeaderType.contenttype,ContentType),

                   HeaderParams.GetHeaderParam(HeaderType.useragent,UserAgent),
                   HeaderParams.GetHeaderParam(HeaderType.accept,Accept),
                   HeaderParams.GetHeaderParam(HeaderType.acceptlanguage,AcceptLanguage),
                },
            };
            
            
            return request;
        }
        public static async Task<string> ProductSearchRequestAsync(string productSku)
        {
            var handler = new System.Net.Http.HttpClientHandler();

            // If you are using .NET Core 3.0+ you can replace `~DecompressionMethods.None` to `DecompressionMethods.All`
            handler.AutomaticDecompression = ~DecompressionMethods.None;

            
            using (var httpClient = new System.Net.Http.HttpClient(handler))
            {
                using (var request = new System.Net.Http.HttpRequestMessage(new System.Net.Http.HttpMethod("GET"), $"https://www.samsclub.com/api/node/vivaldi/browse/v2/products/search?sourceType=1&limit=48&clubId=8166&searchTerm={productSku}"))
                {
                    request.Headers.TryAddWithoutValidation("authority", "www.samsclub.com");
                    request.Headers.TryAddWithoutValidation("accept", "application/json, text/plain, */*");
                    request.Headers.TryAddWithoutValidation("accept-language", "en-US,en;q=0.9");
                    request.Headers.TryAddWithoutValidation("cookie", "vtc=YVJMiYTbwLQKchyGDWclAg; s_ecid=MCMID%7C03844319906081412823575629667706906814; _pxvid=3d3c4a68-9566-11ec-b3fa-574555494f78; _gcl_au=1.1.1842197577.1645702690; _br_uid_2=uid%3D8186448391771%3Av%3D13.0%3Ats%3D1645702692877%3Ahc%3D1; stc116508=tsa:1645702693047.404180905.39682055.5837387162160848.:20220224120813|env:1%7C20220327113813%7C20220224120813%7C1%7C1060345:20230224113813|uid:1645702693046.1833316587.1078982.116508.101863854.:20230224113813|srchist:1060345%3A1%3A20220327113813:20230224113813; _uetvid=3fb61f50956611ecaeb25b5dcfa0bd97; _fbp=fb.1.1645702693614.1113207073; _mibhv=anon-1643463583228-194545123_4591; cto_bundle=q9p5E19pWDJ6MnJ4SkNFM3UwWkRmWGxTSTNFS1AyS2Q4TyUyRkxiU3R0U0hlbm1QaUdRYnpFekRIZ0YzdmlNbkNwSGtBS0lyVThydXRrbERLbjNGZ0xFZThMdkRTZFF4JTJGS2VLRnA3eGtKbUlmOHhucHV0cXQyUHdzdGd4ejZvTnZzNk5BeWRMbllOaWN6cDNXZWhTcEdvbTd3cFZBSyUyRnVhc1l0dGtFVG9pZ1BNRmFmT2NzejljJTJGaEdCRjdXNFV5RFdTQTVnTg; myPreferredClub=8166; myPreferredClubName=Duluth%2C+GA; sod=20220224_93803f64-4db5-437d-8532-9aad2f81c8d3; __gads=ID=15675c6439406d7b:T=1645703282:S=ALNI_MYmCPslI9sr3LjE17E22I4iS4X_OQ; SSLB=1; salsify_session_id=00ec9dda-ee12-4407-95ff-dff80d18ff8b; BVBRANDID=70e218b6-9f25-4a52-a2fe-1ba1a3283ccb; astracxo=pc-9c1d3911-4157-42bd-abb0-bddc7dc180be; QuantumMetricUserID=3e582d2c52c5c2decd26023ba4ea13c6; sxp-rl-SAT_ENABLE_INSTOCK_ALERTS-rn=38; sxp-rl-SAT_WMSP_P13N-rn=83; sxp-rl-SAT_PARITY_NEPSAT-rn=64; sxp-rl-SAT_ADA_CLUB_ORDERS-rn=4; sxp-rl-SAT_DFC2-rn=29; sxp-rl-SAT_SKIP_ATG_LOGOUT-rn=49; az-reg=wus; sxp-rl-SAT_WMSP_SBA-rn=65; sxp-rl-SAT_WMSP_CAT-rn=94; sxp-rl-SAT_SORS_RET-rn=84; sxp-rl-SAT_LIST_MOVE_ITEM-rn=59; sxp-rl-SAT_OD_PP-rn=9; sxp-rl-SAT_WMSP_INGRID-rn=3; AMCVS_B98A1CFE53309C340A490D45%40AdobeOrg=1; s_cc=true; SAT_LIST_MOVE_ITEM=0; SAT_SORS_RET=1; sxp-rl-SAT_WMSP_P13N-c=r|1|35; sxp-rl-SAT_SKIP_ATG_LOGOUT-c=r|1|100; SAT_ENABLE_INSTOCK_ALERTS=0; SAT_ADA_CLUB_ORDERS=1; sxp-rl-SAT_DFC2-c=r|1|100; sxp-rl-SAT_SORS_RET-c=r|1|100; sxp-rl-SAT_OD_PP-c=r|1|100; SAT_DFC2=1; SAT_WMSP_SBA=0; sxp-rl-SAT_WMSP_SBA-c=r|1|35; sxp-rl-SAT_LIST_MOVE_ITEM-c=r|1|10; sxp-rl-SAT_ENABLE_INSTOCK_ALERTS-c=r|1|10; SAT_WMSP_P13N=0; sxp-rl-SAT_PARITY_NEPSAT-c=r|1|20; SAT_SKIP_ATG_LOGOUT=1; SAT_OD_PP=1; SAT_PARITY_NEPSAT=0; sxp-rl-SAT_ADA_CLUB_ORDERS-c=r|1|100; SAT_WMSP_INGRID=1; sxp-rl-SAT_WMSP_CAT-c=r|1|35; SAT_WMSP_CAT=0; sxp-rl-SAT_WMSP_INGRID-c=r|1|35; pxcts=678f416f-bbf6-11ec-9627-55624c615762; memExpired=n; usernameCookie=9019515fbce20dba7e1dfebb3acdad90; SAT_PLP_RATING=1; SAT_ORDER_BY_X=0; SAT_FULLWIDTH_DYF=1; SSSC1=362.G7069491258615440757.33|66788.2281084:67409.2293457:67557.2296617:67561.2296690; bstc=fqpVBm2QuT-FZY1Zdp8KZY; AMCV_B98A1CFE53309C340A490D45%40AdobeOrg=1585540135%7CMCIDTS%7C19102%7CMCMID%7C03844319906081412823575629667706906814%7CMCAAMLH-1650970775%7C3%7CMCAAMB-1650970775%7CRKhpRz8krg2tLO6pguXWp5olkAcUniQYPHaMWWgdJ3xzPWQmdj0y%7CMCOPTOUT-1650373175s%7CNONE%7CMCAID%7CNONE%7CvVersion%7C4.4.0; rcs=eF4FwbsNgEAIANDmKnch4Y9s4BonYmJhp87ve2O8tUly8WoMEcmgZytE0QQNR5zYO3cv9_dch1OIA7mhuGUYOoMQmKbFD7stEhE; bm_mi=67EDA00522D76A12802FA66A4EF07ED9~1KjeAo2/Ib7hk/eS+T7Np5KT4dJqMsXhH68Mm0SXMbdtNx6ehkaUt1DMPdUxcBn66cpLdjVHszqYw7hTihhl//TQd7UGUJ3o7XqNdmqxHJq+7i2hq6qlOz1zSAPT8xAwqJPRxFT4l7czaXbQ/zzTxhRy6SrTl8a5nlGCsNjPyzQxj04WiACqZxODEucwC/lZ5yTspUUmVUiYr7VJCdQM2NvGgwlad7S2dNm9Qui49Xe261D1y3IaghvGtVMmMuLlPPy8nA0Y4zMHFsY3Bnr/0uIN0hEwtRTqj45Fr4D/7UJexwHFwf6v4TIcdC040eRM5rKc39RChcR73bBwX24/xQ==; ak_bmsc=F026580F495F3821934007A4A4399D32~000000000000000000000000000000~YAAQPp4QAmITPQGAAQAAaWJ6QQ9im/kdSufU8yVvZsQmBy5BB5N/oMkJnL1fxTE0ubs5iLh8pxuxV56ty5spWSNxypGAiHq3j4UP+/GP0ClC4LnA55DSROLQEJ5+rmcK4qy3jOloilciyvFAG2wmt5Hjd7xLL1vSDfTkGc76Wh+RXhI/4zFliroBaxTvSea4aMxyB9inWs5tpFBRJpeDdftM2oBU0OpO2Gw80soITmxNWpKWOT2Et14n/dRg4cjxMdbZC5J7NpMpE/48phL7Eea/O+h3+fC9d41uXX7S8bIaF0WU3SAjmf4HHT/wTPXaYUXZ7FQjw4A6bPiQ/OMJrP7A05mloEDUs/GmTO5PjL1t/sM2dKDMBanAGCVG12ut7TFK+2ocRcaU+sjDbaDAjRNKh3N/aihm58uiMhHHvAIEYfWKLO3MVr9xkRidEEoiRxIjD15WPk8bVMBYEMSqQVxCjsBoTnrkBGK+vrNVDg==; smtrsession=authEvent%7Ctrue%5EloggedMemType%7Ctrue; __gpi=UID=00000486aea2bd68:T=1649782799:RT=1650365991:S=ALNI_MblyafQ92-qqTph7oLLHG7d-H_6Tw; samsathrvi=RVI~prod21366457-prod4080002-131507-prod21061457-prod2640357-prod24373702-182959-prod23191518-prod23191526-prod24964974; BVImplmain_site=1337; BVBRANDSID=891aa9ed-ce77-4671-ab74-0acc721cd468; flixgvid=flix57bc9795000000.96400176; inptime0_859_us=0; testVersion=versionA; TS0171ed6c=01538efd7c4ed2ce9b220510f826cdfa758b7c6ecf20f78e89f6fbfbaee6284d9e817eac23d0a2b2f98e6370c3340cac2422393922; astract=ca-d514f237-f82c-41a1-a9a4-25589b114d56; acstkn=86:6#315348453#7=467630228_1650366547942; SSID1=CQA4UR04ABgAAADY4BtidaWADNjgG2IhAAAAAAAiXz9kFJZeYgBjBlEHAQPR_iIAZ0lPYgcA6QcBA3ILIwB2Kl5iAgDkBAEBfM4iABSWXmIBAOUHAQMpCyMA2SBYYgUAAgIBAEEDAQDQAwEAYgMBAM4DAQDzBgEA; smtrrmkr=637859633549806619%5E017f3ce6-67c4-45a3-b6ad-1d8efa04c329%5E0180417a-65e1-4502-a93f-a9d9a4b3964d%5E0%5E175.107.237.107; s_sq=samclub3prod%3D%2526c.%2526a.%2526activitymap.%2526page%253Dhomepage%2526link%253DSearch%2526region%253Dpage-top%2526pageIDType%253D1%2526.activitymap%2526.a%2526.c%2526pid%253Dhomepage%2526pidt%253D1%2526oid%253DfunctionUr%252528%252529%25257B%25257D%2526oidt%253D2%2526ot%253DLABEL; seqnum=19; _px3=00064285e980533cc2aa9c237ec2ae763777c844e30fb47b353c581f5a5ea92f:ycxe0vLHVDe6TsJFyMJB5zIGQD9XqVo4TUw19/uk3fXspa7Lv79Xuc/RDSaf6uBRPEHWTy3V8byZM7q2bz3u5A==:1000:8C39byacAhDABaUj4+gKpvKYzzqlmVB09kPsMDnhpl3/iooxQBjmI5E0ZzlE96wkkD3XhC96ddtCKm8ca1wSp2z1f57VXaLNUdVZyL010c02/H8AjIplHmkTRpq00tOKHiNr1gCnfOlOSdP2EW6pOyxCDTXvhfqGR4KVPxQ6r9aURkuiSwzU24EfICgHgsqrOTebBUHXhVpd2LsTbMu5zw==; _pxde=347a1079aef301b91695e5ffeb62198cec9521305c5188aa891be7ad19f69f21:eyJ0aW1lc3RhbXAiOjE2NTAzNjY3OTE0MzUsImZfa2IiOjAsImlwY19pZCI6W119; SSRT1=qpleYgIDAA; akavpau_P1_Sitewide=1650367491~id=7fadd1f55fb6fa1d06ed93444ce941c6; TS01bc32b7=0130aff23248baceff2a4ddb3e62cf388bf98ad51e50c8114684ba1ca3a172f4a02794056320002f978e5c22d1b0a944c6c122e45a; bm_sv=69773064E0FCA214914C77254A76306B~9+hbDjT5ED0+p4uTHPVMPUHhq9lFn3ymRD77d2/kedvTXKOYznLmNiQqxsEP0S4m4C+3yqsmkjQfoc9rpX6v5r+rpFSvoOObnz5+n7j2pEQPTCf8DGAXC+G3emTz86jGc+m2waW6AiZ59ynfFePPJJf1nTeXoUlYuCZzB8ZurCM=");
                    request.Headers.TryAddWithoutValidation("referer", "https://www.samsclub.com/s/"+productSku);
                    request.Headers.TryAddWithoutValidation("sec-ch-ua", "\" Not A;Brand\";v=\"99\", \"Chromium\";v=\"100\", \"Google Chrome\";v=\"100\"");
                    request.Headers.TryAddWithoutValidation("sec-ch-ua-mobile", "?0");
                    request.Headers.TryAddWithoutValidation("sec-ch-ua-platform", "\"macOS\"");
                    request.Headers.TryAddWithoutValidation("sec-fetch-dest", "empty");
                    request.Headers.TryAddWithoutValidation("sec-fetch-mode", "cors");
                    request.Headers.TryAddWithoutValidation("sec-fetch-site", "same-origin");
                    request.Headers.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.88 Safari/537.36");
                    request.Headers.TryAddWithoutValidation("visitorid", "YVJMiYTbwLQKchyGDWclAg");
                    request.Headers.TryAddWithoutValidation("withdeliveryoffer", "true");

                    var response = await httpClient.SendAsync(request);
                    var contents = await response.Content.ReadAsStringAsync();

                    return contents;
                }
            }
            return "";
        }
        public static async Task<HttpRequester.HttpRequester> GetSimpleRequestAsync(HttpMethod httpMethod, string Url)
        {

            var request = new HttpRequester.HttpRequester()
            {

                Method = HttpMethod.GET,
                Url = "https://samsclub.com"
            };
            var rq = await HttpRequester.HttpHandler.Request(request);

            var ck = new CookieContainer();
            ck.Add(rq.CookieContainer);
            request = new HttpRequester.HttpRequester()
            {
                Method = httpMethod,
                Url = Url,
                IsAuthorized = false,
                CookieContainer = ck,

                Params = new System.Collections.Generic.List<HeaderParams>()
                {

                   HeaderParams.GetHeaderParam(HeaderType.contenttype,ContentType),

                    
                    new HeaderParams()
                    {
                     Key = "user-agent",
                     Value = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.88 Safari/537.36"
                    },
    
                   HeaderParams.GetHeaderParam(HeaderType.accept,Accept),
                   HeaderParams.GetHeaderParam(HeaderType.acceptlanguage,AcceptLanguage),
                },
            };


            return request;
        }

        public static HttpRequester.HttpRequester GetAuthorizedRequest(HttpMethod httpMethod, string Url, bool freshToken = false)
        {

            var request = new HttpRequester.HttpRequester()
            {
                Method = httpMethod,
                Url = Url,
                IsAuthorized = false,
                Params = new System.Collections.Generic.List<HeaderParams>()
                {
                    HeaderParams.GetHeaderParam(HeaderType.authorization,"eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IjQ5NTYzNUQ3N0NGOUM4NDc3RDJDNEUyODA5RThGMTYwMzc0RENFQ0UiLCJ4NXQiOiJTVlkxMTN6NXlFZDlMRTRvQ2VqeFlEZE56czQifQ.eyJpc3MiOiJodHRwczovL3RpdGFuLnNhbXNjbHViLmNvbS9jOGNjMDcwZS1kZmVkLTQ1YzktYmRhNS0yOWQwMDEyMWFjYmIvdjIuMC8iLCJleHAiOjE2Nzc3NzM2OTcsIm5iZiI6MTY3Nzc3MTg5NywiYXVkIjoiMzc2OTdmMjUtYjcyYy00NWRmLWJlNTEtNTU4Zjg4MjRmYzY0IiwidW5xIjoiQjJDIiwic2wiOiJZIiwiZmx0IjoiMTY3Nzc0MTU3OCIsImp0aSI6IjA5MTlhM2EwLWQ5NDItNDcwOC05YWM4LWMwYTdmNjg2MzQ1MiIsImNoaSI6IndlYiIsIm1pIjoiZDc2ZTNmMGE3ZDEzNjU2ZjIwYzQ0MmM4MGEyNjEyODk3MjQ4MDQ0NDg2YTRiYzRkNDVlZDEwNjNiODdhZDk4NCIsInN1YiI6ImJkODJmZGRiLTBmMDEtNDBhZS1iNzNlLWQ2ZGYxOWRlN2QxOSIsImlzc3VlclVzZXJJZCI6IjBmODY3M2U5ZmY4MjBmMjYzZmVmZDE4NGM4ZDhjMzQ3ZjYzZDg3ZWUyZjUyNTMyOTdkZmJmYjViMTAxZGZhZjQiLCJlaWQiOiIiLCJwcmkiOiIiLCJ1Zm4iOiJUaWFyYSIsIm10IjoiQiIsIm1zIjoiQSIsIm1leCI6MTcwMzM3NTk5OSwiZW1jIjpmYWxzZSwicGlkIjoiMTk5NjNjMGY3NTI4MjBmMzJjMjYxYjMyYWQ2MzIzYWYzMmY2ODJiNzAxMTc0Y2Y2MmQwZDRiYTdkOWFhOTkzNDI5N2I5NWIxYjNiMTUwYmQ1NmZhYWNkNmExZjZhYTIwIiwibXV1aWQiOiI0OTE4NjFjN2Y0YmZiZDFmYTA3YjVkNzM5NjhiNmFjZjg3MjRjMGFhY2U5YmQ0NTU2NzQ1ZDNhN2M4YzVkNjJmMDFmODAyYmU0NjljMDcyNmY3MDVhYzcyNDc3MGJmZDEiLCJjc2NwIjoic2MubnMuYSBzYy5zLnIiLCJyZiI6IlkiLCJwYyI6IjgxNjYiLCJidnQiOiIxN2VlMzQ0YWI1ZGFiZGVhNTFjMTlkNGMyODljYzAzNDY0NjE3NDY1M2QzMjMwMzIzMzMwMzMzMDMyMjY2NTZkNjE2OTZjNjE2NDY0NzI2NTczNzMzZDZmNzI2NDY1NzI3MzI1MzQzMDczNjg2ZjcwNjk2ZDZmMmU2MzZmMjY3NTczNjU3MjY5NjQzZDM0MzkzNDMwMzIzMDYyNjYyZDMyMzQzOTYxMmQzNDYzNjUzNDJkNjI2NDMxNjMyZDY2MzQzNjY0NjMzMTMwNjQzMjYyMzM2MSIsImNodCI6IlMiLCJ0cGNuIjoic2Ftcy1zcGEiLCJibSI6IlkiLCJzaWMiOiI1MDk5IiwidGZwIjoiQjJDXzFBX1NpZ25JbldlYldpdGhLbXNpIiwibm9uY2UiOiJkZWZhdWx0Tm9uY2UiLCJzY3AiOiJzYy5zLnIgc2Mucy5hIHNjLm5zLmEiLCJhenAiOiJjZGMxYmJhMi1mMWEyLTQ3MmQtOGU0ZS1mMDZiMjFmM2Q4MWEiLCJ2ZXIiOiIxLjAiLCJpYXQiOjE2Nzc3NzE4OTd9.LZadT9Hfe2h0grkSIe9QB70NCHwvZ8X_sD5WJ36HLdc3Cye0UG4gST4SUlzyENtEm12Cfw1WN4RgqqSeTynFIKHFT1-_KvSlTtBzWWUTJ6hm5Aanj_t9lGQjWP-02OPWXFSD2LN4AhzXCPDNXQAmW-Uz3FoGpetkcPkzsQKJYZy8ELFY2Qff14gXF33JeZFbWThrwP3uamGFGEEm8ktGWAbpp2AS8h-89brLQLOLFmQk5hRSCaC234yv1e7Ec0l45XtMpzStcw0aOsS2YB8QIgbgIv_Bm-PVWLD3lVU1oGci8dEgdQ65O6PfY1yno3lsRIPW0Dx8-2k_PLQXX_Ms-g5QwHFyZvGe3mCNc-lF0vx6_ZOLs9SrI96fQDGaFwYe26aCE4vTmn5Zd-s_W8Wc2bQ7sPHxVmUBLcbWgH5lCgh6RRic3bFyDJ-BRIdprG8h8vJzhCcszWF97BZuCl3VJAuZ3nzfq_q8RlXCiszzzSE_TbS09IdvjCMArKSOrheUBkyGAgC2SlSWTw-a44ITYwcDcooOFPUDmGibTyw5IasFXhgyXKqAYqodNgyI_AZSly-bi_ujC2IhZ-o9-wnrULsVtIyZs9UtvueBpf3lMH2thJTpwoLMH4tZa13ubjmZ5TuNyMDjeW0dRtRPCkia-p9E1l4LtKUdiOYVCJ96pG0"),
                       HeaderParams.GetHeaderParam(HeaderType.contenttype,ContentType),

                   HeaderParams.GetHeaderParam(HeaderType.useragent,UserAgent),
                   HeaderParams.GetHeaderParam(HeaderType.accept,Accept),
                   HeaderParams.GetHeaderParam(HeaderType.acceptlanguage,AcceptLanguage),
                },
            };
            var samsaccount = new SamsAccontDto()
            {
                Token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IjQ5NTYzNUQ3N0NGOUM4NDc3RDJDNEUyODA5RThGMTYwMzc0RENFQ0UiLCJ4NXQiOiJTVlkxMTN6NXlFZDlMRTRvQ2VqeFlEZE56czQifQ.eyJpc3MiOiJodHRwczovL3RpdGFuLnNhbXNjbHViLmNvbS9jOGNjMDcwZS1kZmVkLTQ1YzktYmRhNS0yOWQwMDEyMWFjYmIvdjIuMC8iLCJleHAiOjE2Nzc3NzM2OTcsIm5iZiI6MTY3Nzc3MTg5NywiYXVkIjoiMzc2OTdmMjUtYjcyYy00NWRmLWJlNTEtNTU4Zjg4MjRmYzY0IiwidW5xIjoiQjJDIiwic2wiOiJZIiwiZmx0IjoiMTY3Nzc0MTU3OCIsImp0aSI6IjA5MTlhM2EwLWQ5NDItNDcwOC05YWM4LWMwYTdmNjg2MzQ1MiIsImNoaSI6IndlYiIsIm1pIjoiZDc2ZTNmMGE3ZDEzNjU2ZjIwYzQ0MmM4MGEyNjEyODk3MjQ4MDQ0NDg2YTRiYzRkNDVlZDEwNjNiODdhZDk4NCIsInN1YiI6ImJkODJmZGRiLTBmMDEtNDBhZS1iNzNlLWQ2ZGYxOWRlN2QxOSIsImlzc3VlclVzZXJJZCI6IjBmODY3M2U5ZmY4MjBmMjYzZmVmZDE4NGM4ZDhjMzQ3ZjYzZDg3ZWUyZjUyNTMyOTdkZmJmYjViMTAxZGZhZjQiLCJlaWQiOiIiLCJwcmkiOiIiLCJ1Zm4iOiJUaWFyYSIsIm10IjoiQiIsIm1zIjoiQSIsIm1leCI6MTcwMzM3NTk5OSwiZW1jIjpmYWxzZSwicGlkIjoiMTk5NjNjMGY3NTI4MjBmMzJjMjYxYjMyYWQ2MzIzYWYzMmY2ODJiNzAxMTc0Y2Y2MmQwZDRiYTdkOWFhOTkzNDI5N2I5NWIxYjNiMTUwYmQ1NmZhYWNkNmExZjZhYTIwIiwibXV1aWQiOiI0OTE4NjFjN2Y0YmZiZDFmYTA3YjVkNzM5NjhiNmFjZjg3MjRjMGFhY2U5YmQ0NTU2NzQ1ZDNhN2M4YzVkNjJmMDFmODAyYmU0NjljMDcyNmY3MDVhYzcyNDc3MGJmZDEiLCJjc2NwIjoic2MubnMuYSBzYy5zLnIiLCJyZiI6IlkiLCJwYyI6IjgxNjYiLCJidnQiOiIxN2VlMzQ0YWI1ZGFiZGVhNTFjMTlkNGMyODljYzAzNDY0NjE3NDY1M2QzMjMwMzIzMzMwMzMzMDMyMjY2NTZkNjE2OTZjNjE2NDY0NzI2NTczNzMzZDZmNzI2NDY1NzI3MzI1MzQzMDczNjg2ZjcwNjk2ZDZmMmU2MzZmMjY3NTczNjU3MjY5NjQzZDM0MzkzNDMwMzIzMDYyNjYyZDMyMzQzOTYxMmQzNDYzNjUzNDJkNjI2NDMxNjMyZDY2MzQzNjY0NjMzMTMwNjQzMjYyMzM2MSIsImNodCI6IlMiLCJ0cGNuIjoic2Ftcy1zcGEiLCJibSI6IlkiLCJzaWMiOiI1MDk5IiwidGZwIjoiQjJDXzFBX1NpZ25JbldlYldpdGhLbXNpIiwibm9uY2UiOiJkZWZhdWx0Tm9uY2UiLCJzY3AiOiJzYy5zLnIgc2Mucy5hIHNjLm5zLmEiLCJhenAiOiJjZGMxYmJhMi1mMWEyLTQ3MmQtOGU0ZS1mMDZiMjFmM2Q4MWEiLCJ2ZXIiOiIxLjAiLCJpYXQiOjE2Nzc3NzE4OTd9.LZadT9Hfe2h0grkSIe9QB70NCHwvZ8X_sD5WJ36HLdc3Cye0UG4gST4SUlzyENtEm12Cfw1WN4RgqqSeTynFIKHFT1-_KvSlTtBzWWUTJ6hm5Aanj_t9lGQjWP-02OPWXFSD2LN4AhzXCPDNXQAmW-Uz3FoGpetkcPkzsQKJYZy8ELFY2Qff14gXF33JeZFbWThrwP3uamGFGEEm8ktGWAbpp2AS8h-89brLQLOLFmQk5hRSCaC234yv1e7Ec0l45XtMpzStcw0aOsS2YB8QIgbgIv_Bm-PVWLD3lVU1oGci8dEgdQ65O6PfY1yno3lsRIPW0Dx8-2k_PLQXX_Ms-g5QwHFyZvGe3mCNc-lF0vx6_ZOLs9SrI96fQDGaFwYe26aCE4vTmn5Zd-s_W8Wc2bQ7sPHxVmUBLcbWgH5lCgh6RRic3bFyDJ-BRIdprG8h8vJzhCcszWF97BZuCl3VJAuZ3nzfq_q8RlXCiszzzSE_TbS09IdvjCMArKSOrheUBkyGAgC2SlSWTw-a44ITYwcDcooOFPUDmGibTyw5IasFXhgyXKqAYqodNgyI_AZSly-bi_ujC2IhZ-o9-wnrULsVtIyZs9UtvueBpf3lMH2thJTpwoLMH4tZa13ubjmZ5TuNyMDjeW0dRtRPCkia-p9E1l4LtKUdiOYVCJ96pG0",
                
                
                
                Cookie = "vtc=afMMl8aPCeaRcbs1ofhsmQ; _gcl_au=1.1.1856935416.1674556813; _pxvid=7b2e31f3-9bd3-11ed-ad8f-4b554359546f; __pxvid=7bbb074a-9bd3-11ed-a0d8-0242ac120002; s_ecid=MCMID%7C42687557492704258100021573272982009037; QuantumMetricUserID=f48944b48d94dc4cc549b098d4f39fdd; __gads=ID=e2d66ba97ebcc8a4:T=1674556815:S=ALNI_MYmvYex04xNXoHrmPmPNVdzE26UDw; _mibhv=anon-1674556818895-4701752193_4591; _fbp=fb.1.1674556819530.1114143490; _pin_unauth=dWlkPU1qRTBOalUwWXpVdE1qRTVaaTAwWmpFeUxUaGhNVGN0WTJWaU9UWTRNVFU0TkdReg; salsify_session_id=776c48ec-637a-472e-b820-68b7614d98cc; BVBRANDID=d4c00314-d45c-4602-b816-5d22d3f9ff30; _br_uid_2=uid%3D3571142747270%3Av%3D13.0%3Ats%3D1674556818724%3Ahc%3D2; _tt_enable_cookie=1; _ttp=_vL8jVHKOXggqpZQ6ERqSMe6f1F; _uetvid=7de4b5209bd311edbc7b593114cd2bf3; cto_bundle=3tHUCV9yelJjdmtFVGkyMkxRUmZiTTNORW1WQzRUSUQlMkZ5UEJGZ1A0TmJFRThEMXlIZVg4WHN0aFkwZGJHTnEzbFZuSlFsUllFWExaNjBRVUYlMkYzT0gxNm9MNGpRM2tlJTJGMEdNNGh3MjJZdTlSaWZzS3hkWVFEVkMxOFVSOHlrczJFRXJVNXN4Z2h6bFlhRkg4cThaUEYwNll6JTJCQSUzRCUzRA; sod=20230126_d1778ceb-b344-41af-9c79-44d2bd8cf32b; SSLB=1; astracxo=pc-f18f47c1-13bc-4e90-b4d9-3e6a7c721803; myPreferredClubName=Duluth%2C+GA; myPreferredClub=8166; samsathrvi=RVI~prod14220240-prod24544046-prod25430027-P03003095-prod23132701; pilotusercookie=memId%3Ad76e3f0a7d13656f20c442c80a2612897248044486a4bc4d45ed1063b87ad984%7Cplus%3AN; memType=business; bvUserToken=17ee344ab5dabdea51c19d4c289cc034646174653d323032333033303226656d61696c616464726573733d6f726465727325343073686f70696d6f2e636f267573657269643d34393430323062662d323439612d346365342d626431632d663436646331306432623361; firstNameCookie=Tiara; SAT_SFL_SUBSTITUTIONS=1; SSID1=CQB6oh0OAAgAAACFtc9jcwRBEIW1z2MMAAAAAACXeeFld8QAZABjBqAcAQMTqiQA60QAZAIAwxsBAEAcAQA; SSSC1=362.G7192166713041945715.12|72864.2402835; SSRT1=d8QAZAADAA; sxp-rl-SAT_CART_SUBSTITUTIONS-rn=63; sxp-rl-SAT_REORDER_V3-rn=26; bstc=a7XHKTfCTemWTG8ElwYTzQ; sxp-rl-SAT_WSS01-rn=18; az-reg=scus; sxp-rl-SAT_GEO_LOC-rn=57; TS01bc32b7=01538efd7c5b30b217c12708a9fd1994b013d4501ba93497d7fc07f21753f2b5dc76e3092f948677ed2bf19378cb0eee4c84b2209b; sxp-rl-SAT_CHAT-rn=4; SAT_WPWCNP=0; TS0171ed6c=01538efd7c5b30b217c12708a9fd1994b013d4501ba93497d7fc07f21753f2b5dc76e3092f948677ed2bf19378cb0eee4c84b2209b; sxp-rl-SAT_OD_SURVEY-rn=5; sxp-rl-SAT_DISABLE_SYN_PREQUALIFY-rn=35; bm_mi=8F4748F83EF1FA2AB83A258A91433D61~YAAQRp4QAqWG+pmGAQAAknv/ohLrnbOBHnZHW3rZgBXtV/qJBpBgtV+CMWStMCg/E6D+NTGeUQqnXa5OgWNx90Ud4CC6tYYEHyOqSWKg/9aoPl3KGDviFZxs5SVDT1i0t8duupoSut7hkbGve2OhJtLFPZtp7EazEVlhAq2njGbA1tPb1A+R3R7AZ+BSiiPuJh9k3KQcQWLDvliMZGKks3NGxxTE42CXMojt5JoeK3dehBPOYIy52U6KGLvvVYLa8Mb/VUrk1vjwAPFc6dYZsuZHhHXkzARZ0hoa373Hxavr8QrIqBIAN34Sz5zUCNIla0an7WaqytdIM3kqRCblIYqoPLYKKGWAYsvRbiSrZww83QS8MnGJkOll7bUYTGc=~1; authToken=eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IjQ5NTYzNUQ3N0NGOUM4NDc3RDJDNEUyODA5RThGMTYwMzc0RENFQ0UiLCJ4NXQiOiJTVlkxMTN6NXlFZDlMRTRvQ2VqeFlEZE56czQifQ.eyJpc3MiOiJodHRwczovL3RpdGFuLnNhbXNjbHViLmNvbS9jOGNjMDcwZS1kZmVkLTQ1YzktYmRhNS0yOWQwMDEyMWFjYmIvdjIuMC8iLCJleHAiOjE2Nzc3NzM2OTcsIm5iZiI6MTY3Nzc3MTg5NywiYXVkIjoiMzc2OTdmMjUtYjcyYy00NWRmLWJlNTEtNTU4Zjg4MjRmYzY0IiwidW5xIjoiQjJDIiwic2wiOiJZIiwiZmx0IjoiMTY3Nzc0MTU3OCIsImp0aSI6IjA5MTlhM2EwLWQ5NDItNDcwOC05YWM4LWMwYTdmNjg2MzQ1MiIsImNoaSI6IndlYiIsIm1pIjoiZDc2ZTNmMGE3ZDEzNjU2ZjIwYzQ0MmM4MGEyNjEyODk3MjQ4MDQ0NDg2YTRiYzRkNDVlZDEwNjNiODdhZDk4NCIsInN1YiI6ImJkODJmZGRiLTBmMDEtNDBhZS1iNzNlLWQ2ZGYxOWRlN2QxOSIsImlzc3VlclVzZXJJZCI6IjBmODY3M2U5ZmY4MjBmMjYzZmVmZDE4NGM4ZDhjMzQ3ZjYzZDg3ZWUyZjUyNTMyOTdkZmJmYjViMTAxZGZhZjQiLCJlaWQiOiIiLCJwcmkiOiIiLCJ1Zm4iOiJUaWFyYSIsIm10IjoiQiIsIm1zIjoiQSIsIm1leCI6MTcwMzM3NTk5OSwiZW1jIjpmYWxzZSwicGlkIjoiMTk5NjNjMGY3NTI4MjBmMzJjMjYxYjMyYWQ2MzIzYWYzMmY2ODJiNzAxMTc0Y2Y2MmQwZDRiYTdkOWFhOTkzNDI5N2I5NWIxYjNiMTUwYmQ1NmZhYWNkNmExZjZhYTIwIiwibXV1aWQiOiI0OTE4NjFjN2Y0YmZiZDFmYTA3YjVkNzM5NjhiNmFjZjg3MjRjMGFhY2U5YmQ0NTU2NzQ1ZDNhN2M4YzVkNjJmMDFmODAyYmU0NjljMDcyNmY3MDVhYzcyNDc3MGJmZDEiLCJjc2NwIjoic2MubnMuYSBzYy5zLnIiLCJyZiI6IlkiLCJwYyI6IjgxNjYiLCJidnQiOiIxN2VlMzQ0YWI1ZGFiZGVhNTFjMTlkNGMyODljYzAzNDY0NjE3NDY1M2QzMjMwMzIzMzMwMzMzMDMyMjY2NTZkNjE2OTZjNjE2NDY0NzI2NTczNzMzZDZmNzI2NDY1NzI3MzI1MzQzMDczNjg2ZjcwNjk2ZDZmMmU2MzZmMjY3NTczNjU3MjY5NjQzZDM0MzkzNDMwMzIzMDYyNjYyZDMyMzQzOTYxMmQzNDYzNjUzNDJkNjI2NDMxNjMyZDY2MzQzNjY0NjMzMTMwNjQzMjYyMzM2MSIsImNodCI6IlMiLCJ0cGNuIjoic2Ftcy1zcGEiLCJibSI6IlkiLCJzaWMiOiI1MDk5IiwidGZwIjoiQjJDXzFBX1NpZ25JbldlYldpdGhLbXNpIiwibm9uY2UiOiJkZWZhdWx0Tm9uY2UiLCJzY3AiOiJzYy5zLnIgc2Mucy5hIHNjLm5zLmEiLCJhenAiOiJjZGMxYmJhMi1mMWEyLTQ3MmQtOGU0ZS1mMDZiMjFmM2Q4MWEiLCJ2ZXIiOiIxLjAiLCJpYXQiOjE2Nzc3NzE4OTd9.LZadT9Hfe2h0grkSIe9QB70NCHwvZ8X_sD5WJ36HLdc3Cye0UG4gST4SUlzyENtEm12Cfw1WN4RgqqSeTynFIKHFT1-_KvSlTtBzWWUTJ6hm5Aanj_t9lGQjWP-02OPWXFSD2LN4AhzXCPDNXQAmW-Uz3FoGpetkcPkzsQKJYZy8ELFY2Qff14gXF33JeZFbWThrwP3uamGFGEEm8ktGWAbpp2AS8h-89brLQLOLFmQk5hRSCaC234yv1e7Ec0l45XtMpzStcw0aOsS2YB8QIgbgIv_Bm-PVWLD3lVU1oGci8dEgdQ65O6PfY1yno3lsRIPW0Dx8-2k_PLQXX_Ms-g5QwHFyZvGe3mCNc-lF0vx6_ZOLs9SrI96fQDGaFwYe26aCE4vTmn5Zd-s_W8Wc2bQ7sPHxVmUBLcbWgH5lCgh6RRic3bFyDJ-BRIdprG8h8vJzhCcszWF97BZuCl3VJAuZ3nzfq_q8RlXCiszzzSE_TbS09IdvjCMArKSOrheUBkyGAgC2SlSWTw-a44ITYwcDcooOFPUDmGibTyw5IasFXhgyXKqAYqodNgyI_AZSly-bi_ujC2IhZ-o9-wnrULsVtIyZs9UtvueBpf3lMH2thJTpwoLMH4tZa13ubjmZ5TuNyMDjeW0dRtRPCkia-p9E1l4LtKUdiOYVCJ96pG0; sxp-rl-SAT_GEO_LOC-c=r|1|50; sxp-rl-SAT_REORDER_V3-c=r|1|50; sxp-rl-SAT_OD_SURVEY-c=r|1|20; SAT_CHAT=1; sxp-rl-SAT_WSS01-c=r|1|100; sxp-rl-SAT_DISABLE_SYN_PREQUALIFY-c=r|1|0; SAT_WSS01=1; sxp-rl-SAT_CHAT-c=r|1|100; SAT_DISABLE_SYN_PREQUALIFY=0; SAT_REORDER_V3=1; SAT_CART_SUBSTITUTIONS=1; sxp-rl-SAT_CART_SUBSTITUTIONS-c=r|1|95; SAT_OD_SURVEY=1; SAT_GEO_LOC=0; TS01b2183e=01538efd7c5b30b217c12708a9fd1994b013d4501ba93497d7fc07f21753f2b5dc76e3092f948677ed2bf19378cb0eee4c84b2209b; pxcts=2fd49fb8-b911-11ed-bd8a-594a484f6d4d; _pxff_idp_c=1,s; TS4fc56a0b027=0800b316f6ab2000651fa70caa68c107ea6761f127e4d1a3b1f4f4f69fe710f7fc6b28ccd2653c18089ef1cf1311300075744dab5a4995bfe847c46f076f079905e137c6812f2c72dd92b92c05c28d2bdbc27ef6eb6f0d9ed1c1876219bb5ffb; ak_bmsc=74D20D9A958D7C3EFAF88BF843CD0E77~000000000000000000000000000000~YAAQRp4QAtyG+pmGAQAAYYL/ohLqTSVYSAmXtaY7v6I74hP8PofCts9uEB/LVxL5k5ojueY9eKX462ZapBN4fSHMzxwyjFd7soLW7dMKmRtNrVKSEf9cclUt/dOmxRFl3Qc9D2xxf1sFFJM8u6suEMnz2ZKFr9EdqrGNWsm4vr3LznFSfvWG6YWibNC31WiQZSUOnZoQOlEcHBhlIIlN1Uq3MBUynTHay9kec4zD6JD4+xRZ6XDzK6HGfpW4oOpIesbGjCa+MJxGAv2DDpocTBQ9tT/wnn+TZWU284FV+U+wBFyOKUAoYfRgoyRYRhGtF+IsLULrCg7nWHO5uyVwSZWL11U4phmUVhMblFo6ZKrdSnNks+9AoBGSHUr5oZOykFl75/fu9fYmdZa/HCHA3sH2cF6YBMaZIZ42dkuhP4nAfRJ5dBS6uv6VLQ5Ejg34bRJm7DSPROvu9NgZXxe5E6fuZsvuHZTW5MVOjLQIWvOsbAwA/qpT6fki51Az9ck/dzrMojZyozjst4XOPT8i+JPV1d5BDMhvfXwlclaGc/UVnTYxHCBOqtHW/u8kO8G7jg==; astract=ca-306abc90-c9ac-4728-8aa6-caf51425a592; acstkn=88:7#394400676#7=586541794_1677771899548; _px3=60681ebae12d556529016dc1215ea5d7b2ab360c7cc03a60afe926af79c4f21b:OrWY3uNz/hz0Q3DbFgMz9slHbApDynNANIacXzgogEyVjSKs1mwYhuzD2emtO+LpTxczOGJWtGN5lYYj72yUZQ==:1000:XUTxHhykrN+RtU3KhiQnMIbHKIK/oPIOZaxcBpFaB5/oHPSGtoJV6bAJH/nnEI9fQJuM7cP7xMGcC5c/e5p623kMztReBUOCdZOsMDmMfagRi4IFrsBWkTPEF3E3/OJCk4T6JXTsZ0RWIP5FudH44PebSgv9fXfhMakt8x7PpfMhWTMDX36nMaiBb6NQPajPs1p31qAoBZArWNVv4gnL/A==; AMCVS_B98A1CFE53309C340A490D45%40AdobeOrg=1; AMCV_B98A1CFE53309C340A490D45%40AdobeOrg=1585540135%7CMCIDTS%7C19419%7CMCMID%7C42687557492704258100021573272982009037%7CMCAAMLH-1678376704%7C3%7CMCAAMB-1678376704%7CRKhpRz8krg2tLO6pguXWp5olkAcUniQYPHaMWWgdJ3xzPWQmdj0y%7CMCOPTOUT-1677779104s%7CNONE%7CMCAID%7CNONE%7CvVersion%7C4.4.0; s_cc=true; smtrsession=loggedMemType%7Ctrue%5EauthEvent%7Ctrue; smtrrmkr=638133687062510000%5E0185e35d-464b-4074-9cd0-8e11fc9aa6e1%5E0186a2ff-8b4b-49bb-b5f6-4ca1c6b8877c%5E0%5E103.255.4.77; QuantumMetricSessionID=5479d4d8a488ddd61ae7b8428e1a150d; rcs=eF5j4cotK8lMETA0NzbTNdQ1ZClN9khNNEqztEy10DUwSzbRNUm2NNRNM7Y01U01NDNMtEhKM08xNAEAmHgOeA; __gpi=UID=00000ba88fb3451e:T=1674556815:RT=1677771905:S=ALNI_MYb-7nZwvxg41vddL71uYayJYErBg; TS4c030c5e027=08cb8c7367ab200054818088ff964a79a71ce5ebceef418df0ba1c79847434ba9145997ab82c5e7e081086bcf3113000b182b4f5786581fd9e81d235d1bd23c82ea592029843ae3c91c5a32c17b70d3c8ef29472ed148412e4e7318fd5b97f57; akavpau_P1_Sitewide=1677772508~id=07d7fe1ef190a9cf0ebf401afc593171; _pxde=c8c496fbc5484433d8472f60dc6e110aaca6db86d116532de9b798917ea1d89b:eyJ0aW1lc3RhbXAiOjE2Nzc3NzE5MTEwMzN9; seqnum=9; xptwj=js:cdd8ce03d79ddae6d4d1:wGOmjfOWsJvexZjx8Yi8q1pFWIinymUIsL3x3R1jWS7/+R17nskFPEbYifsLppBZggrp6PZY9PdjeZLHQxnfdBP154GG2JIP/T0MUkEP84ViQ8CAEACXT4qSwuOH9Bm1Sjp7bguKhMmdCym0gGS7J/GF59bK; akavpau_P2_Cart_Checkout=1677772512~id=5dd42d3197363e5f4568731d78c72705; bm_sv=873C2426BFB34DD790DCB05304B5E227~YAAQRp4QAtGH+pmGAQAAHrX/ohJ0aS7fBV23ftZvM1CPzwxnZyLIWJBy5uHw+CvpwLiq241gVyd6yEmrGMh5q7mqdgm6C3F/tjF9Y5VdfkueSJ+14volQbai3YEnRi0CmhMRfQWtZyvN7nq9Ku5cFPL1a52tjDJ6tGsEQZHllDuHycObn9A+ox7RaG4r9iMiwU5t+oTuT1eMB8DHcijAZpqcKPYVowvOPEvglF9W9U3M3EKD6BWQ+9fFEgQsvdvSI3Q=~1"


            };
            
            var token = samsaccount.Token;
            var cok = "s_ecid=MCMID%7C86981462839949121101967625115801104832; vtc=VP6MJJiwRW3uIqMOMNff_U; _pxvid=fdd3365c-bf4f-11ec-830a-646e50554853; __gads=ID=4b9afdea82004fb9:T=1650311083:S=ALNI_MZotvcwBD8PC2OUtb7vGgtYdgKlrw; _gcl_au=1.1.1572813352.1650311085; _mibhv=anon-1650311090068-2573136206_4591; _br_uid_2=uid%3D4707270012557%3Av%3D13.0%3Ats%3D1650311090153%3Ahc%3D1; _fbp=fb.1.1650311091037.1922574638; cto_bundle=jm-p-V9pMThFR1R1WWxFMndBM3BNcERvc3c0ZTlaJTJGVSUyRlVadjV0SXcxcExIdnM5bjBCazA0ZmEwOFl6OSUyRnNMMVNNUUF1MWJrem1FcElURzBWN213ZkUzOUtOakIyQTF3aWNpbzElMkZtVVhlSWljeFkyelNnJTJCQyUyQkFxWFR0NVp2b3ZqS3NHV0w2UlljUjIybWpCSGM0YmNnTUhLd3clM0QlM0Q; salsify_session_id=675b3e3b-cf5c-4d47-9713-28616e5fe18d; _uetvid=02415880bf5011ecb971392d40bb5632; sod=20220419_6743dd67-2b90-4293-9b71-d9a962e45c76; QuantumMetricUserID=dea008921ec724f8769317b8abf9d3fa; SSLB=1; bv_segment=%7B%22testId%22%3A%22test_SamsClub_05052022%22%2C%22segment%22%3A%22RRIPS_LEGACY%22%7D; NoCookie=true; astracxo=pc-ca154f6b-d970-4ac7-a9d7-800643f8b104; myPreferredClub=8166; myPreferredClubName=Duluth%2C+GA; sxp-rl-SAT_WMSP_SBA-rn=54; az-reg=scus; sxp-rl-SAT_WMSP_INGRID-rn=93; sxp-rl-SAT_SORS_RET-rn=53; sxp-rl-SAT_SORS_DFC_CPU_RET-rn=60; sxp-rl-SAT_ENABLE_INSTOCK_ALERTS-rn=41; sxp-rl-SAT_PARITY_NEPSAT-rn=43; sxp-rl-SAT_OD_PP-rn=58; sxp-rl-SAT_SKIP_ATG_LOGOUT-rn=70; sxp-rl-SAT_PASS_ENHANCEMENT-rn=77; sxp-rl-SAT_JN_PLUS_UPSELL-rn=59; sxp-rl-SAT_DFC2-rn=24; sxp-rl-SAT_GEO_LOC-rn=77; sxp-rl-SAT_WMSP_CAT-rn=28; sxp-rl-SAT_WMSP_P13N-rn=73; sxp-rl-SAT_CART_SUBSTITUTIONS-rn=74; sxp-rl-SAT_ADA_CLUB_ORDERS-rn=54; SAT_FULLWIDTH_PHASE_ONE=1; AMCVS_B98A1CFE53309C340A490D45%40AdobeOrg=1; s_cc=true; sxp-rl-SAT_SKIP_ATG_LOGOUT-c=r|1|100; SAT_SORS_RET=1; SAT_JN_PLUS_UPSELL=0; SAT_DFC2=1; sxp-rl-SAT_WMSP_SBA-c=r|1|80; SAT_WMSP_INGRID=0; sxp-rl-SAT_ENABLE_INSTOCK_ALERTS-c=r|1|10; SAT_WMSP_P13N=1; SAT_WMSP_CAT=1; sxp-rl-SAT_PARITY_NEPSAT-c=r|1|50; SAT_SKIP_ATG_LOGOUT=1; sxp-rl-SAT_OD_PP-c=r|1|100; SAT_WMSP_SBA=1; sxp-rl-SAT_DFC2-c=r|1|100; SAT_PARITY_NEPSAT=1; sxp-rl-SAT_WMSP_INGRID-c=r|1|80; SAT_GEO_LOC=0; sxp-rl-SAT_CART_SUBSTITUTIONS-c=r|1|95; sxp-rl-SAT_ADA_CLUB_ORDERS-c=r|1|100; sxp-rl-SAT_SORS_RET-c=r|1|100; sxp-rl-SAT_WMSP_P13N-c=r|1|80; SAT_OD_PP=1; SAT_SORS_DFC_CPU_RET=1; sxp-rl-SAT_SORS_DFC_CPU_RET-c=r|1|100; SAT_CART_SUBSTITUTIONS=1; sxp-rl-SAT_WMSP_CAT-c=r|1|80; SAT_ADA_CLUB_ORDERS=1; SAT_ENABLE_INSTOCK_ALERTS=0; SAT_PASS_ENHANCEMENT=0; sxp-rl-SAT_GEO_LOC-c=r|1|20; sxp-rl-SAT_JN_PLUS_UPSELL-c=r|1|0; sxp-rl-SAT_PASS_ENHANCEMENT-c=r|1|0; pxcts=4f44a230-05a2-11ed-9f85-794f71725853; __gpi=UID=00000524622a606b:T=1650311083:RT=1658043019:S=ALNI_MbmebFkW2fgbgYN_Q5S8N9_rlz7bQ; SAT_LEAVE_CHECKOUT_POPUP=1; SAT_LIST_QUICK_VIEW=0; SSSC1=362.G7088032095632673224.26|68835.2322428:68874.2323392; bstc=e0_AHIAikcnyXj_lvEsh3o; bm_mi=BC3B193EB0C20CA00C9E2EBCA436D722~YAAQT54QApXtzPuBAQAA5KCnCxCwV6/DVEVSKf1r5PLmqk+rKNuGpctyVS78N53JlE2ZoZg1b948vUSK9K8R4mD5EO3PBkqgHjXXVz/lg/X81d/BOGVeSq3IKkmmqBvQ5inwTSo/IODrsM0kzsx9Qv25/mE1YV3XT8ggJlNbFIxoLvd1hl3q4wIRxIkFnPc4Fbagyb6f3/kMhG4LxxkpH5R+75JH35f5g1RALDs1zp/31P5zVJGXsDiAjJbnD15oB0ia87mkx0BbqDNxjmkPu/JzTmTe04N2Be5aAmrN43xUWpyv3wE9u8QrJiQbEa5RJUfFkrD4lGW1lZNJ0hxDTkxJMt2TRBbUo/hvfO0immhw5VQH3gMYjPH5G78L2E0=~1; AMCV_B98A1CFE53309C340A490D45%40AdobeOrg=1585540135%7CMCIDTS%7C19191%7CMCMID%7C86981462839949121101967625115801104832%7CMCAAMLH-1658657711%7C3%7CMCAAMB-1658657711%7CRKhpRz8krg2tLO6pguXWp5olkAcUniQYPHaMWWgdJ3xzPWQmdj0y%7CMCOPTOUT-1658060111s%7CNONE%7CMCAID%7CNONE%7CvVersion%7C4.4.0; ak_bmsc=F8B745985330AA2BB12B844BD729F951~000000000000000000000000000000~YAAQT54QAsHtzPuBAQAAS6anCxCwDRc1whTDwTKuIsJqoSkET1ylrsBGhgsfmcEIXCL7NRLZQdglDAzHek472SI49qyNoMR1oaiGTlBQ8iMY831Y9XFqELC+zgs0LOddiwwUZRuA06raNYsBu2z3GmEeYYf6XDJeCkQe2lvBhdEruwjfLZp+DBTj+NIkP88kDeRpel9tdZiPu9EXi1v/CArg3px1ndOSttdyin/OMxB+dEWt3s7/I/HqDVwtIfq3dppu+BDjW1x4hg6eKYHKCLaAm2M3SWz3KvSK8m4wTc9375VGL1tS49jmmjTQGslE5aiMgzgKKm5yiASkT36gC/VcrwEyZGMJ6ggTl+oLYudVNzcxsUlwNpPy8M/gXnCCbXb67gcBWivKXY9O569TA8QQ3TmYsJ17w5ON1+5tQ+n6n3u0nlUF3l/9pOZEx6f5SehNqyOtooMMBXN0t0ML4Y7qwrdIVGPNywJBDdQfd50ygZ+Ecm3K0iyuZqs2kRQXiELgmrQHjA0HcZIlfjAAb3P7B6m4VQiyWr7u1d6oG0sgKaTiyaf75Z3sFlltFG6ZSA==; smtrsession=loggedMemType%7Ctrue%5EauthEvent%7Ctrue; authToken=eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6IkE1Qzc2OTdBQTUzNkVCMzM1M0IxODcyMzhCMjY2MzJBRTI5N0Q1NkEiLCJ4NXQiOiJwY2RwZXFVMjZ6TlRzWWNqaXlaakt1S1gxV28ifQ.eyJpc3MiOiJodHRwczovL3RpdGFuLnNhbXNjbHViLmNvbS9jOGNjMDcwZS1kZmVkLTQ1YzktYmRhNS0yOWQwMDEyMWFjYmIvdjIuMC8iLCJleHAiOjE2NTgwNTQ3MjQsIm5iZiI6MTY1ODA1MjkyNCwiYXVkIjoiMzc2OTdmMjUtYjcyYy00NWRmLWJlNTEtNTU4Zjg4MjRmYzY0IiwibWkiOiJmZWMxNmM0NDA3YTA1NzUxZWEwM2NjYWE4ZjI2MDA3YzM1MWY4MDMxMDFiM2Q2MGJiODgwYjQ5YTI5Njc4N2UzIiwic2wiOiJZIiwiZmx0IjoiMTY1ODA1MjkyNCIsImp0aSI6IjFlMDA0YWEwLWYzZDEtNGUzZC1iMDFkLTgxMzEyZWIyYWM0YSIsImNoaSI6IndlYiIsInVmbiI6Ikxpb3IiLCJtdCI6IkIiLCJtcyI6IkEiLCJtZXgiOjE2NjM1NDU1OTksImVtYyI6ZmFsc2UsInBpZCI6ImFlNTg0ZGVlYTg2OWIyYTk4NjRiYmFiYjJlNzUwZTI4NzkzODhmNGFhZDIyZDE1NDNkNjRkZWZkMjY3MmMxNjM5ZTNmZTg4NTY3MGQyNTFjODRhMjY0MmQ3MDBjOTU0NCIsIm11dWlkIjoiYzE2Nzc2NzEzYzU1OTc2NmZlOWFkZmU3NzQ0MzI3YTJkY2Q0YzUzNWU1Y2Q5Y2Y5MDk0Yzc4NzRmZDU4M2FkNjUxNjA0NTg5MjhkZWEyOTczYTBjMWU3MmJmZTQ5M2RkIiwidW5xIjoiQjJDIiwiY3NjcCI6InNjLm5zLmEgc2Mucy5yIiwicmYiOiJOIiwiaXNzdWVyVXNlcklkIjoiOTAxOTUxNWZiY2UyMGRiYTdlMWRmZWJiM2FjZGFkOTAiLCJwYyI6IjgxNjYiLCJidnQiOiIyYmU5ZjY3OTIwZTg2YTNiNzZmNTEzZGJhNDdhYjI5NjY0NjE3NDY1M2QzMjMwMzIzMjMwMzczMTM3MjY3NTczNjU3MjY5NjQzZDM2MzAzNTM5MzEzNDM1MzYzMzMyMzIiLCJjaHQiOiJTIiwidHBjbiI6InNhbXMtc3BhIiwiYm0iOiJZIiwic2ljIjoiNTMxMSIsInN1YiI6IjZkNWM5ZjBiLTM0YTQtNDA5Yy1hZWFlLThiYzM1OWYzMDJhZSIsInRmcCI6IkIyQ18xQV9TaWduSW5XZWJXaXRoS21zaSIsIm5vbmNlIjoiZGVmYXVsdE5vbmNlIiwic2NwIjoic2Mucy5yIHNjLnMuYSBzYy5ucy5hIiwiYXpwIjoiY2RjMWJiYTItZjFhMi00NzJkLThlNGUtZjA2YjIxZjNkODFhIiwidmVyIjoiMS4wIiwiaWF0IjoxNjU4MDUyOTI0fQ.BN4G9hrJWWcfqr15ApjVhmLNkwHLqTxtRxqXBB2XxNUD58QlTqZEx9AHSNplmlkrk7G9gnAYzPVHd8Xia9JSpDTHcBJsrPRSSLgBAcW9a43J8W36jleTIcENBQW-veWeE4_95uaSMsm90wnAhNa0cIy8xv96XxC3SaDF-7hMBOMzkYwtntJAJi_aJx4rinqqnEnL10KEsQbnqqCQrWUHkS6X2j3QNUzU-1Z6uqH9VtiaJcEVdG44uUpK7rp_28asZ3PYYNe3hfBZLv_O30ptfI4r8rBuSBbOS4ue44BG6Q8YX-oXOO4k6Xh-TH6isfe3pz_w7pHsnWQFQfwrXxhNLg6T71CQYHQ_DoyExSlYq_lSXg4--GEOAJdL386qfs2Aw_MUVHXmGmUbFaV8EYCd-nscPXPI-cUcuNCMYO6TKxjBQOh74nLRhH0SZUcKPTR4ojvR-l7spKrHXSm7vIzxa9Nipbt2tCaTFi_UdYkEAX-EgRrxBpYhcYv4ckQx9gN2Qyzqu4QpxZnEZ1yYZaXzELdE-DyzsEBfF2Zjumb6w3rQGQSlZ_7ACeA4BXRMSw34u6K1pYsEBmSkuqih0MSMlpbeSu_jVf8IbqT8xx7HbDIViwE9Z7AN7NUfK2PXByvdzkLXGCOtrNPwDpYhp6c5KD8zU4RlmZBWIl2_e_b14Pw; firstNameCookie=Lior; memType=business; memExpired=n; usernameCookie=9019515fbce20dba7e1dfebb3acdad90; pilotusercookie=memId%3Afec16c4407a05751ea03ccaa8f26007c351f803101b3d60bb880b49a296787e3%7Cplus%3AN; bvUserToken=2be9f67920e86a3b76f513dba47ab296646174653d3230323230373137267573657269643d3630353931343536333232; astract=ca-cd697dbf-6677-4931-b2f1-a6a0e56dbc3a; _px3=cc31869707e86f12dd57564775ccf363eba7ecfae3ae5feda1e714fb92ae9d42:oE+YA0tv8aN0pxzYdWswR0St7LPcfCiU+YIbA+TpILOrGBax070NOkgK/BWXFPcruw4mv0c0849tW1CW8zpcZQ==:1000:GSJPoX3NQLydSadv7kB5mF/zrY+bfPEYpEl8z4gsnJnkJUaL2ehayIEvpAdue6sLrSOHiDVw3DS0ZjNOq3jpmCPb24XLVy6WCCvMsu1FJuOzsO6hgUOWcIMWT7lZMGmt5flTFwb/jH0fT3vyxyPehALyEGUvabpqNMeWOB00pCUB+Re7bETqTJuN0JVPZfFd5qXSvckZ0BMsyMvnF6KMYg==; BVImplmain_site=1337; BVBRANDSID=fccb0a2f-e4f4-4d0e-ab35-af12423431bd; BVBRANDID=8f8c733a-3835-4cd6-b72e-9bb253f9dd30; samsathrvi=RVI~prod4030152-prod23651940-prod25562065-prod24964331; TS01bc32b7=01538efd7c9a999f03d7b52970e14cc790645f2f9a7ee07620dd50dde8c732f6c630a609155b56fac13b2ba3c223d975d1ef9c1f3e; s_sq=%5B%5BB%5D%5D; SSID1=CQBFUB0cADgAAACjv11iyMnAEKO_XWIaAAAAAADLBqRkK-HTYgBjBgoNAQPAcyMAH9LCYgcA4wwBA_xvIwAf0sJiBwAzBgEA5QcBAMQIAQD0BgEA6QcBAOwIAQBRBwEAxggBACQKAQAhCgEAqAoBAB8KAQAdCgEAIAsBAA; acstkn=121:6#341142392#7=506829009_1658052966572; TS0171ed6c=01538efd7c9a999f03d7b52970e14cc790645f2f9a7ee07620dd50dde8c732f6c630a609155b56fac13b2ba3c223d975d1ef9c1f3e; TS4fc56a0b027=0800b316f6ab20000f8e40a00fbb6183d9f140a0c5d4b6da4808d0bca364fb1919b66c66fb576357088722570a113000c95cd5bb482782f26ca4c8c65264cf766622574658667d27cae605838f5cfd7bc5bdc088a3319aeece860ea3ddc35ee7; rcs=eF5j4cotK8lMETA0NzbTNdQ1ZClN9jBMTUlLMjJM1DVITTTTNbFINNQ1NzFK0rU0NjOwMEo2ME80NQAAmgUOIw; seqnum=32; smtrrmkr=637936497707450000%5E01803e34-de8d-4b82-afdc-260a80c772e2%5E01820ba7-a735-4bed-9f30-c59a67798633%5E0%5E175.107.237.107; SSRT1=e-HTYgIDAA; TS4c030c5e027=08cb8c7367ab2000f0709841a88f289e9344d4f3b538ed8f5a6aadad5668d6d286cebb9c37f6827e08d127aab4113000b57ba69b4e107a06cadbc466d1433a339c9b35ccadac86f23d2776b0b6c901ded8b8a80fa3b12a2308dcb59a55098554; akavpau_P1_Sitewide=1658053588~id=7534ec451bd4b2da3094ed6aeef74c45; akavpau_P2_Cart_Checkout=1658053588~id=7534ec451bd4b2da3094ed6aeef74c45; bm_sv=CC64AF3B6DABA5122F93FA68B5EBFFC0~YAAQT54QApvzzPuBAQAA1syoCxDXpLRKfElyLrIpGm0+r+nZt1GT7258Plt8UJ+Q2pJDhg4o34G3DffFulJxFiuO20XlLTxLp0EV6xghqiBoDj6HjDPi79F+raK0EvPV3oafvaP7IgQTqttgjHlKjIdHPzDW+2AwJ7CTytBBh+w9MtuvHXHx6X5n6zFdmCT+2tmO2GmGoZhoKq5/NqlskBZ+rKWsnWWHTYNMNeqSDHHIRtdBfmI30eELKtoj61K7RdCq~1";
            cok = samsaccount.Cookie;
            var ncook = "";
            
            cok.Split(';').ToList().ForEach(e =>
            {

                if (e.Contains("authToken"))
                {
                    ncook += e.Split('=')[0] +"="+ token + ";";
                }
                else
                {
                    ncook += e + ";";
                }
            });
            request.Params.Add(new HeaderParams()
            {
                Key = "Cookie",
                Value = ncook

            });
            Cookie = ncook;
            request.CookieContainer = null;

            request.CookieContainer = null;
            
            return request;
        }

        public static async Task<List<LocationInfoDto>> GetStores()
        {
            var url = "https://www.samsclub.com/api/node/vivaldi/browse/v2/clubfinder/list?distance=100&nbrOfStores=20&singleLineAddr=30093";
            var rq = GetAuthorizedRequest(HttpMethod.GET, url);
            var resp = await InvokeRequest(rq);
            var modal = resp.Response.ParseJson<List<LocationInfoDto>>();
            return modal;

        }
        public static async Task SelectClubAsStoreId(int storeId)
        {
            await UpdateUserCart();
           
            var url = $"https://www.samsclub.com/api/node/vivaldi/account/v3/club/{storeId}";
            var req = GetAuthorizedRequest(HttpMethod.Put, url,true);
            await InvokeRequest(req);
            url = $"https://www.samsclub.com/api/node/vivaldi/cxo/v2/carts/{CartId}?rsg=MEDIUM";
            var cartDto = new CartStoreDto();
            cartDto.Payload = new PayloadCustomer();
            cartDto.Payload.CustomerId  = "d76e3f0a7d13656f20c442c80a2612897248044486a4bc4d45ed1063b87ad984";
            cartDto.Payload.PostalCode = 30093;
            cartDto.Payload.ClubId = storeId;
            cartDto.Payload.DelAddrId = "clear";
            var json = cartDto.ConvertToJson();
            SamsClubWorker.storeId = storeId;


            req = GetAuthorizedRequest(HttpMethod.Put, url, true);
            req.Body= json; 
            await InvokeRequest(req);
        }
        public static async Task<SamsWalletInfoDto> GetWalletInfoDto()
        {
            var rq = GetAuthorizedRequest(HttpMethod.GET, WalletInfo, true);
            var resp = await InvokeRequest(rq);
            var wallet = resp.Response.ParseJson<SamsWalletInfoDto>();
            return wallet;
        }
        public static async Task<SamsTrackingDetailsDto> GetSamsNewTracker(string trackerId)
        {
            var resp = GetAuthorizedRequest(HttpMethod.GET, "https://www.samsclub.com/api/node/vivaldi/v2/shipment/"+trackerId);
            var rr = await InvokeRequest(resp);
            return rr.Response.ParseJson<SamsTrackingDetailsDto>();
            return null;
        }
        public static async Task<SamsTrackingDetailsDto> GetSamsTrackingInfo(string trackingId)
        {
            var resp = GetAuthorizedRequest(HttpMethod.Post, "", true);
            var handler = new System.Net.Http.HttpClientHandler();

            // If you are using .NET Core 3.0+ you can replace `~DecompressionMethods.None` to `DecompressionMethods.All`
            handler.AutomaticDecompression = ~DecompressionMethods.All;

           
            var httpClient = new System.Net.Http. HttpClient(handler);
            
                var request = new System.Net.Http.HttpRequestMessage(new System.Net.Http.HttpMethod("GET"), "https://www.samsclub.com/api/node/vivaldi/v2/shipment/"+trackingId);
                

                    request.Headers.TryAddWithoutValidation("authority", "www.samsclub.com");
                    request.Headers.TryAddWithoutValidation("accept", "application/json, text/plain, */*");
                    request.Headers.TryAddWithoutValidation("accept-language", "en-US,en;q=0.9");
            request.Headers.Add("Cookie", "SSID1=CQAodx0AAAAAAADj-AljCWaGGOP4CWMBAAAAAAAAAAAA4_gJYwBjBg; SSLB=1; SSRT1=tPkJYwQAAA; SSSC1=362.G7136508738797987337.1|0.0; TS0171ed6c=01c5a4e2f9442284fc5d1ed9d7a10bad678401956c7bc00033df2b2c92632de48c891bc37f531ddd7fc7761b9d3c507790a54e8632; ak_bmsc=D906D3D94BD63C9DD436823C8D7CF8F7~000000000000000000000000000000~YAAQRPV0aGqaedyCAQAAtA/43hCa99HmlEJ0Nec+HYSykYQj7oyTIkfe/15oI5sCCAQqt8ChCG1gqZDQGSBdvaWtYRswRTSe6dytRUhBMGHinY6AOMi9n0FoRp1AfKpnXr1wkkaalFPhzJ2xHiUguziR0+mDOoS1Q75thhwP9R0wA/vL88o5muJr7+gT5nR/3m1/CBzNiEsKa0ACe6NIlz5D+3HqNjYQS4B+3yB86KPTYERr1GLY/uuDBNe8TC4RhKhx67hT10KJvMKnbzCitNbgSUa3DEJ7c1V1wXJIoMGMnvPbLBomtT8nU5IuD18DOq1A5lo2O2smhOg/XSarLyUpixQKhgQ431EKiQURam7kWjtMyIHg/WJax7FsP6yd46462FYLs1SL36hUOw==; astract=ca-b5f93552-f6dd-4d66-bb48-d8c63ce9d596; bm_sv=90087B00FE8FD90B0A271629A5A4EBFF~YAAQRPV0aM62edyCAQAAomL53hDyJEq/ezL+hj1/oHsGsbeTZRHkaNWNVopnVNLSQFJbMFt9ouQXyzzmlaC1V8JpUUR7Ei9aGSY4xfc8uMaW6IJlpOnkBX/ugvQpIWpUQrHugG8bFDSNIX0qv6Zv00wA97uuDDhMVPytTGaAsHncspSWaw2PpPqaAqtcn6ituwNQB+bkV4tifRJ1wrwBXN/CmGsDKpMnxx1wsKdln8dMJRwR4BwQLlNbS8t7GjhI0dk=~1; bstc=dytIypJRnPY8Adgg2T0qBk; seqnum=8; vtc=dytIypJRnPY8Adgg2T0qBk; TS01bc32b7=01c5a4e2f9442284fc5d1ed9d7a10bad678401956c7bc00033df2b2c92632de48c891bc37f531ddd7fc7761b9d3c507790a54e8632; TS4c030c5e027=08cb8c7367ab2000e29c8681e677a747d41331fd7d1ccfb83314187232fb7c57cd0ba71c95898cb7086bcd058011300080cee3e20b730980c85c86ee5b3666b48af58e3576c1bc7fe49369995fc1b9a833f50573260f56290d39c717672ed0b9; TS4fc56a0b027=0800b316f6ab20008b375b3b3ce60047bdaee2c774675d7f4577f94bc3286b3b25c7da858ecebc2708d6247113113000d780716e5fbd8c2a250128bd371c8bef9f0e6da5fe4d93ba13e68255a1943e443ab72f0223f887a2fdd21378a9b02c58; acstkn=80:6#339271640#7=504289054_1658082368804; akavpau_P1_Sitewide=1661598861~id=d736abdc1f27110526f70078091d91be");
                    
                    var response = await httpClient.SendAsync(request);
                    if (response.ReasonPhrase.Equals("OK"))
                    {
                        var sit = await response.Content.ReadAsStringAsync();
                        var tracking = SamsTrackingDetailsDto.FromJson(sit);
                        return tracking;

                    }
            
            return null;
        }
        public static async Task<SamsAddressesDetailDto> GetAddresses()
        {
            var request = GetAuthorizedRequest(HttpMethod.GET, GetAddressesUrl, true);
            var response = await InvokeRequest(request);
            if (response.Response != null)
            {
                return SamsAddressesDetailDto.FromJson(response.Response);
            }
            return null;

        }
        public static async Task<SamsAddressesResponseDto> ValidateAddress(SamsAddress samsAddress)
        {
            samsAddress.ContactPhone = samsAddress.ContactPhone == null || samsAddress.ContactPhone.IsEmpty() ? DefaultPhoneNumber : samsAddress.ContactPhone.Replace("-", "");
            samsAddress.PostalCode = samsAddress.PostalCode.Split("-")[0];
            var request = GetAuthorizedRequest(HttpMethod.Post, ValidateAddressUrl);
            request.Body = samsAddress.ConvertToJson();

            var resp = await InvokeRequest(request);

            if (resp.StatusCode.Equals("OK") && resp.Response != null)
            {
                return SamsAddressesResponseDto.FromJson(resp.Response);
            }
            else
            {
                if (resp.StatusCode.Equals("BadRequest"))
                {
                    if(samsAddress.LineOne.ToLower().Contains("apt"))
                    {
                        var add = samsAddress.LineOne.ToLower().Split("apt");
                        samsAddress.LineOne = add[0];
                        samsAddress.LineTwo = "Apt "+ add[1];
                    }
                    else
                    if(samsAddress.LineOne.Contains("#"))
                    {
                        var add = samsAddress.LineOne.ToLower().Split("#");
                        samsAddress.LineOne = add[0];
                        samsAddress.LineTwo = add[1];
                    }
                    else
                    {
                        var addressWords = samsAddress.LineOne.Split(" ");
                        var firstLine = "";
                        var lineTwo = "";
                        for (var i = 0; i < addressWords.Length; i++)
                        {
                            if (i <= 2)
                            {
                                firstLine += " " + addressWords[i];
                            }
                            else
                            {
                                lineTwo += " " + addressWords[i];
                            }
                        }
                        samsAddress.LineOne = firstLine;
                        samsAddress.LineTwo = lineTwo;
                        samsAddress.LineTwo = lineTwo.Replace("#", "");
                    }
                    
                    
                    request.Body = samsAddress.ConvertToJson();

                    resp = await InvokeRequest(request);
                }
                if (resp.StatusCode.Equals("Unauthorized"))
                {
                    var token = LayerDao.SiteMetaDAO.GetKey(Generics.SiteMetaConstants.ACCESS_TOKEN_SAMSCLUB);
                    request.CookieContainer.Add(new System.Net.Cookie()
                    {
                        Name = "authToken",
                        Value = token.VALUE,
                        Domain = ".samsclub.com"
                    });
                    Cookie = request.CookieContainer.GetCookieHeader(new System.Uri("https://www.samsclub.com"));
                    return await ValidateAddress(samsAddress);
                }
                else
                    if(resp.StatusCode.Equals("BadRequest"))
                {
                    return new SamsAddressesResponseDto()
                    {
                        Status = "Invalid"
                    };
                }
                return SamsAddressesResponseDto.FromJson(resp.Response); ;
            }
        }
        public static async Task UpdateUserCart()
        {
            var rq = GetAuthorizedRequest(HttpMethod.Post, GetLatestCart());
            var membershipINfo  = await GetMemberShipInfo();
            Thread.Sleep(5000);
            rq.Body = "{\"payload\":{\"postalCode\":\"71227\",\"clubId\":\""+membershipINfo.Payload.Membership.PreferredClub+"\"}}";
            
            
            
            //Cookie = rq.CookieContainer.GetCookieHeader(new System.Uri("https://www.samsclub.com"));
            var response = await HttpRequester.HttpHandler.Request(rq);
            if (response.Response != null)
            {
                var resp = SamsUserCartsDto.FromJson(response.Response);
                CartId = resp.Payload.Id;
            }
        }
        public static async Task<SamsMemberShipInfoDto> GetMemberShipInfo()
        {
            var request = GetAuthorizedRequest(HttpMethod.GET, MemberShipInfoUrl, true);
            var resp = await InvokeRequest(request);
            var model = resp.Response.ParseJson<SamsMemberShipInfoDto>();
            return model;
        }
        public static async Task<SamsOrderReceiverDto> GenerateContractAndPlaceOrder(string AddressId)
        {
            await UpdateUserCart();
            var contract = new AddSamsContractDto()
            {
                Payload = new ContractPayload()
                {
                    CartId = CartId,
                    //AddressId = AddressId
                }
            };
            var httpRequest = GetAuthorizedRequest(HttpMethod.Post, "https://www.samsclub.com/api/node/vivaldi/cxo/v2/contracts");
            httpRequest.Body = contract.ConvertToJson<AddSamsContractDto>();

            var responseContract = await InvokeRequest(httpRequest);
            var samsContract = SamsContactDto.FromJson(responseContract.Response);

            if (samsContract.Errors != null && samsContract.Errors.Count > 0)
            {
                return null;
            }
            await TaxExempt(samsContract.Payload.Id);



            responseContract = await InvokeRequest(httpRequest);
            samsContract = SamsContactDto.FromJson(responseContract.Response);
            if (samsContract.Errors != null && samsContract.Errors.Count > 0)
            {
                return null;
            }
            // time slots ////

            var enableTimeSLot = "{\"payload\":{\"alcoholAgeConsent\":false,\"slotId\":\""+$"{ samsContract.Metadata.Slots.FirstOrDefault(f => f.Status.Equals("AVAILABLE")).SlotId}"+"\",\"type\":\"CLUB_PICKUP\"}}";
            var slotUrl = $"https://www.samsclub.com/api/node/vivaldi/cxo/v3/contracts/{samsContract.Payload.Id}/fulfillments/"+samsContract.Payload.FulfillmentGroups.FirstOrDefault().Id;

            httpRequest = GetAuthorizedRequest(HttpMethod.Put,slotUrl);
            httpRequest.Body = enableTimeSLot;
            await InvokeRequest(httpRequest);   

            var walletInfo = await GetWalletInfoDto();
            
            var orderDto = new SamsOrderPlaceDto()
            {
                Payload = new SamsOrderPayload()
                {
                    ContractId = samsContract.Payload.Id,
                    Payments = new List<Payment>()
                    {
                        new Payment()
                        {

                            Id = walletInfo.Payload.PaymentCards.CreditCards.FirstOrDefault().Id.ToString(),

                            AmountToBeCharged = samsContract.Payload.Totals.OrderTotalAmount,
                            Type = "CREDIT_CARD"

                        }
                    }
                }
            };
            httpRequest = GetAuthorizedRequest(HttpMethod.Post, "https://www.samsclub.com/api/node/vivaldi/cxo/v2/orders");
            httpRequest.Params.Add(new HeaderParams()
            {
                Key = "sams-tracking-id",
                Value = "WPKh5fRmMPYdEMCG7DAK80-c96de3b2-8c5a-4c6e-a53f-8ce6b754c92a"
            });
            httpRequest.Body = orderDto.ConvertToJson<SamsOrderPlaceDto>();


           

            var resp = await InvokeRequest(httpRequest);
            int tries = 1;
            while(resp.StatusCode == "BadRequest" && tries <= 2 )
            {
                orderDto.Payload.Payments = new List<Payment>()
                    {
                        new Payment()
                        {

                            Id = walletInfo.Payload.PaymentCards.CreditCards.Skip(tries).FirstOrDefault().Id.ToString(),

                            AmountToBeCharged = samsContract.Payload.Totals.OrderTotalAmount,
                            Type = "CREDIT_CARD"

                        }
                    };
                tries++;
                httpRequest.Body = orderDto.ConvertToJson<SamsOrderPlaceDto>();
                resp = await InvokeRequest(httpRequest);
            }
            return SamsOrderReceiverDto.FromJson(resp.Response);
        }
        public static async Task<bool> DeleteAddresses(SamsAddressesDetailDto addressesDetailDto, string Id)
        {
            var request = GetAuthorizedRequest(HttpMethod.Delete, RemoveAddress(Id));
            request.Body = addressesDetailDto.ConvertToJson();
            var response = await HttpHandler.Request(request);
            return response.Response != null;
        }


        public static string GetParitcularAddress(SamsAddressesDetailDto list, PlaceOrderAddress address)
        {

            foreach (var ad in list.Addresses)
            {
                bool isValid = true;
                isValid = isValid && ad.ContactFirstName.Trim().ToLower().Equals(address.FirstName.Trim().ToLower());
                isValid = isValid && ad.City.ToLower().Trim().Equals(address.City.ToLower().Trim());
                isValid = isValid && ad.ContactLastName.Trim().ToLower().Equals(address.LastName.Trim().ToLower());
                if (isValid)
                {
                    return ad.Id;
                }
            }
            return "";
        }
        public static async Task<List<PlaceOrderDto>> DoWork(List<PlaceOrderDto> Models)
        {
            await UpdateUserCart();
            var cart = await GetCart();
            foreach (var item in cart.Payload.LineItems)
            {
                await RemoveItemFromCartAsync(item.Value.Id);
            }
            foreach (var Model in Models)
            {
                int checkcart = 30;
                try
                {
                    var isavailable = true;
                    foreach (var orderDto in Model.Items)
                    {
                        if (string.IsNullOrEmpty(orderDto.Sku))
                        {
                            isavailable = false;
                            break;
                        }
                        Thread.Sleep(2000);
                        isavailable = isavailable && await SearchAndAddToProductAsync(orderDto);

                    }
                    if (isavailable)
                    {
                        foreach (var orderDto in Model.Items)
                        {
                            if (string.IsNullOrEmpty(orderDto.Sku))
                            {
                                continue;
                            }
                            var isAdded = await SearchAndAddToProductAsync(orderDto, true);
                            if (!isAdded)
                            {
                                Model.IsOrdered = false;
                                break;
                            }
                            else
                            {
                               
                                Model.IsOrdered = true;
                            }

                        }
                    }
                }
                catch 
                { 
                
                
                }
                
                
            }
            
            
            
            
            
            //var addresses = await GetAddresses();
            //if (addresses.Addresses.Count > 1)
            //{
            //    for (var i = 1; i < addresses.Addresses.ToList().Count; i++)
            //    {
            //        var ad = addresses.Addresses[i];
            //        addresses.Addresses.Remove(addresses.Addresses[i]);
            //        await DeleteAddresses(addresses, ad.Id);
            //    }
            //}
            //var address = await ValidateAddress(new SamsAddress()
            //{
            //    ContactFirstName = Model.Address.FirstName,
            //    ContactLastName = Model.Address.LastName,
            //    City = Model.Address.City,
            //    StateCode = Model.Address.State,
            //    LineOne = Model.Address.LineOne,
            //    LineTwo = Model.Address.LineTwo,
            //    CountryCode = Model.Address.Country,
            //    IsBusiness = !Model.Address.IsResidential,
            //    ContactPhone = Model.Address.PhoneNumber,
            //    ContactPhoneType = "MOBILE",
            //    IsDefault = false,
            //    IsDockDoorPresent = true,
            //    PostalCode = Model.Address.PostalCode
            //});
            //if(address != null && address.Status.Equals("Invalid"))
            //{
            //    orderResponse.IsAddressError = true;
            //    return orderResponse;
            //}
            //if (address == null ||  address.Status.Equals("FAILURE"))
            //{
            //    orderResponse.IsAuthorizationError = true;
            //    return orderResponse;
            //}

            //Thread.Sleep(10000);
            //addresses = await GetAddresses();
            //var addressId = GetParitcularAddress(addresses, Model.Address);
            //if (addressId.IsEmpty())
            //{
            //    orderResponse.IsAuthorizationError = true;
            //    return orderResponse;
            //}
            string addressId = "";
            var orderInfo = await GenerateContractAndPlaceOrder(addressId);
            if (orderInfo == null)
            {
                Thread.Sleep(10000);
                ////addresses = await GetAddresses();
                //addressId = GetParitcularAddress(addresses, Model.Address);
                //if (addressId.IsEmpty())
                //{
                //    orderResponse.IsAuthorizationError = true;
                //    return orderResponse;
                //}
                //Console.WriteLine("Address: " + addresses.Addresses.FirstOrDefault(f=> f.Id.Equals(addressId)).ConvertToJson());
                orderInfo = await GenerateContractAndPlaceOrder(addressId);

            }
           
             return Models;

            
            


        }
        public static async Task TaxExempt(string contractId)
        {
            var ste = new SamsTaxExempt();
            var cart = await GetCart();
            ste.Payload = new TaxPayload
            {
                LineItems = new System.Collections.Generic.List<LineItem>()
            };
            cart.Payload.LineItems.ToList().ForEach(e =>
            {
                ste.Payload.LineItems.Add(new LineItem()
                {
                    Id = e.Key,
                    TaxExempt = true
                });
            });
            var request = GetAuthorizedRequest(HttpMethod.Put, TaxExemptUrl(contractId));
            request.Body = ste.ConvertToJson();
            await InvokeRequest(request);
        }
        public static async Task RemoveItemFromCartAsync(string lineitemId)
        {
            var itemDto = new SamsRemoveItemFromCartDto()
            {
                Payload = new Payload()
                {
                    LineItems = new System.Collections.Generic.List<string>()
                    {
                        lineitemId
                    }
                }
            };
            var request = GetAuthorizedRequest(HttpMethod.Post, RemoveItemsFromCart(CartId));
            request.Body = itemDto.ConvertToJson();
            await HttpHandler.Request(request);
        }
        public static async Task<bool> AddItemFromCartAsync(SamsAddItemToCartDto samsAddItemToCartDto)
        {
            var httpRequest = GetAuthorizedRequest(HttpMethod.Post, AddItemsToCart(CartId), true);
            httpRequest.Body = samsAddItemToCartDto.ConvertToJson();
            //var httpRequest = new HttpRequester.HttpRequester()
            //{
            //    Method = HttpMethod.Post,
            //    Url = AddItemsToCart(CartId),
            //    Params = new System.Collections.Generic.List<HttpRequester.HeaderParams>()
            //    {
            //       //HeaderParams.GetHeaderParam(HeaderType.authorization,Authorization),
            //       HeaderParams.GetHeaderParam(HeaderType.contenttype,ContentType),
            //       HeaderParams.GetHeaderParam(HeaderType.cookie,Cookie),
            //       HeaderParams.GetHeaderParam(HeaderType.useragent,UserAgent),
            //       HeaderParams.GetHeaderParam(HeaderType.accept,Accept),
            //       HeaderParams.GetHeaderParam(HeaderType.acceptlanguage,AcceptLanguage),

            //    },
            //    Body = samsAddItemToCartDto.ConvertToJson()
            //};
            Thread.Sleep(5000);
            var resp = await HttpHandler.Request(httpRequest);
            var cart = SamsCartResponseDto.FromJson(resp.Response);
            if (cart.Payload == null)
            {
                return false;
            }
            var list = cart.Payload.LineItems.ToList();
            if (list == null)
            {

            }
            foreach (var e in list)
            {
                if (e.Value.ProductInfo.StockLevel.Equals("UNAVAILABLE"))
                {
                    return false;
                }
            };
            return true;
        }
        public static async Task<SamsCartResponseDto> GetCart()
        {
            var request = GetAuthorizedRequest(HttpMethod.GET, GetCart(CartId));
            var response = await HttpHandler.Request(request);
            var model = SamsCartResponseDto.FromJson(response.Response);
            return model;
        }
        public static async Task<SamsProductDetailDto> GetProductDetail(string productId, bool authorized= true)
        {
            SamsSearchProductQueryDto sdt = new SamsSearchProductQueryDto()
            {
                ClubId = 8166,
                Type = "LARGE",
                ProductIds = new List<string>()
            };
            sdt.ProductIds.Add(productId);
            var url = "https://www.samsclub.com/api/node/vivaldi/browse/v2/products?includeOptical=true";
            var httpRequest = authorized ? GetAuthorizedRequest(HttpMethod.Post, url, true) : GetRequest(HttpMethod.Post,url);
            httpRequest.Body = sdt.ConvertToJson();
            var response = await InvokeRequest(httpRequest);
            return SamsProductDetailDto.FromJson(response.Response);
        }
        public static async Task<OrderShipperDto> GetTrackingInfo(string orderId)
        {
            try
            {
                orderId = Generics.Functions.RemovesAllCharactersFromString(orderId);
                var url = $"https://www.samsclub.com/api/node/vivaldi/orders/v3/details/{orderId}?ts=9495445191647073849573";
                var request = GetAuthorizedRequest(HttpMethod.GET, url, true);
                var resp = await InvokeRequest(request);
                var dto = SamsTrackingOrderDto.FromJson(resp.Response);
                if (dto == null || dto.Orders.FirstOrDefault().Shipments == null)
                {
                    return null;
                }
                Thread.Sleep(5000);
                var trackingUrl = $"https://www.samsclub.com/api/node/vivaldi/v2/shipment/{dto.Orders.FirstOrDefault().Shipments.FirstOrDefault().TrackingDetails.FirstOrDefault().TrackingNo}";
                request = GetAuthorizedRequest(HttpMethod.GET, trackingUrl, true);
                resp = await InvokeRequest(request);
                var trackingInfo = SamsTrackingInfoDto.FromJson(resp.Response);
                var orderShipperDto = new OrderShipperDto()
                {
                    CarrierCode = "",
                    PlatformOrderId = orderId,
                    ShipstationOrderId = "",
                    TrackingDate = System.DateTimeOffset.ParseExact(trackingInfo.Payload.Progress.ShippedDate, "ddd MMM d HH:mm:ss UTC yyyy", System.Globalization.CultureInfo.InvariantCulture),
                    
                    TrackingId = trackingInfo.Payload.TrackingNumber,
                    TrackingUrl = trackingInfo.Payload.CarrierTrackingUrl.ToString()
                };
                return orderShipperDto;
            }
            catch (Exception e)
            {


            }
            return null;

        }
        public static async Task<bool> SearchAndAddToProductAsync(PlaceOrderItems order,bool addtocart = false)
        {
            var request = GetAuthorizedRequest(HttpMethod.GET, ProductSearcher(order.Sku));
            var response = await HttpHandler.Request(request);
            var respDto = SamsPropertySearchResponseDto.FromJson(response.Response);
            if (respDto.Status == "OK")
            {
                if (respDto.Payload.NumberOfRecords > 0)
                {


                    var items = respDto.Payload.Records.FirstOrDefault(f =>
                    f.Skus.FirstOrDefault(sk => sk.OnlineOffer != null &&
                    sk.OnlineOffer.ItemNumber.ToString().Equals(order.Sku)) != null);
                    var productId = respDto.Payload.Records.FirstOrDefault().Id;
                    var sku = new Skus();
                    if (items == null)
                    {
                        var pd = await GetProductDetail(respDto.Payload.Records.FirstOrDefault().Id);
                        var pddetail = pd.Payload.Products.FirstOrDefault(f => f.Skus.FirstOrDefault(sk => sk.OnlineOffer != null &&
                       sk.OnlineOffer.ItemNumber.ToString().Equals(order.Sku)) != null);
                        if (pddetail != null)
                        {
                            sku = pddetail.Skus.FirstOrDefault(f => f.OnlineOffer.ItemNumber.ToString().Equals(order.Sku));
                        }
                        else
                        {
                            return false;
                        }


                    }
                    else
                    {
                        sku = items.Skus.FirstOrDefault(f => f.OnlineOffer.ItemNumber.ToString().Equals(order.Sku));
                    }

                    if (sku != null)
                    {
                        if (sku.ClubOffer != null && sku.ClubOffer.Inventory.AvailableToSellQuantity != null && sku.ClubOffer.Inventory.AvailableToSellQuantity >= order.Quantity)
                        {
                            if(!addtocart)
                            {
                                return true;    
                            }
                            try
                            {

                            }
                            catch
                            {

                            }
                            //sku.OnlineOffer.Inventory.AvailableToSellQuantity;
                            /*if(sku.OnlineOffer.MinQuantity > )
                            {

                            }*/
                            return await AddItemFromCartAsync(new SamsAddItemToCartDto()
                            {

                                Payload = new Payload1()
                                {

                                    LineItems = new System.Collections.Generic.List<LineItem>()
                                    {
                                        new LineItem()
                                        {
                                            Channel = "CLUB",
                                            Quantity = order.Quantity,
                                            ItemNumber = sku.OnlineOffer.ItemNumber,
                                            ProductId = productId,
                                            SkuId = sku.OnlineOffer.SkuId
                                        }
                                    }
                                }
                            });
                        }
                    }
                }
            }
            return false;

        }

        public static async Task<bool> OutOfStockWorker(PlaceOrderDto placeOrder,Dictionary<string,bool> productStatus)
        {
            var outOfStock = true;
            foreach (var orderDto in placeOrder.Items)
            {
                if(productStatus.ContainsKey(orderDto.Sku))
                {
                    Console.WriteLine("Item Found in Dictionary");
                    outOfStock = outOfStock && productStatus.Get(orderDto.Sku,false);
                }
                else
                {
                    var pd = await SearchProduct(orderDto);
                    productStatus.Add(orderDto.Sku, (pd != null && pd.Quantity != null));
                    if (pd != null && pd.Quantity != null)
                    {

                        OrderPlaceHelper.NotifyEmail(pd);
                    }
                    else
                    {
                        LayerDao.ProductStatusDAO.InsertIfNotFound(new Generics.Db.ProductStatusDto()
                        {
                            Category = pd.Category,
                            InStock = false,
                            ItemSku = pd.Sku,
                            LastUpdated = DateTime.UtcNow,
                            Quantity = 0

                        });
                    }
                    System.Threading.Thread.Sleep(2000);
                    outOfStock = outOfStock && (pd != null && pd.Quantity != null);
                    Thread.Sleep(5000);
                }
                
            }
            return outOfStock;
        }
        public static async Task<InventoryStatusDto> SearchProduct(PlaceOrderItems order)
        {
            var request = GetAuthorizedRequest(HttpMethod.GET,ProductSearcher(order.Sku),true);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(40));
            
            var metas = LayerDao.SiteMetaDAO.GetKey("UU");

            

            request.CookieContainer = null;
            request.Params.Add(new HeaderParams()
            {
                Key  = "Cookie",
                Value = metas.VALUE
            });
            var response = (await InvokeRequest(request)).Response;
            var respDto = SamsPropertySearchResponseDto.FromJson(response);
            if (respDto.Status == "OK")
            {
                if (respDto.Payload.NumberOfRecords > 0)
                {


                    var items = respDto.Payload.Records.FirstOrDefault(f =>
                    f.Skus.FirstOrDefault(sk => sk.OnlineOffer != null &&
                    sk.OnlineOffer.ItemNumber == long.Parse(order.Sku)) != null);
                    var productId = respDto.Payload.Records.FirstOrDefault().Id;
                    var sku = new Skus();
                    var link = "";
                    if (items == null)
                    {
                        Thread.Sleep(5000);
                        var pd = await GetProductDetail(respDto.Payload.Records.FirstOrDefault().Id);
                        var pddetail = pd.Payload.Products.FirstOrDefault(f => f.Skus.FirstOrDefault(sk => sk.OnlineOffer != null &&
                        sk.OnlineOffer.ItemNumber == long.Parse(order.Sku)) != null);

                        if (pddetail != null)
                        {
                            link = pddetail.SearchAndSeo.Url;
                            sku = pddetail.Skus.FirstOrDefault(f => f.OnlineOffer.ItemNumber == long.Parse(order.Sku));
                        }
                        else
                        {
                            return null;
                        }
                        

                    }
                    else
                    {
                        sku = items.Skus.FirstOrDefault(f => f.OnlineOffer.ItemNumber == long.Parse(order.Sku));
                    }

                    if (sku != null)
                    {
                        if (sku.ShippingMethods != null && sku.ShippingMethods.Count > 0)
                        {
                            var inventory = new InventoryStatusDto
                            {
                                Category = "SamsClub",
                                Name = respDto.Payload.Records.FirstOrDefault().SearchAndSeo.Name,
                                ProductId = sku.ProductId,
                                Sku = order.Sku,
                                Status = sku.OnlineOffer.Inventory.Status,
                                Quantity = sku.OnlineOffer.Inventory.AvailableToSellQuantity,
                                Link = link
                            };

                            Console.WriteLine(sku.OnlineOffer.Inventory.Status);

                            return inventory;

                        }
                    }
                }
            }
            var inventoryy = new InventoryStatusDto
            {
                Category = "SamsClub",
                Name = "",
                ProductId = "0",
                Sku = order.Sku,
                Status = "NOT_AVAILABLE",
             
            };
            return inventoryy;
        }
        public static async Task<bool> SearchProductAsync(PlaceOrderItems order)
        {
            var response = await SearchProduct(order);
            return response != null && response.Quantity != null;
        }
    }
}

