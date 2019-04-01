/* Copyright 2019 KLM Team Reimagineers
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
 * IN THE SOFTWARE.
*/

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace XPlaneUDP
{
    internal class UDPReceiver : IDisposable
    {
        private UdpClient client;
        private IPEndPoint sender;

        public UDPReceiver(IPAddress IP, int port)
        {
            IPEndPoint ipep = new IPEndPoint(IP, port);
            client = new UdpClient(ipep);
            Console.WriteLine("Receiving message on IP:" + ipep.Address + " and Port: " + ipep.Port);

            sender = new IPEndPoint(IPAddress.Any, 0);
        }
        
        public void Dispose()
        {
            client.Close();
        }

        /// <summary>
        /// This blocks the thread until a packet is received
        /// </summary>
        /// <param name="printData"></param>
        /// <returns></returns>
        public byte[] Listen(bool printData = false)
        {
            byte[] data = client.Receive(ref sender);

            if (printData)
            {
                Console.WriteLine("Message received from " + sender.Address.ToString() + " on port " + sender.Port.ToString());
                for (int index = 0; index < data.Length; index++)
                {
                    Console.Write("{0},", data[index]);
                }
                Console.WriteLine();
            }

            return data;
        }
    }
}

