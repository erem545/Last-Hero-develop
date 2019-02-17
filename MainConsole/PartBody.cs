using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsole
{
    class PartBody
    {
        string name;
        public float status;
        bool ok;
        PartBody parent;
        public PartBody()
        {
            status = 0;
            ok = false;
        }

        //public PartBody(string _name, PartBody _parent)
        //{
        //    status = 0;
        //    ok = false;
        //    name = _name;
        //    parent = _parent;
        //}

        public PartBody(string _name, PartBody _parent, bool _ok)
        {
            status = 20;
            ok = _ok;
            name = _name;
            parent = _parent;
        }

        /// <summary>
        /// Лечение части тела на stat единиц
        /// </summary>
        /// <param name="stat"></param>
        void Restoration(float stat)
        {
            if (ok)
                status += stat;
        }
        /// <summary>
        /// Повреждение части тела на stat единиц
        /// </summary>
        /// <param name="stat"></param>
        void Damage(float stat)
        {
            if (ok)
            {
                status -= stat;
                if (status <= 0)
                    ok = false; 
            }
        }


        public override string ToString()
        {
            return $"\n{name}\nНаличие:\t{ok}\nСостояние:\t{status*5}%\n";
        }
    }
}
