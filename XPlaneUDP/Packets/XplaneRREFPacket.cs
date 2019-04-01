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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPlaneUDP
{
    public struct XplaneRREFPacket : IXplanePacket
    {
        private byte[] header; // 5 bytes
        private byte[] frequency; // 4 bytes, in times per second
        private byte[] reference; // 4 bytes, internal reference 
        private byte[] drefPath; // 400 bytes

        /// <summary>
        /// Request reference value from XPlane. You can send max 148 requests per frame in xplane
        /// </summary>
        /// <param name="frequency">Times per second Xplane will send the reference value</param>
        /// <param name="reference">Reference number for your own use. Xplane returns this number with the requested value</param>
        /// <param name="drefPath">Dataref string</param>
        public XplaneRREFPacket(int frequency, int reference, string drefPath)
        {
            this.header = new byte[] { (byte)'R', (byte)'R', (byte)'E', (byte)'F', 0 };
            this.frequency = BitConverter.GetBytes(frequency);
            this.reference = BitConverter.GetBytes(reference);
            this.drefPath = new byte[400];

            // initialize name byte array with spaces
            for (int i = 0; i < this.drefPath.Length; i++)
            {
                this.drefPath[i] = (byte)' ';
            }

            // fill byte array with dataref name ending with a null terminator ( = char minvalue)
            var nameAsBytes = Encoding.ASCII.GetBytes(drefPath + char.MinValue);
            Buffer.BlockCopy(nameAsBytes, 0, this.drefPath, 0, nameAsBytes.Length);
        }

        public byte[] ToBytes()
        {
            byte[] bytes = new byte[413];

            Buffer.BlockCopy(header, 0, bytes, 0, header.Length);
            Buffer.BlockCopy(frequency, 0, bytes, 5, frequency.Length);
            Buffer.BlockCopy(reference, 0, bytes, 9, reference.Length);
            Buffer.BlockCopy(drefPath, 0, bytes, 13, drefPath.Length);

            return bytes;
        }
    };

}
