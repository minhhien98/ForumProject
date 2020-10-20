using DomainModel.Entity;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class ThreadService : IThreadService
    {
        private IThreadRepository _threadRepository;
        public ThreadService(IThreadRepository threadRepository)
        {
            _threadRepository = threadRepository;
        }
        public void CreateThread(Thread thread)
        {
            _threadRepository.CreateThread(thread);
        }

        public void DeleteThread(Thread thread)
        {
            _threadRepository.DeleteThread(thread);
        }

        public void EditThread(Thread thread)
        {
            _threadRepository.EditThread(thread);
        }

        public Thread GetThreadById(int id)
        {
            return _threadRepository.GetThreadById(id);
        }

        public IEnumerable<Thread> ThreadList()
        {
            return _threadRepository.ThreadList();
        }
    }
}
