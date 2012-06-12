using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace subserver
{
	class Program
	{
		public static void Main(string[] args)
		{
			short port=23456;
				
			Console.WriteLine("USAGE: subserver <portnumber>");

			if(args.Length>1 && Int16.TryParse(args[1],out port) && port>1024){
				//OK, got good port
			} else {
				port = 23456; //default port
			}
			
			try {
				BinaryServerFormatterSinkProvider serverProv = new BinaryServerFormatterSinkProvider();
				serverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
				BinaryClientFormatterSinkProvider clientProv = new BinaryClientFormatterSinkProvider();
				IDictionary props = new Hashtable();
				props["port"] = port;
				
				TcpChannel chan = new TcpChannel(props, clientProv, serverProv);	//create the tcp channel
				ChannelServices.RegisterChannel(chan,false);	//register the tcp channel
				RemotingConfiguration.RegisterWellKnownServiceType(typeof(subsharelib.subshareRemoteObject),"SUBSERVER",WellKnownObjectMode.Singleton);	//publish the remote object
				
				Console.WriteLine("SERVER STARTED AT PORT "+port);
			} catch (Exception ex) {
				Console.WriteLine("SERVER CAN NOT START! "+ex);
			}
			Console.Write("Press any key to exit . . . ");
			Console.ReadKey(true);
		}
	}
}