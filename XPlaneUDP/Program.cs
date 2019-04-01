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

namespace XPlaneUDP
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // receive DataREF over port
            using (UDPReceiver receiver = new UDPReceiver(IPAddress.Any, 49004))
            using (XplaneUDPSender sender = new XplaneUDPSender(IPAddress.Parse("127.0.0.1"), 49000))
            {
                // data package, should be enabled in xplane
                XplaneDATAPacket throttleOff = new XplaneDATAPacket(25, 0, 0, 0, 0, 0, 0, 0, 0);
                XplaneDATAPacket throttleOn = new XplaneDATAPacket(25, 1, 0, 0, 0, 0, 0, 0, 0);

                // data ref, does not need to be enabled in xplane to edit
                XplaneDREFPacket batteryOn = new XplaneDREFPacket(1, "sim/cockpit/electrical/battery_on");
                XplaneDREFPacket batteryOff = new XplaneDREFPacket(0, "sim/cockpit/electrical/battery_on");

                while (true)
                {
                    //drefReceiver.Listen(true);  <-- blocks thread

                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey();
                        if (key.Key == ConsoleKey.O)
                        {
                            // these three statements do the same
                            sender.Send(batteryOff);
                            //sender.WriteDref(batteryOff);
                            //sender.WriteDref(0, "sim/cockpit/electrical/battery_on");

                            Console.WriteLine("Battery Off");
                        }
                        if (key.Key == ConsoleKey.P)
                        {
                            sender.Send(batteryOn);
                            Console.WriteLine("Battery On");
                        }
                        if (key.Key == ConsoleKey.K)
                        {
                            sender.Send(throttleOff);
                        }
                        if (key.Key == ConsoleKey.L)
                        {
                            sender.Send(throttleOn);
                        }

                        if (key.Key == ConsoleKey.Escape)
                        {
                            break;
                        }
                    }
                }
            }

            Environment.Exit(0);
        }
    }
}
