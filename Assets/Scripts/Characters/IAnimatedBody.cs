namespace Characters
{
    public interface IAnimatedObject
    {
        void PlayAnimation(string animationName);
        bool IsLookAtRight { get; }
        void SetFlag(string flagName, bool value);
    }
}