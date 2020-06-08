# CCG-Technical-Test
Cloud Commerce Group C# Technical Test

## CCG.FormatConverter

The CCG.FormatConverter console application accepts input of specified format from a specified source, and converts it to a specified output format at a specified output path.

## Console Application Command Line Arguments

| Argument  | Shorthand  | Required | Description | Supported Values
|---|---|---|---|---|
| sourceFormat  | -sf  | Yes  | The format of the input data | csv |
| sourcePath  | -sp | Yes | The path to the input data | Any valid file path |
| destinationFormat  | -df | Yes | The format the input data is being converted to  | json, xml |
| destinationPath | -dp | Yes | The path of the data the output file is to be written to | Any valid file path (folder must already exist) |