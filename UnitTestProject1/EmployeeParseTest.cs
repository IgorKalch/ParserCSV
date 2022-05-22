using CompanyTree.Exceptions;
using CompanyTree.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading.Tasks;

namespace UnitTestCompanyTree
{
    [TestClass]
    public class EmployeeParseTest
    {
        [TestMethod]
        public async Task ShouldParseFile()
        {
            var filePath = Path.Combine("TestData", "Emploeyee.txt");
                        
            var employeeParse = await EmployeeParser.ReadFileAsync(filePath);

            Assert.IsNotNull(employeeParse);
            Assert.AreEqual(4, employeeParse.Count);
        }

        [TestMethod]
        public async Task ShouldThrowErrorOnInvalidId()
        {
           var filePath = Path.Combine("TestData", "InvalidId.txt");

           await Assert.ThrowsExceptionAsync<InvalidRowDataException>(async () => await EmployeeParser.ReadFileAsync(filePath));
        }
    }   
}
