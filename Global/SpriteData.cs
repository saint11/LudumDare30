using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Bones
{
    public class SpriteData
    {
        private static XmlDocument Xml;

        public static void Init()
        {
            Xml = new XmlDocument();
            Xml.Load(Bones.GAME_PATH + "Sprites.Xml");
        }

        public static Spritemap<string> GetAnimation(string source)
        {
            XmlElement node = null;

            foreach (XmlNode n in Xml["Sprites"])
            {
                if (n is XmlComment) continue;
                XmlElement xml = n as XmlElement;

                if (xml.AttributeString("name")==source)
                {
                    node = xml;
                    break;
                }
            }
            if (node == null)
            {
                throw new Exception("Cannot find image " + source);
                return null;
            }

            string texture = node.AttributeString("texture");
            string atlasName  = node.AttributeString("atlas");;
            int w = node["Size"].AttributeInt("width");
            int h = node["Size"].AttributeInt("height");

            Spritemap<string> anim = Bones.Atlas[atlasName].CreateSpritemap<string>(texture, w, h);
            
            anim.OriginX = node["Origin"].AttributeInt("x");
            anim.OriginY = node["Origin"].AttributeInt("y");
            

            foreach (XmlNode n in node["Animations"])
            {
                if (n is XmlComment) continue;
                XmlElement animXml = n as XmlElement;

                string frames = "";
                string rawFrames = animXml.AttributeString("frames");
                if (rawFrames.Contains("-"))
                {
                    foreach (string element in rawFrames.Split(','))
                    {
                        if (element.Contains("-"))
                        {
                            string min = element.Substring(0, element.IndexOf('-'));
                            string max = element.Substring(element.IndexOf('-')+1);
                            for (int i = int.Parse(min); i <= int.Parse(max); i++) 
                            {
                                frames += i.ToString()+",";
                            }
                        }
                        else frames += element + ",";
                    }
                }
                else frames = rawFrames;

                if (frames[frames.Length-1] == ',') frames = frames.Substring(0, frames.Length - 1);

                Anim animation = anim.Add(animXml.AttributeString("name"), frames, animXml.AttributeInt("delay",1));
                if (!animXml.AttributeBool("loop", true))
                {
                    animation.NoRepeat();
                }
                if (animXml.AttributeBool("pingPong", false))
                {
                    animation.PingPong(true);
                }

                string next = animXml.AttributeString("next","");
                if (next != "")
                {
                    animation.OnComplete = () => { anim.Play(next); };
                }
            }
            return anim;
        }
    }
}
