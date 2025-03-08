# SolarMauiApp

SolarMauiApp is a .NET MAUI application that interacts with Azure Functions to retrieve solar power data. 
This application demonstrates the use of dependency injection, configuration management, and HTTP client factory in a .NET MAUI project.

## Features

- Retrieve solar power data from Azure Functions
- Display the latest power data for multiple inverters
- Use dependency injection for service management
- Manage configuration using `appsettings.json` and user secrets

## Prerequisites

- .NET 9 SDK
- .NET MAUI workload installed


## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any improvements or bug fixes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.


## Getting Started

### Clone the Repository


### Configuration

1. **appsettings.json**

Ensure you have an `appsettings.json` file in the project directory with the following structure:

```
{
  "AzureFunctions": {
    "BaseUrl": "",
    "Code": ""
  }
}
```

2. **User Secrets**

You can also use user secrets to store sensitive information. To add user secrets, right-click on the project in Visual Studio and select "Manage User Secrets". Add the following structure to the `secrets.json` file:

```
{
  "AzureFunctions": {
    "BaseUrl": "",
    "Code": ""
  }
}
```

### Build the Project

To build the project, open a terminal or command prompt and navigate to the project directory. Run the following command:

`dotnet build`

### Run the Project

To run the project, use the following command:

`dotnet run`

### Publish the Project

To publish the project for android, use the following command:

`dotnet publish -c Release -f net9-android`
