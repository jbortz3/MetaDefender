# Build Instructions

The project was created with Microsoft Visual Studio 2019.

To build the project, simply load OpSwatProject.sln in Visual Studio, and build the solution. 


# Usage Instructions

To scan a file, the user should write the command 'upload_file' followed by the name of the file (within the same directory as the executable) to upload.

Example command:

upload_file samplefile.txt

The file 'samplefile.txt' will then be uploaded and a multi-scan will be performed by OPSWAT MetaDefender. 

If the results of the file have already been cached, the upload and scan process will be skipped and the previous result will be returned to save server time.


Credits:
This project uses Newtonsoft's JSON.NET framework for JSON parsing.
