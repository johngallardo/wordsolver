using System;
using System.Runtime.Serialization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using System.ComponentModel;

namespace WordSolver
{
    [DataContract]
    public sealed class ConstraintState : INotifyPropertyChanged
    {
        [DataMember]
        public string Tiles
        {
            get { return _tiles; }
            set
            {
                if (value != _tiles)
                {
                    _tiles = value;
                    OnPropertyChanged("Tiles");
                }
            }
        }
        private string _tiles;

        [DataMember]
        public string Template 
        {
            get { return _template; }
            set
            {
                if (value != _template)
                {
                    _template = value;
                    OnPropertyChanged("Template");
                }
            }
        }
        private string _template;

        [
        DataMember,        
        ]
        public TemplateFactoryBase TemplateFactory 
        {
            get { return _t; }
            set 
            {
                if (value != _t)
                {
                    _t = value;
                    OnPropertyChanged("TemplateFactory");
                }
            }
        }

        private TemplateFactoryBase _t;

        public string GetAdjustedTemplate()
        {
            if (TemplateFactory == null)
                return Template;
            else
                return TemplateFactory.ConstructTemplate(Template);
        }

        private IList<TemplateFactoryBase> _availableConstraints = new TemplateFactoryCollection();
        public IList<TemplateFactoryBase> AvailableConstraints
        {
            get
            {
                if (_availableConstraints == null)
                    _availableConstraints = new TemplateFactoryCollection();
                return _availableConstraints;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            var h = PropertyChanged;
            if (h != null)
                h(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public enum TemplateType
    {
        None = -1,
        StartsWith = 0,
        EndsWith = 1,
        Contains = 2
    }
}
