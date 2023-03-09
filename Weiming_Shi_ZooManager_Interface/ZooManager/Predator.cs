using System;
namespace ZooManager
{
    public interface Predator
    {
        bool Hunt(string animal);
        void Activate();
    }
 
}

