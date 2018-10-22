/*
Copyright (C) 2004 Jacquelin POTIER <jacquelin.potier@free.fr>
Dynamic aspect ratio code Copyright (C) 2004 Jacquelin POTIER <jacquelin.potier@free.fr>

This program is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation; version 2 of the License.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
*/
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
namespace Tools.Xml
{
    public class XML_access
    {
        /// <summary>
        /// simple sample of call
        /// XMLSerializeObject("samplei.xml",i,typeof(OrderedItem));
        /// sample of call for ArrayList
        /// XMLSerializeObject("sampleal.xml",(OrderedItem[])al.ToArray(typeof(OrderedItem)),typeof(OrderedItem[]));
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="obj"></param>
        /// <param name="typeof_object"></param>
        public static void XMLSerializeObject(string filename,object obj,System.Type typeof_object)
        {
            Stream fs=null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof_object);
                // Create an XmlTextWriter using a FileStream.
                fs = new FileStream(filename, FileMode.Create,System.IO.FileAccess.ReadWrite );
                XmlWriter writer = new XmlTextWriter(fs,System.Text.Encoding.Unicode);
                // Serialize using the XmlTextWriter.
                serializer.Serialize(writer, obj);
                writer.Close();
                fs.Close();
            }
            catch(Exception e)
            {
                if (fs!=null)
                    fs.Close();
                System.Windows.Forms.MessageBox.Show( e.Message,"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// simple sample of call
        /// OrderedItem i=(OrderedItem)XMLDeserializeObject("simple.xml",typeof(OrderedItem));
        /// sample of call for ArrayList
        /// System.Collections.ArrayList al=new System.Collections.ArrayList((OrderedItem[])XMLDeserializeObject("sampleal.xml",typeof(OrderedItem[])));
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="typeof_object"></param>
        /// <returns></returns>
        public static object XMLDeserializeObject(string filename,System.Type typeof_object)
        {   
            // Create an instance of the XmlSerializer specifying type and namespace.
            XmlSerializer serializer = new XmlSerializer(typeof_object);
            // A FileStream is needed to read the XML document.
            FileStream fs = new FileStream(filename, FileMode.Open);
            XmlReader reader = new XmlTextReader(fs);
            // Declare an object variable of the type to be deserialized.
            object obj;
            // Use the Deserialize method to restore the object's state.
            obj = serializer.Deserialize(reader);
            reader.Close();
            fs.Close();
            return obj;
        }
    }
}