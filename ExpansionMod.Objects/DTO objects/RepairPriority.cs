﻿using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionMod.Objects
{
    public class RepairPriority : INotifyPropertyChanged
    {
        private string _templateName;

        public string TemplateName { get; set; }
        public List<ComponentCategoryType> Priority { get; set; }
        public bool UserGenerated { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public RepairPriority Clone()
        {
            return new RepairPriority()
            {
                TemplateName = TemplateName,
                Priority = Priority.ToList(),
            };
        }

        public bool PriorityEquals(RepairPriority other) 
        {
            return this.Priority.SequenceEqual(other.Priority);
        }
    }
}
