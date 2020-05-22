using Foundation;

namespace CodeOnlyDemo.Data
{
    // https://github.com/xamarin/ios-samples/blob/master/FileSystemSampleCode/FileSystem/Models.cs

    // We use the Preserve attribute to ensure that all the properties of the object
    // are preserve even when the linker is ran on the assembly. The reasoning
    // for this pattern is to ensure that libraries, such as Newsoft.Json, that use
    // reflection can find properties that could be removed by the linker.
    [Preserve]
    public class GuitarEntity
    {
        #region ctor
        public GuitarEntity()
        {
        }
        #endregion

        #region Properties
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int YearIntroduced { get; set; }
        public string SmallImageUrl { get; set; }
        public string LargeImageUrl { get; set; }
        #endregion
    }
}
