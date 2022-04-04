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
        // EULYNX TEST (eulynxTrialMeggen)
        string xmlFilePath4 = "refxml/EULYNX_DP_V1.0/eulynxScheibenberg.euxml";
        string[] xsdFilePath4 = {"refxml/EULYNX_DP_V1.0/DB.xsd",
                                 "refxml/EULYNX_DP_V1.0/Generic.xsd",
                                 "refxml/EULYNX_DP_V1.0/NR.xsd",
                                 "refxml/EULYNX_DP_V1.0/ProRail.xsd",
                                 "refxml/EULYNX_DP_V1.0/RFI.xsd",
                                 "refxml/EULYNX_DP_V1.0/RsmCommon.xsd",
                                 "refxml/EULYNX_DP_V1.0/RsmNetEntity.xsd",
                                 "refxml/EULYNX_DP_V1.0/RsmSignalling.xsd",
                                 "refxml/EULYNX_DP_V1.0/RsmTrack.xsd",
                                 "refxml/EULYNX_DP_V1.0/Signalling.xsd",
                                 "refxml/EULYNX_DP_V1.0/SNCF.xsd",
                                 "refxml/EULYNX_DP_V1.0/TRV.xsd"};
        string[] targetNamespace4 = {"http://dataprep.eulynx.eu/schema/DB/1.0",
                                     "http://dataprep.eulynx.eu/schema/Generic/1.0", 
                                     "http://dataprep.eulynx.eu/schema/NR/1.0",
                                     "http://dataprep.eulynx.eu/schema/ProRail/1.0",
                                     "http://dataprep.eulynx.eu/schema/RFI/1.0",
                                     "http://www.railsystemmodel.org/schemas/Common/1.2",
                                     "http://www.railsystemmodel.org/schemas/NetEntity/1.2",
                                     "http://www.railsystemmodel.org/schemas/Signalling/1.2",
                                     "http://www.railsystemmodel.org/schemas/Track/1.2",
                                     "http://dataprep.eulynx.eu/schema/Signalling/1.0",
                                     "http://dataprep.eulynx.eu/schema/SNCF/1.0",
                                     "http://dataprep.eulynx.eu/schema/TRV/1.0"};

        // OLD EULYNX TEST (eulynxTrialMeggen)
        string xmlFilePath5 = "refxml/XSD_OLD/eulynxScheibenberg.euxml";
        string[] xsdFilePath5 = {"refxml/XSD_OLD/Eulynx_Schema/Signalling.xsd",
                                 "refxml/XSD_OLD/Eulynx_Schema/Generic.xsd",
                                 "refxml/XSD_OLD/Eulynx_Schema/DB.xsd",
                                 "refxml/XSD_OLD/RSM_Schema/Common.xsd",
                                 "refxml/XSD_OLD/RSM_Schema/NetEntity.xsd",
                                 "refxml/XSD_OLD/RSM_Schema/Signalling.xsd",
                                 "refxml/XSD_OLD/RSM_Schema/Track.xsd"};
        string[] targetNamespace5 = {"http://dataprep.eulynx.eu/schema/Signalling",
                                     "http://dataprep.eulynx.eu/schema/Generic", 
                                     "http://dataprep.eulynx.eu/schema/DB", 
                                     "http://www.railsystemmodel.org/schemas/RSM1.2beta/Common",
                                     "http://www.railsystemmodel.org/schemas/RSM1.2beta/NetEntity",
                                     "http://www.railsystemmodel.org/schemas/RSM1.2beta/Signalling",
                                     "http://www.railsystemmodel.org/schemas/RSM1.2beta/Track"};

        static List<string> logs = new List<string>();

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
                case "eulynx" :
                    this.xmlFilePath = this.xmlFilePath4;
                    this.xsdFilePath = this.xsdFilePath4;
                    this.targetNamespace = this.targetNamespace4;
                    break;
                case "oldeulynx" :
                    this.xmlFilePath = this.xmlFilePath5;
                    this.xsdFilePath = this.xsdFilePath5;
                    this.targetNamespace = this.targetNamespace5;
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
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.Schemas.Add(GenerateSchemaSet(this.targetNamespace, this.xsdFilePath));
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallback);   

            // Create the XmlReader object
            XmlReader xmlReader = XmlReader.Create(new StringReader(File.ReadAllText(this.xmlFilePath)), settings);

            // Parse the file
            while (xmlReader.Read()) {}
            
            // Shows that validation is completed
            Console.WriteLine("Validation completed");

            // Close xmlReader object
            xmlReader.Close();

            // LOGS

            // Set a variable to the Documents path.
            string docPath = Environment.CurrentDirectory;

            // Write the string array to a new file named "logs.txt".
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "logs.txt")))
            {
            foreach (string line in logs)
                outputFile.WriteLine(line);
            }
             
        }

        private static void ValidationCallback(object? sender, ValidationEventArgs args) 
        {
            string msg = String.Format(
                    Environment.NewLine + 
                    args.Severity +
                    ": Line: {0}, Position {1}: \"{2}\"",
                    args.Exception.LineNumber,
                    args.Exception.LinePosition,
                    args.Exception.Message);

            Console.WriteLine(msg);
                    
            logs.Add(msg);

            // Exit code if error on validation
            // Environment.Exit(1);
        }
    }
}
