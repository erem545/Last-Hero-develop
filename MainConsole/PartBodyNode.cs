using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsole
{
    class PartBodyNode
    {
        public float MaxSumStatus;
        public float SumStatus;

        public PartBody body;
        public PartBody head;
        public PartBody lhand;
        public PartBody rhand;
        public PartBody lfoot;
        public PartBody rfoot;

        public bool ok;
        public PartBodyNode(float _maxSum)
        {
            ok = true;
            body  = new PartBody("Body", _maxSum);
            head  = new PartBody("Head", _maxSum);
            lhand = new PartBody("Left Hand", _maxSum);
            rhand = new PartBody("Right Hand", _maxSum);
            lfoot = new PartBody("Left Foot", _maxSum);
            rfoot = new PartBody("Right Foot", _maxSum);
            MaxSumStatus = body.Status + head.Status + lhand.Status + rhand.Status + lfoot.Status + rfoot.Status;
        }
        void Dead()
        {
            ok = false;
            body.ok = false;
            head.ok =  false;
            lhand.ok = false;
            rhand.ok = false;
            lfoot.ok = false;
            rfoot.ok = false;
             body.Refresh();
             head.Refresh();
            lhand.Refresh();
            rhand.Refresh();
            lfoot.Refresh();
            rfoot.Refresh();
            SumStatus = 0;            
        }
        public void Refresh()
        {
            if (ok)
            {
                if ((head.Status < 1) || (body.Status < 1))
                {
                    Console.WriteLine("Персонаж погиб!");
                    Dead();
                }
                else
                    SumStatus = body.Status + head.Status + lhand.Status + rhand.Status + lfoot.Status + rfoot.Status;
            }
 
        }
        internal void ShowDetals()
        {
            Console.WriteLine(body.ToString());
            Console.WriteLine(head.ToString());
            Console.WriteLine(lhand.ToString());
            Console.WriteLine(rhand.ToString());
            Console.WriteLine(lfoot.ToString());
            Console.WriteLine(rfoot.ToString());
            Console.WriteLine();
        }
    }
}
