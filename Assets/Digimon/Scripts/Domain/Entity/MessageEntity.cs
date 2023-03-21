using System;
using System.IO;
using UniRx;

namespace Digimon.Digimon.Scripts.Domain.Entity
{
    public sealed class MessageEntity
    {
        private readonly Subject<StringReader> _message = new();

        public IObservable<StringReader> OnReadFileAsObservable()
        {
            return _message.Share();
        }

        public void Training()
        {
            
        }
    }
}