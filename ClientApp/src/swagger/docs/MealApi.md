# IO.Swagger.Api.MealApi

All URIs are relative to */*

Method | HTTP request | Description
------------- | ------------- | -------------
[**MealIdDelete**](MealApi.md#mealiddelete) | **DELETE** /Meal/{id} | 
[**MealIdGet**](MealApi.md#mealidget) | **GET** /Meal/{id} | 
[**MealIdIngredientsDelete**](MealApi.md#mealidingredientsdelete) | **DELETE** /Meal/{id}/ingredients | 
[**MealIdIngredientsPost**](MealApi.md#mealidingredientspost) | **POST** /Meal/{id}/ingredients | 
[**MealIdPut**](MealApi.md#mealidput) | **PUT** /Meal/{id} | 
[**MealPost**](MealApi.md#mealpost) | **POST** /Meal | 

<a name="mealiddelete"></a>
# **MealIdDelete**
> void MealIdDelete (Guid? id)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class MealIdDeleteExample
    {
        public void main()
        {
            var apiInstance = new MealApi();
            var id = new Guid?(); // Guid? | 

            try
            {
                apiInstance.MealIdDelete(id);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling MealApi.MealIdDelete: " + e.Message );
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
<a name="mealidget"></a>
# **MealIdGet**
> void MealIdGet (Guid? id)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class MealIdGetExample
    {
        public void main()
        {
            var apiInstance = new MealApi();
            var id = new Guid?(); // Guid? | 

            try
            {
                apiInstance.MealIdGet(id);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling MealApi.MealIdGet: " + e.Message );
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
<a name="mealidingredientsdelete"></a>
# **MealIdIngredientsDelete**
> void MealIdIngredientsDelete (Guid? id, RemoveIngredientDto body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class MealIdIngredientsDeleteExample
    {
        public void main()
        {
            var apiInstance = new MealApi();
            var id = new Guid?(); // Guid? | 
            var body = new RemoveIngredientDto(); // RemoveIngredientDto |  (optional) 

            try
            {
                apiInstance.MealIdIngredientsDelete(id, body);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling MealApi.MealIdIngredientsDelete: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | [**Guid?**](Guid?.md)|  | 
 **body** | [**RemoveIngredientDto**](RemoveIngredientDto.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="mealidingredientspost"></a>
# **MealIdIngredientsPost**
> void MealIdIngredientsPost (Guid? id, AddIngredientDto body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class MealIdIngredientsPostExample
    {
        public void main()
        {
            var apiInstance = new MealApi();
            var id = new Guid?(); // Guid? | 
            var body = new AddIngredientDto(); // AddIngredientDto |  (optional) 

            try
            {
                apiInstance.MealIdIngredientsPost(id, body);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling MealApi.MealIdIngredientsPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | [**Guid?**](Guid?.md)|  | 
 **body** | [**AddIngredientDto**](AddIngredientDto.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="mealidput"></a>
# **MealIdPut**
> void MealIdPut (Guid? id, UpdateMealDto body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class MealIdPutExample
    {
        public void main()
        {
            var apiInstance = new MealApi();
            var id = new Guid?(); // Guid? | 
            var body = new UpdateMealDto(); // UpdateMealDto |  (optional) 

            try
            {
                apiInstance.MealIdPut(id, body);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling MealApi.MealIdPut: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | [**Guid?**](Guid?.md)|  | 
 **body** | [**UpdateMealDto**](UpdateMealDto.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="mealpost"></a>
# **MealPost**
> void MealPost (CreateMealDto body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class MealPostExample
    {
        public void main()
        {
            var apiInstance = new MealApi();
            var body = new CreateMealDto(); // CreateMealDto |  (optional) 

            try
            {
                apiInstance.MealPost(body);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling MealApi.MealPost: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**CreateMealDto**](CreateMealDto.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
