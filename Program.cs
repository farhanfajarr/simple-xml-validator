using Validator;

namespace MainProgram
{
    public class Program
    {
        static void Main (String[] args) 
        {

            XMLValidator xmlValidator = new XMLValidator("test1");
            xmlValidator.ValidateTest();
            
        }
    }
}
