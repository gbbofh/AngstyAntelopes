namespace Stats
{
    public class Rage : Core.Stat
    {
        // Start is called before the first frame update
        void Start() {

            MaxValue = 100.0f;
            MinValue = 0.0f;

            CurrentValue = MaxValue;
        }
    }
}