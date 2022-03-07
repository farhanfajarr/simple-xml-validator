using Validator;

namespace MainProgram
{
    public class Program
    {
        static void Main (String[] args) 
        {

            String xmlFilepath = "refxml/test0/breakfast-menu.xml";
            String xsdFilePath = "refxml/test0/breakfast-menu.xsd";

            XMLValidator xmlValidator = new XMLValidator(xmlFilepath, xsdFilePath);
            xmlValidator.Validate();
            
        }
    }
}
