using System.Collections.Generic;

namespace UGPangya.API
{
    public class GenericDisposableCollection<T> where T : class, IDisposeable
    {
        private List<T> _model;

        public GenericDisposableCollection()
        {
            Model = new List<T>();
        }

        public int Count => _model.Count;

        public List<T> Model
        {
            get
            {
                //Rmove pessoas Disposed
                _model?.RemoveAll(p => p != null && p.Disposed);

                return _model;
            }
            set => _model = value;
        }

        public T this[int index]
        {
            get => Model[index];
            set => Model[index] = value;
        }

        public void Add(T pessoa)
        {
            Model.Add(pessoa);
        }

        public List<T> ToList()
        {
            return Model;
        }
    }
}