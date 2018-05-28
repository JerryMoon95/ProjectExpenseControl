using Rotativa.MVC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using ProjectExpenseControl.CustomAuthentication;

namespace ProjectExpenseControl.Controllers
{
    
    public class CreatePDFController : Controller
    {
        
        public ActionResult CreateInvoicePDF(bool pdf = false)
        {
            Comprobante oComprobante;
            string pathXML = Server.MapPath("~") + "/xml/archivoXML4.xml";
            XmlSerializer oSerializer = new XmlSerializer(typeof(Comprobante));
            using (StreamReader reader = new StreamReader(pathXML))
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
            var report = new ActionAsPdf("CreateInvoicePDF", new { pdf = true }) { FileName="Factura.pdf"};
            return report;
        }
    }
}