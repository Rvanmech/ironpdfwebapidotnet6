## IronPDF .NET 6 Demo WebApp

Creating repo to help illustrate problem of subsequent runs of IronPDF in .NET6 Debian 11 shutting down during rendering PDF.

## How to run the app in Docker:

If you are using Visual Studio, simply open the .sln file, and run the app using the docker-compose launch setting. Otherwise, in the root level of the project, do a docker-compose command.

``` script
docker-compose up
```

Navigate to the swagger page to call the PDF controller method in order to invoke the IronPDF rendering code.
Or use curl to call the api using the command line.

``` script
curl -X 'GET' \
  'http://localhost:63106/Pdf' \
  -H 'accept: */*'
```

Note: The API method will work the first time and create a PDF, but on all subsequent runs, the application will shutdown as soon as the Renderer.RenderHtmlAsPdf(...) method is invoked.

The issue is the same as this one reported in stackoverflow: https://stackoverflow.com/questions/72392631/c-sharp-ironpdf-shuts-down-net-6-web-api-under-linux-docker
