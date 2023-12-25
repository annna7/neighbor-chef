# IO.Swagger.Api.CustomerApi

All URIs are relative to */*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CustomerCustomerIdGet**](CustomerApi.md#customercustomeridget) | **GET** /Customer/{customerId} | 
[**CustomerGet**](CustomerApi.md#customerget) | **GET** /Customer | 
[**CustomerOrdersOrderIdPut**](CustomerApi.md#customerordersorderidput) | **PUT** /Customer/orders/{orderId} | 

<a name="customercustomeridget"></a>
# **CustomerCustomerIdGet**
> void CustomerCustomerIdGet (Guid? customerId)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CustomerCustomerIdGetExample
    {
        public void main()
        {
            var apiInstance = new CustomerApi();
            var customerId = new Guid?(); // Guid? | 

            try
            {
                apiInstance.CustomerCustomerIdGet(customerId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CustomerApi.CustomerCustomerIdGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **customerId** | [**Guid?**](Guid?.md)|  | 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="customerget"></a>
# **CustomerGet**
> void CustomerGet ()



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CustomerGetExample
    {
        public void main()
        {
            var apiInstance = new CustomerApi();

            try
            {
                apiInstance.CustomerGet();
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CustomerApi.CustomerGet: " + e.Message );
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
<a name="customerordersorderidput"></a>
# **CustomerOrdersOrderIdPut**
> void CustomerOrdersOrderIdPut (Guid? orderId, UpdateOrderStatusDto body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CustomerOrdersOrderIdPutExample
    {
        public void main()
        {
            var apiInstance = new CustomerApi();
            var orderId = new Guid?(); // Guid? | 
            var body = new UpdateOrderStatusDto(); // UpdateOrderStatusDto |  (optional) 

            try
            {
                apiInstance.CustomerOrdersOrderIdPut(orderId, body);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CustomerApi.CustomerOrdersOrderIdPut: " + e.Message );
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
