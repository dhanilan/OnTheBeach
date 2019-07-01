namespace JobSequencing
{
    public interface IJobParser
    {
        Job Parse(string input);
    }
}