using Validator;

namespace MainProgram
{
    public class Program
    {
        static void Main (String[] args) 
        {

            XMLValidator xmlValidator = new XMLValidator("oldeulynx");
            xmlValidator.ValidateTest();
            
        }
    }
}
