using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Bones
{
    public class Terrain : Entity
    {
        private GridCollider Solids;

        public Terrain(string source)
        {
            
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(Bones.GAME_PATH + "Maps/" + source + ".oel");

            XmlElement xml = xmlDoc["level"];

            OgmoLayer solids = new OgmoLayer(xml["solid"]);

            Solids = new GridCollider(xml.AttributeInt("width"), xml.AttributeInt("height"), solids.GridWidth, solids.GridHeight);
            
            AddColliders(Solids);
        }
    }
}
