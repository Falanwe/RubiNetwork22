using System;

namespace Asynchronism
{
    public interface MyInterface { }

    public class MyClass : MyInterface
    {
        public int SomeInt { get; set; }
        public string SomeString { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyInterface myInterface = new MyClass { SomeInt = 42, SomeString = "Life, the Universe and Everything" };

            switch(myInterface)
            {
                case MyClass { SomeInt: 666, SomeString: var s1 } when s1 is not null:
                    Console.WriteLine("it's evil");
                    Console.WriteLine(s1);
                    break;
                case MyClass { SomeInt: 42, SomeString: var s2 }:
                    Console.WriteLine("it's galactic");
                    Console.WriteLine(s2);
                    break;
            }

            var (comment, s) = myInterface switch
            {
                MyClass { SomeInt: 666, SomeString: var s1 } => ("it's evil", s1),
                MyClass { SomeInt: 42, SomeString: var s1 } => ("it's galactic", s1),
                _ => throw new InvalidOperationException()
            };

            var s3 = "tartempion";
            if(DateTime.TryParse(s3, out _))
            {

            }
        }
    }
}
