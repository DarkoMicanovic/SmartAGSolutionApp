using System;
using System.Collections.Generic;
using System.Text;

namespace SmartAGSolutionApp.Model
{
    public class Greenhouse
    {
        public Greenhouse()
        {
            PhoneNumber = Name = Description = string.Empty;
        }

        public Greenhouse(string phoneNumber, string name, string description)
        {
            PhoneNumber = phoneNumber;
            Name = name;
            Description = description;
        }

        public override bool Equals(object obj)
        {
            if (obj is Greenhouse)
                return ((Greenhouse)obj).Name == this.Name && ((Greenhouse)obj).PhoneNumber == this.PhoneNumber;
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region Properties

        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        #endregion

    }
}
