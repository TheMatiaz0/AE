namespace AE
{
    public class PuzzleStep
    {
        public PuzzleReference Reference { get; }
        public bool IsCompleted { get; set; }

        public PuzzleStep(PuzzleReference reference)
        {
            Reference = reference;
            IsCompleted = false;
        }
    }
}
