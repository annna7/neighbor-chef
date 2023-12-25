# IO.Swagger.Api.AccountApi

All URIs are relative to */*

Method | HTTP request | Description
------------- | ------------- | -------------
[**AccountLoginPost**](AccountApi.md#accountloginpost) | **POST** /Account/login | 
[**AccountRegisterPost**](AccountApi.md#accountregisterpost) | **POST** /Account/register | 

<a name="accountloginpost"></a>
# **AccountLoginPost**
> void AccountLoginPost (UserLoginDto body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AccountLoginPostExample
    {
        public void main()
        {
            var apiInstance = new AccountApi();
            var body = new UserLoginDto(); // UserLoginDto |  (optional) 

            try
            {
                apiInstance.AccountLoginPost(body);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AccountApi.AccountLoginPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**UserLoginDto**](UserLoginDto.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="accountregisterpost"></a>
# **AccountRegisterPost**
> void AccountRegisterPost ()



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class AccountRegisterPostExample
    {
        public void main()
        {
            var apiInstance = new AccountApi();

            try
            {
                apiInstance.AccountRegisterPost();
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AccountApi.AccountRegisterPost: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
