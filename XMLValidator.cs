using System.Xml;
using System.Xml.Schema;

namespace Validator
{
    public class XMLValidator 
    {
        String xmlFilePath;
        String xsdFilePath;

        public XMLValidator(String xmlFilePath, String xsdFilePath) 
        {
            this.xmlFilePath = xmlFilePath;
            this.xsdFilePath = xsdFilePath;
        }

        public void Validate()
        {
            // Set the validation settings
            // - Schema type (XSD)
            // - Add the schema
            // - Add event handler (catch the error if validation is not match)
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas.Add("https://www.menu.com", this.xsdFilePath);
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallback);     

               
            // Create the XmlReader object
            XmlReader xmlReader = XmlReader.Create(this.xmlFilePath, settings);

            // Parse the file
            while (xmlReader.Read()) {}
            
            // Code reach here if there is no validation (validation passed)
            Console.WriteLine("Validation Passed");
             
        }

        private static void ValidationCallback(object? sender, ValidationEventArgs args) 
        {
            Console.WriteLine($"Validation Error: \n {args.Message}\n");
            // Need to xmlReader.close() ?
        }
    }
}
