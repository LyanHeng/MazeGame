using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace SwinAdventure
{
    public class IdentifiableObject
    {
        private List<string> _identifiers;

        // constructor
        public IdentifiableObject(string[] identifiers)
        {
            _identifiers = (Array.ConvertAll(identifiers,d=>d.ToLower())).ToList();
        }

        // check if in the list
        public bool AreYou(string id)
        {
            id = id.ToLower();
            return _identifiers.Exists(i => i == id);
        }

        // return the first id
        public string FirstID
        {
            get
            {
                if (_identifiers.Any())
                {
                    return _identifiers[0];
                }
                return " ";
            }
        }

        // convert to lower and store in _identifiers
        public void AddIdentifier(string id)
        {
            _identifiers.Add(id.ToLower());
        }
    }
}
