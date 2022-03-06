using Validator;

namespace MainProgram
{
    public class Program
    {
        static void Main (String[] args) 
        {

            String xmlFilepath = "C:/Users/farha/Documents/git/simple-xml-validator/refxml/test1/breakfast-menu.xml";
            String xsdFilePath = "C:/Users/farha/Documents/git/simple-xml-validator/refxml/test1/breakfast-menu.xsd";

            XMLValidator xmlValidator = new XMLValidator(xmlFilepath, xsdFilePath);
            xmlValidator.Validate();
            
        }
    }
}