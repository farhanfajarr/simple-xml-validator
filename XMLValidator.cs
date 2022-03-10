using System.Xml;
using System.Xml.Schema;

namespace Validator
{
    public class XMLValidator 
    {
        string xmlFilePath = "";
        string[] xsdFilePath = {};
        string[] targetNamespace = {};

        // TEST1 (Breakfast Menu)
        string xmlFilePath1 = "refxml/test1/breakfast-menu.xml";
        string[] xsdFilePath1 = {"refxml/test1/breakfast-menu.xsd"};
        string[] targetNamespace1 = {"https://www.menu.com"};
        // TEST2 (One side family tree)
        string xmlFilePath2 = "refxml/test2/families.xml";
        string[] xsdFilePath2 = {"refxml/test2/families.xsd"};
        string[] targetNamespace2 = {"https://www.families.com"};
        // TEST3 (Employees with many schema)
        string xmlFilePath3 = "refxml/test3/employees.xml";
        string[] xsdFilePath3 = {"refxml/test3/employees.xsd",
                                 "refxml/test3/employee.xsd",
                                 "refxml/test3/contact.xsd",
                                 "refxml/test3/person.xsd",
                                 "refxml/test3/company.xsd",
                                 "refxml/test3/location.xsd"};
        string[] targetNamespace3 = {"https://www.refxml.com/test3/employees",
                                     "https://www.refxml.com/test3/employee",
                                     "https://www.refxml.com/test3/contact",
                                     "https://www.refxml.com/test3/person",
                                     "https://www.refxml.com/test3/company",
                                     "https://www.refxml.com/test3/location"};

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
                case "test3" :
                    this.xmlFilePath = this.xmlFilePath3;
                    this.xsdFilePath = this.xsdFilePath3;
                    this.targetNamespace = this.targetNamespace3;
                    break; 
            }
        }


        public XmlSchemaSet GenerateSchemaSet(string[] tNs, string[] schemaPaths)
        {
            // Create the XmlSchemaSet class
            XmlSchemaSet schemaSet = new XmlSchemaSet();

            // Add the schemas to the collection
            for (int i = 0; i < tNs.Length; i++)
            {
               schemaSet.Add(tNs[i], schemaPaths[i]); 
            }

            return schemaSet;
        }

        public void ValidateTest()
        {
            // Set the XMLReaderSettings
            // - Schema type (XSD)
            // - Generate XmlSchemaSet and add them to settings
            // - Add event handler (catch the error if validation is not match)
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas.Add(GenerateSchemaSet(this.targetNamespace, this.xsdFilePath));
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
