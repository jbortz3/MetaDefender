using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OPSwatProject
{
    class Program
    {
        //API Access Key In String Format
        static string apiKey   = "**********************";

        //JObject used to store the final API response
        JObject scanJson = new JObject(); 

        //Stored response of the initial hash check
        System.Net.HttpStatusCode hashResponse = System.Net.HttpStatusCode.NotFound;

        //using a single static HttpClient instance to prevent waste
        static readonly HttpClient client = new HttpClient();


        /************************************************************************
        * FUNCTION :       GetHashResponse
        *
        * DESCRIPTION :
        *       Checks the API's response for the file hash, and stores
        *       the JSON data if it is found
        *
        *  INPUTS:  string hash : The hash to check
        *
        ************************************************************************/

        private async Task GetHashResponse(string hash)
        {
            //make the request and capture the response
            HttpResponseMessage message = await client.GetAsync("https://api.metadefender.com/v4/hash/" + hash);
            string response             = await message.Content.ReadAsStringAsync();

            //store the status code and confirm the request processed okay
            hashResponse = message.StatusCode;
            if (!requestProcessedSuccessfully(message))
                return;

            //store the JSON data if the hash is successfully found
            scanJson = JObject.Parse(response);
        }

        /************************************************************************
        * FUNCTION :       UploadFile
        *
        * DESCRIPTION :
        *       Uploads the specified file and begins polling the data id
        *
        *  INPUTS:  string fileName : The name of the same-directory file to
        *                             be uploaded
        *
        ************************************************************************/

        private async Task UploadFile(string fileName)
        {
            //Set up the request
            string dir  = Directory.GetCurrentDirectory() + "\\" + fileName;
            var values  = new Dictionary<string, string> { { "filename", dir } };
            var content = new FormUrlEncodedContent(values);

            content.Headers.ContentType.MediaType = "application/octet-stream";
            Console.WriteLine("Uploading file...");

            //Send the request and capture the response
            HttpResponseMessage message = await client.PostAsync("https://api.metadefender.com/v4/file", content);
            string response             = await message.Content.ReadAsStringAsync();

            //Confirm the request processed okay
            if (!requestProcessedSuccessfully(message))
                return;

            //grab the data_id from the response and begin the polling process
            dynamic obj    = JsonConvert.DeserializeObject(response);
            string data_id = obj.data_id;
            await PollDataID(data_id);
        }

        /************************************************************************
        * FUNCTION :       PollDataID
        *
        * DESCRIPTION :
        *       Performs polling on the specified data id until the scan
        *       is complete. Stores the resulting JSON in scanJson.
        *
        *  INPUTS:  string data_id : The data ID to poll
        *
        *
        ************************************************************************/

        private async Task PollDataID(string data_id)
        {
            //Send a GET request on a timed delay until scan is complete
            while (true)
            {
                //Send the request and capture the response
                HttpResponseMessage message = await client.GetAsync("https://api.metadefender.com/v4/file/" + data_id);
                string response             = await message.Content.ReadAsStringAsync();

                //Confirm the request processed okay
                if (!requestProcessedSuccessfully(message))
                    return;

                //Grab the scan progress from the response
                dynamic obj  = JsonConvert.DeserializeObject(response);
                int progress = obj.scan_results.progress_percentage;

                //If scan is complete, store the JSON object and end the process
                if (progress == 100)
                {
                    scanJson = JObject.Parse(response);
                    break;
                }

                //Print the scan progress and wait another 500 ms
                Console.WriteLine("Scan progress: " + progress + "%");
                await Task.Delay(500);
            }
        }

        /************************************************************************
        * FUNCTION :       displayResults
        *
        * DESCRIPTION :
        *       Displays the scan results to the console window
        *       
        * INPUTS:  string fileName : The name of the file scanned   
        *
        *
        ************************************************************************/

        private void displayResults(string fileName)
        {
            //grab the overall scan result
            dynamic obj = scanJson;
            string overall_result = obj.scan_results.scan_all_result_a;

            //print the file's name and the overall status of the scan
            Console.WriteLine("\nfilename: "     + fileName);
            Console.WriteLine("overall_status: " + overall_result);

            //prints the result of each engine's individual scan
            foreach(var result in scanJson["scan_results"]["scan_details"].Children().ToList())
                Console.WriteLine("engine: " + result.ToString());
        }

        /************************************************************************
        * FUNCTION :       calculateMD5FileHash
        *
        * DESCRIPTION :
        *       Calculates and returns the MD5 hash for file 'fileName' located
        *       within the same directory as the executable
        *
        *  INPUTS:  string fileName : The name of the same-directory file for scanning
        *
        *  OUTPUTS: string : the calculated MD5 hash in string format
        *
        ************************************************************************/

        private string calculateMD5FileHash(string fileName)
        {
            using (var md5 = MD5.Create())
            {
                try
                {
                    //calculate the hash in byte[] format
                    var stream       = File.OpenRead(fileName);
                    byte[] hashBytes = md5.ComputeHash(stream);

                    //convert the hash to string format and return
                    StringBuilder toString = new StringBuilder(hashBytes.Length * 2);

                    for (int i = 0; i < hashBytes.Length; i++)
                        toString.Append(hashBytes[i].ToString(false ? "X2" : "x2"));

                    return toString.ToString();
                }
                catch (System.IO.FileNotFoundException)
                {
                    //The file was not found within the executable directory
                    Console.WriteLine("Unable to load file '" + fileName + "' from the executable directory");
                    Console.WriteLine("Please ensure the file is available and try again.");
                    return "";
                }
            }
        }

        /************************************************************************
        * FUNCTION :       requestProcessedSuccessfully
        *
        * DESCRIPTION :
        *       Checks a given message's response code and alerts the user
        *       to basic errors; Returns true if okay.
        *
        *  INPUTS:  HttpResponseMessage message : The HTTP response to check
        *
        *  OUTPUTS: bool : true if the message indicates an okay response
        *
        ************************************************************************/

        private bool requestProcessedSuccessfully(HttpResponseMessage message)
        {
            //check the status code and alert the user to some standard errors
            switch (message.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return true;
                case System.Net.HttpStatusCode.Unauthorized:
                    Console.WriteLine("Error: Bad API Key, please verify credentials");
                    return false;
                case System.Net.HttpStatusCode.BadRequest:
                    Console.WriteLine("Error: Bad Request");
                    return false;
                case System.Net.HttpStatusCode.InternalServerError:
                    Console.WriteLine("Error: Internal Server Error, please try again later");
                    return false;
                default:
                    return false;
            }
        }

        static async Task Main(string[] args)
        {
            Program program = new Program();
            client.DefaultRequestHeaders.Add("apikey", apiKey); // add the API access key as a header
            string fileName;

            //First Prompt the user for a command, and continue until a file name is entered
            //Example: 'upload_file samplefile.txt'
            Console.WriteLine("Enter a command: (upload_file *fileName*)");
            while (true)
            {
                string command = Console.ReadLine();
                var spl = command.Split(" ");
                if (spl.Length > 1 && spl[0] == "upload_file")
                {
                    fileName = spl[1];
                    break;
                }
                Console.WriteLine("Invalid Command!");
                Console.WriteLine("Proper Format: 'upload_file *fileName*' where *fileName* exists in the same directory as the executable");
            }


            //Calculate the MD5 hash of samplefile.txt
            string hash = program.calculateMD5FileHash(fileName);
            if (hash == "")
                return; //File not found

            //Perform a hash lookup against the API and check if there are cached results
            await program.GetHashResponse(hash);

            //If cached results are not found, proceed to file upload
            if (program.hashResponse == System.Net.HttpStatusCode.NotFound)
            {
                //upload the file and await scan completion
                await program.UploadFile(fileName);

                //Display the results in the specified format
                program.displayResults(fileName);
            }
            else if (program.hashResponse == System.Net.HttpStatusCode.OK)
            {
                //Display the results in the specified format
                program.displayResults(fileName);
            }
        }

    }
}
