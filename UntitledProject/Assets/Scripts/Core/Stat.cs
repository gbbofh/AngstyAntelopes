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

        public float MaxValue {

            get; set;
        }

        public float MinValue {

            get; set;
        }

        public float CurrentValue {

            get; protected set;
        }

        public void Increment(float amount) {

            CurrentValue = Mathf.Clamp(CurrentValue + amount, MinValue, MaxValue);

            if (onIncrement != null) {

                onIncrement();
            }

            if(CurrentValue >= MaxValue && onFull != null) {

                onFull();
            }
        }

        public void Decrement(float amount) {

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
