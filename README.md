# Our.Umbraco.OpenKeyValue
### A Service To Access Umbraco's Key-Value Table ###
A simple service for CRUD operations on the umbracoKeyValue database table.

### Getting Started ###
Nuget Package: ` Install-Package Our.Umbraco.OpenKeyValue `



### Usage ###

```
using Our.Umbraco.OpenKeyValue.Core.Services;
```


IOpenKeyValueService is already registered in Umbraco, so it will be automatically injected into your controllers and services etc.

```
public HomeController(IOpenKeyValueService service)
{
	_service = service;
}
```


```
// insert
var item = _service.Set(key, value);

// exists
bool exists = _service.Exists(key);

// get 
var retrievedItem = _service.Get(key);

// update
var updatedItem = _service.Set(key, $"{value}_updated");

// delete
_service.Delete(key);


```



login: test@test.com
password: test@test.com