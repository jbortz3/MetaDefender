# Build Instructions

The project was created with Microsoft Visual Studio 2019.

To build the project, follow the three steps listed below:
1. Load OpSwatProject.sln in Visual Studio
2. Replace the value of apiKey on line 17 of Program.cs with your API access key
3. Build the solution


# Usage Instructions

To scan a file, the user should write the command 'upload_file' followed by the name of the file (within the same directory as the executable) to upload.

Example command:

upload_file samplefile.txt

The file 'samplefile.txt' will then be uploaded and a multi-scan will be performed by OPSWAT MetaDefender. 

If the results of the file have already been cached, the upload and scan process will be skipped and the previous result will be returned to save server time.


Example Output:

filename: samplefile.txt
overall_status: No Threat Detected
engine: "AegisLab": {
  "threat_found": "",
  "scan_time": 0,
  "scan_result_i": 0,
  "def_time": "2021-04-09T07:30:39Z"
}
engine: "Ahnlab": {
  "threat_found": "",
  "scan_time": 2,
  "scan_result_i": 0,
  "def_time": "2021-04-09T00:00:00Z"
}
engine: "Antiy": {
  "threat_found": "",
  "scan_time": 0,
  "scan_result_i": 0,
  "def_time": "2021-04-01T15:48:00Z"
}
engine: "Avira": {
  "threat_found": "",
  "scan_time": 1,
  "scan_result_i": 0,
  "def_time": "2021-04-09T07:51:00Z"
}
engine: "BitDefender": {
  "threat_found": "",
  "scan_time": 4,
  "scan_result_i": 0,
  "def_time": "2021-04-09T03:21:00Z"
}
engine: "ByteHero": {
  "threat_found": "",
  "scan_time": 687,
  "scan_result_i": 0,
  "def_time": "2021-04-06T00:00:00Z"
}
engine: "ClamAV": {
  "threat_found": "",
  "scan_time": 17,
  "scan_result_i": 0,
  "def_time": "2021-04-08T11:08:38Z"
}
engine: "Comodo": {
  "threat_found": "",
  "scan_time": 18,
  "scan_result_i": 0,
  "def_time": "2021-04-09T04:40:39Z"
}
engine: "CrowdStrike Falcon ML": {
  "threat_found": "",
  "scan_time": 0,
  "scan_result_i": 23,
  "def_time": "2021-04-09T00:00:00Z"
}
engine: "Cyren": {
  "threat_found": "",
  "scan_time": 14,
  "scan_result_i": 0,
  "def_time": "2021-04-09T08:14:00Z"
}
engine: "ESET": {
  "threat_found": "",
  "scan_time": 0,
  "scan_result_i": 0,
  "def_time": "2021-04-09T00:00:00Z"
}
engine: "Emsisoft": {
  "threat_found": "",
  "scan_time": 22,
  "scan_result_i": 0,
  "def_time": "2021-04-09T03:37:00Z"
}
engine: "F-prot": {
  "threat_found": "",
  "scan_time": 0,
  "scan_result_i": 0,
  "def_time": "2021-01-25T02:30:00Z"
}
engine: "Filseclab": {
  "threat_found": "",
  "scan_time": 845,
  "scan_result_i": 0,
  "def_time": "2021-04-04T22:56:00Z"
}
engine: "Fortinet": {
  "threat_found": "",
  "scan_time": 178,
  "scan_result_i": 0,
  "def_time": "2021-04-07T00:00:00Z"
}
engine: "Hauri": {
  "threat_found": "",
  "scan_time": 0,
  "scan_result_i": 0,
  "def_time": "2021-04-09T00:00:00Z"
}
engine: "Huorong": {
  "threat_found": "",
  "scan_time": 0,
  "scan_result_i": 0,
  "def_time": "2021-04-08T09:48:00Z"
}
engine: "Ikarus": {
  "threat_found": "",
  "scan_time": 0,
  "scan_result_i": 0,
  "def_time": "2021-04-09T07:48:58Z"
}
engine: "Jiangmin": {
  "threat_found": "",
  "scan_time": 3795,
  "scan_result_i": 0,
  "def_time": "2021-04-05T19:40:00Z"
}
engine: "K7": {
  "threat_found": "",
  "scan_time": 0,
  "scan_result_i": 0,
  "def_time": "2021-04-09T05:58:00Z"
}
engine: "Kaspersky": {
  "threat_found": "",
  "scan_time": 2,
  "scan_result_i": 0,
  "def_time": "2021-04-09T03:54:00Z"
}
engine: "McAfee": {
  "threat_found": "",
  "scan_time": 1,
  "scan_result_i": 0,
  "def_time": "2021-04-08T00:00:00Z"
}
engine: "NANOAV": {
  "threat_found": "",
  "scan_time": 1,
  "scan_result_i": 0,
  "def_time": "2021-04-09T04:28:00Z"
}
engine: "Preventon": {
  "threat_found": "",
  "scan_time": 1106,
  "scan_result_i": 0,
  "def_time": "2021-04-09T01:42:00Z"
}
engine: "Quick Heal": {
  "threat_found": "",
  "scan_time": 1,
  "scan_result_i": 0,
  "def_time": "2021-04-08T19:05:00Z"
}
engine: "RocketCyber": {
  "threat_found": "",
  "scan_time": 2,
  "scan_result_i": 23,
  "def_time": "2021-04-09T00:00:00Z"
}
engine: "Sophos": {
  "threat_found": "",
  "scan_time": 11,
  "scan_result_i": 0,
  "def_time": "2021-04-09T01:42:40Z"
}
engine: "SUPERAntiSpyware": {
  "threat_found": "",
  "scan_time": 3066,
  "scan_result_i": 0,
  "def_time": "2021-04-08T20:06:00Z"
}
engine: "TACHYON": {
  "threat_found": "",
  "scan_time": 5,
  "scan_result_i": 0,
  "def_time": "2021-04-09T00:00:00Z"
}
engine: "TrendMicro": {
  "threat_found": "",
  "scan_time": 2358,
  "scan_result_i": 0,
  "def_time": "2021-04-08T20:22:00Z"
}
engine: "TrendMicro House Call": {
  "threat_found": "",
  "scan_time": 2788,
  "scan_result_i": 0,
  "def_time": "2021-04-07T20:38:00Z"
}
engine: "VirusBlokAda": {
  "threat_found": "",
  "scan_time": 0,
  "scan_result_i": 0,
  "def_time": "2021-04-09T08:59:00Z"
}
engine: "Webroot SMD": {
  "threat_found": "",
  "scan_time": 1,
  "scan_result_i": 23,
  "def_time": "2021-04-08T21:00:12Z"
}
engine: "Windows Defender": {
  "threat_found": "",
  "scan_time": 77,
  "scan_result_i": 0,
  "def_time": "2021-04-09T04:46:18Z"
}
engine: "Xvirus Personal Guard": {
  "threat_found": "",
  "scan_time": 49,
  "scan_result_i": 0,
  "def_time": "2021-04-07T10:17:25Z"
}
engine: "Zillya!": {
  "threat_found": "",
  "scan_time": 1,
  "scan_result_i": 0,
  "def_time": "2021-04-09T07:31:00Z"
}
engine: "Vir.IT eXplorer": {
  "threat_found": "",
  "scan_time": 1,
  "scan_result_i": 0,
  "def_time": "2021-04-08T12:40:00Z"
}


Credits:
This project uses Newtonsoft's JSON.NET framework for JSON parsing.
