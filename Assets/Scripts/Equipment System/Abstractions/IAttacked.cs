using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IAttacked: ISelectable
{
    public float Attack { get; }
    public float Defends { get; }
}

