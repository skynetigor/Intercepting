namespace Benchmark.Abstract
{
    public interface IBenchmark
    {
        void Start(int repeats);
        void SwitchToDirectCalling();
        void SwitchToIntercepting();
    }
}