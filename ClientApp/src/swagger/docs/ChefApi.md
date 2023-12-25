# IO.Swagger.Api.ChefApi

All URIs are relative to */*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ChefChefIdDatesDelete**](ChefApi.md#chefchefiddatesdelete) | **DELETE** /Chef/{chefId}/dates | 
[**ChefChefIdDatesGet**](ChefApi.md#chefchefiddatesget) | **GET** /Chef/{chefId}/dates | 
[**ChefChefIdDatesPost**](ChefApi.md#chefchefiddatespost) | **POST** /Chef/{chefId}/dates | 
[**ChefChefIdGet**](ChefApi.md#chefchefidget) | **GET** /Chef/{chefId} | 
[**ChefGet**](ChefApi.md#chefget) | **GET** /Chef | 
[**ChefOrdersOrderIdPut**](ChefApi.md#chefordersorderidput) | **PUT** /Chef/orders/{orderId} | 

<a name="chefchefiddatesdelete"></a>
# **ChefChefIdDatesDelete**
> void ChefChefIdDatesDelete (Guid? chefId, DateDto body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ChefChefIdDatesDeleteExample
    {
        public void main()
        {
            var apiInstance = new ChefApi();
            var chefId = new Guid?(); // Guid? | 
            var body = new DateDto(); // DateDto |  (optional) 

            try
            {
                apiInstance.ChefChefIdDatesDelete(chefId, body);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ChefApi.ChefChefIdDatesDelete: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **chefId** | [**Guid?**](Guid?.md)|  | 
 **body** | [**DateDto**](DateDto.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="chefchefiddatesget"></a>
# **ChefChefIdDatesGet**
> void ChefChefIdDatesGet (Guid? chefId)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ChefChefIdDatesGetExample
    {
        public void main()
        {
            var apiInstance = new ChefApi();
            var chefId = new Guid?(); // Guid? | 

            try
            {
                apiInstance.ChefChefIdDatesGet(chefId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ChefApi.ChefChefIdDatesGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **chefId** | [**Guid?**](Guid?.md)|  | 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="chefchefiddatespost"></a>
# **ChefChefIdDatesPost**
> void ChefChefIdDatesPost (Guid? chefId, DateDto body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ChefChefIdDatesPostExample
    {
        public void main()
        {
            var apiInstance = new ChefApi();
            var chefId = new Guid?(); // Guid? | 
            var body = new DateDto(); // DateDto |  (optional) 

            try
            {
                apiInstance.ChefChefIdDatesPost(chefId, body);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ChefApi.ChefChefIdDatesPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **chefId** | [**Guid?**](Guid?.md)|  | 
 **body** | [**DateDto**](DateDto.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="chefchefidget"></a>
# **ChefChefIdGet**
> void ChefChefIdGet (Guid? chefId)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ChefChefIdGetExample
    {
        public void main()
        {
            var apiInstance = new ChefApi();
            var chefId = new Guid?(); // Guid? | 

            try
            {
                apiInstance.ChefChefIdGet(chefId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ChefApi.ChefChefIdGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **chefId** | [**Guid?**](Guid?.md)|  | 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="chefget"></a>
# **ChefGet**
> void ChefGet ()



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ChefGetExample
    {
        public void main()
        {
            var apiInstance = new ChefApi();

            try
            {
                apiInstance.ChefGet();
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ChefApi.ChefGet: " + e.Message );
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
<a name="chefordersorderidput"></a>
# **ChefOrdersOrderIdPut**
> void ChefOrdersOrderIdPut (Guid? orderId, UpdateOrderStatusDto body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ChefOrdersOrderIdPutExample
    {
        public void main()
        {
            var apiInstance = new ChefApi();
            var orderId = new Guid?(); // Guid? | 
            var body = new UpdateOrderStatusDto(); // UpdateOrderStatusDto |  (optional) 

            try
            {
                apiInstance.ChefOrdersOrderIdPut(orderId, body);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ChefApi.ChefOrdersOrderIdPut: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **orderId** | [**Guid?**](Guid?.md)|  | 
 **body** | [**UpdateOrderStatusDto**](UpdateOrderStatusDto.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
