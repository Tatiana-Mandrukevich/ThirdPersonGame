using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace GameAssets.Scripts.DanceBattle
{
    public class ThirdPartService : IDisposable
    {
        private bool[] _bools = new bool[2];
        private GameObject _gameObject;
        
        public ThirdPartService()
        {
            _gameObject = new GameObject();
        }
        
        public void Dispose()
        {
            Object.DestroyImmediate(_gameObject);
        }
        
        public bool DoAction1()
        {
            throw new Exception();
        }
        
        public bool DoAction2(int index)
        {
            if (index >= _bools.Length || index < 0)
            {
                var exception = new IndexOutOfRangeException();
                exception.Data["Index"] = index;
                throw exception;
            }
            
            return _bools[index];
        }

        public void DoAction3()
        {
            throw new MyException("My exception");
        }

        public async Task<bool> LoadScene(int index)
        {
            await SceneManager.LoadSceneAsync(index);
            return true;
        }

        public void StopLoading()
        {
            Debug.Log("Stop loading");
        }
    }
    
    public class MyException : Exception
    {
        public MyException(string message) : base(message)
        {
        }
    }
}