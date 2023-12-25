# IO.Swagger.Api.PersonApi

All URIs are relative to */*

Method | HTTP request | Description
------------- | ------------- | -------------
[**PersonDeleteIdDelete**](PersonApi.md#persondeleteiddelete) | **DELETE** /Person/delete/{id} | 
[**PersonGetByEmailEmailGet**](PersonApi.md#persongetbyemailemailget) | **GET** /Person/getByEmail/{email} | 
[**PersonGetByEmailGet**](PersonApi.md#persongetbyemailget) | **GET** /Person/getByEmail | 
[**PersonGetByIdIdGet**](PersonApi.md#persongetbyididget) | **GET** /Person/getById/{id} | 
[**PersonUpdateByEmailEmailPut**](PersonApi.md#personupdatebyemailemailput) | **PUT** /Person/updateByEmail/{email} | 
[**PersonUpdateIdPut**](PersonApi.md#personupdateidput) | **PUT** /Person/update/{id} | 
[**PersonUpdatePut**](PersonApi.md#personupdateput) | **PUT** /Person/update | 

<a name="persondeleteiddelete"></a>
# **PersonDeleteIdDelete**
> void PersonDeleteIdDelete (Guid? id)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class PersonDeleteIdDeleteExample
    {
        public void main()
        {
            var apiInstance = new PersonApi();
            var id = new Guid?(); // Guid? | 

            try
            {
                apiInstance.PersonDeleteIdDelete(id);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PersonApi.PersonDeleteIdDelete: " + e.Message );
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
<a name="persongetbyemailemailget"></a>
# **PersonGetByEmailEmailGet**
> void PersonGetByEmailEmailGet (string email)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class PersonGetByEmailEmailGetExample
    {
        public void main()
        {
            var apiInstance = new PersonApi();
            var email = email_example;  // string | 

            try
            {
                apiInstance.PersonGetByEmailEmailGet(email);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PersonApi.PersonGetByEmailEmailGet: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **email** | **string**|  | 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="persongetbyemailget"></a>
# **PersonGetByEmailGet**
> void PersonGetByEmailGet ()



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class PersonGetByEmailGetExample
    {
        public void main()
        {
            var apiInstance = new PersonApi();

            try
            {
                apiInstance.PersonGetByEmailGet();
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PersonApi.PersonGetByEmailGet: " + e.Message );
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
<a name="persongetbyididget"></a>
# **PersonGetByIdIdGet**
> void PersonGetByIdIdGet (Guid? id)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class PersonGetByIdIdGetExample
    {
        public void main()
        {
            var apiInstance = new PersonApi();
            var id = new Guid?(); // Guid? | 

            try
            {
                apiInstance.PersonGetByIdIdGet(id);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PersonApi.PersonGetByIdIdGet: " + e.Message );
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
<a name="personupdatebyemailemailput"></a>
# **PersonUpdateByEmailEmailPut**
> void PersonUpdateByEmailEmailPut (string email, UpdatePersonDto body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class PersonUpdateByEmailEmailPutExample
    {
        public void main()
        {
            var apiInstance = new PersonApi();
            var email = email_example;  // string | 
            var body = new UpdatePersonDto(); // UpdatePersonDto |  (optional) 

            try
            {
                apiInstance.PersonUpdateByEmailEmailPut(email, body);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PersonApi.PersonUpdateByEmailEmailPut: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **email** | **string**|  | 
 **body** | [**UpdatePersonDto**](UpdatePersonDto.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="personupdateidput"></a>
# **PersonUpdateIdPut**
> void PersonUpdateIdPut (Guid? id, UpdatePersonDto body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class PersonUpdateIdPutExample
    {
        public void main()
        {
            var apiInstance = new PersonApi();
            var id = new Guid?(); // Guid? | 
            var body = new UpdatePersonDto(); // UpdatePersonDto |  (optional) 

            try
            {
                apiInstance.PersonUpdateIdPut(id, body);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PersonApi.PersonUpdateIdPut: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **id** | [**Guid?**](Guid?.md)|  | 
 **body** | [**UpdatePersonDto**](UpdatePersonDto.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="personupdateput"></a>
# **PersonUpdatePut**
> void PersonUpdatePut (Person body = null)



### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class PersonUpdatePutExample
    {
        public void main()
        {
            var apiInstance = new PersonApi();
            var body = new Person(); // Person |  (optional) 

            try
            {
                apiInstance.PersonUpdatePut(body);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling PersonApi.PersonUpdatePut: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**Person**](Person.md)|  | [optional] 

### Return type

void (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: Not defined

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
