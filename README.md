![ServiceBricks Logo](https://raw.githubusercontent.com/holomodular/ServiceBricks/main/Logo.png) 

[![NuGet version](https://badge.fury.io/nu/ServiceBricks.Logging.Microservice.svg)](https://badge.fury.io/nu/ServiceBricks.Logging.Microservice)
![badge](https://img.shields.io/endpoint?url=https://gist.githubusercontent.com/holomodular-support/2214c3b2c703476cbafeb647eb655fc8/raw/servicebrickslogging-codecoverage.json)
[![License: MIT](https://img.shields.io/badge/License-MIT-389DA0.svg)](https://opensource.org/licenses/MIT)

# ServiceBricks Logging Microservice

## Overview

This repository contains the logging microservice built using the ServiceBricks foundation.
The logging microservice provides logging support for any hosted application using the Microsoft.Extensions.Logging library.
It provides web request logging functionality, so you can audit all web requests coming into the application.


## Data Transfer Objects

### LogMessageDto - Admin Policy
Used to store application diagnositics.

```csharp

public class LogMessageDto : DataTransferObject
{
    public virtual DateTimeOffset CreateDate { get; set; }
    public virtual string Application { get; set; }
    public virtual string Server { get; set; }
    public virtual string Category { get; set; }
    public virtual string UserStorageKey { get; set; }
    public virtual string Path { get; set; }
    public virtual string Level { get; set; }
    public virtual string Message { get; set; }
    public virtual string Exception { get; set; }
    public virtual string Properties { get; set; }
}

```

#### Business Rules

* DomainCreateDateRule - CreateDate property

### WebRequestMessageDto - Admin Policy
Used to store web request auditing messages.

```csharp

 public class WebRequestMessageDto : DataTransferObject
 {
     public DateTimeOffset CreateDate { get; set; }
     public string RequestIPAddress { get; set; }
     public string RequestProtocol { get; set; }
     public string RequestScheme { get; set; }
     public string RequestMethod { get; set; }
     public string RequestBody { get; set; }
     public string RequestPath { get; set; }
     public string RequestPathBase { get; set; }
     public string RequestQueryString { get; set; }
     public string RequestQuery { get; set; }
     public string RequestRouteValues { get; set; }
     public string RequestHost { get; set; }
     public bool? RequestHasFormContentType { get; set; }
     public string RequestCookies { get; set; }
     public string RequestContentType { get; set; }
     public long? RequestContentLength { get; set; }
     public string RequestHeaders { get; set; }
     public bool? RequestIsHttps { get; set; }
     public string RequestUserId { get; set; }
     public int? ResponseStatusCode { get; set; }
     public string ResponseHeaders { get; set; }
     public string ResponseCookies { get; set; }
     public string ResponseContentType { get; set; }
     public long? ResponseContentLength { get; set; }
     public long? ResponseTotalMilliseconds { get; set; }
     public string ResponseBody { get; set; }
 }

```

#### Business Rules

* DomainCreateDateRule - CreateDate property


## Background Tasks and Timers

### LoggingWriteMessageTimer class
This background timer runs executes the LoggingWriteMessageTask.

[View Source](https://github.com/holomodular/ServiceBricks-Logging/blob/main/src/V1/ServiceBricks.Logging/BackgroundTask/LoggingWriteMessageTimer.cs)

### LoggingWriteMessageTask class
This background task pulls records off of custom logger inmemory queue and writes them to the ILogMessageAPIService.

[View Source](https://github.com/holomodular/ServiceBricks-Logging/blob/main/src/V1/ServiceBricks.Logging/BackgroundTask/LoggingWriteMessageTask.cs)

## Events
None

## Middleware

### LogMessageMiddleware
This middleware is responsible for plugging into the pipeline and storing minimal HttpRequest information (such as user, request, path, etc) to store along with log messages.

In your program.cs file, add the custom logger with **AddServiceBricksLogging()** in the ConfigureLogging section.
```csharp

    .ConfigureLogging((hostingContext, logging) =>
    {
        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
        logging.AddConsole();
        logging.AddDebug();
        logging.AddServiceBricksLogging();
    })

```


The following code should be added to your web application's startup. This middleware should be added to the pipeline after UseAuth() so that any authenticated user is available.
```csharp
  app.UseMiddleware<LogMessageMiddleware>();
```

[View Source](https://github.com/holomodular/ServiceBricks-Logging/blob/main/src/V1/ServiceBricks.Logging/Middleware/LogMessageMiddleware.cs)

### WebRequestMessageMiddleware class
This middleware is responsible for plugging into the pipeline and pulling the HttpRequest and HttpResponse properties of the current web request and storing them for auditing purposes.

The following code should be added to your web application's startup. 
This middleware should be added to the pipeline after UseAuth(), so that any authenticated user is available.
```csharp
  app.UseMiddleware<WebRequestMessageMiddleware>();
```
[View Source](https://github.com/holomodular/ServiceBricks-Logging/blob/main/src/V1/ServiceBricks.Logging/Middleware/WebRequestMessageMiddleware.cs)

## Processes
None

## Service Bus

### CreateApplicationLogBroadcast
This microservice subscribes to the CreateApplicationLogBroadcast message.
It is associated to the [CreateApplicationLogRule](https://github.com/holomodular/ServiceBricks-Logging/blob/main/src/V1/ServiceBricks.Logging/Rule/CreateApplicationLogRule.cs) Business Rule.
When receiving the message, it will simply create a record in storage.

```csharp

public class CreateApplicationLogBroadcast : DomainBroadcast<ApplicationLogDto>
{
    public CreateApplicationLogBroadcast(ApplicationLogDto obj)
    {
        DomainObject = obj;
    }
}

```

## Additional
None

## Application Settings

```json
{
  // System default logging option
  "Logging": {
    "LogLevel": {
      // Specify custom logging levels for components here
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.Hosting.Lifetime": "Warning"
    }
  },

  // ServiceBricks Settings
  "ServiceBricks": {

    // Logging Microservice Settings
    "Logging": {

      // WebRequestMessageMiddleware Settings
      "WebRequestMessage": {
      "EnableLogging": false,
      "EnableExceptions": true,
      "EnableUserStorageKey": true,
      "EnableRequestIPAddress": true,
      "EnableRequestBody": true,
      "EnableRequestBodyOnError": true,
      "EnableRequestProtocol": true,
      "EnableRequestScheme": true,
      "EnableRequestMethod": true,
      "EnableRequestPath": true,
      "EnableRequestPathBase": true,
      "EnableRequestQueryString": true,
      "EnableRequestQuery": true,
      "EnableRequestRouteValues": true,
      "EnableRequestHost": true,
      "EnableRequestHasFormContentType": true,
      "EnableRequestCookies": true,
      "EnableRequestContentType": true,
      "EnableRequestContentLength": true,
      "EnableRequestHeaders": true,
      "EnableRequestIsHttps": true,
      "EnableResponseStatusCode": true,
      "EnableResponseHeaders": true,
      "EnableResponseCookies": true,
      "EnableResponseContentType": true,
      "EnableResponseContentLength": true,
      "EnableResponseTotalMilliseconds": true,
      "EnableResponseBody": true,
      "EnableExcludeRequestPathsRegExExpressions": false,
      "ExcludeRequestPaths": [
        //"/css/",
        //"/img/",
        //"/js/",
      ],
      "EnableExcludeIpAddressesRegExExpressions": false,
      "ExcludeIpAddresses": [
        // "127.0.0.1",
        // "::1"
      ]
    }
   }
  }
}
```



# About ServiceBricks

ServiceBricks is the cornerstone for building a microservices foundation.
Visit https://ServiceBricks.com to learn more.

