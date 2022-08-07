using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public abstract class GameObject : IdentifiableObject
    {
        private string _description;
        private string _name;

        public string Name 
        { 
            get => _name; 
        }

        public string ShortDescription
        {
            get => Name + " (" + FirstID + ")";
        }

        public virtual string FullDescription
        {
            get => _description;
        }

        public GameObject(string[] ids, string name, string desc) : base(ids)
        {
            _name = name;
            _description = desc;
        }
    }
}
