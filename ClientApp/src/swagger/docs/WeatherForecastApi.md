# IO.Swagger.Api.WeatherForecastApi

All URIs are relative to */*

Method | HTTP request | Description
------------- | ------------- | -------------
[**WeatherForecastGet**](WeatherForecastApi.md#weatherforecastget) | **GET** /WeatherForecast | 

<a name="weatherforecastget"></a>
# **WeatherForecastGet**
> List<WeatherForecast> WeatherForecastGet ()



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class WeatherForecastGetExample
    {
        public void main()
        {
            var apiInstance = new WeatherForecastApi();

            try
            {
                List&lt;WeatherForecast&gt; result = apiInstance.WeatherForecastGet();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling WeatherForecastApi.WeatherForecastGet: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**List<WeatherForecast>**](WeatherForecast.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
