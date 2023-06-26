using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Unity.Netcode.Transports.UTP;

public class NetworkCommandLine : MonoBehaviour
{
   private NetworkManager netManager;

   void Start()
   {
       netManager = GetComponentInParent<NetworkManager>();

    /*
       netManager.GetComponent<UnityTransport>().SetConnectionData(
        "127.0.0.1",  // The IP address is a string
        (ushort)9000, // The port number is an unsigned short
        "0.0.0.0" // The server listen address is a string.
       );
       */
    

       if (Application.isEditor) return;

       var args = GetCommandlineArgs();

       if (args.TryGetValue("-mode", out string mode))
       {
           switch (mode)
           {
               case "server":
                   netManager.StartServer();
                   break;
               case "host":
                   netManager.StartHost();
                   Debug.Log("host started");
                   break;
               case "client":

                   netManager.StartClient();
                   break;
           }
       }
   }

   private Dictionary<string, string> GetCommandlineArgs()
   {
       Dictionary<string, string> argDictionary = new Dictionary<string, string>();

       var args = System.Environment.GetCommandLineArgs();

       for (int i = 0; i < args.Length; ++i)
       {
           var arg = args[i].ToLower();
           if (arg.StartsWith("-"))
           {
               var value = i < args.Length - 1 ? args[i + 1].ToLower() : null;
               value = (value?.StartsWith("-") ?? false) ? null : value;

               argDictionary.Add(arg, value);
           }
       }
       return argDictionary;
   }
}