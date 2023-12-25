# IO.Swagger.Api.OrderApi

All URIs are relative to */*

Method | HTTP request | Description
------------- | ------------- | -------------
[**OrderChefIdPost**](OrderApi.md#orderchefidpost) | **POST** /Order/{chefId} | 
[**OrderCustomerIdOrderIdDelete**](OrderApi.md#ordercustomeridorderiddelete) | **DELETE** /Order/{customerId}/{orderId} | 
[**OrderOrderIdGet**](OrderApi.md#orderorderidget) | **GET** /Order/{orderId} | 
[**OrderOrderIdPut**](OrderApi.md#orderorderidput) | **PUT** /Order/{orderId} | 

<a name="orderchefidpost"></a>
# **OrderChefIdPost**
> void OrderChefIdPost (Guid? chefId, CreateOrderDto body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OrderChefIdPostExample
    {
        public void main()
        {
            var apiInstance = new OrderApi();
            var chefId = new Guid?(); // Guid? | 
            var body = new CreateOrderDto(); // CreateOrderDto |  (optional) 

            try
            {
                apiInstance.OrderChefIdPost(chefId, body);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling OrderApi.OrderChefIdPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **chefId** | [**Guid?**](Guid?.md)|  | 
 **body** | [**CreateOrderDto**](CreateOrderDto.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="ordercustomeridorderiddelete"></a>
# **OrderCustomerIdOrderIdDelete**
> void OrderCustomerIdOrderIdDelete (Guid? customerId, Guid? orderId)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OrderCustomerIdOrderIdDeleteExample
    {
        public void main()
        {
            var apiInstance = new OrderApi();
            var customerId = new Guid?(); // Guid? | 
            var orderId = new Guid?(); // Guid? | 

            try
            {
                apiInstance.OrderCustomerIdOrderIdDelete(customerId, orderId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling OrderApi.OrderCustomerIdOrderIdDelete: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **customerId** | [**Guid?**](Guid?.md)|  | 
 **orderId** | [**Guid?**](Guid?.md)|  | 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="orderorderidget"></a>
# **OrderOrderIdGet**
> void OrderOrderIdGet (Guid? orderId)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OrderOrderIdGetExample
    {
        public void main()
        {
            var apiInstance = new OrderApi();
            var orderId = new Guid?(); // Guid? | 

            try
            {
                apiInstance.OrderOrderIdGet(orderId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling OrderApi.OrderOrderIdGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **orderId** | [**Guid?**](Guid?.md)|  | 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="orderorderidput"></a>
# **OrderOrderIdPut**
> void OrderOrderIdPut (Guid? orderId, UpdateOrderDto body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class OrderOrderIdPutExample
    {
        public void main()
        {
            var apiInstance = new OrderApi();
            var orderId = new Guid?(); // Guid? | 
            var body = new UpdateOrderDto(); // UpdateOrderDto |  (optional) 

            try
            {
                apiInstance.OrderOrderIdPut(orderId, body);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling OrderApi.OrderOrderIdPut: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **orderId** | [**Guid?**](Guid?.md)|  | 
 **body** | [**UpdateOrderDto**](UpdateOrderDto.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
