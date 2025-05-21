

# ScannerApi

ScannerApi is a C#-based middleware API designed to interface with scanning devices, providing a seamless integration layer for applications requiring scanning capabilities. It abstracts the complexities of direct scanner interactions, offering a unified and straightforward API for developers.

## Features

* **Unified Scanner Interface**: Interact with various scanner models through a consistent API.
* **Asynchronous Operations**: Supports non-blocking scanning processes to enhance application responsiveness.
* **Configurable Settings**: Customize scanning parameters such as resolution, color mode, and file format.
* **Error Handling**: Robust mechanisms to capture and relay scanner errors and statuses.
* **Extensible Architecture**: Designed with modularity in mind, allowing easy addition of new scanner drivers or features.

## Getting Started

### Prerequisites

* .NET 6.0 SDK or later
* Compatible scanning device with appropriate drivers installed

### Installation

1. **Clone the repository**:

   ```bash
   git clone https://github.com/MurtadhaJasim/ScannerApi.git
   ```



2. **Navigate to the project directory**:

   ```bash
   cd ScannerApi
   ```



3. **Build the project**:

   ```bash
   dotnet build
   ```



4. **Run the API**:

   ```bash
   dotnet run
   ```



## Usage

Once the API is running, you can interact with it using HTTP requests. Below are some example endpoints:

* **Initiate a Scan**:

```http
  POST /api/scan
```



**Request Body**:

```json
  {
    "resolution": 300,
    "colorMode": "Color",
    "format": "PDF"
  }
```



* **Retrieve Scan Status**:

```http
  GET /api/scan/status/{scanId}
```



* **Download Scanned File**:

```http
  GET /api/scan/download/{scanId}
```



## Configuration

Configuration settings can be adjusted in the `appsettings.json` file:

```json
{
  "ScannerSettings": {
    "DefaultResolution": 300,
    "DefaultColorMode": "Color",
    "SupportedFormats": [ "PDF", "JPEG", "PNG" ]
  }
}
```



## Contributing

Contributions are welcome! Please fork the repository and submit a pull request for any enhancements or bug fixes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

