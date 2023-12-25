# IO.Swagger.Api.ReviewApi

All URIs are relative to */*

Method | HTTP request | Description
------------- | ------------- | -------------
[**ReviewChefChefIdGet**](ReviewApi.md#reviewchefchefidget) | **GET** /Review/chef/{chefId} | 
[**ReviewGet**](ReviewApi.md#reviewget) | **GET** /Review | 
[**ReviewIdDelete**](ReviewApi.md#reviewiddelete) | **DELETE** /Review/{id} | 
[**ReviewIdGet**](ReviewApi.md#reviewidget) | **GET** /Review/{id} | 
[**ReviewIdPut**](ReviewApi.md#reviewidput) | **PUT** /Review/{id} | 
[**ReviewPost**](ReviewApi.md#reviewpost) | **POST** /Review | 

<a name="reviewchefchefidget"></a>
# **ReviewChefChefIdGet**
> void ReviewChefChefIdGet (Guid? chefId)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ReviewChefChefIdGetExample
    {
        public void main()
        {
            var apiInstance = new ReviewApi();
            var chefId = new Guid?(); // Guid? | 

            try
            {
                apiInstance.ReviewChefChefIdGet(chefId);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReviewApi.ReviewChefChefIdGet: " + e.Message );
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
<a name="reviewget"></a>
# **ReviewGet**
> void ReviewGet ()



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ReviewGetExample
    {
        public void main()
        {
            var apiInstance = new ReviewApi();

            try
            {
                apiInstance.ReviewGet();
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReviewApi.ReviewGet: " + e.Message );
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
<a name="reviewiddelete"></a>
# **ReviewIdDelete**
> void ReviewIdDelete (Guid? id)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ReviewIdDeleteExample
    {
        public void main()
        {
            var apiInstance = new ReviewApi();
            var id = new Guid?(); // Guid? | 

            try
            {
                apiInstance.ReviewIdDelete(id);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReviewApi.ReviewIdDelete: " + e.Message );
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
<a name="reviewidget"></a>
# **ReviewIdGet**
> void ReviewIdGet (Guid? id)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ReviewIdGetExample
    {
        public void main()
        {
            var apiInstance = new ReviewApi();
            var id = new Guid?(); // Guid? | 

            try
            {
                apiInstance.ReviewIdGet(id);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReviewApi.ReviewIdGet: " + e.Message );
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
<a name="reviewidput"></a>
# **ReviewIdPut**
> void ReviewIdPut (Guid? id, UpdateReviewDto body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ReviewIdPutExample
    {
        public void main()
        {
            var apiInstance = new ReviewApi();
            var id = new Guid?(); // Guid? | 
            var body = new UpdateReviewDto(); // UpdateReviewDto |  (optional) 

            try
            {
                apiInstance.ReviewIdPut(id, body);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReviewApi.ReviewIdPut: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | [**Guid?**](Guid?.md)|  | 
 **body** | [**UpdateReviewDto**](UpdateReviewDto.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="reviewpost"></a>
# **ReviewPost**
> void ReviewPost (CreateReviewDto body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ReviewPostExample
    {
        public void main()
        {
            var apiInstance = new ReviewApi();
            var body = new CreateReviewDto(); // CreateReviewDto |  (optional) 

            try
            {
                apiInstance.ReviewPost(body);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ReviewApi.ReviewPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**CreateReviewDto**](CreateReviewDto.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
