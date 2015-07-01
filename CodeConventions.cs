using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///////////////////////////////////////////////////////////////////////////////////////////////////
//     CODING CONVENTIONS
///////////////////////////////////////////////////////////////////////////////////////////////////
/// This file will lay out code conventions designed to help keep the code manageable.
/// Misc Notes:
/// 1. Use standard Visual Studio tab indentation unless noted otherwise in this file.
///////////////////////////////////////////////////////////////////////////////////////////////////

class CodingConventions
{
    CodingConventions()
    { }

    // 1. Code lines should be no longer than 100 characters (the length of the above line). If a 
    //    line containing a list of parameters goes over 100 characters, each parameter should 
    //    either be on a new line, indented to match the first paramater as seen below, or multiple 
    //    lines indented to each be less than 100 lines.
    // 2. Arguments should be sorted by tileType. Primitives first, complex types last.
    //NO
    private static void ReallyReallyReallyLongMethod(int param1, string param2, int param3, string param4, int param5)
    { }

    // YES
    private static void ReallyReallyReallyLongMethod(int param1,
                                                     int param2,
                                                     int param3,
                                                     string param4,
                                                     string param5)
    { }

    private static void ReallyReallyReallyLongMethod(int param1, int param2, int param3, 
                                                     int param4, int param5, string param6, 
                                                     string param7, List<int> param8)
    { }

    // If possible braceless if statements should be used (except for multiple heirarchical 
    // conditions, doing these braceless makes it incredibly easy to break code unknowingly). If 
    // this is not possible, have the opening brace on the if line, and the closing on a seperate 
    // line. This allows for easy code extensions.
    private void IllustratingIfs()
    {
        // NO
        if (1 < 3)
            if (2 < 4)
                return;

        // YES
        if (1 < 3){
            if (2 < 3)
                return;
        }
    }

    // Methods
    // 1. Methods should be named with Camel/Pascal case.
    // 2. Opening braces should be on the line below the declaration.
    // 3. Closing braces should be on a seperate line to make code easily extensible.
    // 4. All methods should use XML style comments.
    //     4.1. The closing summary tag should be on the final line of the summary, not on its own 
    //          line.
    //     4.2. XML documentation lines that go over 100 characters should be broken into two lines
    //          with the second line indented to line up with the end of that comments opening tag.
    
    // NO
    // Returns 1.
    private int methodOne(){
        return 1;}

    /// <summary>
    /// Adds two numbers.
    /// </summary>
    /// <param name="param1">This is an uneccesssiraly long line used just to state that the param1 
    ///                      is the first number and illustrate handling long comments.</param>
    /// <param name="param2">The second number.</param>
    /// <returns>The sum.</returns>
    private int MethodTwo(int param1, int param2)
    {
        return param1 + param2;
    }
    
    // Variables
    // 1. Class level variables should use Pascal/Camel casing.
    // 2. Private variables should have a standard comment illustrating use if its usage is not 
    //    obvious.
    // 3. Public class level variables should have XML documentation.

    // NO
    // This is an integer.
    public int anInteger = 1;

    // YES
    /// <summary>
    /// This is a public integer.</summary>
    public int AnInteger =1;

    // This is a private integer.
    private int AnInteger2 = 1;
}