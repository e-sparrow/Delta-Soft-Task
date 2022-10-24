namespace Game.Services.Sound.Pitch.Interfaces
{
    public interface IPitchSettings
    {
        float Value
        {
            get;
        }
        
        float Step
        {
            get;
        }

        float Time
        {
            get;
        }

        int StepsCount
        {
            get;
        }
    }
}