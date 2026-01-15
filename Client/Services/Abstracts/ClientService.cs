using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Torico.Shared.Responses;
using Torico.Shared.Utils;
using Torico.Client.Utilities;
using Torico.Client.Services.Interfaces;

namespace Torico.Client.Services.Abstracts;

public abstract class ClientService
{
    protected const string ApiBase = "api/";
    protected HttpClient HttpClient { get; }

    protected ClientService(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    protected async Task<HttpResponseMessage> PerformGetRequest<T>(string endPoint, Dictionary<string, string> queryParams)
    {
        var queryString = JsonUtility.BuildQueryString(queryParams);
        var uri = $"{GetBaseUrl()}{endPoint}";

        if (!string.IsNullOrWhiteSpace(queryString)) uri += $"?{queryString}";

        return await HttpClient.GetAsync(uri);
    }

    protected async Task<HttpResponseMessage> PerformPostRequest<T>(string endPoint, T model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        var payload = JsonUtility.GenerateStringContent(JsonUtility.SerializeObj(model));
        return await HttpClient.PostAsync($"{GetBaseUrl()}{endPoint}", payload);
    }

    protected async Task<ServiceResponse> PerformPostRequestWithResponse<T>(string endPoint, T model)
    {
        var response = await PerformPostRequest(endPoint, model);

        if (!response.IsSuccessStatusCode)
            return new ServiceResponse(false, "Error occurred. Try again later...");

        var apiResponse = await response.Content.ReadAsStringAsync();
        return JsonUtility.DeserializeJsonString<ServiceResponse>(apiResponse);
    }

    protected async Task<List<T>> PerformGetRequestWithListResponse<T>(string endPoint, Dictionary<string, string> queryParams)
    {
        var response = await PerformGetRequest<T>(endPoint, queryParams);

        if (!response.IsSuccessStatusCode) return null!;

        var result = await response.Content.ReadAsStringAsync();
        return JsonUtility.DeserializeJsonStringList<T>(result).ToList();
    }

    protected string BuildBaseUrl(string endpoint) => ApiBase + endpoint;
    protected abstract string GetBaseUrl();
}
