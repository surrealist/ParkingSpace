using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using System.IO;

namespace ParkingSpace.Facts.Sample {
  public class SampleFileFacts {

    public static IEnumerable<object[]> getFilesFromCurrentFolder() {
      var di = new DirectoryInfo(Environment.CurrentDirectory);
      foreach(var file in di.EnumerateFiles()) {
        yield return new object[] { file.Name };
      }
    }
    
    [Trait("Category", "Sample")]
    [Theory]
    [MemberData("getFilesFromCurrentFolder")]
    //[InlineData("sample1.txt")]
    //[InlineData("sample20.exe")]
    public void FileNameMustHasThreeCharactersExtension(string fileName) {
      var length = Path.GetExtension(fileName).Length;

      Assert.Equal(4, length); // include 'dot' in front of the extension (.dll, .exe)
    }
  }
}
