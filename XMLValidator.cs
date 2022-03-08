using System.Xml;
using System.Xml.Schema;

namespace Validator
{
    public class XMLValidator 
    {
        String xmlFilePath = "";
        String xsdFilePath = "";
        String targetNamespace = "";

        String xmlFilePath1 = "refxml/test1/breakfast-menu.xml";
        String xsdFilePath1 = "refxml/test1/breakfast-menu.xsd";
        String targetNamespace1 = "https://www.menu.com";

        String xmlFilePath2 = "refxml/test2/families.xml";
        String xsdFilePath2 = "refxml/test2/families.xsd";
        String targetNamespace2 = "https://www.families.com";

        public XMLValidator(String test) 
        {
            switch (test) 
            {
                case "test1" : 
                    this.xmlFilePath = this.xmlFilePath1;
                    this.xsdFilePath = this.xsdFilePath1;
                    this.targetNamespace = this.targetNamespace1;
                    break;
                case "test2" : 
                    this.xmlFilePath = this.xmlFilePath2;
                    this.xsdFilePath = this.xsdFilePath2;
                    this.targetNamespace = this.targetNamespace2;
                    break;
            }
        }

        public void ValidateTest()
        {
            // Set the XMLReaderSettings
            // - Schema type (XSD)
            // - Add the schema
            // - Add event handler (catch the error if validation is not match)
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas.Add(this.targetNamespace, this.xsdFilePath);
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

            // Exit code if error on validation
            Environment.Exit(1);
        }
    }
}
