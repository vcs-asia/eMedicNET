using eMedicEntityModel.Models.v1;
using Newtonsoft.Json;
using System.Net;

namespace eMedicNETv6.Extensions
{
    public static class ApiExecute
    {
        public static async Task<ResponseModel?> GetAsync(string url)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", SessionData.AuthToken);
                using (var response = await httpClient.GetAsync(url))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return JsonConvert.DeserializeObject<ResponseModel>(await response.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        return JsonConvert.DeserializeObject<ResponseModel>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
        }

        public static async Task<ResponseModel?> AuthAsync(string url, Dictionary<string, string> parameters)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync(url, new FormUrlEncodedContent(parameters)))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return JsonConvert.DeserializeObject<ResponseModel>(await response.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        return JsonConvert.DeserializeObject<ResponseModel>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
        }

        public static async Task<ResponseModel?> PostAsync(string url, StringContent stringContent)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", SessionData.AuthToken);
                using (var response = await httpClient.PostAsync(url, stringContent))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return JsonConvert.DeserializeObject<ResponseModel>(await response.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        return JsonConvert.DeserializeObject<ResponseModel>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
        }

        public static async Task<ResponseModel?> PutAsync(string url, StringContent stringContent)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", SessionData.AuthToken);
                using (var response = await httpClient.PutAsync(url, stringContent))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return JsonConvert.DeserializeObject<ResponseModel>(await response.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        return JsonConvert.DeserializeObject<ResponseModel>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
        }

        public static async Task<ResponseModel?> DeleteAsync(string url, StringContent stringContent)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", SessionData.AuthToken);
                using (var response = await httpClient.DeleteAsync(url))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return JsonConvert.DeserializeObject<ResponseModel>(await response.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        return JsonConvert.DeserializeObject<ResponseModel>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
        }
    }
}
