abstract class NameValidationResult
{
    public class Pass : NameValidationResult {}

    public class IsBlank : NameValidationResult {}

    public class Clash : NameValidationResult
    {
        public int Pos { get; }

        public Clash(int pos)
        {
            this.Pos = pos;
        }
    }
}
