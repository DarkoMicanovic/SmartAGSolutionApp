using System;
using System.Collections.Generic;
using System.Text;

namespace SmartAGSolutionApp.Model
{
    public class Greenhouse
    {
        private Guid id;

        public Greenhouse()
        {
            this.id = default;
            this.PhoneNumber = this.Name = this.Description = string.Empty;
        }

        public Greenhouse(string phoneNumber, string name, string description)
        {
            this.id = Guid.NewGuid();
            this.PhoneNumber = phoneNumber;
            this.Name = name;
            this.Description = description;
        }

        public override bool Equals(object obj)
        {
            if (obj is Greenhouse)
                return ((Greenhouse)obj).ID == this.ID;
            return false;
        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

        #region Properties

        public Guid ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string IDString
        {
            get { return this.id.ToString(); }
        }

        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        #endregion

        public void Update(string name, string phoneNumber, string description)
        {
            this.Name = name;
            this.PhoneNumber = phoneNumber;
            this.Description = description;
        }
    }
}
