namespace SystemSplit.Models.Components.Bases
{
    public abstract class Component
    {
        private string name;
        private string type;

        protected Component(string name, string type)
        {
            this.Name = name;
            this.Type = type;
        }

        public string Name
        {
            get { return this.name; }

            protected set
            {
                this.name = value;
            }
        }

        public string Type
        {
            get { return this.type; }

            protected set
            {
                this.type = value;
            }
        }
    }
}
