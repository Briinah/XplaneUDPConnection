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

using System.Net;

namespace XPlaneUDP
{
    public class XplaneUDPSender : UDPSender
    {
        public XplaneUDPSender(IPAddress IP, int port) : base(IP, port)
        {
        }

        /// <summary>
        /// Write reference value to xplane
        /// </summary>
        /// <param name="value"></param>
        /// <param name="drefPath"></param>
        public void WriteDref(float value, string drefPath)
        {
            var packet = new XplaneDREFPacket(value, drefPath);
            Send(packet);
        }
        /// <summary>
        /// Write reference value to xplane
        /// </summary>
        public void WriteDref(XplaneDREFPacket packet)
        {
            Send(packet);
        }

        /// <summary>
        /// Request reference value from XPlane. You can send max 148 requests per frame in xplane
        /// </summary>
        /// <param name="frequency">Times per second Xplane will send the reference value</param>
        /// <param name="reference">Reference number for your own use. Xplane returns this number with the requested value</param>
        /// <param name="drefPath">Dataref string</param>
        public void RequestDref(int frequency, int reference, string drefPath)
        {
            var packet = new XplaneRREFPacket(frequency, reference, drefPath);
            Send(packet);
        }
        /// <summary>
        /// Request reference value from XPlane. You can send max 148 requests per frame in xplane
        /// </summary>
        public void RequestDref(XplaneRREFPacket packet)
        {
            Send(packet);
        }

        /// <summary>
        /// Request and/or write data packets. 
        /// For what the data represents, see XPlane docs. 
        /// If you do not want to change the data put -999
        /// </summary>
        /// <param name="index">Index of data packet, can be found in XPlane</param>
        /// <param name="data1"></param>
        /// <param name="data2"></param>
        /// <param name="data3"></param>
        /// <param name="data4"></param>
        /// <param name="data5"></param>
        /// <param name="data6"></param>
        /// <param name="data7"></param>
        /// <param name="data8"></param>
        public void ReadWriteDataPacket(byte index, float data1, float data2, float data3, float data4,
            float data5, float data6, float data7, float data8)
        {
            var packet = new XplaneDATAPacket(index, data1, data2, data3, data4, data5, data6, data7, data8);
            Send(packet);
        }
        /// <summary>
        /// Request and/or write data packets. 
        /// For what the data represents, see XPlane docs. 
        /// If you do not want to change the data put -999
        /// </summary>
        public void ReadWriteDataPacket(XplaneDATAPacket packet)
        {
            Send(packet);
        }
    }
}
