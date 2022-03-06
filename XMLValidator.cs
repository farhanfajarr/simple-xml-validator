using System.Xml;
using System.Xml.Schema;

namespace Validator
{
    public class XMLValidator 
    {
        String xmlFilePath;
        String xsdFilePath;
        XmlReader? xmlReader;
        XmlReaderSettings settings;
        XmlSchemaSet schema;

        public XMLValidator(String xmlFilePath, String xsdFilePath) 
        {
            this.xmlFilePath = xmlFilePath;
            this.xsdFilePath = xsdFilePath;

            this.xmlReader = null;
            this.settings = new XmlReaderSettings();

            this.schema = new XmlSchemaSet();
            this.schema.Add("https://www.menu.com", this.xsdFilePath);
        }

        public void Validate()
        {
            try {
                this.settings.ValidationType = ValidationType.Schema;
                this.settings.Schemas = this.schema;
                // this.settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
                // this.settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
                this.settings.ValidationEventHandler += new ValidationEventHandler((sender, args) =>
                {
                    throw new Exception ("\r\n\tValidation Error: " + args.Message);
                });
                xmlReader = XmlReader.Create(this.xmlFilePath, this.settings);
                while (this.xmlReader.Read()) {}
                Console.WriteLine("Validation Passed");
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            } finally 
            {
                if (this.xmlReader != null)
                {
                    this.xmlReader.Close();
                }
            }
        }
    }
}