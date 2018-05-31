using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using ProjectExpenseControl.CustomAuthentication;
using Rotativa;
using Rotativa.Options;

namespace ProjectExpenseControl.Controllers
{
    
    public class CreatePDFController : Controller
    {
        private static string path;

        public ActionResult CreateInvoicePDF(string pathXML, bool pdf = false)
        {
            Comprobante oComprobante;
            //string pathXML = Server.MapPath("~") + "/xml/archivoXML4.xml";
            if (!string.IsNullOrEmpty(pathXML))
            {
                path = pathXML;
            }
            XmlSerializer oSerializer = new XmlSerializer(typeof(Comprobante));
            using (StreamReader reader = new StreamReader(path))
            {
                oComprobante = (Comprobante)oSerializer.Deserialize(reader);
                var b = oComprobante.SubTotal;
                foreach (var oComplemento in oComprobante.Complemento)
                {
                    foreach (var oComplementoInterior in oComplemento.Any)
                    {
                        if (oComplementoInterior.Name.Contains("TimbreFiscalDigital"))
                        {
                            XmlSerializer oSerializerComplemento = new XmlSerializer(typeof(TimbreFiscalDigital));
                            using (var readerComplemento = new StringReader(oComplementoInterior.OuterXml))
                            {
                                oComprobante.TimbreFiscalDigital = (TimbreFiscalDigital)oSerializerComplemento.Deserialize(readerComplemento);
                            }
                        }
                    }
                }
            }

            ViewBag.PDF = pdf;

            return View(oComprobante);
        }

        public ActionResult PrintAllReport()
        {
            return new ActionAsPdf("CreateInvoicePDF", new { pdf = true })
            {
                FileName = "Factura.pdf",
                PageSize = Size.Letter,
                PageOrientation = Orientation.Portrait
            };
        }
    }
}