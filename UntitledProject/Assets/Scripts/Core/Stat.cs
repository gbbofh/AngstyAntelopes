using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class Stat : MonoBehaviour
    {
        public UnityAction onIncrement;
        public UnityAction onDecrement;
        public UnityAction onEmpty;
        public UnityAction onFull;

        public int MaxValue {

            get; set;
        }

        public int MinValue {

            get; set;
        }

        public int CurrentValue {

            get; set;
        }

        public void Increment(int amount) {

            CurrentValue = Mathf.Clamp(CurrentValue + amount, MinValue, MaxValue);

            if (onIncrement != null) {

                onIncrement();
            }

            if(CurrentValue >= MaxValue && onFull != null) {

                onFull();
            }
        }

        public void Decrement(int amount) {

            CurrentValue = Mathf.Clamp(CurrentValue - amount, MinValue, MaxValue);

            if(onDecrement != null) {

                onDecrement();
            }

            if(CurrentValue <= MinValue && onEmpty != null) {

                onEmpty();
            }
        }
    }
}
