using System;
namespace ZooManager
{
    public interface Prey
    {
        bool Flee(string animal);

        void Activate();
    }
}

