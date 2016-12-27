using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Threading.Tasks;
namespace FRCSB.FRC
{
    class FRCFiles
    {
        public static async void Serialize(object obj,string name="")
        {
            try
            {
                string jsonContents = JsonConvert.SerializeObject(obj);
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                string filename = name+GlobalSettings.year+ ".json";
                StorageFile textFile = await localFolder.CreateFileAsync(filename,
                                             CreationCollisionOption.ReplaceExisting);
                // Open the file...      
                using (IRandomAccessStream textStream = await textFile.OpenAsync(FileAccessMode.ReadWrite))
                {
                    // write the JSON string!
                    using (DataWriter textWriter = new DataWriter(textStream))
                    {
                        textWriter.WriteString(jsonContents);
                        await textWriter.StoreAsync();
                       
                    }
                }
            }
            catch
            {
                DebuggerClass.reportError("error saving to file");
            }
        }
      
        public static async Task<T> Deserialize<T>(string name="")
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            T val;
            string file = name + GlobalSettings.year+ ".json";
            try
            {
               
                // Getting JSON from file if it exists, or file not found exception if it does not  
                
                StorageFile textFile = await localFolder.GetFileAsync(file);
                using (IRandomAccessStream textStream = await textFile.OpenReadAsync())
                {
                    // Read text stream     
                    using (DataReader textReader = new DataReader(textStream))
                    {
                        //get size                       
                        uint textLength = (uint)textStream.Size;
                        await textReader.LoadAsync(textLength);
                        // read it                    
                        string jsonContents = textReader.ReadString(textLength);
                        // deserialize back to our product!  
                      
                        val = JsonConvert.DeserializeObject<T>(jsonContents);
                        return val;
                    }
                }
            }
            catch (Exception ex)
            {
               DebuggerClass.reportError("Error deserializing: "+file+" "+ex.Message);
            
            }
            return default(T);
        }
    }
}
