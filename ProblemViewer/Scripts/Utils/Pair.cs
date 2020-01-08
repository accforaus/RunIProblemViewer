using System;

namespace Utils {
    public class Pair<T, R> {
        private T first;
        private R second;

        public T First {
            get => first;
            set => first = value;
        }

        public R Second {
            get => second;
            set => second = value;
        }

        public Pair() {}
        
        public Pair(T first, R second) {
            this.first = first;
            this.second = second;
        }
    }
}