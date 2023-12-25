# IO.Swagger.Api.CategoryApi

All URIs are relative to */*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CategoryGet**](CategoryApi.md#categoryget) | **GET** /Category | 
[**CategoryGetByNameNameGet**](CategoryApi.md#categorygetbynamenameget) | **GET** /Category/getByName/{name} | 
[**CategoryIdDelete**](CategoryApi.md#categoryiddelete) | **DELETE** /Category/{id} | 
[**CategoryIdGet**](CategoryApi.md#categoryidget) | **GET** /Category/{id} | 
[**CategoryPost**](CategoryApi.md#categorypost) | **POST** /Category | 

<a name="categoryget"></a>
# **CategoryGet**
> void CategoryGet ()



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CategoryGetExample
    {
        public void main()
        {
            var apiInstance = new CategoryApi();

            try
            {
                apiInstance.CategoryGet();
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CategoryApi.CategoryGet: " + e.Message );
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
<a name="categorygetbynamenameget"></a>
# **CategoryGetByNameNameGet**
> void CategoryGetByNameNameGet (string name)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CategoryGetByNameNameGetExample
    {
        public void main()
        {
            var apiInstance = new CategoryApi();
            var name = name_example;  // string | 

            try
            {
                apiInstance.CategoryGetByNameNameGet(name);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CategoryApi.CategoryGetByNameNameGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **name** | **string**|  | 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="categoryiddelete"></a>
# **CategoryIdDelete**
> void CategoryIdDelete (Guid? id)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CategoryIdDeleteExample
    {
        public void main()
        {
            var apiInstance = new CategoryApi();
            var id = new Guid?(); // Guid? | 

            try
            {
                apiInstance.CategoryIdDelete(id);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CategoryApi.CategoryIdDelete: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | [**Guid?**](Guid?.md)|  | 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="categoryidget"></a>
# **CategoryIdGet**
> void CategoryIdGet (Guid? id)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CategoryIdGetExample
    {
        public void main()
        {
            var apiInstance = new CategoryApi();
            var id = new Guid?(); // Guid? | 

            try
            {
                apiInstance.CategoryIdGet(id);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CategoryApi.CategoryIdGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | [**Guid?**](Guid?.md)|  | 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="categorypost"></a>
# **CategoryPost**
> void CategoryPost (CreateCategoryDto body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CategoryPostExample
    {
        public void main()
        {
            var apiInstance = new CategoryApi();
            var body = new CreateCategoryDto(); // CreateCategoryDto |  (optional) 

            try
            {
                apiInstance.CategoryPost(body);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling CategoryApi.CategoryPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**CreateCategoryDto**](CreateCategoryDto.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
