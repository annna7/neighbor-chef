# IO.Swagger.Api.OidcConfigurationApi

All URIs are relative to */*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ConfigurationClientIdGet**](OidcConfigurationApi.md#configurationclientidget) | **GET** /_configuration/{clientId} | 

<a name="configurationclientidget"></a>
# **ConfigurationClientIdGet**
> void ConfigurationClientIdGet (string clientId)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ConfigurationClientIdGetExample
    {
        public void main()
        {
            var apiInstance = new OidcConfigurationApi();
            var clientId = clientId_example;  // string | 

            try
            {
                apiInstance.ConfigurationClientIdGet(clientId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling OidcConfigurationApi.ConfigurationClientIdGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **clientId** | **string**|  | 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
