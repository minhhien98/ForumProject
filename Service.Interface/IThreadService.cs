using DomainModel.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IThreadService
    {
        void CreateThread(Thread thread);
        void EditThread(Thread thread);
        void DeleteThread(Thread thread);
        Thread GetThreadById(int id);
        IEnumerable<Thread> ThreadList();
    }
}
