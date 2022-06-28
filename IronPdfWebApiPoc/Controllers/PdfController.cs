using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.IO;

namespace IronPdfWebApiPoc.Controllers;

[ApiController]
[Route("[controller]")]
public class PdfController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public PdfController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetPdf")]
    public ActionResult Get()
    {
        IronPdf.Installation.ChromeGpuMode = IronPdf.Engines.Chrome.ChromeGpuModes.Disabled;
        IronPdf.Installation.LinuxAndDockerDependenciesAutoConfig = false;

        IronPdf.Logging.Logger.EnableDebugging = true;
        IronPdf.Logging.Logger.LogFilePath = "Default.log"; //May be set to a directory name or full file
        IronPdf.Logging.Logger.LoggingMode = IronPdf.Logging.Logger.LoggingModes.All;

        IronPdf.Installation.Initialize();
        var Renderer = new IronPdf.ChromePdfRenderer();

        string longPdfContent = System.IO.File.ReadAllText("PDFContentSimple.html");

        using var PDF = Renderer.RenderHtmlAsPdf(longPdfContent);

        PDF.SaveAs("test.pdf");

        return Ok();
    }
}

