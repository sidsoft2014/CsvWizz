# CsvWizz
Simple CSV writing and reading tool. Only contains 3 commands:
  1) `CsvWriter.Write(IEnumerable<object> rows, string path, bool appendIfFileExists = true, string delimiter = ",", bool includeHeaders = true)` => Writes a collection of objects to a csv file
  2) `CsvReader.ReadToJSON(string path, string delimiter = ",")` => Reads a .csv file into a JSON string
  3) `CsvReader.ReadToObject<T>(string path, string delimiter = ",")` => Reads a .csv file and maps to given type

### Example usage:

```C#
using CsvWizz;

// Create simple dataset
var data = new MyClass[] {
  new MyClass{
    Name = "test1",
    Id = 1
  },
  new MyClass{
    Name = "test2",
    Id = 2
  }
};

// Try to write data to .csv file
if(CsvWriter.Write(data, "path\\to\\csv")){
  // Read data from .csv into JSON
  var json = CsvReader.ReadToJSON("path\\to\\csv");
  // Read data from .csv and map to class
  var model = CsvReader.ReadToObject<MyClass>("path\\to\\csv");
}
```
