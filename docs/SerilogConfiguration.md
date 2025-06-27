# Serilog Configuration for Log2Console

Log2Console now has excellent support for Serilog, the popular structured logging library for .NET. This guide shows you how to configure Serilog to send logs to Log2Console.

## Prerequisites

- .NET 8+ application with Serilog installed
- Log2Console .NET 8 Edition running
- Appropriate Serilog sink package installed

## UDP Configuration (Recommended)

### 1. Install the UDP Sink
```bash
dotnet add package Serilog.Sinks.Udp
```

### 2. Configure Serilog in your application
```csharp
using Serilog;
using Serilog.Formatting.Json;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.Udp("localhost", 7071, new JsonFormatter())
    .CreateLogger();

// Usage examples
Log.Information("Application started at {StartTime}", DateTime.Now);
Log.Warning("User {UserId} attempted invalid operation", 12345);
Log.Error(ex, "Database connection failed");
```

### 3. Configure Log2Console
1. Open Log2Console
2. Go to **Settings → Receivers**
3. Click **Add** → Select **UDP Receiver**
4. Set **Port**: `7071`
5. Set **Buffer Size**: `10000`
6. Click **OK** and **Start**

## TCP Configuration (More Reliable)

### 1. Install the Network Sink
```bash
dotnet add package Serilog.Sinks.Network
```

### 2. Configure Serilog
```csharp
using Serilog;
using Serilog.Formatting.Json;

Log.Logger = new LoggerConfiguration()
    .WriteTo.TCPSink("localhost", 4505, new JsonFormatter())
    .CreateLogger();
```

### 3. Configure Log2Console
1. **Settings → Receivers**
2. **Add** → **TCP Receiver**
3. Set **Port**: `4505`
4. **Start**

## File-based Configuration

### 1. Configure file output
```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/app-.log",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();
```

### 2. Configure Log2Console
1. **Settings → Receivers**
2. **Add** → **File Receiver**
3. Set **File Path**: `logs/app-{date}.log`
4. Enable **Watch File**
5. **Start**

## Advanced Configuration

### Multiple Outputs with Different Formats
```csharp
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", "MyApp")
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.File("logs/app-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7)
    .WriteTo.Udp("localhost", 7071, new JsonFormatter()) // For Log2Console
    .CreateLogger();
```

### Structured Logging Examples
```csharp
// Simple structured logging
Log.Information("User {UserId} logged in from {IPAddress}",
    userId, Request.HttpContext.Connection.RemoteIpAddress);

// Complex object logging
var orderDetails = new { OrderId = 123, CustomerId = 456, Total = 99.99m };
Log.Information("Order processed: {@OrderDetails}", orderDetails);

// Performance logging
using (LogContext.PushProperty("OperationId", Guid.NewGuid()))
{
    var sw = Stopwatch.StartNew();

    // Your operation here
    DoSomeWork();

    Log.Information("Operation completed in {ElapsedMs}ms", sw.ElapsedMilliseconds);
}
```

## ASP.NET Core Integration

### Program.cs (.NET 6+)
```csharp
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .WriteTo.Console()
        .WriteTo.Udp("localhost", 7071, new JsonFormatter()); // Log2Console
});

var app = builder.Build();

// Add request logging
app.UseSerilogRequestLogging();

app.Run();
```

### appsettings.json
```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "Console"
      },
      {
        "Name": "Udp",
        "Args": {
          "remoteAddress": "localhost",
          "remotePort": 7071,
          "formatter": "Serilog.Formatting.Json.JsonFormatter, Serilog"
        }
      },
      {
        "Name": "File",
        "Args": {
          "path": "logs/app-.log",
          "rollingInterval": "Day",
          "retainedFileCountLimit": 7
        }
      }
    ],
    "Enrich": ["FromLogContext", "WithMachineName", "WithThreadId"]
  }
}
```

## Log2Console Configuration Tips

### Viewing Structured Data
- Log2Console will display JSON-formatted logs with proper structure
- Use the **Properties** column to see structured data
- Enable **Message Details** to view full JSON payload

### Performance Optimization
- Use **UDP** for high-throughput scenarios (fire-and-forget)
- Use **TCP** when you need delivery guarantees
- Adjust **Buffer Size** in receivers based on log volume
- Consider **Bulk Processing** for high-volume scenarios

### Filtering and Searching
- Use **Level filtering** to focus on specific log levels
- **Search** in message content and properties
- **Group by** logger name or other properties
- Set up **Custom Columns** to display specific structured properties

## Troubleshooting

### Common Issues
1. **No logs appearing**: Check port numbers match between Serilog and Log2Console
2. **Partial logs**: Increase buffer size in Log2Console receiver
3. **Performance issues**: Consider using async sinks or reducing log level
4. **Firewall blocking**: Ensure Windows Firewall allows the specified ports

### Verification Steps
1. Check that Serilog is configured correctly with proper sink
2. Verify Log2Console receiver is started and listening
3. Test with simple log messages first
4. Check for any exceptions in your application logs

## Best Practices

1. **Always include structured data** for better analysis
2. **Use consistent property names** across your application
3. **Include correlation IDs** for request tracing
4. **Log at appropriate levels** (avoid Debug in production)
5. **Consider performance impact** of logging frequency
6. **Use enrichers** to add context automatically

## Example Output in Log2Console

When properly configured, you'll see:
- **Timestamp**: Exact log time with millisecond precision
- **Level**: Color-coded log levels (Info=Green, Warning=Orange, Error=Red)
- **Logger**: The source logger name or class
- **Message**: The formatted log message
- **Properties**: Structured data in expandable format
- **Exception**: Full exception details if present

This integration provides a powerful debugging and monitoring experience for .NET applications using Serilog.
