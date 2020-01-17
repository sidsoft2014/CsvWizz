# CsvWizz
Simple CSV writing and reading tool

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
if(CsvWriter.Write(data, "path/to/csv")){
  // Read data from .csv into JSON
  var json = CsvReader.ReadToJSON("path\\to\\csv");
  // Read data from .csv and map to class
  var model = CsvReader.ReadToObject<MyClass>("path\\to\\csv");
}
```
